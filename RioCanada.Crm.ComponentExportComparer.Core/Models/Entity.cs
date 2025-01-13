using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Messages;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    [EntityLogicalName(EntityLogicalName)]
    internal class CRMEntityTable : Entity
    {
        public const string EntityLogicalName = "entity";

        public CRMEntityTable() : base(EntityLogicalName) { }

        public CRMEntityTable(Guid id) : base(EntityLogicalName, id) { }

        public string AddressTableName { get => this.Get<string>("addresstablename"); set => this.Set("addresstablename", value); }
        public string AttributeLogicalName { get => this.Get<string>("logicalname"); set => this.Set("logicalname", value); }
        public string BaseTableName { get => this.Get<string>("basetablename"); set => this.Set("basetablename", value); }
        public string CollectionName { get => this.Get<string>("collectionname"); set => this.Set("collectionname", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public string ExtensionTableName { get => this.Get<string>("extensiontablename"); set => this.Set("extensiontablename", value); }
        public string ExternalCollectionName { get => this.Get<string>("externalcollectionname"); set => this.Set("externalcollectionname", value); }
        public string ExternalName { get => this.Get<string>("externalname"); set => this.Set("externalname", value); }
        public string EntitySetName { get => this.Get<string>("entitysetname"); set => this.Set("entitysetname", value); }
        public string LogicalCollectionName { get => this.Get<string>("logicalcollectionname"); set => this.Set("logicalcollectionname", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public string OriginalLocalizedCollectionName { get => this.Get<string>("originallocalizedcollectionname"); set => this.Set("originallocalizedcollectionname", value); }
        public string OriginalLocalizedName { get => this.Get<string>("originallocalizedname"); set => this.Set("originallocalizedname", value); }
        public DateTime? OverwriteTime { get => this.Get<DateTime?>("overwritetime"); set => this.Set("overwritetime", value); }
        public string ParentControllingAttributeName { get => this.Get<string>("parentcontrollingattributename"); set => this.Set("parentcontrollingattributename", value); }
        public string PhysicalName { get => this.Get<string>("physicalname"); set => this.Set("physicalname", value); }
        public string ReportViewName { get => this.Get<string>("reportviewname"); set => this.Set("reportviewname", value); }
        public EntityReference SolutionId { get => this.Get<EntityReference>("solutionid"); set => this.Set("solutionid", value); }
        public int VersionNumber { get => this.Get<int>("versionnumber"); set => this.Set("versionnumber", value); }

        private static ColumnSet DefaultColumnSet = new ColumnSet(
            "entityid", "addresstablename", "logicalname", "basetablename", "collectionname", "componentstate",
            "extensiontablename", "externalcollectionname", "externalname", "entitysetname", "logicalcollectionname",
            "name", "originallocalizedcollectionname", "originallocalizedname",
            "parentcontrollingattributename", "physicalname", "reportviewname"
        );
        public static List<CRMEntityTable> GetByObjectIds(OrganizationService service, IEnumerable<Guid> objectIds)
        {
            // Below field can return duplicate records
            // solutionid, versionnumber, overwritetime
            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = DefaultColumnSet
            };
            return service.GetData<CRMEntityTable>(query, "entityid", objectIds);
        }

        public static List<CRMEntityTable> FindByLogicalName(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (service.ConnectionInfo?.OrganizationMajorVersion == 8)
            {
                RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest
                {
                    EntityFilters = Microsoft.Xrm.Sdk.Metadata.EntityFilters.Entity,
                };

                var response = (RetrieveAllEntitiesResponse)service.Execute(request);

                var tables = response.EntityMetadata
                    .Where(x => x.EntitySetName != null)
                    .Select(x => new CRMEntityTable
                {
                    AttributeLogicalName = x.LogicalName,
                    Id = x.MetadataId.Value,
                    Name = x.SchemaName,
                }).ToList();

                if (solutionIds != null && solutionIds.Count() > 0)
                {
                    var solutionComponents = SolutionComponent.GetBySolutionIds(service, solutionIds, (int)ComponentType.Entity);

                    for (var i = 0; i < tables.Count; i++)
                    {
                        if (solutionComponents.FindIndex(x => x.ObjectId == tables[i].Id) == -1)
                        {
                            tables.RemoveAt(i);
                            i--;
                        }
                    }
                }

                for (var i = 0; i < tables.Count; i++)
                {
                    if (!Utilities.Helper.IsWildcardStringMatch(tables[i].AttributeLogicalName, patterns))
                    {
                        tables.RemoveAt(i);
                        i--;
                    }
                }

                return tables;
            }

            if (patterns.Count() == 0)
            {
                return new List<CRMEntityTable>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<CRMEntityTable>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.Distinct = true;
            query.ColumnSet = DefaultColumnSet;

            query.Criteria.AddCondition("logicalcollectionname", ConditionOperator.NotNull);
            query.Criteria.AddCondition("logicalname", ConditionOperator.NotLike, "new[_]reserveentity[_]%");

            Utilities.Helper.ApplyPatternFilter(query, "logicalname", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<CRMEntityTable>(query);
        }
    }
}
