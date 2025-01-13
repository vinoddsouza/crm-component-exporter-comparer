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
    [EntityLogicalName(EntityLogicalName)]
    class SecurityRole : Entity
    {
        public const string EntityLogicalName = "role";
        public SecurityRole() : base(EntityLogicalName) { }

        public SecurityRole(Guid id) : base(EntityLogicalName, id) { }

        public EntityReference BusinessUnitId { get => this.Get<EntityReference>("businessunitid"); set => this.Set("businessunitid", value); }
        public BooleanManagedProperty CanBeDeleted { get => this.Get<BooleanManagedProperty>("canbedeleted"); set => this.Set("canbedeleted", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public OptionSetValue IsInherited { get => this.Get<OptionSetValue>("isinherited"); set => this.Set("isinherited", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public Guid OrganizationId { get => this.Get<Guid>("organizationid"); set => this.Set("organizationid", value); }
        public EntityReference ParentRoleId { get => this.Get<EntityReference>("parentroleid"); set => this.Set("parentroleid", value); }
        public EntityReference ParentRootRoleId { get => this.Get<EntityReference>("parentrootroleid"); set => this.Set("parentrootroleid", value); }
        public Guid RoleIdUnique { get => this.Get<Guid>("roleidunique"); set => this.Set("roleidunique", value); }
        public EntityReference RoleTemplateId { get => this.Get<EntityReference>("roletemplateid"); set => this.Set("roletemplateid", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }

        public static SecurityRole GetById(OrganizationService service, Guid id)
        {
            return service.Retrieve<SecurityRole>(EntityLogicalName, id);
        }

        public static string GetNameById(OrganizationService service, Guid id)
        {
            string key = "SECURITYROLE_DATA";

            if (!service.CacheData.ContainsKey(key))
            {
                QueryExpression query = new QueryExpression(EntityLogicalName);
                query.ColumnSet.AddColumns(EntityLogicalName + "id", "name");
                service.CacheData[key] = service.GetData<SecurityRole>(query);
            }

            var data = service.CacheData[key] as List<SecurityRole>;

            var item = data.Find(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Record not found in \"{EntityLogicalName}\" for id {id}");
            }

            return item.Name;
        }

        public static List<SecurityRole> FindSecurityRoleByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<SecurityRole>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<SecurityRole>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("roleid", "name")
            };

            // Child role have same permission as parent
            query.Criteria.AddCondition("parentroleid", ConditionOperator.Null);

            // Exclude system roles
            query.Criteria.AddCondition("name", ConditionOperator.NotEqual, "System Administrator");
            query.Criteria.AddCondition("name", ConditionOperator.NotEqual, "System Support");
            query.Criteria.AddCondition("name", ConditionOperator.NotEqual, "System Customizer");
            query.Criteria.AddCondition("name", ConditionOperator.NotEqual, "Support User");

            Utilities.Helper.ApplyPatternFilter(query, "name", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<SecurityRole>(query);
        }

        public Transform.Metadata.SecurityRoleMetadata GetMetadataObject(bool includeAllProperty, bool RoleById)
        {
            var result = new Transform.Metadata.SecurityRoleMetadata
            {
                BusinessUnitId = this.BusinessUnitId,
                CanBeDeleted = this.CanBeDeleted?.GetMetadataObject(includeAllProperty),
                ComponentState = this.ComponentState,
                CreatedBy = this.CreatedBy,
                CreatedOn = this.CreatedOn,
                Id = this.Id,
                IsCustomizable = this.IsCustomizable?.GetMetadataObject(includeAllProperty),
                IsInherited = this.IsInherited,
                IsManaged = this.IsManaged,
                ModifiedBy = this.ModifiedBy,
                ModifiedOn = this.ModifiedOn,
                Name = this.Name,
                OrganizationId = this.OrganizationId,
                ParentRoleId = this.ParentRoleId,
                ParentRootRoleId = this.ParentRootRoleId,
                RoleIdUnique = this.RoleIdUnique,
                RoleTemplateId = this.RoleTemplateId,
                SolutionId = this.SolutionId,
            };

            if (!includeAllProperty)
            {
                result.Id = RoleById ? this.Id : (Guid?)null;
                result.ChangeIncludeAllProperty(false);
            }

            return result;
        }
    }
}
