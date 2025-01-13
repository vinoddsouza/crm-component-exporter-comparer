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
    class SdkMessageProcessingStepImage : Entity
    {
        public const string EntityLogicalName = "sdkmessageprocessingstepimage";
        public SdkMessageProcessingStepImage() : base(EntityLogicalName) { }

        public SdkMessageProcessingStepImage(Guid id) : base(EntityLogicalName, id) { }

        public new string Attributes { get => this.Get<string>("attributes"); set => this.Set("attributes", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public OptionSetValue ComponentState { get => this.Get<OptionSetValue>("componentstate"); set => this.Set("componentstate", value); }
        public int? CustomizationLevel { get => this.Get<int?>("customizationlevel"); set => this.Set("customizationlevel", value); }
        public string EntityAlias { get => this.Get<string>("entityalias"); set => this.Set("entityalias", value); }
        public OptionSetValue ImageType { get => this.Get<OptionSetValue>("imagetype"); set => this.Set("imagetype", value); }
        public string IntroducedVersion { get => this.Get<string>("introducedversion"); set => this.Set("introducedversion", value); }
        public BooleanManagedProperty IsCustomizable { get => this.Get<BooleanManagedProperty>("iscustomizable"); set => this.Set("iscustomizable", value); }
        public bool IsManaged { get => this.Get<bool>("ismanaged"); set => this.Set("ismanaged", value); }
        public string MessagePropertyName { get => this.Get<string>("messagepropertyname"); set => this.Set("messagepropertyname", value); }
        public EntityReference ModifiedBy { get => this.Get<EntityReference>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public EntityReference SdkMessageProcessingStepId { get => this.Get<EntityReference>("sdkmessageprocessingstepid"); set => this.Set("sdkmessageprocessingstepid", value); }
        public Guid? SdkMessageProcessingStepImageIdUnique { get => this.Get<Guid?>("sdkmessageprocessingstepimageidunique"); set => this.Set("sdkmessageprocessingstepimageidunique", value); }
        public Guid? SolutionId { get => this.Get<Guid?>("solutionid"); set => this.Set("solutionid", value); }

        public object GetMetadataObject(bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    this.Attributes,
                    this.CreatedBy,
                    this.CreatedOn,
                    this.ComponentState,
                    this.CustomizationLevel,
                    this.EntityAlias,
                    this.ImageType,
                    this.IntroducedVersion,
                    this.IsCustomizable,
                    this.IsManaged,
                    this.MessagePropertyName,
                    this.ModifiedBy,
                    this.ModifiedOn,
                    this.Name,
                    this.OrganizationId,
                    this.SdkMessageProcessingStepId,
                    this.SdkMessageProcessingStepImageIdUnique,
                    this.SolutionId,
                };
            }
            else
            {
                return new
                {
                    this.Attributes,
                    this.ComponentState,
                    this.CustomizationLevel,
                    this.EntityAlias,
                    this.ImageType,
                    IsCustomizable = this.IsCustomizable.GetMetadataObject(includeAllProperty),
                    this.MessagePropertyName,
                    this.Name,
                    this.SdkMessageProcessingStepId,
                    //this.SdkMessageProcessingStepImageIdUnique,
                };
            }
        }
    }
}
