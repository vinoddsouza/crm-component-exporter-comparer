using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Extensions
{
    static class AttributeMetadataExtension
    {
        private static void ExtendBigIntAttributeMetadata(BigIntAttributeMetadata source, Models.Transform.Metadata.BigIntAttributeMetadata target, bool includeAllProperty)
        {
            target.MaxValue = source.MaxValue;
            target.MinValue = source.MinValue;
        }

        private static void ExtendBooleanAttributeMetadata(BooleanAttributeMetadata source, Models.Transform.Metadata.BooleanAttributeMetadata target, bool includeAllProperty)
        {
            target.DefaultValue = source.DefaultValue;
            target.FormulaDefinition = source.FormulaDefinition;
            target.SourceTypeMask = source.SourceTypeMask;
            target.OptionSet = source.OptionSet?.GetMetadataObject(includeAllProperty);
        }

        private static void ExtendDateTimeAttributeMetadata(DateTimeAttributeMetadata source, Models.Transform.Metadata.DateTimeAttributeMetadata target, bool includeAllProperty)
        {
            target.Format = source.Format;
            target.ImeMode = source.ImeMode;
            target.SourceTypeMask = source.SourceTypeMask;
            target.FormulaDefinition = source.FormulaDefinition;
            target.DateTimeBehavior = source.DateTimeBehavior;
            target.CanChangeDateTimeBehavior = source.CanChangeDateTimeBehavior?.GetMetadataObject(includeAllProperty);
        }

        private static void ExtendDecimalAttributeMetadata(DecimalAttributeMetadata source, Models.Transform.Metadata.DecimalAttributeMetadata target, bool includeAllProperty)
        {
            target.MaxValue = source.MaxValue;
            target.MinValue = source.MinValue;
            target.Precision = source.Precision;
            target.ImeMode = source.ImeMode;
            target.FormulaDefinition = source.FormulaDefinition;
            target.SourceTypeMask = source.SourceTypeMask;
        }

        private static void ExtendDoubleAttributeMetadata(DoubleAttributeMetadata source, Models.Transform.Metadata.DoubleAttributeMetadata target, bool includeAllProperty)
        {
            target.ImeMode = source.ImeMode;
            target.MaxValue = source.MaxValue;
            target.MinValue = source.MinValue;
            target.Precision = source.Precision;
        }

        private static void ExtendEntityNameAttributeMetadata(EntityNameAttributeMetadata source, Models.Transform.Metadata.EntityNameAttributeMetadata target, bool includeAllProperty)
        {
            target.IsEntityReferenceStored = source.IsEntityReferenceStored;
            ExtendEnumAttributeMetadata(source, target, includeAllProperty);
        }

        private static void ExtendEnumAttributeMetadata(EnumAttributeMetadata source, Models.Transform.Metadata.EnumAttributeMetadata target, bool includeAllProperty)
        {
            target.DefaultFormValue = source.DefaultFormValue;
            target.OptionSet = source.OptionSet?.GetMetadataObject(includeAllProperty);

            if (target.OptionSet?.IsGlobal == true)
            {
                target.OptionSet.OptionsSpecified = false;
                target.OptionSet.ParentOptionSetNameSpecified = false;
                target.OptionSet.DescriptionSpecified = false;
                target.OptionSet.DisplayNameSpecified = false;
                target.OptionSet.IsCustomOptionSetSpecified = false;
                target.OptionSet.IsCustomizableSpecified = false;
                target.OptionSet.ExternalTypeNameSpecified = false;
                target.OptionSet.OptionSetTypeSpecified = false;
                target.OptionSet.HasChangedSpecified = false;
            }
        }

        private static void ExtendFileAttributeMetadata(FileAttributeMetadata source, Models.Transform.Metadata.FileAttributeMetadata target, bool includeAllProperty)
        {
            target.MaxSizeInKB = source.MaxSizeInKB;
        }

        private static void ExtendImageAttributeMetadata(ImageAttributeMetadata source, Models.Transform.Metadata.ImageAttributeMetadata target, bool includeAllProperty)
        {
            target.IsPrimaryImage = source.IsPrimaryImage;
            target.MaxHeight = source.MaxHeight;
            target.MaxWidth = source.MaxWidth;
            target.MaxSizeInKB = source.MaxSizeInKB;
            target.CanStoreFullImage = source.CanStoreFullImage;
        }

        private static void ExtendIntegerAttributeMetadata(IntegerAttributeMetadata source, Models.Transform.Metadata.IntegerAttributeMetadata target, bool includeAllProperty)
        {
            target.Format = source.Format;
            target.MaxValue = source.MaxValue;
            target.MinValue = source.MinValue;
            target.FormulaDefinition = source.FormulaDefinition;
            target.SourceTypeMask = source.SourceTypeMask;
        }

        private static void ExtendLookupAttributeMetadata(LookupAttributeMetadata source, Models.Transform.Metadata.LookupAttributeMetadata target, bool includeAllProperty)
        {
            target.Targets = source.Targets;
            target.Format = source.Format;
        }

        private static void ExtendManagedPropertyAttributeMetadata(ManagedPropertyAttributeMetadata source, Models.Transform.Metadata.ManagedPropertyAttributeMetadata target, bool includeAllProperty)
        {
            target.ManagedPropertyLogicalName = source.ManagedPropertyLogicalName;
            target.ParentComponentType = source.ParentComponentType;
            target.ParentAttributeName = source.ParentAttributeName;
            target.ValueAttributeTypeCode = source.ValueAttributeTypeCode;
        }

        private static void ExtendMemoAttributeMetadata(MemoAttributeMetadata source, Models.Transform.Metadata.MemoAttributeMetadata target, bool includeAllProperty)
        {
            target.Format = source.Format;
            target.ImeMode = source.ImeMode;
            target.MaxLength = source.MaxLength;
            target.IsLocalizable = source.IsLocalizable;
        }

        private static void ExtendMoneyAttributeMetadata(MoneyAttributeMetadata source, Models.Transform.Metadata.MoneyAttributeMetadata target, bool includeAllProperty)
        {
            target.ImeMode = source.ImeMode;
            target.MaxValue = source.MaxValue;
            target.MinValue = source.MinValue;
            target.Precision = source.Precision;
            target.PrecisionSource = source.PrecisionSource;
            target.CalculationOf = source.CalculationOf;
            target.FormulaDefinition = source.FormulaDefinition;
            target.SourceTypeMask = source.SourceTypeMask;
            target.IsBaseCurrency = source.IsBaseCurrency;
        }

        private static void ExtendMultiSelectPicklistAttributeMetadata(MultiSelectPicklistAttributeMetadata source, Models.Transform.Metadata.MultiSelectPicklistAttributeMetadata target, bool includeAllProperty)
        {
            target.FormulaDefinition = source.FormulaDefinition;
            target.SourceTypeMask = source.SourceTypeMask;
            target.ParentPicklistLogicalName = source.ParentPicklistLogicalName;
            target.ChildPicklistLogicalNames = source.ChildPicklistLogicalNames;
            ExtendEnumAttributeMetadata(source, target, includeAllProperty);
        }

        private static void ExtendPicklistAttributeMetadata(PicklistAttributeMetadata source, Models.Transform.Metadata.PicklistAttributeMetadata target, bool includeAllProperty)
        {
            target.FormulaDefinition = source.FormulaDefinition;
            target.SourceTypeMask = source.SourceTypeMask;
            target.ParentPicklistLogicalName = source.ParentPicklistLogicalName;
            target.ChildPicklistLogicalNames = source.ChildPicklistLogicalNames;
            ExtendEnumAttributeMetadata(source, target, includeAllProperty);
        }

        private static void ExtendStateAttributeMetadata(StateAttributeMetadata source, Models.Transform.Metadata.StateAttributeMetadata target, bool includeAllProperty)
        {
            ExtendEnumAttributeMetadata(source, target, includeAllProperty);
        }

        private static void ExtendStatusAttributeMetadata(StatusAttributeMetadata source, Models.Transform.Metadata.StatusAttributeMetadata target, bool includeAllProperty)
        {
            ExtendEnumAttributeMetadata(source, target, includeAllProperty);
        }

        private static void ExtendStringAttributeMetadata(StringAttributeMetadata source, Models.Transform.Metadata.StringAttributeMetadata target, bool includeAllProperty)
        {
            target.DatabaseLength = source.DatabaseLength;
            target.Format = source.Format;
            target.FormatName = source.FormatName;
            target.FormulaDefinition = source.FormulaDefinition;
            target.ImeMode = source.ImeMode;
            target.IsLocalizable = source.IsLocalizable;
            target.MaxLength = source.MaxLength;
            target.SourceTypeMask = source.SourceTypeMask;
            target.YomiOf = source.YomiOf;
        }

        private static void ExtendUniqueIdentifierAttributeMetadata(UniqueIdentifierAttributeMetadata source, Models.Transform.Metadata.UniqueIdentifierAttributeMetadata target, bool includeAllProperty)
        {
            // No additional property
        }

        public static object GetMetadataObject(this AttributeMetadata obj, bool includeAllProperty)
        {
            Models.Transform.Metadata.AttributeMetadata result;

            if (obj is BigIntAttributeMetadata)
            {
                result = new Models.Transform.Metadata.BigIntAttributeMetadata();
                ExtendBigIntAttributeMetadata(obj as BigIntAttributeMetadata, result as Models.Transform.Metadata.BigIntAttributeMetadata, includeAllProperty);
            }
            else if (obj is BooleanAttributeMetadata)
            {
                result = new Models.Transform.Metadata.BooleanAttributeMetadata();
                ExtendBooleanAttributeMetadata(obj as BooleanAttributeMetadata, result as Models.Transform.Metadata.BooleanAttributeMetadata, includeAllProperty);
            }
            else if (obj is DateTimeAttributeMetadata)
            {
                result = new Models.Transform.Metadata.DateTimeAttributeMetadata();
                ExtendDateTimeAttributeMetadata(obj as DateTimeAttributeMetadata, result as Models.Transform.Metadata.DateTimeAttributeMetadata, includeAllProperty);
            }
            else if (obj is DecimalAttributeMetadata)
            {
                result = new Models.Transform.Metadata.DecimalAttributeMetadata();
                ExtendDecimalAttributeMetadata(obj as DecimalAttributeMetadata, result as Models.Transform.Metadata.DecimalAttributeMetadata, includeAllProperty);
            }
            else if (obj is DoubleAttributeMetadata)
            {
                result = new Models.Transform.Metadata.DoubleAttributeMetadata();
                ExtendDoubleAttributeMetadata(obj as DoubleAttributeMetadata, result as Models.Transform.Metadata.DoubleAttributeMetadata, includeAllProperty);
            }
            else if (obj is EntityNameAttributeMetadata)
            {
                result = new Models.Transform.Metadata.EntityNameAttributeMetadata();
                ExtendEntityNameAttributeMetadata(obj as EntityNameAttributeMetadata, result as Models.Transform.Metadata.EntityNameAttributeMetadata, includeAllProperty);
            }
            else if (obj is EnumAttributeMetadata)
            {
                result = new Models.Transform.Metadata.EnumAttributeMetadata();
                ExtendEnumAttributeMetadata(obj as EnumAttributeMetadata, result as Models.Transform.Metadata.EnumAttributeMetadata, includeAllProperty);
            }
            else if (obj is FileAttributeMetadata)
            {
                result = new Models.Transform.Metadata.FileAttributeMetadata();
                ExtendFileAttributeMetadata(obj as FileAttributeMetadata, result as Models.Transform.Metadata.FileAttributeMetadata, includeAllProperty);
            }
            else if (obj is ImageAttributeMetadata)
            {
                result = new Models.Transform.Metadata.ImageAttributeMetadata();
                ExtendImageAttributeMetadata(obj as ImageAttributeMetadata, result as Models.Transform.Metadata.ImageAttributeMetadata, includeAllProperty);
            }
            else if (obj is IntegerAttributeMetadata)
            {
                result = new Models.Transform.Metadata.IntegerAttributeMetadata();
                ExtendIntegerAttributeMetadata(obj as IntegerAttributeMetadata, result as Models.Transform.Metadata.IntegerAttributeMetadata, includeAllProperty);
            }
            else if (obj is LookupAttributeMetadata)
            {
                result = new Models.Transform.Metadata.LookupAttributeMetadata();
                ExtendLookupAttributeMetadata(obj as LookupAttributeMetadata, result as Models.Transform.Metadata.LookupAttributeMetadata, includeAllProperty);
            }
            else if (obj is ManagedPropertyAttributeMetadata)
            {
                result = new Models.Transform.Metadata.ManagedPropertyAttributeMetadata();
                ExtendManagedPropertyAttributeMetadata(obj as ManagedPropertyAttributeMetadata, result as Models.Transform.Metadata.ManagedPropertyAttributeMetadata, includeAllProperty);
            }
            else if (obj is MemoAttributeMetadata)
            {
                result = new Models.Transform.Metadata.MemoAttributeMetadata();
                ExtendMemoAttributeMetadata(obj as MemoAttributeMetadata, result as Models.Transform.Metadata.MemoAttributeMetadata, includeAllProperty);
            }
            else if (obj is MoneyAttributeMetadata)
            {
                result = new Models.Transform.Metadata.MoneyAttributeMetadata();
                ExtendMoneyAttributeMetadata(obj as MoneyAttributeMetadata, result as Models.Transform.Metadata.MoneyAttributeMetadata, includeAllProperty);
            }
            else if (obj is MultiSelectPicklistAttributeMetadata)
            {
                result = new Models.Transform.Metadata.MultiSelectPicklistAttributeMetadata();
                ExtendMultiSelectPicklistAttributeMetadata(obj as MultiSelectPicklistAttributeMetadata, result as Models.Transform.Metadata.MultiSelectPicklistAttributeMetadata, includeAllProperty);
            }
            else if (obj is PicklistAttributeMetadata)
            {
                result = new Models.Transform.Metadata.PicklistAttributeMetadata();
                ExtendPicklistAttributeMetadata(obj as PicklistAttributeMetadata, result as Models.Transform.Metadata.PicklistAttributeMetadata, includeAllProperty);
            }
            else if (obj is StateAttributeMetadata)
            {
                result = new Models.Transform.Metadata.StateAttributeMetadata();
                ExtendStateAttributeMetadata(obj as StateAttributeMetadata, result as Models.Transform.Metadata.StateAttributeMetadata, includeAllProperty);
            }
            else if (obj is StatusAttributeMetadata)
            {
                result = new Models.Transform.Metadata.StatusAttributeMetadata();
                ExtendStatusAttributeMetadata(obj as StatusAttributeMetadata, result as Models.Transform.Metadata.StatusAttributeMetadata, includeAllProperty);
            }
            else if (obj is StringAttributeMetadata)
            {
                result = new Models.Transform.Metadata.StringAttributeMetadata();
                ExtendStringAttributeMetadata(obj as StringAttributeMetadata, result as Models.Transform.Metadata.StringAttributeMetadata, includeAllProperty);
            }
            else if (obj is UniqueIdentifierAttributeMetadata)
            {
                result = new Models.Transform.Metadata.UniqueIdentifierAttributeMetadata();
                ExtendUniqueIdentifierAttributeMetadata(obj as UniqueIdentifierAttributeMetadata, result as Models.Transform.Metadata.UniqueIdentifierAttributeMetadata, includeAllProperty);
            }
            else
            {
                result = new Models.Transform.Metadata.AttributeMetadata();
            }

            result.AttributeOf = obj.AttributeOf;
            result.AttributeType = obj.AttributeType;
            result.AttributeTypeName = obj.AttributeTypeName;
            result.AutoNumberFormat = obj.AutoNumberFormat;
            result.CanBeSecuredForCreate = obj.CanBeSecuredForCreate;
            result.CanBeSecuredForRead = obj.CanBeSecuredForRead;
            result.CanBeSecuredForUpdate = obj.CanBeSecuredForUpdate;
            result.CanModifyAdditionalSettings = obj.CanModifyAdditionalSettings.GetMetadataObject(includeAllProperty);
            result.ColumnNumber = obj.ColumnNumber;
            result.CreatedOn = obj.CreatedOn;
            result.DeprecatedVersion = obj.DeprecatedVersion;
            result.Description = obj.Description.GetMetadataObject(includeAllProperty);
            result.DisplayName = obj.DisplayName.GetMetadataObject(includeAllProperty);
            result.EntityLogicalName = obj.EntityLogicalName;
            result.ExternalName = obj.ExternalName;
            result.HasChanged = obj.HasChanged;
            result.InheritsFrom = obj.InheritsFrom;
            result.IntroducedVersion = obj.IntroducedVersion;
            result.IsAuditEnabled = obj.IsAuditEnabled.GetMetadataObject(includeAllProperty);
            result.IsCustomAttribute = obj.IsCustomAttribute;
            result.IsCustomizable = obj.IsCustomizable.GetMetadataObject(includeAllProperty);
            result.IsDataSourceSecret = obj.IsDataSourceSecret;
            result.IsFilterable = obj.IsFilterable;
            result.IsGlobalFilterEnabled = obj.IsGlobalFilterEnabled.GetMetadataObject(includeAllProperty);
            result.IsLogical = obj.IsLogical;
            result.IsManaged = obj.IsManaged;
            result.IsPrimaryId = obj.IsPrimaryId;
            result.IsPrimaryName = obj.IsPrimaryName;
            result.IsRenameable = obj.IsRenameable.GetMetadataObject(includeAllProperty);
            result.IsRequiredForForm = obj.IsRequiredForForm;
            result.IsRetrievable = obj.IsRetrievable;
            result.IsSearchable = obj.IsSearchable;
            result.IsSecured = obj.IsSecured;
            result.IsSortableEnabled = obj.IsSortableEnabled.GetMetadataObject(includeAllProperty);
            result.IsValidForAdvancedFind = obj.IsValidForAdvancedFind.GetMetadataObject(includeAllProperty);
            result.IsValidForCreate = obj.IsValidForCreate;
            result.IsValidForForm = obj.IsValidForForm;
            result.IsValidForGrid = obj.IsValidForGrid;
            result.IsValidForRead = obj.IsValidForRead;
            result.IsValidForUpdate = obj.IsValidForUpdate;
            result.IsValidODataAttribute = obj.IsValidODataAttribute;
            result.LinkedAttributeId = obj.LinkedAttributeId;
            result.LogicalName = obj.LogicalName;
            result.MetadataId = obj.MetadataId;
            result.ModifiedOn = obj.ModifiedOn;
            result.RequiredLevel = obj.RequiredLevel;
            result.SchemaName = obj.SchemaName;
            result.SourceType = obj.SourceType;

            if (!includeAllProperty)
            {
                result.AutoNumberFormat = string.IsNullOrEmpty(obj.AutoNumberFormat) ? null : obj.AutoNumberFormat;
                result.ColumnNumberSpecified = false;
                result.CreatedOnSpecified = false;
                result.IntroducedVersionSpecified = false;
                result.IsManagedSpecified = false;
                result.MetadataIdSpecified = false;
                result.ModifiedOnSpecified = false;
                result.IsSearchableSpecified = false;
                result.IsRetrievableSpecified = false;
            }

            return result;
        }
    }
}
