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
    class Privilege : Entity
    {
        public const string EntityLogicalName = "privilege";
        public Privilege() : base(EntityLogicalName) { }

        public Privilege(Guid id) : base(EntityLogicalName, id) { }

        public int AccessRight { get => this.Get<int>("accessright"); set => this.Set("accessright", value); }
        public bool CanBeBasic { get => this.Get<bool>("canbebasic"); set => this.Set("canbebasic", value); }
        public bool CanBeDeep { get => this.Get<bool>("canbedeep"); set => this.Set("canbedeep", value); }
        public bool CanBeEntityReference { get => this.Get<bool>("canbeentityreference"); set => this.Set("canbeentityreference", value); }
        public bool CanBeGlobal { get => this.Get<bool>("canbeglobal"); set => this.Set("canbeglobal", value); }
        public bool CanBeLocal { get => this.Get<bool>("canbelocal"); set => this.Set("canbelocal", value); }
        public bool CanBeParentEntityReference { get => this.Get<bool>("canbeparententityreference"); set => this.Set("canbeparententityreference", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public Guid PrivilegeRowId { get => this.Get<Guid>("privilegerowid"); set => this.Set("privilegerowid", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }

        public static List<Privilege> GetByRoleId(OrganizationService service, Guid roleId)
        {
            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.ColumnSet.AllColumns = true;
            var linkEntity = query.AddLink("roleprivileges", "privilegeid", "privilegeid");
            linkEntity.LinkCriteria.AddCondition("roleid", ConditionOperator.Equal, roleId);

            return service.GetData<Privilege>(query);
        }

        public static List<Privilege> GetByRoleIds(OrganizationService service, IEnumerable<Guid> roleIds)
        {
            var count = roleIds.Count();

            if (count == 0) return new List<Privilege>();
            if (count > 100) throw new ArgumentException("Maximum 100 role id can be passed", nameof(roleIds));

            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.ColumnSet.AllColumns = true;
            var linkEntity = query.AddLink("roleprivileges", "privilegeid", "privilegeid");
            linkEntity.EntityAlias = "roleprivileges";
            linkEntity.Columns.AddColumn("roleid");
            linkEntity.JoinOperator = JoinOperator.Inner;

            var condition = new ConditionExpression("roleid", ConditionOperator.In);
            linkEntity.LinkCriteria.AddCondition(condition);
            roleIds.ToList().ForEach(x => condition.Values.Add(x));

            var privileges = service.GetBigData<Privilege>(query);

            if (IsPrivilegeContainsDuplicate(privileges))
            {
                System.Threading.Thread.Sleep(2000);
                privileges = service.GetBigData<Privilege>(query);

                if (IsPrivilegeContainsDuplicate(privileges))
                {
                    System.Threading.Thread.Sleep(2000);
                    privileges = service.GetBigData<Privilege>(query);
                }
            }

            return privileges;
        }

        private static bool IsPrivilegeContainsDuplicate(List<Privilege> privileges)
        {
            return privileges.GroupBy(x => new
            {
                Name = x.Name,
                RoleId = (Guid)x.Get<AliasedValue>("roleprivileges.roleid").Value,
            }).Where(x => x.Count() > 1).Count() > 0;
        }

        public Transform.Metadata.PrivilegeMetadata GetMetadataObject(bool includeAllProperty)
        {
            var result = new Transform.Metadata.PrivilegeMetadata
            {
                AccessRight = this.AccessRight,
                CanBeBasic = this.CanBeBasic,
                CanBeDeep = this.CanBeDeep,
                CanBeEntityReference = this.CanBeEntityReference,
                CanBeGlobal = this.CanBeGlobal,
                CanBeLocal = this.CanBeLocal,
                CanBeParentEntityReference = this.CanBeParentEntityReference,
                ComponentState = this.ComponentState,
                IntroducedVersion = this.IntroducedVersion,
                IsManaged = this.IsManaged,
                Name = this.Name,
                PrivilegeRowId = this.PrivilegeRowId,
                SolutionId = this.SolutionId,
            };

            if (!includeAllProperty)
            {
                result.ChangeIncludeAllProperty(false);
            }

            return result;
        }
    }
}
