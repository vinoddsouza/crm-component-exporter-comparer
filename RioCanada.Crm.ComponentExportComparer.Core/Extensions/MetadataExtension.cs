using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Extensions
{
    static class MetadataExtension
    {
        public static object GetExportableObject(this EntityMetadata obj, bool includeAllProperty, bool replaceEmptyStringByNull)
        {
            if (includeAllProperty)
            {
                return new
                {
                    obj.ActivityTypeMask,
                    obj.AutoCreateAccessTeams,
                    obj.AutoRouteToOwnerQueue,
                    obj.CanBeInCustomEntityAssociation,
                    obj.CanBeInManyToMany,
                    obj.CanBePrimaryEntityInRelationship,
                    obj.CanBeRelatedEntityInRelationship,
                    obj.CanChangeHierarchicalRelationship,
                    obj.CanChangeTrackingBeEnabled,
                    obj.CanCreateAttributes,
                    obj.CanCreateCharts,
                    obj.CanCreateForms,
                    obj.CanCreateViews,
                    obj.CanEnableSyncToExternalSearchIndex,
                    obj.CanModifyAdditionalSettings,
                    obj.CanTriggerWorkflow,
                    obj.ChangeTrackingEnabled,
                    obj.CollectionSchemaName,
                    obj.DataProviderId,
                    obj.DataSourceId,
                    obj.DaysSinceRecordLastModified,
                    obj.Description,
                    obj.DisplayCollectionName,
                    obj.DisplayName,
                    obj.EnforceStateTransitions,
                    obj.EntityColor,
                    EntityHelpUrl = replaceEmptyStringByNull && string.IsNullOrWhiteSpace(obj.EntityHelpUrl) ? null : obj.EntityHelpUrl,
                    obj.EntityHelpUrlEnabled,
                    obj.EntitySetName,
                    obj.ExternalCollectionName,
                    obj.ExternalName,
                    obj.HasActivities,
                    obj.HasFeedback,
                    obj.HasNotes,
                    obj.IconLargeName,
                    obj.IconMediumName,
                    obj.IconSmallName,
                    IconVectorName = replaceEmptyStringByNull && string.IsNullOrWhiteSpace(obj.IconVectorName) ? null : obj.IconVectorName,
                    obj.IntroducedVersion,
                    obj.IsAIRUpdated,
                    obj.IsActivity,
                    obj.IsActivityParty,
                    obj.IsAuditEnabled,
                    obj.IsAvailableOffline,
                    obj.IsBPFEntity,
                    obj.IsBusinessProcessEnabled,
                    obj.IsChildEntity,
                    obj.IsConnectionsEnabled,
                    obj.IsCustomEntity,
                    obj.IsCustomizable,
                    obj.IsDocumentManagementEnabled,
                    obj.IsDocumentRecommendationsEnabled,
                    obj.IsDuplicateDetectionEnabled,
                    obj.IsEnabledForCharts,
                    obj.IsEnabledForExternalChannels,
                    obj.IsEnabledForTrace,
                    obj.IsImportable,
                    obj.IsInteractionCentricEnabled,
                    obj.IsIntersect,
                    obj.IsKnowledgeManagementEnabled,
                    obj.IsLogicalEntity,
                    obj.IsMSTeamsIntegrationEnabled,
                    obj.IsMailMergeEnabled,
                    obj.IsManaged,
                    obj.IsMappable,
                    obj.IsOfflineInMobileClient,
                    obj.IsOneNoteIntegrationEnabled,
                    obj.IsOptimisticConcurrencyEnabled,
                    obj.IsPrivate,
                    obj.IsQuickCreateEnabled,
                    obj.IsReadOnlyInMobileClient,
                    obj.IsReadingPaneEnabled,
                    obj.IsRenameable,
                    obj.IsSLAEnabled,
                    obj.IsSolutionAware,
                    obj.IsStateModelAware,
                    obj.IsValidForAdvancedFind,
                    obj.IsValidForQueue,
                    obj.IsVisibleInMobile,
                    obj.IsVisibleInMobileClient,
                    obj.LogicalCollectionName,
                    obj.LogicalName,
                    obj.MobileOfflineFilters,
                    obj.ObjectTypeCode,
                    obj.OwnershipType,
                    obj.PrimaryIdAttribute,
                    obj.PrimaryImageAttribute,
                    obj.PrimaryNameAttribute,
                    obj.RecurrenceBaseEntityLogicalName,
                    obj.ReportViewName,
                    obj.SchemaName,
                    obj.SettingOf,
                    obj.SyncToExternalSearchIndex,
                    obj.UsesBusinessDataLabelTable,
                };
            }
            else
            {
                return new
                {
                    obj.ActivityTypeMask,
                    obj.AutoCreateAccessTeams,
                    obj.AutoRouteToOwnerQueue,
                    CanBeInCustomEntityAssociation = obj.CanBeInCustomEntityAssociation.GetMetadataObject(includeAllProperty),
                    CanBeInManyToMany = obj.CanBeInManyToMany.GetMetadataObject(includeAllProperty),
                    CanBePrimaryEntityInRelationship = obj.CanBePrimaryEntityInRelationship.GetMetadataObject(includeAllProperty),
                    CanBeRelatedEntityInRelationship = obj.CanBeRelatedEntityInRelationship.GetMetadataObject(includeAllProperty),
                    CanChangeHierarchicalRelationship = obj.CanChangeHierarchicalRelationship.GetMetadataObject(includeAllProperty),
                    CanChangeTrackingBeEnabled = obj.CanChangeTrackingBeEnabled.GetMetadataObject(includeAllProperty),
                    CanCreateAttributes = obj.CanCreateAttributes.GetMetadataObject(includeAllProperty),
                    CanCreateCharts = obj.CanCreateCharts.GetMetadataObject(includeAllProperty),
                    CanCreateForms = obj.CanCreateForms.GetMetadataObject(includeAllProperty),
                    CanCreateViews = obj.CanCreateViews.GetMetadataObject(includeAllProperty),
                    CanEnableSyncToExternalSearchIndex = obj.CanEnableSyncToExternalSearchIndex.GetMetadataObject(includeAllProperty),
                    CanModifyAdditionalSettings = obj.CanModifyAdditionalSettings.GetMetadataObject(includeAllProperty),
                    obj.CanTriggerWorkflow,
                    obj.ChangeTrackingEnabled,
                    obj.CollectionSchemaName,
                    obj.DataProviderId,
                    obj.DataSourceId,
                    obj.DaysSinceRecordLastModified,
                    Description = obj.Description.GetMetadataObject(includeAllProperty),
                    DisplayCollectionName = obj.DisplayCollectionName.GetMetadataObject(includeAllProperty),
                    DisplayName = obj.DisplayName.GetMetadataObject(includeAllProperty),
                    obj.EnforceStateTransitions,
                    obj.EntityColor,
                    EntityHelpUrl = replaceEmptyStringByNull && string.IsNullOrWhiteSpace(obj.EntityHelpUrl) ? null : obj.EntityHelpUrl,
                    obj.EntityHelpUrlEnabled,
                    obj.EntitySetName,
                    obj.ExternalCollectionName,
                    obj.ExternalName,
                    obj.HasActivities,
                    obj.HasFeedback,
                    obj.HasNotes,
                    obj.IconLargeName,
                    obj.IconMediumName,
                    obj.IconSmallName,
                    IconVectorName = replaceEmptyStringByNull && string.IsNullOrWhiteSpace(obj.IconVectorName) ? null : obj.IconVectorName,
                    obj.IsAIRUpdated,
                    obj.IsActivity,
                    obj.IsActivityParty,
                    IsAuditEnabled = obj.IsAuditEnabled.GetMetadataObject(includeAllProperty),
                    obj.IsAvailableOffline,
                    obj.IsBPFEntity,
                    obj.IsBusinessProcessEnabled,
                    obj.IsChildEntity,
                    IsConnectionsEnabled = obj.IsConnectionsEnabled.GetMetadataObject(includeAllProperty),
                    obj.IsCustomEntity,
                    IsCustomizable = obj.IsCustomizable.GetMetadataObject(includeAllProperty),
                    obj.IsDocumentManagementEnabled,
                    obj.IsDocumentRecommendationsEnabled,
                    IsDuplicateDetectionEnabled = obj.IsDuplicateDetectionEnabled.GetMetadataObject(includeAllProperty),
                    obj.IsEnabledForCharts,
                    obj.IsEnabledForExternalChannels,
                    obj.IsEnabledForTrace,
                    obj.IsImportable,
                    obj.IsInteractionCentricEnabled,
                    obj.IsIntersect,
                    obj.IsKnowledgeManagementEnabled,
                    obj.IsLogicalEntity,
                    obj.IsMSTeamsIntegrationEnabled,
                    IsMailMergeEnabled = obj.IsMailMergeEnabled.GetMetadataObject(includeAllProperty),
                    IsMappable = obj.IsMappable.GetMetadataObject(includeAllProperty),
                    IsOfflineInMobileClient = obj.IsOfflineInMobileClient.GetMetadataObject(includeAllProperty),
                    obj.IsOneNoteIntegrationEnabled,
                    obj.IsOptimisticConcurrencyEnabled,
                    obj.IsPrivate,
                    obj.IsQuickCreateEnabled,
                    IsReadOnlyInMobileClient = obj.IsReadOnlyInMobileClient.GetMetadataObject(includeAllProperty),
                    obj.IsReadingPaneEnabled,
                    IsRenameable = obj.IsRenameable.GetMetadataObject(includeAllProperty),
                    obj.IsSLAEnabled,
                    obj.IsSolutionAware,
                    obj.IsStateModelAware,
                    obj.IsValidForAdvancedFind,
                    obj.IsValidForQueue,
                    IsVisibleInMobile = obj.IsVisibleInMobile.GetMetadataObject(includeAllProperty),
                    IsVisibleInMobileClient = obj.IsVisibleInMobileClient.GetMetadataObject(includeAllProperty),
                    obj.LogicalCollectionName,
                    obj.LogicalName,
                    obj.MobileOfflineFilters,
                    obj.OwnershipType,
                    obj.PrimaryIdAttribute,
                    obj.PrimaryImageAttribute,
                    obj.PrimaryNameAttribute,
                    obj.RecurrenceBaseEntityLogicalName,
                    obj.ReportViewName,
                    obj.SchemaName,
                    obj.SettingOf,
                    obj.SyncToExternalSearchIndex,
                    obj.UsesBusinessDataLabelTable,
                };
            }
        }

        private static void ExtendOptionSetMetadataBase(OptionSetMetadataBase source, Models.Transform.Metadata.OptionSetMetadataBase target, bool includeAllProperty)
        {
            target.Description = source.Description?.GetMetadataObject(includeAllProperty);
            target.DisplayName = source.DisplayName?.GetMetadataObject(includeAllProperty);
            target.ExternalTypeName = source.ExternalTypeName;
            target.IntroducedVersion = source.IntroducedVersion;
            target.IsCustomOptionSet = source.IsCustomOptionSet;
            target.IsGlobal = source.IsGlobal;
            target.IsManaged = source.IsManaged;
            target.IsCustomizable = source.IsCustomizable?.GetMetadataObject(includeAllProperty);
            target.Name = source.Name;
            target.OptionSetType = source.OptionSetType;

            if (!includeAllProperty)
            {
                target.IntroducedVersionSpecified = false;
                target.IsManagedSpecified = false;
            }
        }

        public static Models.Transform.Metadata.OptionSetMetadataBase GetMetadataObject(this OptionSetMetadataBase obj, bool includeAllProperty)
        {
            if (obj is OptionSetMetadata optionSet)
            {
                return optionSet.GetMetadataObject(includeAllProperty);
            }

            var result = new Models.Transform.Metadata.OptionSetMetadataBase();

            ExtendOptionSetMetadataBase(obj, result, includeAllProperty);

            return result;
        }

        public static Models.Transform.Metadata.OptionSetMetadata GetMetadataObject(this OptionSetMetadata obj, bool includeAllProperty)
        {
            var result = new Models.Transform.Metadata.OptionSetMetadata
            {
                Options = obj.Options.Select(x => x.GetMetadataObject(includeAllProperty)).ToList(),
                ParentOptionSetName = obj.ParentOptionSetName,
            };

            ExtendOptionSetMetadataBase(obj, result, includeAllProperty);

            return result;
        }

        public static Models.Transform.Metadata.OptionMetadata GetMetadataObject(this OptionMetadata obj, bool includeAllProperty)
        {
            var result = new Models.Transform.Metadata.OptionMetadata
            {
                Color = obj.Color,
                Description = obj.Description.GetMetadataObject(includeAllProperty),
                ExternalValue = obj.ExternalValue,
                HasChanged = obj.HasChanged,
                IsManaged = obj.IsManaged,
                Label = obj.Label.GetMetadataObject(includeAllProperty),
                MetadataId = obj.MetadataId,
                ParentValues = obj.ParentValues,
                Value = obj.Value,
            };

            if (!includeAllProperty)
            {
                result.MetadataIdSpecified = false;
                result.IsManagedSpecified = false;
            }

            return result;
        }

        public static Models.Transform.Metadata.BooleanOptionSetMetadata GetMetadataObject(this BooleanOptionSetMetadata obj, bool includeAllProperty)
        {
            var result = new Models.Transform.Metadata.BooleanOptionSetMetadata
            {
                TrueOption = obj.TrueOption?.GetMetadataObject(includeAllProperty),
                FalseOption = obj.FalseOption?.GetMetadataObject(includeAllProperty),
            };

            return result;
        }

        public static Models.Transform.Metadata.BooleanManagedProperty GetMetadataObject(this BooleanManagedProperty obj, bool includeAllProperty)
        {
            if (obj == null) return null;

            var result = new Models.Transform.Metadata.BooleanManagedProperty
            {
                CanBeChanged = obj.CanBeChanged,
                ManagedPropertyLogicalName = obj.ManagedPropertyLogicalName,
                Value = obj.Value,
            };

            if (!includeAllProperty)
            {
                result.CanBeChangedSpecified = false;
            }

            return result;
        }

        public static Models.Transform.Metadata.LocalizedLabel GetMetadataObject(this LocalizedLabel obj, bool includeAllProperty)
        {
            var result = new Models.Transform.Metadata.LocalizedLabel
            {
                HasChanged = obj.HasChanged,
                Label = obj.Label,
                LanguageCode = obj.LanguageCode,
                MetadataId = obj.MetadataId,
            };

            if (!includeAllProperty)
            {
                result.MetadataIdSpecified = false;
            }

            return result;
        }

        public static Models.Transform.Metadata.Label GetMetadataObject(this Label obj, bool includeAllProperty)
        {
            return new Models.Transform.Metadata.Label
            {
                LocalizedLabels = obj.LocalizedLabels.Select(x => x.GetMetadataObject(includeAllProperty)).ToList(),
                UserLocalizedLabel = obj.UserLocalizedLabel?.GetMetadataObject(includeAllProperty),
            };
        }

        public static object GetMetadataObject(this OneToManyRelationshipMetadata obj, bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    AssociatedMenuConfiguration = obj.AssociatedMenuConfiguration.GetMetadataObject(includeAllProperty),
                    CascadeConfiguration = obj.CascadeConfiguration.GetMetadataObject(),
                    obj.EntityKey,
                    obj.HasChanged,
                    obj.IntroducedVersion,
                    obj.IsCustomizable,
                    obj.IsCustomRelationship,
                    obj.IsHierarchical,
                    obj.IsManaged,
                    obj.IsValidForAdvancedFind,
                    obj.MetadataId,
                    obj.ReferencedAttribute,
                    obj.ReferencedEntity,
                    obj.ReferencedEntityNavigationPropertyName,
                    obj.ReferencingEntityNavigationPropertyName,
                    obj.ReferencingAttribute,
                    obj.ReferencingEntity,
                    obj.RelationshipAttributes,
                    obj.RelationshipBehavior,
                    obj.RelationshipType,
                    obj.SchemaName,
                    obj.SecurityTypes,
                };
            }
            else
            {
                return new
                {
                    AssociatedMenuConfiguration = obj.AssociatedMenuConfiguration.GetMetadataObject(includeAllProperty),
                    CascadeConfiguration = obj.CascadeConfiguration.GetMetadataObject(),
                    obj.EntityKey,
                    obj.HasChanged,
                    IsCustomizable = obj.IsCustomizable.GetMetadataObject(includeAllProperty),
                    obj.IsCustomRelationship,
                    obj.IsHierarchical,
                    obj.IsValidForAdvancedFind,
                    obj.ReferencedAttribute,
                    obj.ReferencedEntity,
                    obj.ReferencedEntityNavigationPropertyName,
                    obj.ReferencingEntityNavigationPropertyName,
                    obj.ReferencingAttribute,
                    obj.ReferencingEntity,
                    obj.RelationshipAttributes,
                    obj.RelationshipBehavior,
                    obj.RelationshipType,
                    obj.SchemaName,
                    obj.SecurityTypes,
                };
            }
        }

        public static object GetMetadataObject(this ManyToManyRelationshipMetadata obj, bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    Entity1AssociatedMenuConfiguration = obj.Entity1AssociatedMenuConfiguration.GetMetadataObject(includeAllProperty),
                    obj.Entity1IntersectAttribute,
                    obj.Entity1LogicalName,
                    obj.Entity1NavigationPropertyName,
                    Entity2AssociatedMenuConfiguration = obj.Entity2AssociatedMenuConfiguration.GetMetadataObject(includeAllProperty),
                    obj.Entity2IntersectAttribute,
                    obj.Entity2LogicalName,
                    obj.Entity2NavigationPropertyName,
                    obj.HasChanged,
                    obj.IntersectEntityName,
                    obj.IntroducedVersion,
                    IsCustomizable = obj.IsCustomizable.GetMetadataObject(includeAllProperty),
                    obj.IsCustomRelationship,
                    obj.IsManaged,
                    obj.IsValidForAdvancedFind,
                    obj.MetadataId,
                    obj.RelationshipType,
                    obj.SchemaName,
                    obj.SecurityTypes,
                };
            }
            else
            {
                return new
                {
                    Entity1AssociatedMenuConfiguration = obj.Entity1AssociatedMenuConfiguration.GetMetadataObject(includeAllProperty),
                    obj.Entity1IntersectAttribute,
                    obj.Entity1LogicalName,
                    obj.Entity1NavigationPropertyName,
                    Entity2AssociatedMenuConfiguration = obj.Entity2AssociatedMenuConfiguration.GetMetadataObject(includeAllProperty),
                    obj.Entity2IntersectAttribute,
                    obj.Entity2LogicalName,
                    obj.Entity2NavigationPropertyName,
                    obj.HasChanged,
                    obj.IntersectEntityName,
                    IsCustomizable = obj.IsCustomizable.GetMetadataObject(includeAllProperty),
                    obj.IsCustomRelationship,
                    obj.IsValidForAdvancedFind,
                    obj.RelationshipType,
                    obj.SchemaName,
                    obj.SecurityTypes,
                };
            }
        }

        public static object GetMetadataObject(this AssociatedMenuConfiguration obj, bool includeAllProperty)
        {
            if (includeAllProperty)
            {
                return new
                {
                    obj.AvailableOffline,
                    obj.Behavior,
                    obj.Group,
                    obj.Icon,
                    obj.IsCustomizable,
                    obj.Label,
                    obj.MenuId,
                    obj.Order,
                    obj.QueryApi,
                    obj.ViewId,
                };
            }
            else
            {
                return new
                {
                    obj.AvailableOffline,
                    obj.Behavior,
                    obj.Group,
                    obj.Icon,
                    obj.IsCustomizable,
                    Label = obj.Label?.GetMetadataObject(includeAllProperty),
                    obj.MenuId,
                    obj.Order,
                    obj.QueryApi,
                    obj.ViewId,
                };
            }
        }

        public static object GetMetadataObject(this CascadeConfiguration obj)
        {
            return new
            {
                obj.Assign,
                obj.Delete,
                obj.Merge,
                obj.Reparent,
                obj.Share,
                obj.Unshare,
                obj.RollupView,
            };
        }
    }
}

namespace Test
{
    class AttributeMetadata
    {
        public string EntityLogicalName { get; set; }
        public string AutoNumberFormat { get; set; }
        [JsonIgnore]
        public bool AutoNumberFormatSpecified { get; set; } = true;
        public bool ShouldSerializeAutoNumberFormat() { return AutoNumberFormatSpecified; }

    }

    class PicklistAttributeMetadata : AttributeMetadata
    {
        public string ParentPicklistLogicalName { get; set; }
    }
}
