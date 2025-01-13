using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    [Obsolete]
    [EntityLogicalName(EntityLogicalName)]
    class SolutionComponentSummary : Entity
    {
        public const string EntityLogicalName = "msdyn_solutioncomponentsummary";

        public SolutionComponentSummary() : base(EntityLogicalName) { }

        public SolutionComponentSummary(Guid id) : base(EntityLogicalName, id) { }

        public override Guid Id { get => this.Get<Guid>("msdyn_solutioncomponentsummaryId"); set => this.Set("msdyn_solutioncomponentsummaryId", value); }

        public string ComponentLogicalName { get => this.Get<string>("msdyn_componentlogicalname"); set => this.Set("msdyn_componentlogicalname", value); }
        public int ComponentType { get => this.Get<int>("msdyn_componenttype"); set => this.Set("msdyn_componenttype", value); }
        public string ComponentTypeName { get => this.Get<string>("msdyn_componenttypename"); set => this.Set("msdyn_componenttypename", value); }
        public string CreatedOn { get => this.Get<string>("msdyn_createdon"); set => this.Set("msdyn_createdon", value); }
        public string Deployment { get => this.Get<string>("msdyn_deployment"); set => this.Set("msdyn_deployment", value); }
        public string Description { get => this.Get<string>("msdyn_description"); set => this.Set("msdyn_description", value); }
        public string DisplayName { get => this.Get<string>("msdyn_displayname"); set => this.Set("msdyn_displayname", value); }
        public string EventHandler { get => this.Get<string>("msdyn_eventhandler"); set => this.Set("msdyn_eventhandler", value); }
        public string FieldSecurity { get => this.Get<string>("msdyn_fieldsecurity"); set => this.Set("msdyn_fieldsecurity", value); }
        public string FieldType { get => this.Get<string>("msdyn_fieldtype"); set => this.Set("msdyn_fieldtype", value); }
        public string IsAuditEnabled { get => this.Get<string>("msdyn_isauditenabled"); set => this.Set("msdyn_isauditenabled", value); }
        //public string IsAuditEnabledName { get => this.Get<string>("msdyn_isauditenabledname"); set => this.Set("msdyn_isauditenabledname", value); }
        public bool IsCustomizable { get => this.Get<bool>("msdyn_iscustomizable"); set => this.Set("msdyn_iscustomizable", value); }
        //public string IsCustomizableName { get => this.Get<string>("msdyn_iscustomizablename"); set => this.Set("msdyn_iscustomizablename", value); }
        public bool IsCustom { get => this.Get<bool>("msdyn_iscustom"); set => this.Set("msdyn_iscustom", value); }
        //public string IsCustomName { get => this.Get<string>("msdyn_iscustomname"); set => this.Set("msdyn_iscustomname", value); }
        public bool IsManaged { get => this.Get<bool>("msdyn_ismanaged"); set => this.Set("msdyn_ismanaged", value); }
        //public string IsManagedName { get => this.Get<string>("msdyn_ismanagedname"); set => this.Set("msdyn_ismanagedname", value); }
        public string LogicalCollectionName { get => this.Get<string>("msdyn_logicalcollectionname"); set => this.Set("msdyn_logicalcollectionname", value); }
        public string ModifiedOn { get => this.Get<string>("msdyn_modifiedon"); set => this.Set("msdyn_modifiedon", value); }
        public string Name { get => this.Get<string>("msdyn_name"); set => this.Set("msdyn_name", value); }
        public Guid ObjectId { get => this.Get<Guid>("msdyn_objectid"); set => this.Set("msdyn_objectid", value); }
        public string ObjectTypeCode { get => this.Get<string>("msdyn_objecttypecode"); set => this.Set("msdyn_objecttypecode", value); }
        public string PrimaryEntityName { get => this.Get<string>("msdyn_primaryentityname"); set => this.Set("msdyn_primaryentityname", value); }
        public string PrimaryIdAttribute { get => this.Get<string>("msdyn_primaryidattribute"); set => this.Set("msdyn_primaryidattribute", value); }
        public string RelatedEntity { get => this.Get<string>("msdyn_relatedentity"); set => this.Set("msdyn_relatedentity", value); }
        public string RelatedEntityAttribute { get => this.Get<string>("msdyn_relatedentityattribute"); set => this.Set("msdyn_relatedentityattribute", value); }
        public string SchemaName { get => this.Get<string>("msdyn_schemaname"); set => this.Set("msdyn_schemaname", value); }
        public string SolutionId { get => this.Get<string>("msdyn_solutionid"); set => this.Set("msdyn_solutionid", value); }
        public string StatusName { get => this.Get<string>("msdyn_statusname"); set => this.Set("msdyn_statusname", value); }
        public string SubType { get => this.Get<string>("msdyn_subtype"); set => this.Set("msdyn_subtype", value); }
        public string SyncToExternalSearchIndex { get => this.Get<string>("msdyn_synctoexternalsearchindex"); set => this.Set("msdyn_synctoexternalsearchindex", value); }
        public int Total { get => this.Get<int>("msdyn_total"); set => this.Set("msdyn_total", value); }
        public string TypeName { get => this.Get<string>("msdyn_typename"); set => this.Set("msdyn_typename", value); }

        [Obsolete]

        private static List<SolutionComponentSummary> FindByNames(OrganizationService service, int componentType, FilterExpression additionalFilter, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<SolutionComponentSummary>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<SolutionComponentSummary>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("msdyn_solutioncomponentsummaryId", "msdyn_name", "msdyn_objectid")
            };

            query.Criteria.AddCondition("msdyn_componenttype", ConditionOperator.Equal, componentType);

            if (additionalFilter != null)
            {
                query.Criteria.AddFilter(additionalFilter);
            }

            if (componentType == (int)Models.ComponentType.OptionSet)
            {
                if (patterns.Where(x => x == "*").Count() == 0)
                {
                    var filter = new FilterExpression
                    {
                        FilterOperator = LogicalOperator.Or
                    };
                    foreach (var pattern in patterns)
                    {
                        if (pattern.Contains("*"))
                        {
                            filter.AddCondition("msdyn_name", ConditionOperator.Like, pattern.Replace("*", ""));
                        }
                        else
                        {
                            filter.AddCondition("msdyn_name", ConditionOperator.Equal, pattern);
                        }
                    }

                    query.Criteria.AddFilter(filter);
                }
            }
            else
            {
                if (patterns.Where(x => x == "*").Count() == 0)
                {
                    var filter = new FilterExpression
                    {
                        FilterOperator = LogicalOperator.Or
                    };
                    foreach (var pattern in patterns)
                    {
                        if (pattern.Contains("*"))
                        {
                            filter.AddCondition("msdyn_name", ConditionOperator.Like, pattern.Replace("*", "%"));
                        }
                        else
                        {
                            filter.AddCondition("msdyn_name", ConditionOperator.Equal, pattern);
                        }
                    }

                    query.Criteria.AddFilter(filter);
                }
            }

            if (solutionIds != null && solutionIds.Count() > 0)
            {
                FilterExpression filter = new FilterExpression(LogicalOperator.Or);
                solutionIds.ToList().ForEach(x => filter.Conditions.Add(new ConditionExpression("msdyn_solutionid", ConditionOperator.Equal, x)));
                query.Criteria.Filters.Add(filter);
            }

            return service.GetBigData<SolutionComponentSummary>(query);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindOptionSetByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)Models.ComponentType.OptionSet, null, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindDashboardByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var filter = new FilterExpression(LogicalOperator.Or);
            filter.AddCondition("msdyn_subtype", ConditionOperator.Equal, "0");
            filter.AddCondition("msdyn_subtype", ConditionOperator.Equal, "10");
            filter.AddCondition("msdyn_subtype", ConditionOperator.Equal, "103");
            return FindByNames(service, (int)Models.ComponentType.SystemForm, filter, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindSiteMapByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)Models.ComponentType.SiteMap, null, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindModelDrivenAppByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, 80, null, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindProcessByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition("msdyn_workflowcategory", ConditionOperator.NotEqual, "5");
            return FindByNames(service, (int)Models.ComponentType.Workflow, filter, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindWorkflowByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition("msdyn_workflowcategory", ConditionOperator.Equal, "0");
            return FindByNames(service, (int)Models.ComponentType.Workflow, filter, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindBusinessRuleByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition("msdyn_workflowcategory", ConditionOperator.Equal, "2");
            return FindByNames(service, (int)Models.ComponentType.Workflow, filter, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindActionByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition("msdyn_workflowcategory", ConditionOperator.Equal, "3");
            return FindByNames(service, (int)Models.ComponentType.Workflow, filter, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindBusinessProcessFlowByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var filter = new FilterExpression(LogicalOperator.And);
            filter.AddCondition("msdyn_workflowcategory", ConditionOperator.Equal, "4");
            return FindByNames(service, (int)Models.ComponentType.Workflow, filter, patterns, solutionIds);
        }

        [Obsolete]
        public static List<SolutionComponentSummary> FindSecurityRoleByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            return FindByNames(service, (int)Models.ComponentType.Role, null, patterns, solutionIds);
        }
    }
}
