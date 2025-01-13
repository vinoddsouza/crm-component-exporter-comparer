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
    class SiteMap : Entity
    {
        public const string EntityLogicalName = "sitemap";

        public string UniqueKey { get => string.IsNullOrEmpty(this.SiteMapNameUnique) ? this.Id.ToString() : this.SiteMapNameUnique; }

        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public bool IsAppAware { get => this.Get<bool>("isappaware"); set => this.Set("isappaware", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public Guid SiteMapIdUnique { get => this.Get<Guid>("sitemapidunique"); set => this.Set("sitemapidunique", value); }
        public string SiteMapName { get => this.Get<string>("sitemapname"); set => this.Set("sitemapname", value); }
        public string SiteMapNameUnique { get => this.Get<string>("sitemapnameunique"); set => this.Set("sitemapnameunique", value); }
        public string SiteMapXML { get => this.Get<string>("sitemapxml"); set => this.Set("sitemapxml", value); }
        public Guid SolutionId { get => this.Get<Guid>("solutionid"); set => this.Set("solutionid", value); }

        public static SiteMap GetById(OrganizationService service, Guid id)
        {
            return service.Retrieve<SiteMap>(EntityLogicalName, id);
        }

        public static string GetUniqueKeyById(OrganizationService service, Guid id)
        {
            string key = "SITEMAP_DATA";

            if (!service.CacheData.ContainsKey(key))
            {
                QueryExpression query = new QueryExpression(EntityLogicalName);
                query.ColumnSet.AddColumns(EntityLogicalName + "id", "sitemapnameunique");
                query.Criteria.AddCondition("sitemapid", ConditionOperator.NotEqual, Guid.Empty);
                service.CacheData[key] = service.GetData<SiteMap>(query);
            }

            var data = service.CacheData[key] as List<SiteMap>;

            var item = data.Find(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Record not found in \"sitemap\" for id {id}");
            }

            return item.UniqueKey;
        }

        public static List<SiteMap> FindSiteMapByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<SiteMap>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<SiteMap>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("sitemapid", "sitemapname")
            };

            query.Criteria.AddCondition("sitemapid", ConditionOperator.NotEqual, Guid.Empty);

            Utilities.Helper.ApplyPatternFilter(query, "sitemapname", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<SiteMap>(query);
        }

        public object GetMetadataObject(bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.ComponentState,
                    this.CreatedBy,
                    this.CreatedOn,
                    this.Id,
                    this.IsAppAware,
                    this.IsManaged,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.OrganizationId,
                    this.SiteMapIdUnique,
                    this.SiteMapName,
                    this.SiteMapNameUnique,
                    this.SolutionId,
                };
            }
            else
            {
                return new
                {
                    this.ComponentState,
                    //this.Id, // Id can different for different env
                    this.IsAppAware,
                    this.SiteMapName,
                    this.SiteMapNameUnique,
                };
            }
        }
    }
}
