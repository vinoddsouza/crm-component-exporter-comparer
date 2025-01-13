using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    static class UriGenerator
    {
        public static void Open(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        public static string PowerApps(Guid environmentId)
        {
            return PowerApps(environmentId.ToString());
        }

        public static string PowerApps(string environmentId)
        {
            return $"https://make.powerapps.com/environments/{environmentId}/solutions";
        }

        public static string Dynamics365(string server)
        {
            return $"https://{server}";
        }

        public static string Table(string environmentId, string tableSchemaName)
        {
            return $"https://make.powerapps.com/environments/{environmentId}/entities/{environmentId}/{tableSchemaName}";
        }

        public static string Form(string environmentId, string tableSchemaName, string formId)
        {
            return $"https://make.powerapps.com/e/{environmentId}/s/00000001-0000-0000-0001-00000000009b/entity/{tableSchemaName}/form/edit/{formId}?source=powerappsportal";
        }

        public static string WebResource(string server, string webResourceId)
        {
            if (!server.StartsWith("http")) server = "https://" + server;
            return $"{server}/main.aspx?etc=9333&id={webResourceId}&pagetype=webresourceedit";
        }

        public static string SecurityRole(string server, string roleId)
        {
            if (!server.StartsWith("http")) server = "https://" + server;
            return $"{server}/biz/roles/edit.aspx?id={roleId}";
        }
    }
}
