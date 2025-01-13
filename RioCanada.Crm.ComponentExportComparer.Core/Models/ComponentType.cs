using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models
{
    enum ComponentType
    {
        Entity = 1,
        [Description("Attribute")]
        Attribute = 2,
        Relationship = 3,
        [Description("Attribute Picklist Value")]
        AttributePicklistValue = 4,
        [Description("Attribute Lookup Value")]
        AttributeLookupValue = 5,
        ViewAttribute = 6,
        LocalizedLabel = 7,
        RelationshipExtraCondition = 8,
        OptionSet = 9,
        EntityRelationship = 10,
        EntityRelationshipRole = 11,
        EntityRelationshipRelationships = 12,
        ManagedProperty = 13,
        EntityKey = 14,
        Privilege = 16,
        PrivilegeObjectTypeCode = 17,
        Role = 20,
        RolePrivilege = 21,
        DisplayString = 22,
        DisplayStringMap = 23,
        Form = 24,
        Organization = 25,
        SavedQuery = 26,
        Workflow = 29,
        Report = 31,
        ReportEntity = 32,
        ReportCategory = 33,
        ReportVisibility = 34,
        [Description("Attachment")]
        Attachment = 35,
        EmailTemplate = 36,
        ContractTemplate = 37,
        KBArticleTemplate = 38,
        MailMergeTemplate = 39,
        DuplicateRule = 44,
        DuplicateRuleCondition = 45,
        EntityMap = 46,
        [Description("Attribute Map")]
        AttributeMap = 47,
        RibbonCommand = 48,
        RibbonContextGroup = 49,
        RibbonCustomization = 50,
        RibbonRule = 52,
        RibbonTabToCommandMap = 53,
        RibbonDiff = 55,
        SavedQueryVisualization = 59,
        SystemForm = 60,
        WebResource = 61,
        SiteMap = 62,
        [Description("Connection Role")]
        ConnectionRole = 63,
        [Description("Complex Control")]
        ComplexControl = 64,
        FieldSecurityProfile = 70,
        FieldPermission = 71,
        PluginType = 72,
        PluginAssembly = 91,
        SDKMessageProcessingStep = 92,
        SDKMessageProcessingStepImage = 93,
        ServiceEndpoint = 95,
        RoutingRule = 150,
        RoutingRuleItem = 151,
        SLA = 152,
        SLAItem = 153,
        ConvertRule = 154,
        ConvertRuleItem = 155,
        HierarchyRule = 65,
        MobileOfflineProfile = 161,
        MobileOfflineProfileItem = 162,
        SimilarityRule = 165,
        CustomControl = 66,
        CustomControlDefaultConfig = 68,
        DataSourceMapping = 166,
        SDKMessage = 201,
        SDKMessageFilter = 202,
        SdkMessagePair = 203,
        SdkMessageRequest = 204,
        SdkMessageRequestField = 205,
        SdkMessageResponse = 206,
        SdkMessageResponseField = 207,
        WebWizard = 210,
        Index = 18,
        ImportMap = 208,
        [Description("Canvas App")]
        CanvasApp = 300,
        [Description("Connector")]
        Connector = 371,
        Connector_1 = 372,
        EnvironmentVariableDefinition = 380,
        EnvironmentVariableValue = 381,
        [Description("AI Project Type")]
        AIProjectType = 400,
        [Description("AI Project")]
        AIProject = 401,
        [Description("AI Configuration")]
        AIConfiguration = 402,
        EntityAnalyticsConfiguration = 430,
        [Description("Attribute Image Configuration")]
        AttributeImageConfiguration = 431,
        EntityImageConfiguration = 432,

        // TODO: map more description
    }
}
