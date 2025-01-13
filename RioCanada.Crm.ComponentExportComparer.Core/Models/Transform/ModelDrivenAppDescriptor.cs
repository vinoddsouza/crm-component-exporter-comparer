using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.ModelDrivenAppDescriptorClasses
{
    public class Descriptor
    {
        public Appinfo appInfo { get; set; }
        public Webresourceinfo webResourceInfo { get; set; }
        public Welcomepageinfo welcomePageInfo { get; set; }
        private Publisherinfo _publisherInfo { get; set; }
        public Publisherinfo publisherInfo { get => _publisherInfo; set { _publisherInfo = value; publisherInfoSpecified = true; } }
        [JsonIgnore]
        public bool publisherInfoSpecified { get; set; }
        public bool ShouldSerializepublisherInfo() { return publisherInfoSpecified; }
        public object[] eventHandlers { get; set; }
    }

    public class Appinfo
    {
        private string appId { get; set; }
        public string AppId { get => appId; set { appId = value; AppIdSpecified = true; } }
        [JsonIgnore]
        public bool AppIdSpecified { get; set; }
        public bool ShouldSerializeAppId() { return AppIdSpecified; }
        public string Title { get; set; }
        public string UniqueName { get; set; }
        private string _Description { get; set; }
        public string Description { get => _Description; set { _Description = value; DescriptionSpecified = true; } }
        [JsonIgnore]
        public bool DescriptionSpecified { get; set; }
        public bool ShouldSerializeDescription() { return DescriptionSpecified; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }
        private string _AppUrl { get; set; }
        public string AppUrl { get => _AppUrl; set { _AppUrl = value; AppUrlSpecified = true; } }
        [JsonIgnore]
        public bool AppUrlSpecified { get; set; }
        public bool ShouldSerializePublishedon () { return AppUrlSpecified; }
        private string publishedOn { get; set; }
        public string PublishedOn { get => publishedOn; set { publishedOn = value; PublishedOnSpecified = true; } }
        [JsonIgnore]
        public bool PublishedOnSpecified { get; set; }
        public bool ShouldSerializePublishedOn() { return PublishedOnSpecified; }
        public int ClientType { get; set; }
        public string webResourceId { get; set; }
        private string _welcomePageId { get; set; }
        public string welcomePageId { get => _welcomePageId; set { _welcomePageId = value; welcomePageIdSpecified = true; } }
        [JsonIgnore]
        public bool welcomePageIdSpecified { get; set; }
        public bool ShouldSerializewelcomePageId() { return welcomePageIdSpecified; }
        public Component[] Components { get; set; }
        public object[] AppElements { get; set; }
        public int NavigationType { get; set; }
        private string _OptimizedFor { get; set; }
        public string OptimizedFor { get => _OptimizedFor; set { _OptimizedFor = value; OptimizedForSpecified = true; } }
        [JsonIgnore]
        public bool OptimizedForSpecified { get; set; }
        public bool ShouldSerializeOptimizedFor() { return OptimizedForSpecified; }
        public int IsFeatured { get; set; }
        private string solutionId { get; set; }
        public string SolutionId { get => solutionId; set { solutionId = value; SolutionIdSpecified = true; } }
        [JsonIgnore]
        public bool SolutionIdSpecified { get; set; }
        public bool ShouldSerializeSolutionId() { return SolutionIdSpecified; }
        public Appcomponents AppComponents { get; set; }
    }

    public class Appcomponents
    {
        public Entity[] Entities { get; set; }
    }

    public class Entity
    {
        private string id { get; set; }
        public string Id { get => id; set { id = value; IdSpecified = true; } }
        [JsonIgnore]
        public bool IdSpecified { get; set; }
        public bool ShouldSerializeId() { return IdSpecified; }
        public string LogicalName { get; set; }
    }

    public class Component
    {
        public int Type { get; set; }
        private string id { get; set; }
        public string Id { get => id; set { id = value; IdSpecified = true; } }
        [JsonIgnore]
        public bool IdSpecified { get; set; }
        public bool ShouldSerializeId() { return IdSpecified; }
        private string name { get; set; }
        public string Name { get => name; set { name = value; NameSpecified = true; } }
        [JsonIgnore]
        public bool NameSpecified { get; set; }
        public bool ShouldSerializeName() { return NameSpecified; }
    }

    public class Webresourceinfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int WebResourceType { get; set; }
        public string Guid { get; set; }
    }

    public class Welcomepageinfo
    {
        public int WebResourceType { get; set; }
    }

    public class Publisherinfo
    {
        private string id { get; set; }
        public string Id { get => id; set { id = value; IdSpecified = true; } }
        [JsonIgnore]
        public bool IdSpecified { get; set; }
        private string name { get; set; }
        public string Name { get => name; set { name = value; NameSpecified = true; } }
        [JsonIgnore]
        public bool NameSpecified { get; set; }
        public bool ShouldSerializeName() { return NameSpecified; }
    }
}
