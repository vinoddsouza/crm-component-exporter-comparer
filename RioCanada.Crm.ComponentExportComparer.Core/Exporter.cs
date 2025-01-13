using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using RioCanada.Crm.ComponentExportComparer.Core.Models;
using RioCanada.Crm.ComponentExportComparer.Core.Utilities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core
{
    class Exporter
    {
        readonly ArgumentQueryResponse ArgumentQueryResponse;
        readonly List<string> ArgumentQueries;
        readonly Action<ExportProgressSnapshot> OnProgress;
        readonly ExportService.OutFileHandler OutFileFunc;
        readonly BackgroundWorker BgWorker;
        readonly ExportSetting Setting;
        readonly OrganizationService Service;
        readonly Logger Logger;
        readonly bool GenerateIndexFile;
        readonly List<IndexLineItem> IndexData = new List<IndexLineItem>();

        readonly int EntityProgressWeight = 2;
        readonly int WebResourceWeight = 1;
        readonly int PluginWeight = 1;
        readonly int OptionSetWeight = 1;
        readonly int DashboardWeight = 1;
        readonly int SiteMapWeight = 1;
        readonly int SecurityRoleWeight = 2;
        readonly int WorkflowWeight = 1;
        readonly int BusinessRuleWeight = 1;
        readonly int ActionWeight = 1;
        readonly int BusinessProcessFlowWeight = 1;
        readonly int ModelDrivenAppWeight = 1;

        //readonly int ENTITY_BUFFER_SIZE = 5;
        readonly int WEBRESOURCE_BUFFER_SIZE = 50;
        //readonly int OPTIONSET_BUFFER_SIZE = 5;
        readonly int DASHBOARD_BUFFER_SIZE = 10;
        readonly int SITEMAP_BUFFER_SIZE = 10;
        readonly int SECURITY_ROLE_BUFFER_SIZE = 10;
        readonly int WORKFLOW_BUFFER_SIZE = 50;
        readonly int MODEL_DRIVEN_APP_BUFFER_SIZE = 10;

        bool IncludeAllProperty { get => this.Setting.IncludeAllProperty; }
        bool ReplaceEmptyStringByNull  { get => this.Setting.ReplaceEmptyStringByNull; }

        int OverallCompleted = 0;
        readonly int OverallTotal = 0;

        int CurrentCompleted = 0;
        int CurrentTotal = 0;
        string CurrentLabel = string.Empty;

        public Exporter(
            OrganizationService service,
            List<string> argumentQueries,
            ArgumentQueryResponse argumentQueryResponse,
            ExportSetting setting,
            Logger logger,
            ExportService.OutFileHandler outFileFunc,
            Action<ExportProgressSnapshot> onProgress,
            BackgroundWorker bgWorker = null,
            bool generateIndexFile = false
        )
        {
            this.ArgumentQueries = argumentQueries;
            this.ArgumentQueryResponse = argumentQueryResponse;
            this.OnProgress = onProgress;
            this.OutFileFunc = outFileFunc;
            this.BgWorker = bgWorker;
            this.Setting = setting;
            this.Service = service;
            this.Logger = logger;
            this.GenerateIndexFile = generateIndexFile;

            if (setting.IncludeEntityColumn) EntityProgressWeight++;
            if (setting.IncludeEntityRelationship) EntityProgressWeight++;
            if (setting.IncludeEntityForm || setting.IncludeEntityDashboard) EntityProgressWeight++;
            if (setting.IncludeEntityView) EntityProgressWeight++;
            if (setting.IncludeEntityRibbon) EntityProgressWeight += 3;

            OverallTotal = argumentQueryResponse.Entities.Count * EntityProgressWeight
                + argumentQueryResponse.WebResources.Count * WebResourceWeight
                + argumentQueryResponse.PluginSteps.Count * PluginWeight
                + argumentQueryResponse.OptionSets.Count * OptionSetWeight
                + argumentQueryResponse.Dashboards.Count * DashboardWeight
                + argumentQueryResponse.SiteMaps.Count * SiteMapWeight
                + argumentQueryResponse.SecurityRoles.Count * SecurityRoleWeight
                + argumentQueryResponse.Workflows.Count * WorkflowWeight
                + argumentQueryResponse.BusinessRules.Count * BusinessRuleWeight
                + argumentQueryResponse.Actions.Count * ActionWeight
                + argumentQueryResponse.BusinessProcessFlows.Count * BusinessProcessFlowWeight
                + argumentQueryResponse.ModelDrivenApps.Count * ModelDrivenAppWeight
                ;
        }

        public void Execute()
        {
            this.ValidateData();

            this.ExportEntity(ArgumentQueryResponse.Entities, EntityProgressWeight); // Entities
            this.ExportWebresource(ArgumentQueryResponse.WebResources, WebResourceWeight); // Webresources
            this.ExportPlugin(ArgumentQueryResponse.PluginSteps, PluginWeight); // Plugins
            this.ExportOptionSet(ArgumentQueryResponse.OptionSets, OptionSetWeight); // OptionSets
            this.ExportDashboard(ArgumentQueryResponse.Dashboards, DashboardWeight); // Dashboards
            this.ExportSitemap(ArgumentQueryResponse.SiteMaps, SiteMapWeight); // SiteMaps
            this.ExportSecurityrole(ArgumentQueryResponse.SecurityRoles, SecurityRoleWeight); // SecurityRoles
            this.ExportWorkflow(ArgumentQueryResponse.Workflows, WorkflowWeight); // Workflows
            this.ExportBusinessrule(ArgumentQueryResponse.BusinessRules, BusinessRuleWeight); // BusinessRules
            this.ExportAction(ArgumentQueryResponse.Actions, ActionWeight); // Actions
            this.ExportBusinessProcessFlow(ArgumentQueryResponse.BusinessProcessFlows, BusinessProcessFlowWeight); // Business Process Flows
            this.ExportModelDrivenApp(ArgumentQueryResponse.ModelDrivenApps, ModelDrivenAppWeight); // Model Driven Apps

            if (BgWorker?.CancellationPending == true) return;

            if (this.GenerateIndexFile)
            {
                IndexData.ForEach(x => CalculateIndexChecksum(x));
                var rootIndexItem = new IndexLineItem
                {
                    Children = IndexData,
                };
                rootIndexItem.Metadata.Add("EnvironmentId", this.Service.EnvironmentId);
                rootIndexItem.Metadata.Add("Server", this.Service.Server);
                rootIndexItem.Metadata.Add("Queries", this.ArgumentQueries);
                rootIndexItem.Metadata.Add("Settings", this.Setting);

                OutFileFunc("index.json", SerializeUtility.SerializeJson(rootIndexItem), null);
            }

            OnProgress(new ExportProgressSnapshot
            {
                CurrentLabel = "Exporting completed.",
                CurrentProgress = 100,
                OverallProgress = 100,
                CurrentCompleted = 0,
                CurrentTotal = 0,
            });
        }

        private static int GetProgressValue(int min, int max, int value)
        {
            return (((max - min) * Math.Min(100, Math.Max(0, value))) / 100) + min;
        }

        private static int GetProgressValue(int min, int max, int total, int completed)
        {
            if (total == 0) return 0;
            return GetProgressValue(min, max, (completed * 100) / total);
        }

        void ValidateData()
        {
            if (!this.Setting.RoleById)
            {
                var duplicateItem = ArgumentQueryResponse.SecurityRoles.GroupBy(x => x.Name.ToLower()).Where(x => x.Count() > 1).FirstOrDefault();

                if (duplicateItem != null)
                {
                    throw new Exception($"Role \"{duplicateItem.FirstOrDefault().Name}\" found as duplicate. Enable \"Compare role by id instead name\" in Query Option or remove duplicate role.");
                }
            }
        }

        void ExportEntity(List<CRMEntityTable> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting tables...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "tables", Name = "Tables", Type = IndexItemType.Folder, Order = 1, Children = new List<IndexLineItem>() });

            var entityFilter = EntityFilters.Entity;
            if (Setting.IncludeEntityColumn) entityFilter |= EntityFilters.Attributes;
            if (Setting.IncludeEntityRelationship) entityFilter |= EntityFilters.Relationships;

            SendProgressSnapshot();
            foreach (var item in items)
            {
                if (BgWorker?.CancellationPending == true) return;
                var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = item.AttributeLogicalName, Name = item.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });
                currentIndexItem.Metadata.Add("Type", "Table");
                currentIndexItem.Metadata.Add("SchemaName", item.AttributeLogicalName);

                RetrieveEntityRequest request = new RetrieveEntityRequest
                {
                    LogicalName = item.AttributeLogicalName,
                    EntityFilters = entityFilter,
                };
                var response = (RetrieveEntityResponse)Service.Execute(request);

                HandleOutFile(
                    $@"tables\{item.AttributeLogicalName}\metadata.json",
                    SerializeUtility.SerializeJson(response.EntityMetadata.GetExportableObject(IncludeAllProperty, ReplaceEmptyStringByNull)),
                    null,
                    currentIndexItem.Children,
                    new IndexLineItem { Key = "metadata.json", Name = "Metadata", Type = IndexItemType.FileJson }
                );
                
                if (Setting.IncludeEntityColumn)
                {
                    var metadata = new Dictionary<string, object>
                    {
                        { "Type", IndexLineItemContentType.Columns },
                        { "Id", item.AttributeLogicalName }
                    };

                    HandleOutFile(
                        $@"tables\{item.AttributeLogicalName}\columns.json",
                        SerializeUtility.SerializeJson(response.EntityMetadata.Attributes.ToList().OrderBy(x => x.SchemaName).Select(x => x.GetMetadataObject(IncludeAllProperty))),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "columns.json", Name = "Columns", Type = IndexItemType.FileJson, ContentType = IndexLineItemContentType.Columns, Metadata = metadata }
                    );
                }

                if (Setting.IncludeEntityRelationship)
                {
                    var relationshipIndexItem = AddIndexItem(currentIndexItem.Children, new IndexLineItem { Key = "relationships", Name = "Relationships", Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    var metadata = new Dictionary<string, object>
                    {
                        { "Type", IndexLineItemContentType.ManyToMany },
                        { "Id", item.AttributeLogicalName }
                    };

                    HandleOutFile(
                        $@"tables\{item.AttributeLogicalName}\relationships\manytomanyrelationships.json",
                        SerializeUtility.SerializeJson(response.EntityMetadata.ManyToManyRelationships.OrderBy(x => x.SchemaName).Select(x => x.GetMetadataObject(IncludeAllProperty))),
                        null,
                        relationshipIndexItem.Children,
                        new IndexLineItem { Key = "manytomanyrelationships.json", Name = "Many To Many", Type = IndexItemType.FileJson, ContentType = IndexLineItemContentType.ManyToMany, Metadata = metadata }
                        );

                    metadata = new Dictionary<string, object>
                    {
                        { "Type", IndexLineItemContentType.ManyToOne },
                        { "Id", item.AttributeLogicalName }
                    };

                    HandleOutFile(
                        $@"tables\{item.AttributeLogicalName}\relationships\manytoonerelationships.json",
                        SerializeUtility.SerializeJson(response.EntityMetadata.ManyToOneRelationships.OrderBy(x => x.SchemaName).Select(x => x.GetMetadataObject(IncludeAllProperty))),
                        null,
                        relationshipIndexItem.Children,
                        new IndexLineItem { Key = "manytoonerelationships.json", Name = "Many To One", Type = IndexItemType.FileJson, ContentType = IndexLineItemContentType.ManyToOne, Metadata = metadata }
                    );

                    metadata = new Dictionary<string, object>
                    {
                        { "Type", IndexLineItemContentType.OneToMany },
                        { "Id", item.AttributeLogicalName }
                    };

                    HandleOutFile(
                        $@"tables\{item.AttributeLogicalName}\relationships\onetomanyrelationships.json",
                        SerializeUtility.SerializeJson(response.EntityMetadata.OneToManyRelationships.OrderBy(x => x.SchemaName).Select(x => x.GetMetadataObject(IncludeAllProperty))), null,
                        relationshipIndexItem.Children,
                        new IndexLineItem { Key = "onetomanyrelationships.json", Name = "One To Many", Type = IndexItemType.FileJson, ContentType = IndexLineItemContentType.OneToMany, Metadata = metadata }
                    );
                }

                // TODO:
                //EntitySetting[] Settings
                //EntityKeyMetadata[] Keys
                //SecurityPrivilegeMetadata[]

                if (BgWorker?.CancellationPending == true) return;
                if (Setting.IncludeEntityForm || Setting.IncludeEntityDashboard)
                {
                    var systemforms = SystemForm.RetriveMultipleByObjectTypeCode(Service, response.EntityMetadata.ObjectTypeCode ?? 0);

                    if (Setting.IncludeEntityForm)
                    {
                        List<int> formTypes = new List<int> { (int)FormType.Main, (int)FormType.QuickCreate, (int)FormType.QuickViewForm, (int)FormType.Card };

                        var forms = systemforms.FindAll(x => x.Type != null && formTypes.IndexOf(x.Type.Value) != -1);

                        if (forms.Count > 0)
                        {
                            var formsIndexItem = AddIndexItem(currentIndexItem.Children, new IndexLineItem { Key = "forms", Name = "Forms", Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                            forms.ForEach(x =>
                            {
                                var formIndexItem = AddIndexItem(formsIndexItem.Children, new IndexLineItem { Key = x.Id.ToString(), Name = x.Name + (x.Type == null ? string.Empty : $" ({((FormType)x.Type.Value).GetDescription()})"), Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });
                                formIndexItem.Metadata.Add("Type", "Form");
                                formIndexItem.Metadata.Add("Id", x.Id.ToString());

                                HandleOutFile(
                                    $@"tables\{item.AttributeLogicalName}\forms\{x.Id}\metadata.json",
                                    SerializeUtility.SerializeJson(x.GetMetadataObject(IncludeAllProperty)),
                                    null,
                                    formIndexItem.Children,
                                    new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson, ContentType = IndexLineItemContentType.Columns }
                                );

                                OutFileFunc($@"tables\{item.AttributeLogicalName}\forms\{x.Id}\form.json", SerializeUtility.FormatJson(x.FormJson), null);
                                //HandleOutFile(
                                //    $@"tables\{item.AttributeLogicalName}\forms\{x.Id}\form.json",
                                //    SerializeUtility.FormatJson(x.FormJson),
                                //    null,
                                //    formIndexItem.Children,
                                //    new IndexLineItem { Key = "form.json", Name = "form.json", Type = IndexItemType.FileJson }
                                //);

                                OutFileFunc($@"tables\{item.AttributeLogicalName}\forms\{x.Id}\form_old.xml", x.FormXml, null);

                                HandleOutFile(
                                    $@"tables\{item.AttributeLogicalName}\forms\{x.Id}\form.xml",
                                    DataTransformer.TransformFormXml(x.FormXml, Setting.VerifyTransformedData, this.Service, this.Logger),
                                    null,
                                    formIndexItem.Children,
                                    new IndexLineItem { Key = "form.xml", Name = "form.xml", Type = IndexItemType.FileXml }
                               );
                            });
                        }
                    }

                    if (Setting.IncludeEntityDashboard)
                    {
                        List<int> formTypes = new List<int> { (int)FormType.Dashboard, (int)FormType.InteractionCentricDashboard, (int)FormType.PowerBIDashboard };

                        var forms = systemforms.FindAll(x => x.Type != null && formTypes.IndexOf(x.Type.Value) != -1);

                        if (forms.Count > 0)
                        {
                            var formsIndexItem = AddIndexItem(currentIndexItem.Children, new IndexLineItem { Key = "dashboards", Name = "Dashboards", Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                            forms.ForEach(x =>
                            {
                                var formIndexItem = AddIndexItem(formsIndexItem.Children, new IndexLineItem { Key = x.Id.ToString(), Name = x.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                                HandleOutFile(
                                    $@"tables\{item.AttributeLogicalName}\dashboards\{x.Id}\metadata.json",
                                    SerializeUtility.SerializeJson(x.GetMetadataObject(IncludeAllProperty)),
                                    null,
                                    formIndexItem.Children,
                                    new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                                );
                                HandleOutFile(
                                    $@"tables\{item.AttributeLogicalName}\dashboards\{x.Id}\form.json",
                                    SerializeUtility.FormatJson(x.FormJson),
                                    null,
                                    formIndexItem.Children,
                                    new IndexLineItem { Key = "form.json", Name = "form.json", Type = IndexItemType.FileJson }
                                );
                                HandleOutFile(
                                    $@"tables\{item.AttributeLogicalName}\dashboards\{x.Id}\form.xml",
                                    SerializeUtility.FormatFormXml(x.FormXml, IncludeAllProperty),
                                    null,
                                    formIndexItem.Children,
                                    new IndexLineItem { Key = "form.xml", Name = "form.xml", Type = IndexItemType.FileXml }
                               );
                            });
                        }
                    }
                }

                if (BgWorker?.CancellationPending == true) return;
                if (Setting.IncludeEntityView)
                {
                    var views = SavedQuery.RetriveMultipleByObjectTypeCode(Service, response.EntityMetadata.ObjectTypeCode ?? 0);

                    if (views.Count > 0)
                    {
                        var viewsIndexItem = AddIndexItem(currentIndexItem.Children, new IndexLineItem { Key = "views", Name = "Views", Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                        views.ForEach(x =>
                        {
                            var viewIndexItem = AddIndexItem(viewsIndexItem.Children, new IndexLineItem { Key = x.Id.ToString(), Name = x.Name, Children = new List<IndexLineItem>() });

                            HandleOutFile(
                                $@"tables\{item.AttributeLogicalName}\views\{x.Id}\metadata.json",
                                SerializeUtility.SerializeJson(x.GetMetadataObject(IncludeAllProperty)),
                                null,
                                viewIndexItem.Children,
                                new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                            );
                            HandleOutFile(
                                $@"tables\{item.AttributeLogicalName}\views\{x.Id}\fetch.xml",
                                SerializeUtility.FormatXml(x.FetchXml),
                                null,
                                viewIndexItem.Children,
                                new IndexLineItem { Key = "fetch.xml", Name = "fetch.xml", Type = IndexItemType.FileXml }
                            );
                            HandleOutFile(
                                $@"tables\{item.AttributeLogicalName}\views\{x.Id}\layout.json",
                                SerializeUtility.FormatViewLayoutJson(x.LayoutJson, IncludeAllProperty),
                                null,
                                viewIndexItem.Children,
                                new IndexLineItem { Key = "layout.json", Name = "layout.json", Type = IndexItemType.FileJson }
                            );
                            HandleOutFile(
                                $@"tables\{item.AttributeLogicalName}\views\{x.Id}\layout.xml",
                                SerializeUtility.FormatViewLayoutXml(x.LayoutXml, IncludeAllProperty),
                                null,
                                viewIndexItem.Children,
                                new IndexLineItem { Key = "layout.xml", Name = "layout.xml", Type = IndexItemType.FileXml }
                            );
                        });
                    }
                }

                if (BgWorker?.CancellationPending == true) return;
                if (Setting.IncludeEntityRibbon)
                {
                    RetrieveEntityRibbonRequest ribbonRequest = new RetrieveEntityRibbonRequest
                    {
                        EntityName = item.AttributeLogicalName,
                        RibbonLocationFilter = RibbonLocationFilters.All,
                    };

                    var ribbonResponse = (RetrieveEntityRibbonResponse)Service.Execute(ribbonRequest);

                    var ribbonXml = UnzipRibbon(ribbonResponse.CompressedEntityXml);

                    var transformedRibbonxml = DataTransformer.TransformRibbonXml(ribbonXml, Setting.VerifyTransformedData, this.Logger);

                    OutFileFunc($@"tables\{item.AttributeLogicalName}\ribbon_old.xml", null, ribbonXml);

                    HandleOutFile(
                        $@"tables\{item.AttributeLogicalName}\ribbon.xml",
                        transformedRibbonxml,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "ribbon.xml", Name = "ribbon.xml", Type = IndexItemType.FileXml, ContentType = IndexLineItemContentType.Ribbon }
                    );
                }

                CurrentCompleted++;
                OverallCompleted += weight;
                SendProgressSnapshot();
            }
        }

        void ExportWebresource(List<WebResource> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting webresources...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "webresources", Name = "Webresources", Type = IndexItemType.Folder, Order = 2, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = WEBRESOURCE_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var webresources = this.Service.GetData<WebResource>(WebResource.EntityLogicalName, ids);

                foreach (var record in webresources)
                {
                    var metadata = new Dictionary<string, object>
                    {
                        { "Type", "WebResource" },
                        { "Id", record.Id.ToString() }
                    };

                    HandleOutFile(
                        $@"webresources\{record.Name}.metadata.json",
                        SerializeUtility.SerializeJson(record.GetMetadataObject(IncludeAllProperty)),
                        null,
                        indexItem.Children,
                        new IndexLineItem { Key = $"{record.Name}.metadata.json", Name = $"{record.Name}.metadata.json", Type = IndexItemType.FileJson, Metadata = metadata }
                    );
                    HandleOutFile(
                        $@"webresources\{record.Name}",
                        null,
                        Convert.FromBase64String(record.Content),
                        indexItem.Children,
                        new IndexLineItem { Key = record.Name, Name = record.Name, Type = IndexItemType.File, Metadata = metadata }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportPlugin(List<SdkMessageProcessingStep> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting pluginsteps...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "plugings", Name = "Plugins", Type = IndexItemType.Folder, Order = 3, Children = new List<IndexLineItem>() });

            var pluginStepImages = this.Service.GetData<SdkMessageProcessingStepImage, Guid>(
                SdkMessageProcessingStepImage.EntityLogicalName,
                "sdkmessageprocessingstepid",
                items.Select(x => x.Id).ToList()
            );

            SendProgressSnapshot();
            foreach (var item in items)
            {
                if (BgWorker?.CancellationPending == true) return;

                var images = pluginStepImages.FindAll(x => x.SdkMessageProcessingStepId?.Id == item.Id);
                HandleOutFile(
                    $@"plugings\{item.Id}.metadata.json",
                    SerializeUtility.SerializeJson(item.GetMetadataObject(IncludeAllProperty, images, this.Service)),
                    null,
                    indexItem.Children,
                    new IndexLineItem { Key = $"{item.Id}.metadata.json", Name = item.Name, Type = IndexItemType.FileJson }
                );

                CurrentCompleted++;
                OverallCompleted += weight;

                SendProgressSnapshot();
            }
        }

        void ExportOptionSet(List<OptionSetMetadataBase> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting choices...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "choices", Name = "Choices", Type = IndexItemType.Folder, Order = 4, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            foreach (var item in items)
            {
                if (BgWorker?.CancellationPending == true) return;
                HandleOutFile(
                    $@"choices\{item.Name}.metadata.json",
                    SerializeUtility.SerializeJson(item.GetMetadataObject(IncludeAllProperty)),
                    null,
                    indexItem.Children,
                    new IndexLineItem { Key = $"{item.Name}.metadata.json", Name = item.Name, Type = IndexItemType.FileJson }
                );

                CurrentCompleted++;
                OverallCompleted += weight;

                SendProgressSnapshot();
            }
        }

        void ExportDashboard(List<SystemForm> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting dashboards...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "dashboards", Name = "Dashboards", Type = IndexItemType.Folder, Order = 5, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = DASHBOARD_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var forms = this.Service.GetData<SystemForm, Guid>(SystemForm.EntityLogicalName, "formid", ids);

                foreach (var form in forms)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = form.Id.ToString(), Name = form.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"dashboards\{form.Id}\metadata.json",
                        SerializeUtility.SerializeJson(form.GetMetadataObject(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"dashboards\{form.Id}\form.json",
                        SerializeUtility.FormatJson(form.FormJson),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "form.json", Name = "form.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"dashboards\{form.Id}\form.xml",
                        SerializeUtility.FormatXml(form.FormXml),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "form.xml", Name = "form.xml", Type = IndexItemType.FileXml }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportSitemap(List<SiteMap> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting sitemaps...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "sitemaps", Name = "Sitemaps", Type = IndexItemType.Folder, Order = 6, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = SITEMAP_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var sitemaps = this.Service.GetData<SiteMap>(SiteMap.EntityLogicalName, ids);

                foreach (var sitemap in sitemaps)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = sitemap.UniqueKey, Name = sitemap.SiteMapName ?? sitemap.SiteMapNameUnique ?? sitemap.Id.ToString(), Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"sitemaps\{sitemap.UniqueKey}\metadata.json",
                        SerializeUtility.SerializeJson(sitemap.GetMetadataObject(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );

                    var transformedSitemapXml = DataTransformer.TransformSiteMapXml(sitemap.SiteMapXML, Setting.VerifyTransformedData, this.Logger);

                    OutFileFunc($@"sitemaps\{sitemap.UniqueKey}\sitemap_old.xml", sitemap.SiteMapXML, null);

                    HandleOutFile(
                        $@"sitemaps\{sitemap.UniqueKey}\sitemap.xml",
                        transformedSitemapXml,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "sitemap.xml", Name = "sitemap.xml", Type = IndexItemType.FileXml }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportSecurityrole(List<SecurityRole> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting securityroles...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "securityroles", Name = "Security Roles", Type = IndexItemType.Folder, Order = 7, Children = new List<IndexLineItem>(), ContentType = IndexLineItemContentType.SecurityRoleFolder });

            SendProgressSnapshot();
            int bufferSize = SECURITY_ROLE_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var roles = this.Service.GetData<SecurityRole>(SecurityRole.EntityLogicalName, ids);
                var filteredSecurityRolePrivileges = Privilege.GetByRoleIds(Service, ids);

                foreach (var securityRole in roles)
                {
                    var key = this.Setting.RoleById ? securityRole.Id.ToString() : securityRole.Name;

                    var securityRolePrivileges = filteredSecurityRolePrivileges.FindAll(x => (Guid)x.Get<Microsoft.Xrm.Sdk.AliasedValue>("roleprivileges.roleid").Value == securityRole.Id);

                    var metadata = new Dictionary<string, object>
                    {
                        { "Type", "SecurityRole" },
                        { "Id", securityRole.Id.ToString() }
                    };

                    HandleOutFile(
                        $@"securityroles\{key}.json",
                        SerializeUtility.SerializeJson(new Models.Transform.Metadata.SecurityRoleMergedData
                        {
                            Metadata = securityRole.GetMetadataObject(IncludeAllProperty, this.Setting.RoleById),
                            Privilege = securityRolePrivileges.OrderBy(x => x.Name).Select(x => x.GetMetadataObject(IncludeAllProperty)).ToList(),
                        }),
                        null,
                        indexItem.Children,
                        new IndexLineItem { Key = $"{key}.json", Name = securityRole.Name, Type = IndexItemType.FileJson, ContentType = IndexLineItemContentType.SecurityRole, Metadata = metadata }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportWorkflow(List<Workflow> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting workflows...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "workflows", Name = "Workflows", Type = IndexItemType.Folder, Order = 8, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = WORKFLOW_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var workflows = this.Service.GetData<Workflow>(Workflow.EntityLogicalName, ids);

                foreach (var workflow in workflows)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = workflow.UniqueKey, Name = workflow.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"workflows\{workflow.UniqueKey}\metadata.json",
                        SerializeUtility.SerializeJson(workflow.GetMetadataObjectByCategory(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"workflows\{workflow.UniqueKey}\xaml.xml",
                        SerializeUtility.FormatXml(workflow.Xaml),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "xaml.xml", Name = "xaml.xml", Type = IndexItemType.FileXml }
                    );
                    HandleOutFile(
                        $@"workflows\{workflow.UniqueKey}\clientdata.txt",
                        workflow.ClientData,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "clientdata.txt", Name = "clientdata.txt", Type = IndexItemType.FileTxt }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportBusinessrule(List<Workflow> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting businessrules...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "businessrules", Name = "Business Rules", Type = IndexItemType.Folder, Order = 9, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = WORKFLOW_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var workflows = this.Service.GetData<Workflow>(Workflow.EntityLogicalName, ids);

                foreach (var workflow in workflows)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = workflow.UniqueKey, Name = workflow.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"businessrules\{workflow.UniqueKey}\metadata.json",
                        SerializeUtility.SerializeJson(workflow.GetMetadataObjectByCategory(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"businessrules\{workflow.UniqueKey}\xaml.xml",
                        SerializeUtility.FormatXml(workflow.Xaml),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "xaml.xml", Name = "xaml.xml", Type = IndexItemType.FileXml }
                    );
                    HandleOutFile(
                        $@"businessrules\{workflow.UniqueKey}\clientdata.txt", workflow.ClientData,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "clientdata.txt", Name = "clientdata.txt", Type = IndexItemType.FileTxt }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportAction(List<Workflow> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting actions...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "actions", Name = "Actions", Type = IndexItemType.Folder, Order = 10, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = WORKFLOW_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var workflows = this.Service.GetData<Workflow>(Workflow.EntityLogicalName, ids);

                foreach (var workflow in workflows)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = workflow.UniqueKey, Name = workflow.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"actions\{workflow.UniqueKey}\metadata.json",
                        SerializeUtility.SerializeJson(workflow.GetMetadataObjectByCategory(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"actions\{workflow.UniqueKey}\xaml.xml",
                        SerializeUtility.FormatXml(workflow.Xaml),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "xaml.xml", Name = "xaml.xml", Type = IndexItemType.FileXml }
                    );
                    HandleOutFile(
                        $@"actions\{workflow.UniqueKey}\clientdata.txt", workflow.ClientData,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "clientdata.txt", Name = "clientdata.txt", Type = IndexItemType.FileTxt }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportBusinessProcessFlow(List<Workflow> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting businessprocessflows...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "businessprocessflows", Name = "Business Process Flows", Type = IndexItemType.Folder, Order = 11, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = WORKFLOW_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var workflows = this.Service.GetData<Workflow>(Workflow.EntityLogicalName, ids);

                foreach (var workflow in workflows)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = workflow.UniqueKey, Name = workflow.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"businessprocessflows\{workflow.UniqueKey}\metadata.json",
                        SerializeUtility.SerializeJson(workflow.GetMetadataObjectByCategory(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"businessprocessflows\{workflow.UniqueKey}\xaml.xml",
                        SerializeUtility.FormatXml(workflow.Xaml),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "xaml.xml", Name = "xaml.xml", Type = IndexItemType.FileXml }
                    );
                    HandleOutFile(
                        $@"businessprocessflows\{workflow.UniqueKey}\clientdata.txt", workflow.ClientData,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "clientdata.txt", Name = "clientdata.txt", Type = IndexItemType.FileTxt }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void ExportModelDrivenApp(List<AppModule> items, int weight)
        {
            if (BgWorker?.CancellationPending == true) return;
            if (items.Count == 0) return;

            CurrentCompleted = 0;
            CurrentTotal = items.Count;
            CurrentLabel = "Exporting modeldrivenapps...";
            var indexItem = AddIndexItem(IndexData, new IndexLineItem { Key = "modeldrivenapps", Name = "Model Driven Apps", Type = IndexItemType.Folder, Order = 12, Children = new List<IndexLineItem>() });

            SendProgressSnapshot();
            int bufferSize = MODEL_DRIVEN_APP_BUFFER_SIZE;
            for (var i = 0; i < items.Count; i += bufferSize)
            {
                if (BgWorker?.CancellationPending == true) return;
                var ids = items.Select(x => x.Id).Skip(i).Take(bufferSize).ToList();
                var appModules = this.Service.GetData<AppModule>(AppModule.EntityLogicalName, ids);

                foreach (var appModule in appModules)
                {
                    var currentIndexItem = AddIndexItem(indexItem.Children, new IndexLineItem { Key = appModule.UniqueName, Name = appModule.Name, Type = IndexItemType.Folder, Children = new List<IndexLineItem>() });

                    HandleOutFile(
                        $@"modeldrivenapps\{appModule.UniqueName}\metadata.json",
                        SerializeUtility.SerializeJson(appModule.GetMetadataObject(IncludeAllProperty)),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "metadata.json", Name = "metadata.json", Type = IndexItemType.FileJson }
                    );
                    HandleOutFile(
                        $@"modeldrivenapps\{appModule.UniqueName}\appmodulemanaged.xml",
                        SerializeUtility.FormatXml(appModule.AppModuleXmlManaged),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "appmodulemanaged.xml", Name = "appmodulemanaged.xml", Type = IndexItemType.FileXml }
                    );

                    var transformedConfigXml = DataTransformer.TransformModelDrivenAppConfigXml(appModule.ConfigXml, Setting.VerifyTransformedData, this.Logger);

                    OutFileFunc($@"modeldrivenapps\{appModule.UniqueName}\config_old.xml", appModule.ConfigXml, null);

                    HandleOutFile(
                        $@"modeldrivenapps\{appModule.UniqueName}\config.xml",
                        transformedConfigXml,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "config.xml", Name = "config.xml", Type = IndexItemType.FileXml }
                    );
                    HandleOutFile(
                        $@"modeldrivenapps\{appModule.UniqueName}\eventhandlers.json",
                        SerializeUtility.FormatJson(appModule.EventHandlers),
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "eventhandlers.json", Name = "eventhandlers.json", Type = IndexItemType.FileXml }
                    );

                    var transformedDescriptorJson = DataTransformer.TransformModelDrivenAppDescriptor(appModule.Descriptor, Setting.VerifyTransformedData, this.Service, this.Logger);

                    OutFileFunc($@"modeldrivenapps\{appModule.UniqueName}\descriptor_old.json", SerializeUtility.FormatJson(appModule.Descriptor), null);

                    HandleOutFile(
                        $@"modeldrivenapps\{appModule.UniqueName}\descriptor.json",
                        transformedDescriptorJson,
                        null,
                        currentIndexItem.Children,
                        new IndexLineItem { Key = "descriptor.json", Name = "descriptor.json", Type = IndexItemType.FileJson }
                    );
                }

                CurrentCompleted += ids.Count;
                OverallCompleted += ids.Count * weight;
                SendProgressSnapshot();
            }
        }

        void SendProgressSnapshot()
        {
            OnProgress(new ExportProgressSnapshot
            {
                CurrentLabel = CurrentLabel,
                CurrentProgress = GetProgressValue(0, 100, CurrentTotal, CurrentCompleted),
                OverallProgress = GetProgressValue(0, 100, OverallTotal, OverallCompleted),
                CurrentTotal = CurrentTotal,
                CurrentCompleted = CurrentCompleted,
            });
        }

        IndexLineItem AddIndexItem(List<IndexLineItem> items, IndexLineItem item)
        {
            if (this.GenerateIndexFile)
            {
                items.Add(item);
            }

            return item;
        }

        void HandleOutFile(string path, string content, byte[] data, List<IndexLineItem> listIndexItems, IndexLineItem indexLineItem)
        {
            OutFileFunc(path, content, data);

            if (this.GenerateIndexFile)
            {
                bool isEmpty = string.IsNullOrEmpty(content) && (data == null || data.Length == 0);
                if (!isEmpty)
                {
                    indexLineItem.Checksum = GetChecksum(content, data);
                    AddIndexItem(listIndexItems, indexLineItem);
                }
            }
        }

        /// <summary>
        /// A helper method that decompresses the Ribbon data returned
        /// </summary>
        /// <param name="data">The compressed ribbon data</param>
        /// <returns></returns>
        private static byte[] UnzipRibbon(byte[] data)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            memStream.Write(data, 0, data.Length);
            System.IO.Packaging.ZipPackage package = (System.IO.Packaging.ZipPackage)System.IO.Packaging.Package.Open(memStream, System.IO.FileMode.Open);

            System.IO.Packaging.ZipPackagePart part = (System.IO.Packaging.ZipPackagePart)package.GetPart(new Uri("/RibbonXml.xml", UriKind.Relative));
            using (System.IO.Stream strm = part.GetStream())
            {
                long len = strm.Length;
                byte[] buff = new byte[len];
                strm.Read(buff, 0, (int)len);
                return buff;
            }
        }

        private static void CalculateIndexChecksum(IndexLineItem item)
        {
            if (item.Children != null && item.Children.Count > 0)
            {
                item.Children.ForEach(x => CalculateIndexChecksum(x));
                item.Checksum = GetChecksum(string.Join("-", item.Children.OrderBy(x => x.Checksum).Select(x => x.Checksum)), null);
            }
        }

        private static string GetChecksum(string content, byte[] data2)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data;
            if (data2 != null && data2.Length > 0)
            {
                data = md5Hash.ComputeHash(data2);
            }
            else if (string.IsNullOrEmpty(content))
            {
                return "00000000000000000000000000000000";
            }
            else
            {

                data = md5Hash.ComputeHash(System.Text.Encoding.ASCII.GetBytes(content));
            }

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
