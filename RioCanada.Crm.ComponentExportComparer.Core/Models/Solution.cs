using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    [EntityLogicalName(EntityLogicalName)]
    class Solution : Entity
    {
        public const string EntityLogicalName = "solution";

        public Solution() : base(EntityLogicalName) { }

        public Solution(Guid id) : base(EntityLogicalName, id) { }

        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public string ConfigurationPageId { get => this.Get<string>("configurationpageid"); set => this.Set("configurationpageid", value); }
        public string Description { get => this.Get<string>("description"); set => this.Set("description", value); }
        public string FriendlyName { get => this.Get<string>("friendlyname"); set => this.Set("friendlyname", value); }
        public DateTime? InstalledOn { get => this.Get<DateTime?>("installedon"); set => this.Set("installedon", value); }
        public bool IsApiManaged { get => this.Get<bool>("isapimanaged"); set => this.Set("isapimanaged", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public bool IsVisible { get => this.Get<bool>("isvisible"); set => this.Set("isvisible", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public EntityReference ParentSolutionId { get => this.Get<EntityReference>("parentsolutionid"); set => this.Set("parentsolutionid", value); }
        public EntityReference PublisherId { get => this.Get<EntityReference>("publisherid"); set => this.Set("publisherid", value); }
        public string SolutionPackageVersion { get => this.Get<string>("solutionpackageversion"); set => this.Set("solutionpackageversion", value); }
        public string SolutionType { get => this.Get<string>("solutiontype"); set => this.Set("solutiontype", value); }
        public string Version { get => this.Get<string>("version"); set => this.Set("version", value); }
        public DateTime? UpdatedOn { get => this.Get<DateTime?>("updatedon"); set => this.Set("description", value); }
        public string UniqueName { get => this.Get<string>("uniquename"); set => this.Set("uniquename", value); }

        public static List<Solution> FindSolutions(OrganizationService service, IEnumerable<string> patterns)
        {
            if (patterns.Count() == 0)
            {
                return new List<Solution>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<Solution>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.ColumnSet.AllColumns = true;

            Utilities.Helper.ApplyPatternFilter(query, "friendlyname", patterns);

            return service.GetData<Solution>(query);
        }

        public static ILookup<Guid, string> GetSolutionLookup(OrganizationService service)
        {
            QueryExpression query = new QueryExpression(Solution.EntityLogicalName);
            query.ColumnSet.AddColumns("solutionid", "friendlyname");

            return service.GetData<Solution>(query).ToLookup(x => x.Id, x => x.FriendlyName);
        }
    }
}
