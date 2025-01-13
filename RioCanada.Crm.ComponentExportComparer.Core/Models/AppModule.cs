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
    class AppModule : Entity
    {
        public const string EntityLogicalName = "appmodule";
        public AppModule() : base(EntityLogicalName) { }

        public AppModule(Guid id) : base(EntityLogicalName, id) { }

        public Guid? AppModuleIdUnique { get => this.Get<Guid?>("appmoduleidunique"); set => this.Set("appmoduleidunique", value); }
        public string AppModuleXmlManaged { get => this.Get<string>("appmodulexmlmanaged"); set => this.Set("appmodulexmlmanaged", value); }
        public int? ClientType { get => this.Get<int?>("clienttype"); set => this.Set("clienttype", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public string ConfigXml { get => this.Get<string>("configxml"); set => this.Set("configxml", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public string Description { get => this.Get<string>("description"); set => this.Set("description", value); }
        public string Descriptor { get => this.Get<string>("descriptor"); set => this.Set("descriptor", value); }
        public string EventHandlers { get => this.Get<string>("eventhandlers"); set => this.Set("eventhandlers", value); }
        public int? FormFactor { get => this.Get<int?>("formfactor"); set => this.Set("formfactor", value); }
        public bool IsDefault { get => this.Get<bool>("isdefault"); set => this.Set("isdefault", value); }
        public bool IsFeatured { get => this.Get<bool>("isfeatured"); set => this.Set("isfeatured", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public OptionSetValue NavigationType { get => this.Get<OptionSetValue>("navigationtype"); set => this.Set("navigationtype", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public string PptimizedFor { get => this.Get<string>("optimizedfor"); set => this.Set("optimizedfor", value); }
        public DateTime? PublishedOn { get => this.Get<DateTime?>("publishedon"); set => this.Set("publishedon", value); }
        public EntityReference PublisherId { get => this.Get<EntityReference>("publisherid"); set => this.Set("publisherid", value); }
        public Guid? SolutionId { get => this.Get<Guid?>("solutionid"); set => this.Set("solutionid", value); }
        public OptionSetValue StateCode { get => this.Get<OptionSetValue>("statecode"); set => this.Set("statecode", value); }
        public OptionSetValue StatusCode { get => this.Get<OptionSetValue>("statuscode"); set => this.Set("statuscode", value); }
        public string UniqueName { get => this.Get<string>("uniquename"); set => this.Set("uniquename", value); }
        public Guid? WebresourceId { get => this.Get<Guid?>("webresourceid"); set => this.Set("webresourceid", value); }
        public Guid? WelcomepageId { get => this.Get<Guid?>("welcomepageid"); set => this.Set("welcomepageid", value); }

        public static AppModule GetById(OrganizationService service, Guid id)
        {
            return service.Retrieve<AppModule>(EntityLogicalName, id);
        }

        public static List<AppModule> FindModelDrivenAppByNames(OrganizationService service, IEnumerable<string> patterns, IEnumerable<Guid> solutionIds)
        {
            if (patterns.Count() == 0)
            {
                return new List<AppModule>();
            }

            if (patterns.Where(x => !string.IsNullOrWhiteSpace(x)).Count() == 0)
            {
                return new List<AppModule>();
            }

            QueryExpression query = new QueryExpression(EntityLogicalName)
            {
                Distinct = true,
                ColumnSet = new ColumnSet("appmoduleid", "name")
            };

            Utilities.Helper.ApplyPatternFilter(query, "name", patterns);
            Utilities.Helper.ApplySolutionFilter(query, EntityLogicalName + "id", solutionIds);

            return service.GetBigData<AppModule>(query);
        }

        public object GetMetadataObject(bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.AppModuleIdUnique,
                    this.ClientType,
                    this.ComponentState,
                    this.CreatedBy,
                    this.CreatedOn,
                    this.Description,
                    this.FormFactor,
                    this.Id,
                    this.IsDefault,
                    this.IsFeatured,
                    this.IsManaged,
                    this.IntroducedVersion,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.Name,
                    this.NavigationType,
                    this.OrganizationId,
                    this.PptimizedFor,
                    this.PublishedOn,
                    this.PublisherId,
                    this.SolutionId,
                    this.StateCode,
                    this.StatusCode,
                    this.UniqueName,
                    this.WebresourceId,
                    this.WelcomepageId,
                };
            }
            else
            {
                return new
                {
                    this.ClientType,
                    this.ComponentState,
                    //this.CreatedBy,
                    this.Description,
                    this.FormFactor,
                    //this.Id,
                    this.IsDefault,
                    this.IsFeatured,
                    //this.ModifiedBy,
                    this.Name,
                    this.NavigationType,
                    this.PptimizedFor,
                    //this.PublisherId,
                    this.StateCode,
                    this.StatusCode,
                    this.UniqueName,
                    this.WebresourceId,
                    this.WelcomepageId,
                };
            }
        }
    }
}
