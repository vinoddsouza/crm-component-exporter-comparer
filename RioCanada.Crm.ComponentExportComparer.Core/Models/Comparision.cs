using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    public enum IndexItemType
    {
        Folder = 1,
        File = 2,
        FileJson = 6,
        FileXml = 7,
        FileTxt = 8,
    }

    public static class IndexLineItemContentType
    {
        public static string SecurityRole = "SecurityRole";
        public static string SecurityRoleFolder = "SecurityRoleFolder";
        public static string Columns = "Columns";
        public static string ManyToMany = "Many To Many";
        public static string ManyToOne = "Many To One";
        public static string OneToMany = "One To Many";
        public static string Ribbon = "Ribbon";
    }

    public class IndexLineItem
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int? Order { get; set; }
        public IndexItemType Type { get; set; }
        public string ContentType { get; set; }
        public string Checksum { get; set; }
        public List<IndexLineItem> Children { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
        public bool ShouldSerializeMetadata() { return this.Metadata != null && this.Metadata.Count > 0; }

        public IndexLineItem Parent { get; set; }
        public bool ShouldSerializeParent() { return false; }

        public string Content { get; set; }
        public bool ShouldSerializeContent() { return false; }

        public string IconName { get; set; }
        public bool ShouldSerializeIconName() { return false; }

        public override string ToString()
        {
            return $"{this.Name}, {this.Key}";
        }
    }
}
