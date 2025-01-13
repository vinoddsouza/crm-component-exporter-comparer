using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    enum WebResourceType
    {
        [Description("Webpage (HTML)")]
        HTML = 1,
        [Description("Style Sheet (CSS)")]
        CSS = 2,
        [Description("Script (JScript)")]
        JScript = 3,
        [Description("Data (XML)")]
        DataXML = 4,
        [Description("PNG format")]
        PNG = 5,
        [Description("JPG format")]
        JPG = 6,
        [Description("GIF format")]
        GIF = 7,
        [Description("Silverlight (XAP)")]
        SilverlightXAP = 8,
        [Description("Style Sheet (XLS)")]
        XLS = 9,
        [Description("ICO format")]
        ICO = 10,
        [Description("Vector format (SVG)")]
        SVG = 11,
        [Description("String (RESX)")]
        StringRESX = 12,
    }

    [EntityLogicalName(EntityLogicalName)]
    class WebResource : Entity
    {
        public const string EntityLogicalName = "webresource";

        public WebResource() : base(EntityLogicalName) { }

        public WebResource(Guid id) : base(EntityLogicalName, id) { }

        public BooleanManagedProperty CanBeDeleted { get => this.Get<BooleanManagedProperty>("canbedeleted"); set => this.Set("canbedeleted", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public string Content { get => this.Get<string>("content"); set => this.Set("content", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public string DependencyXML { get => this.Get<string>("dependencyxml"); set => this.Set("dependencyxml", value); }
        public string Description { get => this.Get<string>("description"); set => this.Set("description", value); }
        public string DisplayName { get => this.Get<string>("displayname"); set => this.Set("displayname", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public bool IsAvailableForMobileOffline { get => this.Get<bool>("isavailableformobileoffline"); set => this.Set("isavailableformobileoffline", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public bool IsEnabledForMobileClient { get => this.Get<bool>("isenabledformobileclient"); set => this.Set("isenabledformobileclient", value); }
        public BooleanManagedProperty IsHidden { get => this.Get<BooleanManagedProperty>("ishidden"); set => this.Set("ishidden", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public DateTime? OverwriteTime { get => this.Get<DateTime?>("overwritetime"); set => this.Set("overwritetime", value); }
        public Guid? SolutionId { get => this.Get<Guid?>("solutionid"); set => this.Set("solutionid", value); }
        public int? VersionNumber { get => this.Get<int?>("versionnumber"); set => this.Set("versionnumber", value); }
        public OptionSetValue WebResourceType { get => this.Get<OptionSetValue>("webresourcetype"); set => this.Set("webresourcetype", value); }
        public Guid? WebResourceidUnique { get => this.Get<Guid?>("webresourceidunique"); set => this.Set("webresourceidunique", value); }

        //private static ColumnSet DefaultColumnSet = new ColumnSet(
        //    "webresourceid", "canbedeleted", "componentstate", "createdby", "createdon",
        //    "description", "displayname", "introducedversion", "isavailableformobileoffline",
        //    "iscustomizable", "isenabledformobileclient", "ishidden", "ismanaged", "modifiedby",
        //    "modifiedon", "name", "organizationid",
        //    "webresourcetype", "webresourceidunique"
        //);

        public static List<WebResource> FindByNames(OrganizationService service, bool includeSystemWebresource, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<WebResource>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<WebResource>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("webresourceid", "name")
            };

            if (!includeSystemWebresource)
            {
                query.Criteria.AddCondition("name", ConditionOperator.NotLike, "msdyn[_]%");
                query.Criteria.AddCondition("name", ConditionOperator.NotLike, "adx[_]%");
                query.Criteria.AddCondition("name", ConditionOperator.NotLike, "cc[_]MscrmControls%");
                query.Criteria.AddCondition("name", ConditionOperator.NotLike, "%/%");
            }

            Utilities.Helper.ApplyPatternFilter(query, "name", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<WebResource>(query);
        }

        public static WebResource GetById(OrganizationService service, Guid id)
        {
            return service.Retrieve<WebResource>(EntityLogicalName, id);
        }

        public object GetMetadataObject(bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.CanBeDeleted,
                    this.ComponentState,
                    this.CreatedBy,
                    this.CreatedOn,
                    this.DependencyXML,
                    this.Description,
                    this.DisplayName,
                    this.Id,
                    this.IntroducedVersion,
                    this.IsAvailableForMobileOffline,
                    this.IsCustomizable,
                    this.IsEnabledForMobileClient,
                    this.IsHidden,
                    this.IsManaged,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.Name,
                    this.OrganizationId,
                    this.WebResourceType,
                    this.WebResourceidUnique,
                };
            }
            else
            {
                return new
                {
                    CanBeDeleted = this.CanBeDeleted.GetMetadataObject(includeAllProperty),
                    this.ComponentState,
                    this.DependencyXML,
                    this.Description,
                    this.DisplayName,
                    this.IsAvailableForMobileOffline,
                    IsCustomizable = this.IsCustomizable.GetMetadataObject(includeAllProperty),
                    this.IsEnabledForMobileClient,
                    IsHidden = this.IsHidden.GetMetadataObject(includeAllProperty),
                    this.Name,
                    this.WebResourceType,
                };
            }
        }
    }
}
