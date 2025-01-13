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
    class PluginType : Entity
    {
        public const string EntityLogicalName = "plugintype";

        public PluginType() : base(EntityLogicalName) { }

        public PluginType(Guid id) : base(EntityLogicalName, id) { }

        public string AssemblyName { get => this.Get<string>("assemblyname"); set => this.Set("assemblyname", value); }
        public string Name { get => this.Get<string>("name"); set => this.Set("name", value); }
        public string Typename { get => this.Get<string>("typename"); set => this.Set("typename", value); }

        private static PluginType GetById(OrganizationService service, Guid id)
        {
            string key = "PLUGINTYPE_DATA";

            if (!service.CacheData.ContainsKey(key))
            {
                QueryExpression query = new QueryExpression(EntityLogicalName);
                query.ColumnSet.AddColumns(EntityLogicalName + "id", "assemblyname", "typename", "name");
                service.CacheData[key] = service.GetBigData<PluginType>(query);
            }

            var data = service.CacheData[key] as List<PluginType>;

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
            return GetById(service, id.Value).Name;
        }
    }
}
