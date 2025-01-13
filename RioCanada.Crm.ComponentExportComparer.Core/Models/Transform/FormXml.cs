using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.FormXmlClasses
{
    [Serializable()]
    [XmlRoot("form")]
    public class Form
    {
        [XmlElement("ancestor")]
        public Ancestor Ancestor { get; set; }

        [XmlArray("hiddencontrols")]
        [XmlArrayItem("data")]
        public ControlBase[] HiddenControls { get; set; }

        [XmlElement("tabs")]
        public Tabs Tabs { get; set; }

        [XmlElement("header")]
        public Header Header { get; set; }

        [XmlElement("footer")]
        public Footer Footer { get; set; }

        [XmlArray("events")]
        [XmlArrayItem("event")]
        public Event[] Events { get; set; }

        [XmlElement("clientresources")]
        public ClientResources ClientResources { get; set; }

        public Navigation Navigation { get; set; }

        [XmlArray("formLibraries")]
        [XmlArrayItem("Library")]
        public Library[] FormLibraries { get; set; }

        [XmlArray("controlDescriptions")]
        [XmlArrayItem("controlDescription")]
        public ControlDescription[] ControlDescriptions { get; set; }

        [XmlAttribute("shownavigationbar")]
        public bool ShowNavigationBar { get; set; }

        [XmlIgnore]
        public bool ShowNavigationBarSpecified { get; set; }

        [XmlAttribute("showImage")]
        public bool ShowImage { get; set; }

        [XmlIgnore]
        public bool ShowImageSpecified { get; set; }

        [XmlAttribute("maxWidth")]
        public uint MaxWidth { get; set; }

        [XmlIgnore]
        public bool MaxWidthSpecified { get; set; }

        [XmlAttribute("headerdensity")]
        public string HeaderDensity { get; set; }

        [XmlIgnore]
        public bool HeaderDensitySpecified { get; set; }

        [XmlAttribute("hasmargin")]
        public bool HasMargin { get; set; }

        [XmlIgnore]
        public bool HasMarginSpecified { get; set; }

        public DisplayConditions DisplayConditions { get; set; }
    }

    [Serializable]
    public class DisplayConditions
    {
        [XmlAttribute]
        public bool FallbackForm { get; set; }
        
        [XmlIgnore]
        public bool FallbackFormSpecified { get; set; }

        [XmlAttribute]
        public uint Order { get; set; }

        [XmlIgnore]
        public bool OrderSpecified { get; set; }

        [XmlElement("Role")]
        public Role[] Roles { get; set; }

        public Everyone Everyone { get; set; }
    }

    [Serializable]
    public class Role
    {
        [XmlAttribute]
        public string Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public bool NameSpecified { get; set; }
    }

    [Serializable]
    public class Everyone { }

    [Serializable()]
    public partial class Ancestor
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [Serializable()]
    public class ControlDescription
    {
        [XmlAttribute("forControl")]
        public string ForControl { get; set; }

        [XmlElement("customControl")]
        public CustomControl[] CustomControls { get; set; }
    }

    [Serializable]
    public class CustomControl
    {
        [XmlAttribute("formFactor")]
        public uint FormFactor { get; set; }

        [XmlIgnore]
        public bool FormFactorSpecified { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }

        [XmlElement("parameters")]
        public CustomControlParameters Parameters { get; set; }
    }

    [Serializable]
    public class CustomControlParameters
    {
        [XmlAnyElement]
        public System.Xml.XmlElement[] Items { get; set; }
        //[XmlElement("ContactRecordLookup", typeof(CustomControlParameterContactRecordLookup))]
        //[XmlElement("CompanyName", typeof(string))]
        //[XmlElement("CompanyWebsite", typeof(string))]
        //[XmlElement("Connections", typeof(CustomControlParameterBase))]
        //[XmlElement("datafieldname", typeof(string))]
        //[XmlElement("data-set", typeof(CustomControlParameterDataSet))]
        //[XmlElement("EntityTypeCode", typeof(CustomControlParameterBase))]
        //[XmlElement("GetIntroduced", typeof(CustomControlParameterBase))]
        //[XmlElement("Icebreakers", typeof(CustomControlParameterBase))]
        //[XmlElement("ListItemCommands", typeof(CustomControlParameterBase))]
        //[XmlElement("Location", typeof(CustomControlParameterBase))]
        //[XmlElement("Messaging", typeof(CustomControlParameterBase))]
        //[XmlElement("msinternal.isvisibleinmocaonly", typeof(CustomControlParameterBase))]
        //[XmlElement("News", typeof(CustomControlParameterBase))]
        //[XmlElement("RecommendedLeads", typeof(CustomControlParameterBase))]
        //[XmlElement("RelatedLeads", typeof(CustomControlParameterBase))]
        //[XmlElement("ShowChrome", typeof(CustomControlParameterBase))]
        //[XmlElement("TopCard", typeof(CustomControlParameterBase))]
        //[XmlElement("TopCard2", typeof(CustomControlParameterBase))]
        //[XmlElement("value", typeof(string))]
        //[XmlChoiceIdentifier("ItemsElementName")]
        //public object[] Items { get; set; }

        //[XmlElement("ItemsElementName")]
        //[XmlIgnore()]
        //public CustomControlParameterChoiceType[] ItemsElementName { get; set; }
    }

    [Serializable()]
    [XmlType(IncludeInSchema = false)]
    public enum CustomControlParameterChoiceType
    {
        ContactRecordLookup,
        CompanyName,
        CompanyWebsite,
        Connections,
        datafieldname,
        [XmlEnum("data-set")]
        dataset,
        EntityTypeCode,
        GetIntroduced,
        Icebreakers,
        ListItemCommands,
        Location,
        Messaging,
        [XmlEnum("msinternal.isvisibleinmocaonly")]
        isvisibleinmocaonly,
        News,
        RecommendedLeads,
        RelatedLeads,
        ShowChrome,
        TopCard,
        TopCard2,
        value,
    }

    [Serializable]
    public class CustomControlParameterDataSet
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement]
        public bool EnableViewPicker { get; set; }

        [XmlIgnore]
        public bool EnableViewPickerSpecified { get; set; }

        [XmlElement]
        public string FilteredViewIds { get; set; }

        [XmlElement]
        public bool IsUserView { get; set; }

        [XmlElement]
        public string TargetEntityType { get; set; }

        [XmlElement]
        public string ViewId { get; set; }

        [XmlElement]
        public string RelationshipName { get; set; }
    }

    [Serializable]
    public class CustomControlParameterContactRecordLookup
    {
        public bool AllowFilterOff { get; set; }
        public string AvailableViewIds { get; set; }
        public string BindAttribute { get; set; }
        public string DefaultViewId { get; set; }
        public string DependentAttributeName { get; set; }
        public string DependentAttributeType { get; set; }
        public bool DisableQuickFind { get; set; }
        public bool DisableViewPicker { get; set; }
        public string FilterRelationshipName { get; set; }
    }

    public class CustomControlParameterBase
    {
        [XmlAttribute("static")]
        public bool Static { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }

    [Serializable]
    public class Tabs
    {
        [XmlAttribute("showlabels")]
        public bool ShowLabels { get; set; }

        [XmlIgnore]
        public bool ShowLabelsSpecified { get; set; }

        [XmlElement("tab")]
        public Tab[] TabItems { get; set; }
    }

    [Serializable()]
    public class Tab
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public Label[] Labels { get; set; }

        [XmlArray("columns")]
        [XmlArrayItem("column")]
        public TabColumn[] Columns { get; set; }

        [XmlArray("events")]
        [XmlArrayItem("event")]
        public Event[] Events { get; set; }

        [XmlAttribute("group")]
        public string Group { get; set; }

        [XmlIgnore]
        public bool GroupSepecified { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }

        [XmlAttribute()]
        public uint IsUserDefined { get; set; }

        [XmlIgnore]
        public bool IsUserDefinedSpecified { get; set; }

        [XmlAttribute("showlabel")]
        public bool ShowLabel { get; set; }

        [XmlIgnore]
        public bool ShowLabelSpecified { get; set; }

        [XmlAttribute("labelid")]
        public string LabelId { get; set; }

        [XmlIgnore]
        public bool LabelIdSpecified { get; set; }

        [XmlAttribute("expanded")]
        public bool Expanded { get; set; }

        [XmlIgnore]
        public bool ExpandedSpecified { get; set; }

        [XmlAttribute("locklevel")]
        public uint LockLevel { get; set; }

        [XmlIgnore()]
        public bool LockLevelSpecified { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }

        [XmlIgnore()]
        public bool VisibleSpecified { get; set; }

        [XmlAttribute("verticallayout")]
        public bool VerticalLayout { get; set; }

        [XmlIgnore()]
        public bool VerticalLayoutSpecified { get; set; }

        [XmlAttribute("availableforphone")]
        public bool AvailableForPhone { get; set; }

        [XmlIgnore]
        public bool AvailableForPhoneSpecified { get; set; }
    }

    [Serializable()]
    public class Label
    {
        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlAttribute("languagecode")]
        public uint LanguageCode { get; set; }
    }

    [Serializable()]
    public class TabColumn
    {
        [XmlArray("sections")]
        [XmlArrayItem("section")]
        public TabColumnSection[] Sections { get; set; }

        [XmlAttribute("width")]
        public string Width { get; set; }
    }

    [Serializable()]
    public class TabColumnSection
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public Label[] Labels { get; set; }

        [XmlArray("rows")]
        [XmlArrayItem("row")]
        public formTabColumnSectionRow[] Rows { get; set; }

        [XmlAttribute("group")]
        public string Group { get; set; }

        [XmlIgnore]
        public bool GroupSepecified { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("labelid")]
        public string LabelId { get; set; }

        [XmlIgnore]
        public bool LabelIdSpecified { get; set; }

        [XmlAttribute("showlabel")]
        public bool ShowLabel { get; set; }

        [XmlIgnore]
        public bool ShowLabelSpecified { get; set; }

        [XmlAttribute("showbar")]
        public bool ShowBar { get; set; }

        [XmlIgnore]
        public bool ShowBarSpecified { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }

        [XmlAttribute()]
        public uint IsUserDefined { get; set; }

        [XmlIgnore]
        public bool IsUserDefinedSpecified { get; set; }

        [XmlAttribute("layout")]
        public string Layout { get; set; }

        [XmlAttribute("columns")]
        public uint Columns { get; set; }

        [XmlIgnore()]
        public bool ColumnsSpecified { get; set; }

        [XmlAttribute("labelwidth")]
        public uint LabelWidth { get; set; }

        [XmlIgnore()]
        public bool LabelWidthSpecified { get; set; }

        [XmlAttribute("celllabelposition")]
        public string CellLabelPosition { get; set; }

        [XmlAttribute("locklevel")]
        public uint LockLevel { get; set; }

        [XmlIgnore()]
        public bool LockLevelSpecified { get; set; }

        [XmlAttribute("celllabelalignment")]
        public string CellLabelAlignment { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }

        [XmlIgnore()]
        public bool VisibleSpecified { get; set; }
    }

    [Serializable()]
    public class formTabColumnSectionRow
    {
        [XmlElement("cell")]
        public SectionRowCell[] Cells { get; set; }

        [XmlAttribute("height")]
        public string Height { get; set; }
    }

    public abstract class CellBase
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public Label[] Labels { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }

        [XmlAttribute("showlabel")]
        public bool ShowLabel { get; set; }

        [XmlIgnore()]
        public bool ShowLabelSpecified { get; set; }

        [XmlAttribute("labelid")]
        public string LabelId { get; set; }

        [XmlIgnore]
        public bool LabelIdSpecified { get; set; }

        [XmlAttribute("locklevel")]
        public uint LockLevel { get; set; }

        [XmlIgnore()]
        public bool LockLevelSpecified { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }

        [XmlIgnore]
        public bool VisibleSpecified { get; set; }

        [XmlAttribute("availableforphone")]
        public bool AvailableForPhone { get; set; }

        [XmlIgnore]
        public bool AvailableForPhoneSpecified { get; set; }
    }

    [Serializable()]
    public class SectionRowCell : CellBase
    {
        [XmlElement("control")]
        public SectionRowCellControl Control { get; set; }

        [XmlArray("events")]
        [XmlArrayItem("event")]
        public Event[] Events { get; set; }

        [XmlAttribute("colspan")]
        public uint ColSpan { get; set; }

        [XmlIgnore()]
        public bool ColSpanSpecified { get; set; }

        [XmlAttribute("rowspan")]
        public uint RowSpan { get; set; }

        [XmlIgnore()]
        public bool RowSpanSpecified { get; set; }

        [XmlAttribute("auto")]
        public bool Auto { get; set; }

        [XmlIgnore()]
        public bool AutoSpecified { get; set; }

        [XmlAttribute("userspacer")]
        public bool UserSpacer { get; set; }

        [XmlIgnore()]
        public bool UserSpacerSpecified { get; set; }
    }

    public class ControlBase
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("classid")]
        public string ClassId { get; set; }

        [XmlAttribute("datafieldname")]
        public string DataFieldName { get; set; }

        [XmlAttribute("disabled")]
        public bool Disabled { get; set; }

        [XmlIgnore()]
        public bool DisabledSpecified { get; set; }

        [XmlAttribute("isrequired")]
        public bool IsRequired { get; set; }

        [XmlIgnore]
        public bool IsRequiredSpecified { get; set; }

        [XmlAttribute("uniqueid")]
        public string UniqueId { get; set; }

        [XmlIgnore]
        public bool UniqueIdSpecified { get; set; }

        [XmlAttribute("relationship")]
        public string Relationship { get; set; }

        [XmlIgnore]
        public bool RelationshipSpecified { get; set; }

        [XmlElement("parameters")]
        public SectionRowCellControlParameters Parameters { get; set; }
    }

    [Serializable()]
    public class SectionRowCellControl : ControlBase
    {
        [XmlAttribute("indicationOfSubgrid")]
        public bool IndicationOfSubgrid { get; set; }

        [XmlIgnore()]
        public bool IndicationOfSubgridSpecified { get; set; }
    }

    [Serializable()]
    public class SectionRowCellControlParameters
    {
        [XmlAnyElement]
        public System.Xml.XmlElement[] Items { get; set; }

        //[XmlElement("AddressField", typeof(string))]
        //[XmlElement("AllowFilterOff", typeof(bool))]
        //[XmlElement("AutoExpand", typeof(string))]
        //[XmlElement("AutoResolve", typeof(bool))]
        //[XmlElement("AvailableViewIds", typeof(string))]
        //[XmlElement("Border", typeof(bool))]
        //[XmlElement("ChartGridMode", typeof(string))]
        //[XmlElement("ControlMode", typeof(string))]
        //[XmlElement("DefaultTabId", typeof(string))]
        //[XmlElement("DefaultViewId", typeof(string))]
        //[XmlElement("DisableMru", typeof(bool))]
        //[XmlElement("DisableQuickFind", typeof(bool))]
        //[XmlElement("DisableViewPicker", typeof(bool))]
        //[XmlElement("DisplayAsCustomer360Tile", typeof(bool))]
        //[XmlElement("EnableChartPicker", typeof(bool))]
        //[XmlElement("EnableContextualActions", typeof(bool))]
        //[XmlElement("EnableJumpBar", typeof(bool))]
        //[XmlElement("EnableQuickFind", typeof(bool))]
        //[XmlElement("EnableViewPicker", typeof(bool))]
        //[XmlElement("FilterRelationshipName", typeof(string))]
        //[XmlElement("DependentAttributeName", typeof(string))]
        //[XmlElement("DependentAttributeType", typeof(string))]
        //[XmlElement("HeaderColorCode", typeof(string))]
        //[XmlElement("IsUserChart", typeof(bool))]
        //[XmlElement("IsUserView", typeof(bool))]
        //[XmlElement("PassParameters", typeof(bool))]
        //[XmlElement("QuickForms", typeof(string))]
        //[XmlElement("RecordsPerPage", typeof(uint))]
        //[XmlElement("ReferencePanelSubgridIconUrl", typeof(string))]
        //[XmlElement("RelationshipName", typeof(string))]
        //[XmlElement("RelationshipRoleOrdinal", typeof(uint))]
        //[XmlElement("Scrolling", typeof(string))]
        //[XmlElement("Security", typeof(bool))]
        //[XmlElement("ShowOnMobileClient", typeof(bool))]
        //[XmlElement("TargetEntityType", typeof(string))]
        //[XmlElement("Url", typeof(string))]
        //[XmlElement("ViewId", typeof(string))]
        //[XmlElement("ViewIds", typeof(string))]
        //[XmlElement("VisualizationId", typeof(string))]
        //[XmlElement("WebResourceId", typeof(string))]
        //[XmlChoiceIdentifier("ItemsElementName")]
        //public object[] Items { get; set; }

        //[XmlElement("ItemsElementName")]
        //[XmlIgnore()]
        //public ItemsChoiceType[] ItemsElementName { get; set; }
    }

    //[Serializable()]
    //[XmlType(IncludeInSchema = false)]
    //public enum ItemsChoiceType
    //{
    //    AddressField,
    //    AllowFilterOff,
    //    AutoExpand,
    //    AutoResolve,
    //    AvailableViewIds,
    //    Border,
    //    ChartGridMode,
    //    ControlMode,
    //    DefaultTabId,
    //    DefaultViewId,
    //    DisableMru,
    //    DisableQuickFind,
    //    DisableViewPicker,
    //    DisplayAsCustomer360Tile,
    //    EnableChartPicker,
    //    EnableContextualActions,
    //    EnableJumpBar,
    //    EnableQuickFind,
    //    EnableViewPicker,
    //    FilterRelationshipName,
    //    DependentAttributeName,
    //    DependentAttributeType,
    //    HeaderColorCode,
    //    IsUserChart,
    //    IsUserView,
    //    PassParameters,
    //    QuickForms,
    //    RecordsPerPage,
    //    ReferencePanelSubgridIconUrl,
    //    RelationshipName,
    //    RelationshipRoleOrdinal,
    //    Scrolling,
    //    Security,
    //    ShowOnMobileClient,
    //    TargetEntityType,
    //    Url,
    //    ViewId,
    //    ViewIds,
    //    VisualizationId,
    //    WebResourceId,
    //}

    [Serializable()]
    public class Header
    {
        [XmlArray("rows")]
        [XmlArrayItem("row")]
        public HeaderRow[] Rows { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("columns")]
        public uint Columns { get; set; }

        [XmlIgnore]
        public bool ColumnsSpecified { get; set; }

        [XmlAttribute("celllabelalignment")]
        public string CellLabelAlignment { get; set; }

        [XmlIgnore]
        public bool CellLabelAlignmentSpecified { get; set; }

        [XmlAttribute("celllabelposition")]
        public string CellLabelPosition { get; set; }

        [XmlAttribute("labelwidth")]
        public uint LabelWidth { get; set; }

        [XmlIgnore]
        public bool LabelWidthSpecified { get; set; }
    }

    [Serializable()]
    public class HeaderRow
    {
        [XmlElement("cell")]
        public HeaderRowCell[] Cells { get; set; }
    }

    [Serializable()]
    public class HeaderRowCell : CellBase
    {
        public HeaderRowsCellControl control { get; set; }
    }

    [Serializable()]
    public class HeaderRowsCellControl : ControlBase
    {
    }

    [Serializable()]
    public class Event
    {
        [XmlArrayItem("Handler")]
        public Handler[] InternalHandlers { get; set; }

        [XmlArrayItem("Handler")]
        public Handler[] Handlers { get; set; }

        [XmlIgnore]
        public bool HandlersSpecified { get; set; }

        [XmlAttribute()]
        public string name { get; set; }

        [XmlAttribute()]
        public string application { get; set; }

        [XmlAttribute("active")]
        public bool Active { get; set; }

        [XmlIgnore]
        public bool ActiveSpecified { get; set; }

        [XmlAttribute("attribute")]
        public string Attribute { get; set; }

        [XmlIgnore]
        public bool AttributeSpecified { get; set; }

        [XmlAttribute("control")]
        public string Control { get; set; }

        [XmlIgnore]
        public bool ControlSpecified { get; set; }

        [XmlAttribute("relationship")]
        public string Relationship { get; set; }

        [XmlIgnore]
        public bool RelationshipSpecified { get; set; }

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public Dependency[] Dependencies { get; set; }
    }

    [Serializable()]
    public class Handler
    {
        [XmlAttribute("functionName")]
        public string FunctionName { get; set; }

        [XmlAttribute("libraryName")]
        public string LibraryName { get; set; }

        [XmlAttribute("handlerUniqueId")]
        public string HandlerUniqueId { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        [XmlAttribute("parameters")]
        public string Parameters { get; set; }

        [XmlIgnore]
        public bool ParametersSpecified { get; set; }

        [XmlAttribute("passExecutionContext")]
        public bool PassExecutionContext { get; set; }

        [XmlIgnore]
        public bool PassExecutionContextSpecified { get; set; }

        [XmlArray("dependencies")]
        [XmlArrayItem("dependency")]
        public Dependency[] Dependencies { get; set; }
    }

    public class Dependency
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [Serializable()]
    public class ClientResources
    {
        [XmlElement("internalresources")]
        public InternalResources InternalResources { get; set; }
    }

    [Serializable()]
    public class InternalResources
    {
        [XmlElement("clientincludes")]
        public ClientIncludes ClientIncludes { get; set; }

        [XmlElement("clientvariables")]
        public ClientVariables ClientVariables { get; set; }
    }

    [Serializable()]
    public class ClientIncludes
    {
        [XmlElement("internaljscriptfile")]
        public InternalJScriptFile[] InternalJScriptFiles { get; set; }
    }

    [Serializable()]
    public class InternalJScriptFile
    {
        [XmlAttribute("src")]
        public string Src { get; set; }
    }

    [Serializable()]
    public class ClientVariables
    {
        [XmlElement("internaljscriptvariable")]
        public InternalJScriptVariable[] InternalJScriptVariables { get; set; }
    }

    [Serializable]
    public class InternalJScriptVariable
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("resourceid")]
        public string ResourceId { get; set; }
    }

    [Serializable()]
    public class Navigation
    {
        public NavBar NavBar { get; set; }
        
        [XmlArrayItem("NavBarArea")]
        public NavBarArea[] NavBarAreas { get; set; }
    }

    [Serializable]
    public class NavBar
    {
        [XmlElement("NavBarByRelationshipItem")]
        public NavBarByRelationshipItem[] NavBarByRelationshipItems { get; set; }
    }

    [Serializable()]
    public class NavBarByRelationshipItem
    {
        [XmlArrayItem("Privilege")]
        public NavPrivilege[] Privileges { get; set; }

        [XmlArrayItem("Title")]
        public Title[] Titles { get; set; }

        [XmlAttribute()]
        public string RelationshipName { get; set; }

        [XmlAttribute()]
        public string Id { get; set; }

        [XmlAttribute()]
        public uint Sequence { get; set; }

        [XmlIgnore]
        public bool SequenceSpecified { get; set; }

        [XmlAttribute()]
        public string Area { get; set; }

        [XmlAttribute()]
        public bool Show { get; set; }

        [XmlIgnore]
        public bool ShowSpecified { get; set; }

        [XmlAttribute()]
        public string TitleResourceId { get; set; }

        [XmlAttribute]
        public string Icon { get; set; }
        
        [XmlIgnore]
        public bool IconSpecified { get; set; }

        [XmlAttribute]
        public string ViewId { get; set; }

        [XmlIgnore]
        public bool ViewIdSpecified { get; set; }
    }

    [Serializable]
    public class NavBarArea
    {
        [XmlAttribute]
        public string Id { get; set; }

        [XmlArrayItem("Title")]
        public Title[] Titles { get; set; }
    }

    [Serializable()]
    public class NavPrivilege
    {
        [XmlAttribute()]
        public string Entity { get; set; }

        [XmlAttribute()]
        public string Privilege { get; set; }
    }

    [Serializable()]
    public class Title
    {
        [XmlAttribute()]
        public uint LCID { get; set; }

        [XmlAttribute()]
        public string Text { get; set; }
    }

    [Serializable()]
    public class Library
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("libraryUniqueId")]
        public string LibraryUniqueId { get; set; }
    }


    [Serializable()]
    public class Footer
    {
        public object rows { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("columns")]
        public uint Columns { get; set; }

        [XmlIgnore]
        public bool ColumnsSpecified { get; set; }

        [XmlAttribute("labelwidth")]
        public uint LabelWidth { get; set; }

        [XmlIgnore]
        public bool LabelWidthSpecified { get; set; }

        [XmlAttribute("celllabelalignment")]
        public string CellLabelAlignment { get; set; }

        [XmlAttribute("celllabelposition")]
        public string CellLabelPosition { get; set; }
    }
}
