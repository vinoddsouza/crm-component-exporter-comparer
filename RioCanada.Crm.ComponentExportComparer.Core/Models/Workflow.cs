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
    enum WorkflowCategory
    {
        [Description("Workflow")]
        Workflow = 0,
        [Description("Business Rule")]
        BusinessRule = 2,
        [Description("Action")]
        Action = 3,
        [Description("Business Process Flow")]
        BusinessProcessFlow = 4,
        [Description("Modern Flow")]
        ModernFlow = 5,
    }

    [EntityLogicalName(EntityLogicalName)]
    class Workflow : Entity
    {
        public const string EntityLogicalName = "workflow";
        public Workflow() : base(EntityLogicalName) { }

        public Workflow(Guid id) : base(EntityLogicalName, id) { }

        public string UniqueKey
        {
            get
            {
                if (this.Category?.Value == (int)WorkflowCategory.Action)
                {
                    if (!string.IsNullOrEmpty(this.UniqueName))
                    {
                        return $"{this.UniqueName}-{this.Type?.Value}";
                    }
                }

                return this.Id.ToString();
            }
        }

        public EntityReference ActiveWorkflowId { get => this.Get<EntityReference>("activeworkflowid"); set => this.Set("activeworkflowid", value); }
        public bool AsyncAutoDelete { get => this.Get<bool>("asyncautodelete"); set => this.Set("asyncautodelete", value); }
        public OptionSetValue BusinessProcessType { get => this.Get<OptionSetValue>("businessprocesstype"); set => this.Set("businessprocesstype", value); }
        public OptionSetValue Category { get => this.Get<OptionSetValue>("category"); set => this.Set("category", value); }
        public string ClientData { get => this.Get<string>("clientdata"); set => this.Set("clientdata", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public OptionSetValue CreateStage { get => this.Get<OptionSetValue>("createstage"); set => this.Set("createstage", value); }
        public string Description { get => this.Get<string>("description"); set => this.Set("description", value); }
        public Guid? EntityImageId { get => this.Get<Guid?>("entityimageid"); set => this.Set("entityimageid", value); }
        public long? EntityImageTimestamp { get => this.Get<long?>("entityimage_timestamp"); set => this.Set("entityimage_timestamp", value); }
        public string EntityImageUrl { get => this.Get<string>("entityimage_url"); set => this.Set("entityimage_url", value); }
        public Guid? FormId { get => this.Get<Guid?>("formid"); set => this.Set("formid", value); }
        public string InputParameters { get => this.Get<string>("inputparameters"); set => this.Set("inputparameters", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public bool IsCrmUiWorkflow { get => this.Get<bool>("iscrmuiworkflow"); set => this.Set("iscrmuiworkflow", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public BooleanManagedProperty IsCustomProcessingStepAllowedForOtherPublishers { get => this.Get<BooleanManagedProperty>("iscustomprocessingstepallowedforotherpublishers"); set => this.Set("iscustomprocessingstepallowedforotherpublishers", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public bool IsTransacted { get => this.Get<bool>("istransacted"); set => this.Set("istransacted", value); }
        public OptionSetValue Mode { get => this.Get<OptionSetValue>("mode"); set => this.Set("mode", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public bool OnDemand { get => this.Get<bool>("ondemand"); set => this.Set("ondemand", value); }
        public EntityReference OwnerId { get => this.Get<EntityReference>("ownerid"); set => this.Set("ownerid", value); }
        public EntityReference OwningBusinessUnit { get => this.Get<EntityReference>("owningbusinessunit"); set => this.Set("owningbusinessunit", value); }
        public EntityReference OwningUser { get => this.Get<EntityReference>("owninguser"); set => this.Set("owninguser", value); }
        public EntityReference ParentWorkflowId { get => this.Get<EntityReference>("parentworkflowid"); set => this.Set("parentworkflowid", value); }
        public string PrimaryEntity { get => this.Get<string>("primaryentity"); set => this.Set("primaryentity", value); }
        public int? ProcessOrder { get => this.Get<int?>("processorder"); set => this.Set("processorder", value); }
        public string ProcessRoleAssignment { get => this.Get<string>("processroleassignment"); set => this.Set("processroleassignment", value); }
        public Guid? ProcessTriggerFormId { get => this.Get<Guid?>("processtriggerformid"); set => this.Set("processtriggerformid", value); }
        public OptionSetValue ProcessTriggerScope { get => this.Get<OptionSetValue>("processtriggerscope"); set => this.Set("processtriggerscope", value); }
        public OptionSetValue RunAs { get => this.Get<OptionSetValue>("runas"); set => this.Set("runas", value); }
        public OptionSetValue Scope { get => this.Get<OptionSetValue>("scope"); set => this.Set("scope", value); }
        public EntityReference SdkMessageId { get => this.Get<EntityReference>("sdkmessageid"); set => this.Set("sdkmessageid", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }
        public OptionSetValue StateCode { get => this.Get<OptionSetValue>("statecode"); set => this.Set("statecode", value); }
        public OptionSetValue StatusCode { get => this.Get<OptionSetValue>("statuscode"); set => this.Set("statuscode", value); }
        public bool SubProcess { get => this.Get<bool>("subprocess"); set => this.Set("subprocess", value); }
        public bool SyncWorkflowLogOnFailure { get => this.Get<bool>("syncworkflowlogonfailure"); set => this.Set("syncworkflowlogonfailure", value); }
        public bool TriggerOnCreate { get => this.Get<bool>("triggeroncreate"); set => this.Set("triggeroncreate", value); }
        public bool TriggerOnDelete { get => this.Get<bool>("triggerondelete"); set => this.Set("triggerondelete", value); }
        public string TriggerOnUpdateAttributeList { get => this.Get<string>("triggeronupdateattributelist"); set => this.Set("triggeronupdateattributelist", value); }
        public OptionSetValue Type { get => this.Get<OptionSetValue>("type"); set => this.Set("type", value); }
        public string UiData { get => this.Get<string>("uidata"); set => this.Set("uidata", value); }
        public string UniqueName { get => this.Get<string>("uniquename"); set => this.Set("uniquename", value); }
        public OptionSetValue UpdateStage { get => this.Get<OptionSetValue>("updatestage"); set => this.Set("updatestage", value); }
        public Guid WorkflowIdUnique { get => this.Get<Guid>("workflowidunique"); set => this.Set("workflowidunique", value); }
        public string Xaml { get => this.Get<string>("xaml"); set => this.Set("xaml", value); }

        public static Workflow GetById(OrganizationService service, Guid id)
        {
            return service.Retrieve<Workflow>(EntityLogicalName, id);
        }

        private static List<Workflow> FindByNames(OrganizationService service, int category, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<Workflow>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<Workflow>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("workflowid", "name")
            };

            query.Criteria.AddCondition("category", ConditionOperator.Equal, category);

            Utilities.Helper.ApplyPatternFilter(query, "name", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<Workflow>(query);
        }

        public static List<Workflow> FindWorkflowByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)WorkflowCategory.Workflow, patterns, solutionIds);
        }

        public static List<Workflow> FindBusinessRuleByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)WorkflowCategory.BusinessRule, patterns, solutionIds);
        }

        public static List<Workflow> FindActionByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)WorkflowCategory.Action, patterns, solutionIds);
        }

        public static List<Workflow> FindBusinessProcessFlowByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)WorkflowCategory.BusinessProcessFlow, patterns, solutionIds);
        }

        public object GetMetadataObjectByCategory(bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.ActiveWorkflowId,
                    this.AsyncAutoDelete,
                    this.BusinessProcessType,
                    this.Category,
                    this.ComponentState,
                    this.CreatedBy,
                    this.CreatedOn,
                    this.CreateStage,
                    this.Description,
                    this.EntityImageId,
                    this.EntityImageTimestamp,
                    this.EntityImageUrl,
                    this.FormId,
                    this.Id,
                    this.InputParameters,
                    this.IntroducedVersion,
                    this.IsCrmUiWorkflow,
                    this.IsCustomizable,
                    this.IsCustomProcessingStepAllowedForOtherPublishers,
                    this.IsManaged,
                    this.IsTransacted,
                    this.Mode,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.Name,
                    this.OnDemand,
                    this.OwnerId,
                    this.OwningBusinessUnit,
                    this.OwningUser,
                    this.ParentWorkflowId,
                    this.PrimaryEntity,
                    this.ProcessOrder,
                    this.ProcessRoleAssignment,
                    this.ProcessTriggerFormId,
                    this.ProcessTriggerScope,
                    this.RunAs,
                    this.Scope,
                    this.SdkMessageId,
                    this.SolutionId,
                    this.StateCode,
                    this.StatusCode,
                    this.SubProcess,
                    this.SyncWorkflowLogOnFailure,
                    this.TriggerOnCreate,
                    this.TriggerOnDelete,
                    this.TriggerOnUpdateAttributeList,
                    this.Type,
                    this.UiData,
                    this.UniqueName,
                    this.UpdateStage,
                    this.WorkflowIdUnique,
                };
            }
            else
            {
                return new
                {
                    this.AsyncAutoDelete,
                    this.BusinessProcessType,
                    this.Category,
                    this.ComponentState,
                    //this.CreatedBy,
                    this.CreateStage,
                    this.Description,
                    //this.EntityImageId,
                    //this.EntityImageTimestamp,
                    //this.EntityImageUrl,
                    this.FormId,
                    //this.Id,
                    this.InputParameters,
                    this.IsCrmUiWorkflow,
                    IsCustomizable = this.IsCustomizable?.GetMetadataObject(includeAllProperty),
                    IsCustomProcessingStepAllowedForOtherPublishers = this.IsCustomProcessingStepAllowedForOtherPublishers?.GetMetadataObject(includeAllProperty),
                    this.IsTransacted,
                    this.Mode,
                    this.Name,
                    this.OnDemand,
                    //this.OwnerId,
                    //this.OwningBusinessUnit,
                    //this.OwningUser,
                    this.ParentWorkflowId,
                    this.PrimaryEntity,
                    this.ProcessOrder,
                    this.ProcessRoleAssignment,
                    this.ProcessTriggerFormId,
                    this.ProcessTriggerScope,
                    this.RunAs,
                    this.Scope,
                    this.SdkMessageId,
                    this.StateCode,
                    this.StatusCode,
                    this.SubProcess,
                    this.SyncWorkflowLogOnFailure,
                    this.TriggerOnCreate,
                    this.TriggerOnDelete,
                    this.TriggerOnUpdateAttributeList,
                    this.Type,
                    this.UiData,
                    this.UniqueName,
                    this.UpdateStage,
                    this.UniqueKey,
                };
            }
        }
    }
}
