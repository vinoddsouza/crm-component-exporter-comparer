using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.ModelDrivenAppConfigClasses
{
    [Serializable()]
    [XmlRoot]
    public class AppModule
    {
        public string UniqueName { get; set; }

        public string IntroducedVersion { get; set; }

        public string WebResourceId { get; set; }

        public object OptimizedFor { get; set; }

        public object statecode { get; set; }

        public object statuscode { get; set; }

        public object FormFactor { get; set; }

        public object ClientType { get; set; }

        public object NavigationType { get; set; }

        [XmlArrayItem("AppModuleComponent", IsNullable = false)]
        public AppModuleAppModuleComponent[] AppModuleComponents { get; set; }

        public AppModuleEventHandlers EventHandlers { get; set; }
    }

    [Serializable()]
    public class AppModuleAppModuleComponent
    {
        [XmlAttribute()]
        public uint type { get; set; }

        [XmlIgnore]
        public bool typeSpecified { get; set; }

        [XmlAttribute()]
        public string id { get; set; }

        [XmlAttribute()]
        public string schemaName { get; set; }

        [XmlIgnore]
        public bool schemaNameSpecified { get; set; }
    }


    [Serializable()]
    public class AppModuleEventHandlers
    {
        public AppModuleEventHandlersEventHandler EventHandler { get; set; }
    }

    [Serializable()]
    public class AppModuleEventHandlersEventHandler
    {

        [XmlAttribute()]
        public string eventname { get; set; }

        [XmlAttribute()]
        public string functionname { get; set; }

        [XmlAttribute()]
        public string libraryname { get; set; }

        [XmlAttribute()]
        public string parameters { get; set; }

        [XmlAttribute()]
        public bool enable { get; set; }
    }
}
