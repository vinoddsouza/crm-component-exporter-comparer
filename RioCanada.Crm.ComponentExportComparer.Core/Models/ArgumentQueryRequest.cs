using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    public class ArgumentQueryRequest
    {
        public List<string> Solutions { get; } = new List<string>();
        public List<string> EntityPatterns { get; } = new List<string>();
        public List<string> WebResourcePatterns { get; } = new List<string>();
        public List<string> PluginStepPatterns { get; } = new List<string>();
        public List<string> OptionSetPatterns { get; } = new List<string>();
        public List<string> DashboardPatterns { get; } = new List<string>();
        public List<string> SiteMapPatterns { get; } = new List<string>();
        public List<string> SecurityRolePatterns { get; } = new List<string>();
        public List<string> WorkflowPatterns { get; } = new List<string>();
        public List<string> BusinessRulePatterns { get; } = new List<string>();
        public List<string> ActionPatterns { get; } = new List<string>();
        public List<string> BusinessProcessFlowPatterns { get; } = new List<string>();
        public List<string> ModelDrivenAppPatterns { get; } = new List<string>();

        public bool IncludeSystemWebresource { get; set; }
        public bool IncludeSystemPluginStep { get; set; }
        public bool IncludeAllProperty { get; set; }

        public static ArgumentQueryRequest Parse(string str)
        {
            ArgumentQueryRequest query = new ArgumentQueryRequest();

            if (string.IsNullOrWhiteSpace(str))
            {
                return query;
            }

            str.Split(';').ToList().ForEach(x =>
            {
                var items = x.Split('=');
                if (items.Length == 2 && !string.IsNullOrWhiteSpace(items[1]))
                {
                    switch (items[0].ToLower())
                    {
                        case "solution":
                            query.Solutions.AddRange(items[1].Split(','));
                            break;
                        case "table":
                            query.EntityPatterns.AddRange(items[1].Split(','));
                            break;
                        case "webresource":
                            query.WebResourcePatterns.AddRange(items[1].Split(','));
                            break;
                        case "pluginstep":
                            query.PluginStepPatterns.AddRange(items[1].Split(','));
                            break;
                        case "choice":
                            query.OptionSetPatterns.AddRange(items[1].Split(','));
                            break;
                        case "dashboard":
                            query.DashboardPatterns.AddRange(items[1].Split(','));
                            break;
                        case "sitemap":
                            query.SiteMapPatterns.AddRange(items[1].Split(','));
                            break;
                        case "securityrole":
                            query.SecurityRolePatterns.AddRange(items[1].Split(','));
                            break;
                        case "workflow":
                            query.WorkflowPatterns.AddRange(items[1].Split(','));
                            break;
                        case "businessrule":
                            query.BusinessRulePatterns.AddRange(items[1].Split(','));
                            break;
                        case "action":
                            query.ActionPatterns.AddRange(items[1].Split(','));
                            break;
                        case "businessprocessflow":
                            query.BusinessProcessFlowPatterns.AddRange(items[1].Split(','));
                            break;
                        case "modeldrivenapp":
                            query.ModelDrivenAppPatterns.AddRange(items[1].Split(','));
                            break;
                        case "includesystemwebresource":
                            query.IncludeSystemWebresource = GetBooleanValue(items[1]);
                            break;
                        case "includesystempluginstep":
                            query.IncludeSystemPluginStep = GetBooleanValue(items[1]);
                            break;
                        case "includeallproperty":
                            query.IncludeAllProperty = GetBooleanValue(items[1]);
                            break;
                    }
                }
            });

            return query;
        }

        private static bool GetBooleanValue(string str)
        {
            str = str?.Trim();
            if (string.IsNullOrWhiteSpace(str)) return false;
            if (str == "1" || str.ToLower().StartsWith("t")) return true;
            return false;
        }

        public override string ToString()
        {
            List<string> options = new List<string>();
            if (this.Solutions.Count > 0) options.Add($"Solution={string.Join(",", this.Solutions)}");
            if (this.EntityPatterns.Count > 0) options.Add($"Table={string.Join(",", this.EntityPatterns)}");
            if (this.WebResourcePatterns.Count > 0) options.Add($"WebResource={string.Join(",", this.WebResourcePatterns)}");
            if (this.PluginStepPatterns.Count > 0) options.Add($"PluginStep={string.Join(",", this.PluginStepPatterns)}");
            if (this.OptionSetPatterns.Count > 0) options.Add($"Choice={string.Join(",", this.OptionSetPatterns)}");
            if (this.DashboardPatterns.Count > 0) options.Add($"Dashboard={string.Join(",", this.DashboardPatterns)}");
            if (this.SiteMapPatterns.Count > 0) options.Add($"SiteMap={string.Join(",", this.SiteMapPatterns)}");
            if (this.SecurityRolePatterns.Count > 0) options.Add($"SecurityRole={string.Join(",", this.SecurityRolePatterns)}");
            if (this.WorkflowPatterns.Count > 0) options.Add($"Workflow={string.Join(",", this.WorkflowPatterns)}");
            if (this.BusinessRulePatterns.Count > 0) options.Add($"BusinessRule={string.Join(",", this.BusinessRulePatterns)}");
            if (this.ActionPatterns.Count > 0) options.Add($"Action={string.Join(",", this.ActionPatterns)}");
            if (this.BusinessProcessFlowPatterns.Count > 0) options.Add($"BusinessProcessFlow={string.Join(",", this.BusinessProcessFlowPatterns)}");
            if (this.ModelDrivenAppPatterns.Count > 0) options.Add($"ModelDrivenApp={string.Join(",", this.ModelDrivenAppPatterns)}");
            if (this.IncludeSystemWebresource) options.Add($"IncludeSystemWebresource=true");
            if (this.IncludeSystemPluginStep) options.Add($"IncludeSystemPluginStep=true");
            if (this.IncludeAllProperty) options.Add($"IncludeAllProperty=true");

            return string.Join(";", options);
        }
    }
}
