using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.SiteMapClasses
{
    [Serializable()]
    [XmlRoot]
    public class SiteMap
    {
        [XmlElement("Area")]
        public Area[] Area { get; set; }

        [XmlAttribute()]
        public string IntroducedVersion { get; set; }
    }

    public class AreaBase
    {
        [XmlArrayItem("Title")]
        public AreaTitle[] Titles;

        [XmlAttribute()]
        public string Id { get; set; }

        [XmlAttribute()]
        public string ResourceId { get; set; }

        [XmlAttribute()]
        public string DescriptionResourceId { get; set; }

        [XmlAttribute()]
        public string Icon { get; set; }

        [XmlAttribute()]
        public string IntroducedVersion { get; set; }

        [XmlAttribute]
        public string VectorIcon { get; set; }

        [XmlIgnore()]
        public bool VectorIconSpecified { get; set; }

        [XmlAttribute()]
        public string ToolTipResourseId { get; set; }

        [XmlIgnore()]
        public bool ToolTipResourseIdSpecified { get; set; }
    }

    [Serializable()]
    public class Area : AreaBase
    {
        [XmlElement("Group")]
        public Group[] Group { get; set; }

        [XmlAttribute()]
        public bool ShowGroups { get; set; }

        [XmlIgnore()]
        public bool ShowGroupsSpecified { get; set; }
    }

    [Serializable()]
    public class Group
    {
        [XmlArrayItem("Title", IsNullable = false)]
        public GroupTitle[] Titles { get; set; }

        [XmlElement("SubArea")]
        public SubArea[] SubArea { get; set; }

        [XmlAttribute()]
        public string Id { get; set; }

        [XmlAttribute()]
        public string Title { get; set; }

        [XmlIgnore()]
        public bool TitleSpecified { get; set; }

        [XmlAttribute()]
        public string ResourceId { get; set; }

        [XmlAttribute()]
        public string DescriptionResourceId { get; set; }

        [XmlAttribute()]
        public string ToolTipResourseId { get; set; }

        [XmlAttribute()]
        public string IntroducedVersion { get; set; }

        [XmlAttribute()]
        public bool IsProfile { get; set; }

        [XmlIgnore()]
        public bool IsProfileSpecified { get; set; }
    }

    [Serializable()]
    public class GroupTitle
    {
        [XmlAttribute()]
        public uint LCID { get; set; }

        [XmlAttribute()]
        public string Title { get; set; }
    }

    [Serializable()]
    public class AreaTitle
    {
        [XmlAttribute()]
        public uint LCID { get; set; }

        [XmlAttribute()]
        public string Title { get; set; }
    }

    [Serializable()]
    public class SubArea : AreaBase
    {
        [XmlElement("Privilege")]
        public AreaPrivilege[] Privilege { get; set; }

        [XmlAttribute()]
        public string Url { get; set; }

        [XmlAttribute()]
        public string GetStartedPanePath { get; set; }

        [XmlAttribute()]
        public string GetStartedPanePathAdmin { get; set; }

        [XmlAttribute()]
        public string GetStartedPanePathOutlook { get; set; }

        [XmlAttribute()]
        public string GetStartedPanePathAdminOutlook { get; set; }

        [XmlAttribute()]
        public string DefaultDashboard { get; set; }

        [XmlAttribute()]
        public string OutlookShortcutIcon { get; set; }

        [XmlAttribute()]
        public bool AvailableOffline { get; set; }

        [XmlIgnore()]
        public bool AvailableOfflineSpecified { get; set; }

        [XmlAttribute()]
        public string Entity { get; set; }

        [XmlAttribute()]
        public string Description { get; set; }

        [XmlAttribute()]
        public string Title { get; set; }

        [XmlAttribute()]
        public bool PassParams { get; set; }

        [XmlIgnore()]
        public bool PassParamsSpecified { get; set; }

        [XmlAttribute()]
        public string Client { get; set; }

        [XmlAttribute()]
        public string Sku { get; set; }

        [XmlAttribute()]
        public string DynamicPage { get; set; }

        [XmlIgnore]
        public bool DynamicPageSpecified { get; set; }
    }

    [Serializable()]
    public class AreaPrivilege
    {
        [XmlAttribute()]
        public string Entity { get; set; }

        [XmlAttribute()]
        public string Privilege { get; set; }
    }
}
