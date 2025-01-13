using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.RibbonClasses
{
    [Serializable()]
    [XmlRoot]
    public class RibbonDefinitions
    {
        public RibbonDefinition RibbonDefinition { get; set; }
    }

    [Serializable]
    public class RibbonDefinition
    {
        public UI UI { get; set; }
        public Templates Templates { get; set; }

        [XmlArrayItem("CommandDefinition", IsNullable = false)]
        public CommandDefinition[] CommandDefinitions { get; set; }
        public RuleDefinitions RuleDefinitions { get; set; }
    }

    #region UI
    [Serializable]
    public class UI
    {
        public Ribbon Ribbon { get; set; }
    }

    [Serializable]
    public class Ribbon
    {
        public Tabs Tabs { get; set; }

        public ContextualTabs ContextualTabs { get; set; }
    }

    [Serializable]
    public class Tabs : IdProperty
    {
        [XmlElement("Tab")]
        public Tab[] Tab { get; set; }
    }

    [Serializable]
    public class Tab : IdProperty
    {
        public TabScaling Scaling { get; set; }
        public TabGroups Groups { get; set; }
        [XmlAttribute]
        public string Command { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public uint Sequence { get; set; }
    }

    [Serializable]
    public class TabScaling : IdProperty
    {
        [XmlElement("MaxSize")]
        public TabScalingMaxSize[] MaxSize { get; set; }

        [XmlElement("Scale")]
        public TabScalingScale[] Scale { get; set; }
    }

    [Serializable]
    public class TabScalingMaxSize : IdProperty
    {
        [XmlAttribute]
        public string GroupId { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }

        [XmlAttribute]
        public string Size { get; set; }
    }

    [Serializable]
    public class TabScalingScale : IdProperty
    {
        [XmlAttribute]
        public string GroupId { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }

        [XmlAttribute]
        public string Size { get; set; }

        [XmlAttribute]
        public string PopupSize { get; set; }
    }

    [Serializable]
    public class TabGroups : IdProperty
    {
        [XmlElement("Group")]
        public TabGroup[] Group { get; set; }
    }

    [Serializable]
    public class TabGroup : IdProperty
    {
        public Controls Controls { get; set; }

        [XmlAttribute]
        public string Command { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string Image32by32Popup { get; set; }

        [XmlAttribute]
        public string Template { get; set; }
    }

    [Serializable]
    public class Controls : IdProperty
    {
        [XmlElement("Button", typeof(Button))]
        [XmlElement("FlyoutAnchor", typeof(FlyoutAnchor))]
        [XmlElement("SplitButton", typeof(SplitButton))]
        [XmlElement("ToggleButton", typeof(ToggleButton))]
        [XmlElement("Label", typeof(Label))]
        public object[] Items { get; set; }
    }

    public abstract class Control : IdProperty
    {
        [XmlAttribute]
        public string ToolTipTitle { get; set; }

        [XmlAttribute]
        public string ToolTipDescription { get; set; }

        [XmlAttribute]
        public string Command { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }

        [XmlIgnore]
        public bool SequenceSpecified { get; set; }

        [XmlAttribute]
        public string LabelText { get; set; }

        [XmlAttribute]
        public string Alt { get; set; }

        [XmlAttribute]
        public string Image16by16 { get; set; }

        [XmlAttribute]
        public string Image32by32 { get; set; }

        [XmlAttribute]
        public string TemplateAlias { get; set; }
    }

    [Serializable]
    public class Button : Control
    {
        [XmlAttribute]
        public string ModernImage { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string ModernCommandType { get; set; }

        [XmlAttribute]
        public string CommandValueId { get; set; }

        [XmlIgnore]
        public bool CommandValueIdSpecified { get; set; }
    }

    [Serializable]
    public class FlyoutAnchor : Control
    {
        public ControlFlyoutAnchorMenu Menu { get; set; }

        [XmlAttribute]
        public bool PopulateDynamically { get; set; }

        [XmlIgnore]
        public bool PopulateDynamicallySpecified { get; set; }

        [XmlAttribute]
        public string PopulateQueryCommand { get; set; }

        [XmlAttribute]
        public string ModernImage { get; set; }

        [XmlAttribute]
        public bool PopulateOnlyOnce { get; set; }

        [XmlIgnore]
        public bool PopulateOnlyOnceSpecified { get; set; }
    }

    [Serializable]
    public class ToggleButton : Control
    {
        [XmlAttribute]
        public string QueryCommand { get; set; }
    }

    [Serializable]
    public class Label : Control
    {
        [XmlAttribute]
        public string QueryCommand { get; set; }
    }

    [Serializable]
    public class ControlFlyoutAnchorMenu : IdProperty
    {
        public ControlFlyoutAnchorMenuSection MenuSection { get; set; }
    }

    [Serializable]
    public class ControlFlyoutAnchorMenuSection : IdProperty
    {
        public Controls Controls { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }

        [XmlIgnore]
        public bool SequenceSpecified { get; set; }

        [XmlAttribute]
        public string DisplayMode { get; set; }
    }

    [Serializable]
    public class SplitButton : IdProperty
    {
        public ControlsSplitButtonMenu Menu { get; set; }

        [XmlAttribute]
        public string ToolTipTitle { get; set; }

        [XmlAttribute]
        public string ToolTipDescription { get; set; }

        [XmlAttribute]
        public string Command { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }

        [XmlAttribute]
        public string LabelText { get; set; }

        [XmlAttribute]
        public string Alt { get; set; }

        [XmlAttribute]
        public string Image16by16 { get; set; }

        [XmlAttribute]
        public string Image32by32 { get; set; }

        [XmlAttribute]
        public string TemplateAlias { get; set; }

        [XmlAttribute]
        public string ModernImage { get; set; }
    }

    [Serializable]
    public class ControlsSplitButtonMenu : IdProperty
    {
        public ControlFlyoutAnchorMenuSection MenuSection { get; set; }
    }

    [Serializable]
    public class ContextualTabs : IdProperty
    {
        [XmlElement("ContextualGroup")]
        public ContextualGroup[] ContextualGroup { get; set; }
    }

    [Serializable]
    public class ContextualGroup : IdProperty
    {
        public Tab Tab { get; set; }

        [XmlAttribute]
        public string Command { get; set; }

        [XmlAttribute]
        public string Color { get; set; }

        [XmlAttribute]
        public string ContextualGroupId { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public uint Sequence { get; set; }
    }
    #endregion

    #region Templates
    [Serializable]
    public class Templates
    {
        public RibbonTemplates RibbonTemplates { get; set; }
    }

    [Serializable]
    public class RibbonTemplates : IdProperty
    {
        [XmlElement("GroupTemplate")]
        public GroupTemplate[] GroupTemplate { get; set; }
    }

    [Serializable]
    public class GroupTemplate : IdProperty
    {
        [XmlElement("Layout")]
        public Layout[] Layout { get; set; }
    }

    [Serializable]
    public class Layout
    {
        [XmlElement("Section")]
        public Section[] Section { get; set; }

        [XmlElement("OverflowSection")]
        public OverflowSection[] OverflowSection { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string LayoutTitle { get; set; }
    }

    [Serializable]
    public class Section
    {
        [XmlElement("Row")]
        public Row[] Row { get; set; }

        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class Row
    {
        public ControlRef ControlRef { get; set; }
    }

    [Serializable]
    public class ControlRef
    {
        [XmlAttribute]
        public string TemplateAlias { get; set; }

        [XmlAttribute]
        public string DisplayMode { get; set; }
    }

    [Serializable]
    public class OverflowSection
    {
        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string TemplateAlias { get; set; }

        [XmlAttribute]
        public string DisplayMode { get; set; }
    }

    #endregion

    #region Command Definition
    [Serializable]
    public class CommandDefinition : IdProperty
    {
        [XmlElement("EnableRules", IsNullable = false)]
        public CommandDefinitionRules EnableRules { get; set; }

        [XmlElement("DisplayRules", IsNullable = false)]
        public CommandDefinitionRules DisplayRules { get; set; }

        [XmlArrayItem("JavaScriptFunction", IsNullable = false)]
        public CommandDefinitionJavaScriptFunction[] Actions { get; set; }
    }

    [Serializable]
    public class CommandDefinitionRules
    {
        [XmlElement("DisplayRule", typeof(CommandDefinitionDisplayRule))]
        [XmlElement("EnableRule", typeof(CommandDefinitionEnableRule))]
        public object[] Rules { get; set; }
    }

    [Serializable]
    public class CommandDefinitionDisplayRule : IdProperty { };

    [Serializable]
    public class CommandDefinitionEnableRule : IdProperty { };

    [Serializable]
    public class CommandDefinitionJavaScriptFunction
    {
        [XmlElement("BoolParameter", typeof(BoolParameter))]
        [XmlElement("CrmParameter", typeof(CrmParameter))]
        [XmlElement("IntParameter", typeof(IntParameter))]
        [XmlElement("StringParameter", typeof(StringParameter))]
        public object[] Items { get; set; }

        [XmlAttribute]
        public string FunctionName { get; set; }

        [XmlAttribute]
        public string Library { get; set; }
    }
    #endregion

    #region Rule Definitions

    [Serializable]
    public class RuleDefinitions
    {
        [XmlArrayItem("DisplayRule", IsNullable = false)]
        public DisplayRule[] DisplayRules { get; set; }

        [XmlArrayItem("EnableRule", IsNullable = false)]
        public EnableRule[] EnableRules { get; set; }
    }

    public class RuleGroup : IdProperty
    {
        [XmlElement("CommandClientTypeRule", typeof(CommandClientTypeRule))]
        [XmlElement("CrmClientTypeRule", typeof(CrmClientTypeRule))]
        [XmlElement("CrmOfflineAccessStateRule", typeof(CrmOfflineAccessStateRule))]
        [XmlElement("CrmOutlookClientTypeRule", typeof(CrmOutlookClientTypeRule))]
        [XmlElement("CrmOutlookClientVersionRule", typeof(CrmOutlookClientVersionRule))]
        [XmlElement("CustomRule", typeof(CustomRule))]
        [XmlElement("DeviceTypeRule", typeof(DeviceTypeRule))]
        [XmlElement("EntityPrivilegeRule", typeof(EntityPrivilegeRule))]
        [XmlElement("EntityPropertyRule", typeof(EntityPropertyRule))]
        [XmlElement("EntityRule", typeof(EntityRule))]
        [XmlElement("FeatureControlRule", typeof(FeatureControlRule))]
        [XmlElement("FormEntityContextRule", typeof(FormEntityContextRule))]
        [XmlElement("FormStateRule", typeof(FormStateRule))]
        [XmlElement("FormTypeRule", typeof(FormTypeRule))]
        [XmlElement("HideForTabletExperienceRule", typeof(object))]
        [XmlElement("MiscellaneousPrivilegeRule", typeof(MiscellaneousPrivilegeRule))]
        [XmlElement("OptionSetRule", typeof(OptionSetRule))]
        [XmlElement("OrRule", typeof(OrRule))]
        [XmlElement("OrganizationSettingRule", typeof(OrganizationSettingRule))]
        [XmlElement("OutlookItemTrackingRule", typeof(OutlookItemTrackingRule))]
        [XmlElement("OutlookRenderTypeRule", typeof(OutlookRenderTypeRule))]
        [XmlElement("OutlookVersionRule", typeof(OutlookVersionRule))]
        [XmlElement("PageRule", typeof(PageRule))]
        [XmlElement("RecordPrivilegeRule", typeof(RecordPrivilegeRule))]
        [XmlElement("ReferencingAttributeRequiredRule", typeof(ReferencingAttributeRequiredRule))]
        [XmlElement("RelationshipTypeRule", typeof(RelationshipTypeRule))]
        [XmlElement("SelectionCountRule", typeof(SelectionCountRule))]
        [XmlElement("SkuRule", typeof(SkuRule))]
        [XmlElement("ValueRule", typeof(ValueRule))]
        public object[] Items { get; set; }
    }

    [Serializable]
    public class DisplayRule : RuleGroup { }

    [Serializable]
    public class EnableRule : RuleGroup { }

    #endregion

    #region All Rules
    public abstract class BaseRule
    {
        [XmlAttribute]
        public bool Default { get; set; }

        [XmlIgnore]
        public bool DefaultSpecified { get; set; }

        [XmlAttribute]
        public bool InvertResult { get; set; }

        [XmlIgnore]
        public bool InvertResultSpecified { get; set; }
    }

    [Serializable]
    public class CommandClientTypeRule : BaseRule
    {
        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class CrmClientTypeRule : BaseRule
    {
        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class CrmOfflineAccessStateRule : BaseRule
    {
        [XmlAttribute]
        public string State { get; set; }
    }

    [Serializable]
    public class CrmOutlookClientTypeRule : BaseRule
    {
        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class CrmOutlookClientVersionRule : BaseRule
    {
        [XmlAttribute]
        public int Major { get; set; }

        [XmlAttribute]
        public int Minor { get; set; }

        [XmlAttribute]
        public int Build { get; set; }

        [XmlAttribute]
        public int Revision { get; set; }
    }

    [Serializable]
    public class CustomRule : BaseRule
    {
        [XmlElement("CrmParameter", typeof(CrmParameter))]
        [XmlElement("StringParameter", typeof(StringParameter))]
        [XmlElement("BoolParameter", typeof(BoolParameter))]
        public object[] Parameters { get; set; }

        [XmlAttribute]
        public string FunctionName { get; set; }

        [XmlAttribute]
        public string Library { get; set; }
    }

    [Serializable]
    public class DeviceTypeRule : BaseRule
    {
        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class EntityPrivilegeRule : BaseRule
    {
        [XmlAttribute]
        public string AppliesTo { get; set; }

        [XmlAttribute]
        public string PrivilegeType { get; set; }

        [XmlAttribute]
        public string PrivilegeDepth { get; set; }

        [XmlAttribute]
        public string EntityName { get; set; }
    }

    [Serializable]
    public class EntityPropertyRule : BaseRule
    {
        [XmlAttribute]
        public string AppliesTo { get; set; }

        [XmlAttribute]
        public string PropertyName { get; set; }

        [XmlAttribute]
        public bool PropertyValue { get; set; }
    }

    [Serializable]
    public class EntityRule : BaseRule
    {
        [XmlAttribute]
        public string AppliesTo { get; set; }

        [XmlAttribute]
        public string EntityName { get; set; }

        [XmlAttribute]
        public string Context { get; set; }
    }

    [Serializable]
    public class FeatureControlRule : BaseRule
    {
        [XmlAttribute]
        public string FeatureControlBit { get; set; }

        [XmlAttribute]
        public bool ExpectedValue { get; set; }
    }

    [Serializable]
    public class FormEntityContextRule : BaseRule
    {
        [XmlAttribute]
        public string EntityName { get; set; }
    }

    [Serializable]
    public class FormStateRule : BaseRule
    {
        [XmlAttribute]
        public string State { get; set; }
    }

    [Serializable]
    public class FormTypeRule : BaseRule
    {
        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class MiscellaneousPrivilegeRule : BaseRule
    {
        [XmlAttribute]
        public string PrivilegeName { get; set; }

        [XmlAttribute]
        public string PrivilegeDepth { get; set; }
    }

    [Serializable]
    public class OptionSetRule : BaseRule
    {
        [XmlAttribute]
        public string OptionSet { get; set; }

        [XmlAttribute]
        public string StateCode { get; set; }

        [XmlAttribute]
        public string ObjectTypeCode { get; set; }
    }

    [Serializable]
    public class OrRule : BaseRule
    {
        [XmlElement("Or")]
        public RuleGroup[] Or { get; set; }
    }

    [Serializable]
    public class OrganizationSettingRule : BaseRule
    {
        [XmlAttribute]
        public string Setting { get; set; }
    }

    [Serializable]
    public class OutlookItemTrackingRule : BaseRule
    {
        [XmlAttribute]
        public bool TrackedInCrm { get; set; }

        [XmlAttribute]
        public string AppliesTo { get; set; }
    }

    [Serializable]
    public class OutlookRenderTypeRule : BaseRule
    {
        [XmlAttribute]
        public string Type { get; set; }
    }

    [Serializable]
    public class OutlookVersionRule : BaseRule
    {
        [XmlAttribute]
        public uint Version { get; set; }
    }

    [Serializable]
    public class PageRule : BaseRule
    {
        [XmlAttribute]
        public string Address { get; set; }
    }

    [Serializable]
    public class RecordPrivilegeRule : BaseRule
    {
        [XmlAttribute]
        public string AppliesTo { get; set; }

        [XmlAttribute]
        public string PrivilegeType { get; set; }
    }

    [Serializable]
    public class ReferencingAttributeRequiredRule : BaseRule
    {
    }

    [Serializable]
    public class RelationshipTypeRule : BaseRule
    {
        [XmlAttribute]
        public string AppliesTo { get; set; }

        [XmlAttribute]
        public string RelationshipType { get; set; }

        [XmlAttribute]
        public bool AllowCustomRelationship { get; set; }

        [XmlIgnore]
        public bool AllowCustomRelationshipSpecified { get; set; }
    }

    [Serializable]
    public class SkuRule : BaseRule
    {
        [XmlAttribute]
        public string Sku { get; set; }
    }

    [Serializable()]
    public class SelectionCountRule : BaseRule
    {
        [XmlAttribute]
        public string AppliesTo { get; set; }

        [XmlAttribute]
        public int Minimum { get; set; }

        [XmlIgnore()]
        public bool MinimumSpecified { get; set; }

        [XmlAttribute]
        public int Maximum { get; set; }

        [XmlIgnore]
        public bool MaximumSpecified { get; set; }
    }

    [Serializable]
    public class ValueRule : BaseRule
    {
        [XmlAttribute]
        public string Value { get; set; }

        [XmlAttribute]
        public string Field { get; set; }
    }

    #endregion

    #region Common
    [Serializable]
    public class IdProperty
    {
        [XmlAttribute]
        public string Id { get; set; }
    }

    [Serializable]
    public class BoolParameter
    {
        [XmlAttribute]
        public bool Value { get; set; }
    }

    [Serializable]
    public class CrmParameter
    {
        [XmlAttribute]
        public string Value { get; set; }
    }

    [Serializable]
    public class IntParameter
    {
        [XmlAttribute]
        public uint Value { get; set; }
    }

    [Serializable]
    public class StringParameter
    {
        [XmlAttribute]
        public string Value { get; set; }
    }
    #endregion
}
