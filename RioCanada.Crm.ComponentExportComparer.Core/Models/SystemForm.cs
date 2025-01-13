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
    enum FormType
    {
        [Description("Appointment Book")]
        AppointmentBook = 1,
        [Description("Appointment Book Backup")]
        AppointmentBookBackup = 102,
        [Description("Card")]
        Card = 11,
        [Description("ContextualDashboard")]
        ContextualDashboard = 13,
        [Description("Dashboard")]
        Dashboard = 0,
        [Description("Dialog")]
        Dialog = 8,
        [Description("InteractionCentricDashboard")]
        InteractionCentricDashboard = 10,
        [Description("Main - Interaction Experience")]
        MainInteractionExperience = 12,
        [Description("Main")]
        Main = 2,
        [Description("MainBackup")]
        MainBackup = 101,
        [Description("MiniCampaignBO")]
        MiniCampaignBO = 3,
        [Description("Mobile - Express")]
        MobileExpress = 5,
        [Description("Other")]
        Other = 100,
        [Description("Power BI Dashboard")]
        PowerBIDashboard = 103,
        [Description("Preview")]
        Preview = 4,
        [Description("Quick Create")]
        QuickCreate = 7,
        [Description("Quick View Form")]
        QuickViewForm = 6,
        [Description("Task Flow Form")]
        TaskFlowForm = 9,
    }

    [EntityLogicalName(EntityLogicalName)]
    class SystemForm : Entity
    {
        public const string EntityLogicalName = "systemform";

        public SystemForm() : base(EntityLogicalName) { }

        public SystemForm(Guid id) : base(EntityLogicalName, id) { }

        public BooleanManagedProperty CanBeDeleted { get => this.Get<BooleanManagedProperty>("canbedeleted"); set => this.Set("canbedeleted", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public string Description { get => this.Get<string>("description"); set => this.Set("description", value); }
        public OptionSetValue FormActivationState { get => this.Get<OptionSetValue>("formactivationstate"); set => this.Set("formactivationstate", value); }
        public Guid FormIdUnique { get => this.Get<Guid>("formidunique"); set => this.Set("formidunique", value); }
        public string FormJson { get => this.Get<string>("formjson"); set => this.Set("formjson", value); }
        public OptionSetValue FormPresentation { get => this.Get<OptionSetValue>("formpresentation"); set => this.Set("formpresentation", value); }
        public string FormXml { get => this.Get<string>("formxml"); set => this.Set("formxml", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public bool IsDesktOpenabled { get => this.Get<bool>("isdesktopenabled"); set => this.Set("isdesktopenabled", value); }
        public bool IsAirMerged { get => this.Get<bool>("isairmerged"); set => this.Set("isairmerged", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public bool IsDefault { get => this.Get<bool>("isdefault"); set => this.Set("isdefault", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public bool IsTabletEnabled { get => this.Get<bool>("istabletenabled"); set => this.Set("istabletenabled", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public string ObjectTypeCode { get => this.Get<string>("objecttypecode"); set => this.Set("objecttypecode", value); }
        public EntityReference Organizationid { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public DateTime? PublishedOn { get => this.Get<DateTime?>("publishedon"); set => this.Set("publishedon", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }
        public OptionSetValue Type { get => this.Get<OptionSetValue>("type"); set => this.Set("type", value); }
        public string UniqueName { get => this.Get<string>("uniquename"); set => this.Set("uniquename", value); }

        public static List<SystemForm> RetriveMultipleByObjectTypeCode(OrganizationService service, int objectTypeCode)
        {
            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.ColumnSet.AllColumns = true;
            query.Criteria.AddCondition("objecttypecode", ConditionOperator.Equal, objectTypeCode);

            return service.GetData<SystemForm>(query);
        }

        public static SystemForm GetById(OrganizationService service, Guid id)
        {
            return service.Retrieve<SystemForm>(EntityLogicalName, id);
        }

        public static List<SystemForm> FindDashboardByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<SystemForm>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<SystemForm>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("formid", "name")
            };

            // Filter system forms which not related to any entity
            query.Criteria.AddCondition("objecttypecode", ConditionOperator.Equal, 0);

            // Filter system forms by dashboard types only
            var filter = new FilterExpression(LogicalOperator.Or);
            filter.AddCondition("type", ConditionOperator.Equal, (int)FormType.Dashboard);
            filter.AddCondition("type", ConditionOperator.Equal, (int)FormType.InteractionCentricDashboard);
            filter.AddCondition("type", ConditionOperator.Equal, (int)FormType.PowerBIDashboard);
            query.Criteria.Filters.Add(filter);

            Utilities.Helper.ApplyPatternFilter(query, "name", patterns);

            Utilities.Helper.ApplySolutionFilter(query, "formid", solutionIds);

            return service.GetBigData<SystemForm>(query);
        }

        public object GetMetadataObject(bool includeAllProperty = false)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.CanBeDeleted,
                    this.ComponentState,
                    this.Description,
                    this.FormActivationState,
                    this.FormIdUnique,
                    this.FormPresentation,
                    this.Id,
                    this.IntroducedVersion,
                    this.IsDesktOpenabled,
                    this.IsAirMerged,
                    this.IsCustomizable,
                    this.IsDefault,
                    this.IsManaged,
                    this.IsTabletEnabled,
                    this.Name,
                    this.ObjectTypeCode,
                    this.Organizationid,
                    this.PublishedOn,
                    this.SolutionId,
                    this.Type,
                    this.UniqueName,
                };
            }
            else
            {
                return new
                {
                    CanBeDeleted = this.CanBeDeleted.GetMetadataObject(includeAllProperty),
                    this.ComponentState,
                    this.Description,
                    this.FormActivationState,
                    this.FormPresentation,
                    this.Id,
                    this.IsDesktOpenabled,
                    this.IsAirMerged,
                    IsCustomizable = this.IsCustomizable.GetMetadataObject(includeAllProperty),
                    this.IsDefault,
                    this.IsTabletEnabled,
                    this.Name,
                    this.ObjectTypeCode,
                    this.Type,
                    this.UniqueName,
                };
            }
        }
    }
}
