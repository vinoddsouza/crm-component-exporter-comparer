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
    [EntityLogicalName(EntityLogicalName)]
    class SavedQuery : Entity
    {
        public const string EntityLogicalName = "savedquery";

        public SavedQuery() : base(EntityLogicalName) { }

        public SavedQuery(Guid id) : base(EntityLogicalName, id) { }

        public BooleanManagedProperty CanBeDeleted { get => this.Get<BooleanManagedProperty>("canbedeleted"); set => this.Set("canbedeleted", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public string FetchXml { get => this.Get<string>("fetchxml"); set => this.Set("fetchxml", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public bool IsCustom { get => this.Get<bool>("iscustom"); set => this.Set("iscustom", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public bool IsDefault { get => this.Get<bool>("isdefault"); set => this.Set("isdefault", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public bool IsPrivate { get => this.Get<bool>("isprivate"); set => this.Set("isprivate", value); }
        public bool IsQuickfindQuery { get => this.Get<bool>("isquickfindquery"); set => this.Set("isquickfindquery", value); }
        public bool IsUserDefined { get => this.Get<bool>("isuserdefined"); set => this.Set("isuserdefined", value); }
        public string LayoutJson { get => this.Get<string>("layoutjson"); set => this.Set("layoutjson", value); }
        public string LayoutXml { get => this.Get<string>("layoutxml"); set => this.Set("layoutxml", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public string OfflineSqlQuery { get => this.Get<string>("offlinesqlquery"); set => this.Set("offlinesqlquery", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public string QueryApi { get => this.Get<string>("queryapi"); set => this.Set("queryapi", value); }
        public int QueryType { get => this.Get<int>("querytype"); set => this.Set("querytype", value); }
        public string ReturnedTypeCode { get => this.Get<string>("returnedtypecode"); set => this.Set("returnedtypecode", value); }
        public Guid SavedQueryIdUnique { get => this.Get<Guid>("savedqueryidunique"); set => this.Set("savedqueryidunique", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }
        public OptionSetValue StateCode { get => this.Get<OptionSetValue>("statecode"); set => this.Set("statecode", value); }
        public OptionSetValue StatusCode { get => this.Get<OptionSetValue>("statuscode"); set => this.Set("statuscode", value); }

        public static List<SavedQuery> RetriveMultipleByObjectTypeCode(OrganizationService service, int objectTypeCode)
        {
            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.ColumnSet.AllColumns = true;
            query.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, objectTypeCode);

            return service.GetData<SavedQuery>(query);
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
                    this.IntroducedVersion,
                    this.Id,
                    this.IsCustom,
                    this.IsCustomizable,
                    this.IsDefault,
                    this.IsManaged,
                    this.IsPrivate,
                    this.IsQuickfindQuery,
                    this.IsUserDefined,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.Name,
                    this.OfflineSqlQuery,
                    this.OrganizationId,
                    this.QueryApi,
                    this.QueryType,
                    this.ReturnedTypeCode,
                    this.SavedQueryIdUnique,
                    this.SolutionId,
                    this.StateCode,
                    this.StatusCode,
                };
            }
            else
            {
                return new
                {
                    CanBeDeleted = this.CanBeDeleted.GetMetadataObject(includeAllProperty),
                    this.ComponentState,
                    this.Id,
                    IsCustomizable = this.IsCustomizable.GetMetadataObject(includeAllProperty),
                    this.IsDefault,
                    this.IsPrivate,
                    this.IsQuickfindQuery,
                    this.IsUserDefined,
                    this.Name,
                    this.OfflineSqlQuery,
                    this.QueryApi,
                    this.QueryType,
                    this.ReturnedTypeCode,
                    this.StateCode,
                    this.StatusCode,
                };
            }
        }
    }
}
