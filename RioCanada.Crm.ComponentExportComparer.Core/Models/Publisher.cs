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
    class Publisher : Entity
    {
        public const string EntityLogicalName = "publisher";

        public Publisher() : base(EntityLogicalName) { }

        public Publisher(Guid id) : base(EntityLogicalName, id) { }

        public string Address1Addresstypecode { get => this.Get<string>("address1_addresstypecode"); set => this.Set("address1_addresstypecode", value); }
        public string Address1City { get => this.Get<string>("address1_city"); set => this.Set("address1_city", value); }
        public string Address1Country { get => this.Get<string>("address1_country"); set => this.Set("address1_country", value); }
        public string Address1Line1 { get => this.Get<string>("address1_line1"); set => this.Set("address1_line1", value); }
        public string Address1Postalcode { get => this.Get<string>("address1_postalcode"); set => this.Set("address1_postalcode", value); }
        public string Address1Shippingmethodcode { get => this.Get<string>("address1_shippingmethodcode"); set => this.Set("address1_shippingmethodcode", value); }
        public string Address1Stateorprovince { get => this.Get<string>("address1_stateorprovince"); set => this.Set("address1_stateorprovince", value); }
        public string Address1Telephone1 { get => this.Get<string>("address1_telephone1"); set => this.Set("address1_telephone1", value); }
        public string Address2Addresstypecode { get => this.Get<string>("address2_addresstypecode"); set => this.Set("address2_addresstypecode", value); }
        public string Address2Shippingmethodcode { get => this.Get<string>("address2_shippingmethodcode"); set => this.Set("address2_shippingmethodcode", value); }
        public EntityReference CreatedBy { get => this.Get<EntityReference>("createdby"); set => this.Set("createdby", value); }
        public int CustomizationOptionValuePrefix { get => this.Get<int>("customizationoptionvalueprefix"); set => this.Set("customizationoptionvalueprefix", value); }
        public string CustomizationPrefix { get => this.Get<string>("customizationprefix"); set => this.Set("customizationprefix", value); }
        public DateTime? CreatedOn { get => this.Get<DateTime?>("createdon"); set => this.Set("createdon", value); }
        public string Description { get => this.Get<string>("description"); set => this.Set("description", value); }
        public string FriendlyName { get => this.Get<string>("friendlyname"); set => this.Set("friendlyname", value); }
        public string IsReadonly { get => this.Get<string>("isreadonly"); set => this.Set("isreadonly", value); }
        public string ModifiedBy { get => this.Get<string>("modifiedby"); set => this.Set("modifiedby", value); }
        public DateTime? ModifiedOn { get => this.Get<DateTime?>("modifiedon"); set => this.Set("modifiedon", value); }
        public EntityReference OrganizationId { get => this.Get<EntityReference>("organizationid"); set => this.Set("organizationid", value); }
        public string SupportingWebSiteUrl { get => this.Get<string>("supportingwebsiteurl"); set => this.Set("supportingwebsiteurl", value); }
        public string UniqueName { get => this.Get<string>("uniquename"); set => this.Set("uniquename", value); }

        public static string GetUniqueNameById(OrganizationService service, Guid id)
        {
            string key = "PUBLISHER_DATA";

            if (!service.CacheData.ContainsKey(key))
            {
                QueryExpression query = new QueryExpression(EntityLogicalName);
                query.ColumnSet.AddColumns(EntityLogicalName + "id", "uniquename");
                service.CacheData[key] = service.GetData<Publisher>(query);
            }

            var data = service.CacheData[key] as List<Publisher>;

            var item = data.Find(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Record not found in \"publisher\" for id {id}");
            }

            return item.UniqueName;
        }
    }
}
