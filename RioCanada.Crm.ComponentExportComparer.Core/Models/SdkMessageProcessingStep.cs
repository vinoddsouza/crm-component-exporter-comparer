using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    enum InvocationSource
    {
        [Description("Child")]
        Child = 1,
        [Description("Internal")]
        Internal = -1,
        [Description("Parent")]
        Parent = 0,
    }

    enum Stage
    {
        [Description("Final Post-operation (For internal user only)")]
        FinalPostOperation = 55,
        [Description("Initial Pre-operation (For internal use only)")]
        InitialPreOperation = 5,
        [Description("Internal Post-operation After External Plugins (For internal use only)")]
        InternalPostOperationAfterExternalPlugins = 45,
        [Description("Internal Post-operation Before External Plugins (For internal user only)")]
        InternalPostOperationBeforeExternalPlugins = 35,
        [Description("Internal Pre-operation After Extenral Plugins (For internal use only")]
        InternalPreOperationAfterExternalPlugins = 25,
        [Description("Internal Pre-operation Before Extenral Plugins (For internal use only")]
        InternalPreOperationBeforeExternalPlugins = 15,
        [Description("Main Operation (For internal use only)")]
        MainOperation = 30,
        [Description("Post-Commit stage fired after transaction commit (For internal use only)")]
        PostCommitStageFireAfterTransactionCommit = 90,
        [Description("Post-operation")]
        PostOperation = 40,
        [Description("Post-operation (Deprecated)")]
        PostOperationDeprecated = 50,
        [Description("Pre-Commit stage fired before transaction commit (For internal use only)")]
        PreCommitStageFireBeforeTransactionCommit = 80,
        [Description("Pre-operation")]
        PreOperation = 20,
        [Description("Pre-validation")]
        PreValidation = 10,
    }

    enum SupportedDeployment
    {
        [Description("Both")]
        Both = 2,
        [Description("Microsoft Dynamics 365 Client for Outlook Only")]
        MicrosoftDynamics365ClientForOutlookOnly = 1,
        [Description("Server Only")]
        ServerOnly = 0,
    }

    enum Mode
    {
        [Description("Asynchronus")]
        Asynchronus = 1,
        [Description("Synchronus")]
        Synchronus = 0,
    }

    [EntityLogicalName(EntityLogicalName)]
    class SdkMessageProcessingStep : Entity
    {
        public const string EntityLogicalName = "sdkmessageprocessingstep";
        public SdkMessageProcessingStep() : base(EntityLogicalName) { }

        public SdkMessageProcessingStep(Guid id) : base(EntityLogicalName, id) { }

        public bool AsyncAutoDelete { get => this.Get<bool>("asyncautodelete"); set => this.Set("asyncautodelete", value); }
        public bool CanUseReadonlyConnection { get => this.Get<bool>("canusereadonlyconnection"); set => this.Set("canusereadonlyconnection", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public string Configuration { get => this.Get<string>("configuration"); set => this.Set("configuration", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public int CustomizationLevel { get => this.Get<int>("customizationlevel"); set => this.Set("customizationlevel", value); }
        public EntityReference EventHandler { get => this.Get<EntityReference>("eventhandler"); set => this.Set("eventhandler", value); }
        public string FilteringAttributes { get => this.Get<string>("filteringattributes"); set => this.Set("filteringattributes", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public OptionSetValue Invocationsource { get => this.Get<OptionSetValue>("invocationsource"); set => this.Set("invocationsource", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public BooleanManagedProperty IsHidden { get => this.Get<BooleanManagedProperty>("ishidden"); set => this.Set("ishidden", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public OptionSetValue Mode { get => this.Get<OptionSetValue>("mode"); set => this.Set("mode", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public EntityReference PluginTypeId { get => this.Get<EntityReference>("plugintypeid"); set => this.Set("plugintypeid", value); }
        public int Rank { get => this.Get<int>("rank"); set => this.Set("rank", value); }
        public EntityReference SdkMessageFilterId { get => this.Get<EntityReference>("sdkmessagefilterid"); set => this.Set("sdkmessagefilterid", value); }
        public EntityReference SdkMessageId { get => this.Get<EntityReference>("sdkmessageid"); set => this.Set("sdkmessageid", value); }
        public Guid SdkMessageProcessingStepIdUnique { get => this.Get<Guid>("sdkmessageprocessingstepidunique"); set => this.Set("sdkmessageprocessingstepidunique", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }
        public OptionSetValue Stage { get => this.Get<OptionSetValue>("stage"); set => this.Set("stage", value); }
        public OptionSetValue StateCode { get => this.Get<OptionSetValue>("statecode"); set => this.Set("statecode", value); }
        public OptionSetValue StatusCode { get => this.Get<OptionSetValue>("statuscode"); set => this.Set("statuscode", value); }
        public OptionSetValue SupportedDeployment { get => this.Get<OptionSetValue>("supporteddeployment"); set => this.Set("supporteddeployment", value); }

        public static List<SdkMessageProcessingStep> FindByNames(OrganizationService service, bool includeSystemPluginStep, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<SdkMessageProcessingStep>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<SdkMessageProcessingStep>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.Distinct = true;
            query.ColumnSet.AllColumns = true;

            if (!includeSystemPluginStep)
            {
                query.Criteria.AddCondition("name", ConditionOperator.NotEqual, "Ribbon Workbench Action");

                var linkEntity = query.AddLink("plugintype", "eventhandler", "plugintypeid");
                linkEntity.JoinOperator = JoinOperator.Inner;
                linkEntity.LinkCriteria.AddCondition("assemblyname", ConditionOperator.NotLike, "Microsoft.%");
                linkEntity.LinkCriteria.AddCondition("assemblyname", ConditionOperator.NotLike, "ActivityFeeds.%");
            }

            Utilities.Helper.ApplyPatternFilter(query, "name", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<SdkMessageProcessingStep>(query);
        }

        public object GetMetadataObject(bool includeAllProperty, List<SdkMessageProcessingStepImage> images, OrganizationService service)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.AsyncAutoDelete,
                    this.CanUseReadonlyConnection,
                    this.ComponentState,
                    this.Configuration,
                    this.CreatedBy,
                    this.CreatedOn,
                    this.CustomizationLevel,
                    this.EventHandler,
                    this.FilteringAttributes,
                    this.Id,
                    Images = images.ConvertAll(x => x.GetMetadataObject(includeAllProperty)),
                    this.IntroducedVersion,
                    this.Invocationsource,
                    this.IsManaged,
                    this.IsHidden,
                    this.IsCustomizable,
                    this.Mode,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.Name,
                    this.OrganizationId,
                    this.PluginTypeId,
                    this.Rank,
                    this.SdkMessageFilterId,
                    this.SdkMessageId,
                    this.SdkMessageProcessingStepIdUnique,
                    this.SolutionId,
                    this.Stage,
                    this.StateCode,
                    this.StatusCode,
                    this.SupportedDeployment,
                };
            }
            else
            {
                return new
                {
                    this.AsyncAutoDelete,
                    this.CanUseReadonlyConnection,
                    this.ComponentState,
                    this.Configuration,
                    this.CustomizationLevel,
                    //this.EventHandler,
                    EventHandler = PluginType.GetNameById(service, this.EventHandler?.Id),
                    this.FilteringAttributes,
                    this.Id,
                    Images = images.ConvertAll(x => x.GetMetadataObject(includeAllProperty)),
                    this.Invocationsource,
                    IsHidden = this.IsHidden.GetMetadataObject(includeAllProperty),
                    IsCustomizable = this.IsCustomizable.GetMetadataObject(includeAllProperty),
                    this.Mode,
                    this.Name,
                    //this.PluginTypeId,
                    this.Rank,
                    //this.SdkMessageFilterId,
                    SdkMessageFilter = SdkMessageFilter.GetNameById(service, this.SdkMessageFilterId?.Id),
                    this.SdkMessageId,
                    this.SolutionId,
                    this.Stage,
                    this.StateCode,
                    this.StatusCode,
                    this.SupportedDeployment,
                };
            }
        }
    }
}
