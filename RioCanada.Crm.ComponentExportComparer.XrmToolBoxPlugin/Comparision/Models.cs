using RioCanada.Crm.ComponentExportComparer.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.Comparision
{
    class BreadcrumbItem
    {
        public string Text { get; set; }
        public string Key { get; set; }
        public int Index { get; set; }
        public object Data { get; set; }
    }

    public enum ComparisionStatus
    {
        [Description("Unchanged")]
        Unchanged = 1,
        [Description("Only in source")]
        OnlyInSource = 2,
        [Description("Only in target")]
        OnlyInTarget = 4,
        [Description("Modified")]
        Modified = 8,
    }

    public class ComparisionLineItem
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public IndexItemType Type { get; set; }
        public IndexLineItem DefaultItem { get; set; }
        public string ContentType { get; set; }
        public bool IsFile { get; set; }
        public ComparisionStatus Status { get; set; }
        public ComparisionLineItem Parent { get; set; }
        public List<ComparisionLineItem> Children { get; set; }
        public IndexLineItem SourceItem { get; set; }
        public IndexLineItem TargetItem { get; set; }
        public string IconName { get; set; }

        public Bitmap GetIcon()
        {
            if (this.IconName != null)
            {
                object obj = AppResource.ResourceManager.GetObject(this.IconName, AppResource.Culture);
                return ((Bitmap)(obj));
            }

            switch (this.Type)
            {
                case IndexItemType.Folder:
                    return AppResource.FolderIcon;
                case IndexItemType.FileJson:
                    return AppResource.FileJsonIcon;
                case IndexItemType.FileXml:
                    return AppResource.FileXmlIcon;
                case IndexItemType.FileTxt:
                    return AppResource.FileTxtIcon;
                case IndexItemType.File:
                    return AppResource.FileIcon;
                default:
                    return AppResource.FileUnknowIcon;
            }
        }
    }
}
