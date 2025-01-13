using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.Metadata
{
    abstract class ManagedProperty<T>
    {
        public bool CanBeChanged { get; set; }
        public string ManagedPropertyLogicalName { get; set; }
        public T Value { get; set; }

        [JsonIgnore]
        public bool CanBeChangedSpecified { get; set; } = true;
        public bool ShouldSerializeCanBeChanged() { return CanBeChangedSpecified; }
    }

    class BooleanManagedProperty: ManagedProperty<bool>
    {
    }

    class Label
    {
        public List<LocalizedLabel> LocalizedLabels { get; set; }
        public LocalizedLabel UserLocalizedLabel { get; set; }
    }

    class LocalizedLabel : MetadataBase
    {
        public string Label { get; set; }
        public int LanguageCode { get; set; }
        public bool? IsManaged { get; }
    }

    class MetadataBase
    {
        public Guid? MetadataId { get; set; }
        [JsonIgnore]
        public bool MetadataIdSpecified { get; set; } = true;
        public bool ShouldSerializeMetadataId() { return MetadataIdSpecified; }
        public bool? HasChanged { get; set; }
        [JsonIgnore]
        public bool HasChangedSpecified { get; set; } = true;
        public bool ShouldSerializeHasChanged() { return HasChangedSpecified; }
    }

    class AttributeMetadata : MetadataBase
    {
        public string AttributeOf { get; set; }
        public AttributeTypeCode? AttributeType { get; set; }
        public AttributeTypeDisplayName AttributeTypeName { get; set; }
        public string AutoNumberFormat { get; set; }
        public bool? CanBeSecuredForCreate { get; set; }
        public bool? CanBeSecuredForRead { get; set; }
        public bool? CanBeSecuredForUpdate { get; set; }
        public BooleanManagedProperty CanModifyAdditionalSettings { get; set; }
        public int? ColumnNumber { get; set; }
        [JsonIgnore]
        public bool ColumnNumberSpecified { get; set; } = true;
        public bool ShouldSerializeColumnNumber() { return ColumnNumberSpecified; }
        public DateTime? CreatedOn { get; set; }
        [JsonIgnore]
        public bool CreatedOnSpecified { get; set; } = true;
        public bool ShouldSerializeCreatedOn() { return CreatedOnSpecified; }
        public string DeprecatedVersion { get; set; }
        public Label Description { get; set; }
        public Label DisplayName { get; set; }
        public string EntityLogicalName { get; set; }
        public string ExternalName { get; set; }
        public string InheritsFrom { get; set; }
        public string IntroducedVersion { get; set; }
        [JsonIgnore]
        public bool IntroducedVersionSpecified { get; set; } = true;
        public bool ShouldSerializeIntroducedVersion() { return IntroducedVersionSpecified; }
        public BooleanManagedProperty IsAuditEnabled { get; set; }
        public bool? IsCustomAttribute { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public bool? IsDataSourceSecret { get; set; }
        public bool? IsFilterable { get; set; }
        public BooleanManagedProperty IsGlobalFilterEnabled { get; set; }
        public bool? IsLogical { get; set; }
        public bool? IsManaged { get; set; }
        [JsonIgnore]
        public bool IsManagedSpecified { get; set; } = true;
        public bool ShouldSerializeIsManaged() { return IsManagedSpecified; }
        public bool? IsPrimaryId { get; set; }
        public bool? IsPrimaryName { get; set; }
        public BooleanManagedProperty IsRenameable { get; set; }
        public bool? IsRequiredForForm { get; set; }
        public bool? IsRetrievable { get; set; }
        [JsonIgnore]
        public bool IsRetrievableSpecified { get; set; } = true;
        public bool ShouldSerializeIsRetrievable() { return IsRetrievableSpecified; }
        public bool? IsSearchable { get; set; }
        [JsonIgnore]
        public bool IsSearchableSpecified { get; set; } = true;
        public bool ShouldSerializeIsSearchable() { return IsSearchableSpecified; }
        public bool? IsSecured { get; set; }
        public BooleanManagedProperty IsSortableEnabled { get; set; }
        public BooleanManagedProperty IsValidForAdvancedFind { get; set; }
        public bool? IsValidForCreate { get; set; }
        public bool? IsValidForForm { get; set; }
        public bool? IsValidForGrid { get; set; }
        public bool? IsValidForRead { get; set; }
        public bool? IsValidForUpdate { get; set; }
        public bool IsValidODataAttribute { get; set; }
        public Guid? LinkedAttributeId { get; set; }
        public string LogicalName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [JsonIgnore]
        public bool ModifiedOnSpecified { get; set; } = true;
        public bool ShouldSerializeModifiedOn() { return ModifiedOnSpecified; }
        public AttributeRequiredLevelManagedProperty RequiredLevel { get; set; }
        public string SchemaName { get; set; }
        public int? SourceType { get; set; }
    }

    class BigIntAttributeMetadata : AttributeMetadata
    {
        public long? MaxValue { get; set; }
        public long? MinValue { get; set; }
    }

    sealed class BooleanAttributeMetadata : AttributeMetadata
    {
        public bool? DefaultValue { get; set; }
        public string FormulaDefinition { get; set; }
        public int? SourceTypeMask { get; set; }
        public BooleanOptionSetMetadata OptionSet { get; set; }
    }

    sealed class DateTimeAttributeMetadata : AttributeMetadata
    {
        public Microsoft.Xrm.Sdk.Metadata.DateTimeFormat? Format { get; set; }
        public ImeMode? ImeMode { get; set; }
        public int? SourceTypeMask { get; set; }
        public string FormulaDefinition { get; set; }
        public DateTimeBehavior DateTimeBehavior { get; set; }
        public BooleanManagedProperty CanChangeDateTimeBehavior { get; set; }
    }


    sealed class DecimalAttributeMetadata : AttributeMetadata
    {
        public decimal? MaxValue { get; set; }
        public decimal? MinValue { get; set; }
        public int? Precision { get; set; }
        public ImeMode? ImeMode { get; set; }
        public string FormulaDefinition { get; set; }
        public int? SourceTypeMask { get; set; }
    }

    sealed class DoubleAttributeMetadata : AttributeMetadata
    {
        public ImeMode? ImeMode { get; set; }
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public int? Precision { get; set; }
    }

    sealed class EntityNameAttributeMetadata : EnumAttributeMetadata
    {
        public bool IsEntityReferenceStored { get; set; }
    }

    class EnumAttributeMetadata : AttributeMetadata
    {
        public int? DefaultFormValue { get; set; }
        public OptionSetMetadata OptionSet { get; set; }
    }

    sealed class FileAttributeMetadata : AttributeMetadata
    {
        public int? MaxSizeInKB { get; set; }
    }


    sealed class ImageAttributeMetadata : AttributeMetadata
    {
        public bool? IsPrimaryImage { get; set; }
        public short? MaxHeight { get; set; }
        public short? MaxWidth { get; set; }
        public int? MaxSizeInKB { get; set; }
        public bool? CanStoreFullImage { get; set; }
    }

    sealed class IntegerAttributeMetadata : AttributeMetadata
    {
        public IntegerFormat? Format { get; set; }
        public int? MaxValue { get; set; }
        public int? MinValue { get; set; }
        public string FormulaDefinition { get; set; }
        public int? SourceTypeMask { get; set; }
    }


    sealed class LookupAttributeMetadata : AttributeMetadata
    {
        public string[] Targets { get; set; }
        public LookupFormat? Format { get; set; }
    }


    sealed class ManagedPropertyAttributeMetadata : AttributeMetadata
    {
        public string ManagedPropertyLogicalName { get; set; }
        public int? ParentComponentType { get; set; }
        public string ParentAttributeName { get; set; }
        public AttributeTypeCode ValueAttributeTypeCode { get; set; }
    }


    sealed class MemoAttributeMetadata : AttributeMetadata
    {
        public StringFormat? Format { get; set; }
        public ImeMode? ImeMode { get; set; }
        public int? MaxLength { get; set; }
        public bool? IsLocalizable { get; set; }
    }


    sealed class MoneyAttributeMetadata : AttributeMetadata
    {
        public ImeMode? ImeMode { get; set; }
        public double? MaxValue { get; set; }
        public double? MinValue { get; set; }
        public int? Precision { get; set; }
        public int? PrecisionSource { get; set; }
        public string CalculationOf { get; set; }
        public string FormulaDefinition { get; set; }
        public int? SourceTypeMask { get; set; }
        public bool? IsBaseCurrency { get; set; }
    }



    sealed class MultiSelectPicklistAttributeMetadata : EnumAttributeMetadata
    {
        public string FormulaDefinition { get; set; }
        public int? SourceTypeMask { get; set; }
        public string ParentPicklistLogicalName { get; set; }
        public List<string> ChildPicklistLogicalNames { get; set; }
    }

    sealed class PicklistAttributeMetadata : EnumAttributeMetadata
    {
        public string FormulaDefinition { get; set; }
        public int? SourceTypeMask { get; set; }
        public string ParentPicklistLogicalName { get; set; }
        public List<string> ChildPicklistLogicalNames { get; set; }
    }


    sealed class StateAttributeMetadata : EnumAttributeMetadata
    {
    }


    sealed class StatusAttributeMetadata : EnumAttributeMetadata
    {
    }

    sealed class UniqueIdentifierAttributeMetadata : AttributeMetadata
    {
    }

    class StringAttributeMetadata : AttributeMetadata
    {
        public int? DatabaseLength { get; set; }
        public StringFormat? Format { get; set; }
        public StringFormatName FormatName { get; set; }
        public string FormulaDefinition { get; set; }
        public ImeMode? ImeMode { get; set; }
        public bool? IsLocalizable { get; set; }
        public int? MaxLength { get; set; }
        public int? SourceTypeMask { get; set; }
        public string YomiOf { get; set; }
    }

    sealed class BooleanOptionSetMetadata : OptionSetMetadataBase
    {
        public OptionMetadata TrueOption { get; set; }
        public OptionMetadata FalseOption { get; set; }
    }

    class OptionSetMetadataBase : MetadataBase
    {
        public Label Description { get; set; }
        [JsonIgnore]
        public bool DescriptionSpecified { get; set; } = true;
        public bool ShouldSerializeDescription() { return DescriptionSpecified; }
        public Label DisplayName { get; set; }
        [JsonIgnore]
        public bool DisplayNameSpecified { get; set; } = true;
        public bool ShouldSerializeDisplayName() { return DisplayNameSpecified; }
        public bool? IsCustomOptionSet { get; set; }
        [JsonIgnore]
        public bool IsCustomOptionSetSpecified { get; set; } = true;
        public bool ShouldSerializeIsCustomOptionSet() { return IsCustomOptionSetSpecified; }
        public bool? IsGlobal { get; set; }
        public bool? IsManaged { get; set; }
        [JsonIgnore]
        public bool IsManagedSpecified { get; set; } = true;
        public bool ShouldSerializeIsManaged() { return IsManagedSpecified; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        [JsonIgnore]
        public bool IsCustomizableSpecified { get; set; } = true;
        public bool ShouldSerializeIsCustomizable() { return IsCustomizableSpecified; }
        public string Name { get; set; }
        public string ExternalTypeName { get; set; }
        [JsonIgnore]
        public bool ExternalTypeNameSpecified { get; set; } = true;
        public bool ShouldSerializeExternalTypeName() { return ExternalTypeNameSpecified; }
        public OptionSetType? OptionSetType { get; set; }
        [JsonIgnore]
        public bool OptionSetTypeSpecified { get; set; } = true;
        public bool ShouldSerializeOptionSetType() { return OptionSetTypeSpecified; }
        public string IntroducedVersion { get; set; }
        [JsonIgnore]
        public bool IntroducedVersionSpecified { get; set; } = true;
        public bool ShouldSerializeIntroducedVersion() { return IntroducedVersionSpecified; }
    }

    sealed class OptionSetMetadata : OptionSetMetadataBase
    {
        public List<OptionMetadata> Options { get; set; }
        [JsonIgnore]
        public bool OptionsSpecified { get; set; } = true;
        public bool ShouldSerializeOptions() { return OptionsSpecified; }
        public string ParentOptionSetName { get; set; }
        [JsonIgnore]
        public bool ParentOptionSetNameSpecified { get; set; } = true;
        public bool ShouldSerializeParentOptionSetName() { return ParentOptionSetNameSpecified; }
    }

    class OptionMetadata : MetadataBase
    {
        public int? Value { get; set; }
        public Label Label { get; set; }
        public Label Description { get; set; }
        public string Color { get; set; }
        public bool? IsManaged { get; set; }
        [JsonIgnore]
        public bool IsManagedSpecified { get; set; } = true;
        public bool ShouldSerializeIsManaged() { return IsManagedSpecified; }
        public string ExternalValue { get; set; }
        public int[] ParentValues { get; set; }
    }

    class SecurityRoleMergedData
    {
        public SecurityRoleMetadata Metadata { get; set; }
        public List<PrivilegeMetadata> Privilege { get; set; }
    }

    class SecurityRoleMetadata
    {
        public EntityReference BusinessUnitId { get; set; }
        public BooleanManagedProperty CanBeDeleted { get; set; }
        public OptionSetValue ComponentState { get; set; }
        public EntityReference CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? Id { get; set; }
        public BooleanManagedProperty IsCustomizable { get; set; }
        public OptionSetValue IsInherited { get; set; }
        public bool? IsManaged { get; set; }
        public EntityReference ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string Name { get; set; }
        public Guid? OrganizationId { get; set; }
        public EntityReference ParentRoleId { get; set; }
        public EntityReference ParentRootRoleId { get; set; }
        public Guid? RoleIdUnique { get; set; }
        public EntityReference RoleTemplateId { get; set; }
        public Guid? SolutionId { get; set; }

        public void ChangeIncludeAllProperty(bool flag)
        {
            this.BusinessUnitIdSpecified = flag;
            this.CreatedBySpecified = flag;
            this.CreatedOnSpecified = flag;
            this.IsManagedSpecified = flag;
            this.ModifiedBySpecified = flag;
            this.ModifiedOnSpecified = flag;
            this.OrganizationIdSpecified = flag;
            this.ParentRoleIdSpecified = flag;
            this.ParentRootRoleIdSpecified = flag;
            this.RoleIdUniqueSpecified = flag;
            this.SolutionIdSpecified = flag;
        }

        [JsonIgnore]
        public bool BusinessUnitIdSpecified { get; set; } = true;
        public bool ShouldSerializeBusinessUnitId() { return BusinessUnitId != null && BusinessUnitIdSpecified; }

        [JsonIgnore]
        public bool CreatedBySpecified { get; set; } = true;
        public bool ShouldSerializeCreatedBy() { return CreatedBy != null && CreatedBySpecified; }

        [JsonIgnore]
        public bool CreatedOnSpecified { get; set; } = true;
        public bool ShouldSerializeCreatedOn() { return CreatedOn != null && CreatedOnSpecified; }

        [JsonIgnore]
        public bool IsManagedSpecified { get; set; } = true;
        public bool ShouldSerializeIsManaged() { return IsManaged != null && IsManagedSpecified; }

        [JsonIgnore]
        public bool ModifiedBySpecified { get; set; } = true;
        public bool ShouldSerializeModifiedBy() { return ModifiedBy != null && ModifiedBySpecified; }

        [JsonIgnore]
        public bool ModifiedOnSpecified { get; set; } = true;
        public bool ShouldSerializeModifiedOn() { return ModifiedOn != null && ModifiedOnSpecified; }

        [JsonIgnore]
        public bool OrganizationIdSpecified { get; set; } = true;
        public bool ShouldSerializeOrganizationId() { return OrganizationId != null && OrganizationIdSpecified; }

        [JsonIgnore]
        public bool ParentRoleIdSpecified { get; set; } = true;
        public bool ShouldSerializeParentRoleId() { return ParentRoleId != null && ParentRoleIdSpecified; }

        [JsonIgnore]
        public bool ParentRootRoleIdSpecified { get; set; } = true;
        public bool ShouldSerializeParentRootRoleId() { return ParentRootRoleId != null && ParentRootRoleIdSpecified; }

        [JsonIgnore]
        public bool RoleIdUniqueSpecified { get; set; } = true;
        public bool ShouldSerializeRoleIdUnique() { return RoleIdUnique != null && RoleIdUniqueSpecified; }

        [JsonIgnore]
        public bool SolutionIdSpecified { get; set; } = true;
        public bool ShouldSerializeSolutionId() { return SolutionId != null && SolutionIdSpecified; }
    }

    class PrivilegeMetadata
    {
        public int AccessRight { get; set; }
        public bool CanBeBasic { get; set; }
        public bool CanBeDeep { get; set; }
        public bool CanBeEntityReference { get; set; }
        public bool CanBeGlobal { get; set; }
        public bool CanBeLocal { get; set; }
        public bool CanBeParentEntityReference { get; set; }
        public OptionSetValue ComponentState { get; set; }
        public string IntroducedVersion { get; set; }
        public bool? IsManaged { get; set; }
        public string Name { get; set; }
        public Guid? PrivilegeRowId { get; set; }
        public Guid? SolutionId { get; set; }

        public void ChangeIncludeAllProperty(bool flag)
        {
            this.IntroducedVersionSpecified = flag;
            this.IsManagedSpecified = flag;
            this.PrivilegeRowIdSpecified = flag;
            this.SolutionIdSpecified = flag;
        }

        [JsonIgnore]
        public bool IntroducedVersionSpecified { get; set; } = true;
        public bool ShouldSerializeIntroducedVersion() { return IntroducedVersion != null && IntroducedVersionSpecified; }

        [JsonIgnore]
        public bool IsManagedSpecified { get; set; } = true;
        public bool ShouldSerializeIsManaged() { return IsManaged != null && IsManagedSpecified; }

        [JsonIgnore]
        public bool PrivilegeRowIdSpecified { get; set; } = true;
        public bool ShouldSerializePrivilegeRowId() { return PrivilegeRowId != null && PrivilegeRowIdSpecified; }

        [JsonIgnore]
        public bool SolutionIdSpecified { get; set; } = true;
        public bool ShouldSerializeSolutionId() { return SolutionId != null && SolutionIdSpecified; }
    }
}
