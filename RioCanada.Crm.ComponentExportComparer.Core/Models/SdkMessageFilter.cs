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
    class SdkMessageFilter : Entity
    {
        public const string EntityLogicalName = "sdkmessagefilter";

        public SdkMessageFilter() : base(EntityLogicalName) { }

        public SdkMessageFilter(Guid id) : base(EntityLogicalName, id) { }

        public string PrimaryObjectTypeCode { get => this.Get<string>("primaryobjecttypecode"); set => this.Set("primaryobjecttypecode", value); }
        private static SdkMessageFilter GetById(OrganizationService service, Guid id)
        {
            string key = "SDKMESSAGEFILTER_DATA";

            if (!service.CacheData.ContainsKey(key))
            {
                QueryExpression query = new QueryExpression(EntityLogicalName);
                query.ColumnSet.AddColumns(EntityLogicalName + "id", "primaryobjecttypecode");
                service.CacheData[key] = service.GetBigData<SdkMessageFilter>(query);
            }

            var data = service.CacheData[key] as List<SdkMessageFilter>;

            var item = data.Find(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Record not found in \"{EntityLogicalName}\" for id {id}");
            }

            return item;
        }

        public static string GetNameById(OrganizationService service, Guid? id)
        {
            if (id == null) return null;
            return GetById(service, id.Value).PrimaryObjectTypeCode;
        }
    }
}
