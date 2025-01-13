using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    enum RootComponentBehavior
    {
        [Description("Do not include subcomponents")]
        DoNotIncludeSubcomponents = 1,
        [Description("Include As Shell Only")]
        IncludeAsShellOnly = 2,
        [Description("Include Subcomponents")]
        IncludeSubcomponents = 0,
    }

    [EntityLogicalName(EntityLogicalName)]
    class SolutionComponent : Entity
    {
        public const string EntityLogicalName = "solutioncomponent";

        public SolutionComponent() : base(EntityLogicalName) { }

        public SolutionComponent(Guid id) : base(EntityLogicalName, id) { }

        public OptionSetValue ComponentType { get => this.Get<OptionSetValue>("componenttype"); set => this.Set("componenttype", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public bool IsMetaData { get => this.Get<bool>("ismetadata"); set => this.Set("ismetadata", value); }
        public Guid ObjectId { get => this.Get<Guid>("objectid"); set => this.Set("objectid", value); }
        public OptionSetValue RootComponentBehavior { get => this.Get<OptionSetValue>("rootcomponentbehavior"); set => this.Set("rootcomponentbehavior", value); }
        public EntityReference SolutionId { get => this.Get<EntityReference>("solutionid"); set => this.Set("solutionid", value); }

        public static List<SolutionComponent> GetBySolutionIds(OrganizationService service, IEnumerable<Guid> solutionIds, int componentType)
        {
            QueryExpression query = new QueryExpression(EntityLogicalName);
            query.ColumnSet.AllColumns = true;
            query.Criteria.AddCondition("componenttype", ConditionOperator.Equal, componentType);

            var condition = new ConditionExpression("solutionid", ConditionOperator.In);
            query.Criteria.AddCondition(condition);
            solutionIds.ToList().ForEach(x => condition.Values.Add(x));

            return service.GetData<SolutionComponent>(query);
        }

        public static List<OptionSetMetadataBase> FindOptionSetByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            var optionSetRequest = new Microsoft.Xrm.Sdk.Messages.RetrieveAllOptionSetsRequest { };
            var optionSetResponse = (Microsoft.Xrm.Sdk.Messages.RetrieveAllOptionSetsResponse)(service.Execute(optionSetRequest));

            var optionSets = optionSetResponse.OptionSetMetadata.ToList();

            if (solutionIds != null && solutionIds.Count() > 0)
            {
                var solutionComponents = GetBySolutionIds(service, solutionIds, (int)Models.ComponentType.OptionSet);

                for (var i = 0; i < optionSets.Count; i++)
                {
                    if (solutionComponents.FindIndex(x => x.ObjectId == optionSets[i].MetadataId) == -1)
                    {
                        optionSets.RemoveAt(i);
                        i--;
                    }
                }
            }

            for (var i = 0; i < optionSets.Count; i++)
            {
                if (!Utilities.Helper.IsWildcardStringMatch(optionSets[i].Name, patterns))
                {
                    optionSets.RemoveAt(i);
                    i--;
                }
            }

            return optionSets;
        }
    }
}
