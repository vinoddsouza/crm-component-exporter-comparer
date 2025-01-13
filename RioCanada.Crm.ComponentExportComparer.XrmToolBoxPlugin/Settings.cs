using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings
    {
        public string OutputDirectory { get; set; }
        public List<string> Queries { get; set; }
        public bool DeleteDirectory { get; set; }

        public SettingConfiguration Configuration { get; set; }
    }

    public class SettingConfiguration
    {
        public bool IncludeAllProperty { get; set; }
        public bool IncludeSystemWebresource { get; set; } // msdyn_, adx_, cc_MscrmControls
        public bool IncludeSystemPluginStep { get; set; }
        public bool OpenDirectoryAfterExport { get; set; }
        public bool SwapComparisonSide { get; set; }
        public string CompareToolType { get; set; }
        public string CompareToolExecutablePath { get; set; }
        public bool IsCompareToolConfigured { get => !string.IsNullOrWhiteSpace(this.CompareToolExecutablePath); }
        public bool IsCompareToolDefault { get; set; }

        public bool IncludeEntityColumn { get; set; } = true;
        public bool IncludeEntityRelationship { get; set; } = true;
        public bool IncludeEntityForm { get; set; } = true;
        public bool IncludeEntityDashboard { get; set; } = true;
        public bool IncludeEntityView { get; set; } = true;
        public bool IncludeEntityRibbon { get; set; } = false;
        public bool ReplaceEmptyStringByNull { get; set; } = true;

        public bool RoleById { get; set; }

        public IDE DefaultIDE { get; set; }
        public bool IsIDEDefault { get; set; }

        public bool VerifyTransformedData { get; set; }

        public bool RunSimultaneously { get; set; } = true;

        internal Core.ExportSetting ToExportSetting()
        {
            return new Core.ExportSetting
            {
                IncludeAllProperty = this.IncludeAllProperty,
                IncludeSystemWebresource = this.IncludeSystemWebresource,
                IncludeSystemPluginStep = this.IncludeSystemPluginStep,
                IncludeEntityColumn = this.IncludeEntityColumn,
                IncludeEntityRelationship = this.IncludeEntityRelationship,
                IncludeEntityForm = this.IncludeEntityForm,
                IncludeEntityDashboard = this.IncludeEntityDashboard,
                IncludeEntityView = this.IncludeEntityView,
                IncludeEntityRibbon = this.IncludeEntityRibbon,
                ReplaceEmptyStringByNull = this.ReplaceEmptyStringByNull,

                RoleById = this.RoleById,

                VerifyTransformedData = this.VerifyTransformedData,
            };
        }
    }
}