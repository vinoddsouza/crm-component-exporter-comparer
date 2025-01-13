using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RioCanada.Crm.ComponentExportComparer.Core.Utilities
{
    static class SerializeUtility
    {
        public static string FormatXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return xml;
            }

            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        public static string FormatJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return json;
            }

            if (json.StartsWith("[") && json.EndsWith("]"))
            {
                return JsonConvert.SerializeObject(JObject.Parse("{\"data\": " + json + "}").GetValue("data"), Formatting.Indented);
            }

            return JsonConvert.SerializeObject(JObject.Parse(json), Formatting.Indented);
        }

        public static string FormatViewLayoutJson(string json, bool includeAllProperty)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return json;
            }

            var obj = JObject.Parse(json);

            if (!includeAllProperty)
            {
                obj.Remove("Object");
            }

            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static string FormatViewLayoutXml(string xml, bool includeAllProperty)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return xml;
            }

            try
            {
                XDocument doc = XDocument.Parse(xml);

                if (!includeAllProperty)
                {
                    doc.Root.Attribute("object")?.Remove();
                }

                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        public static string FormatFormXml(string xml, bool includeAllProperty)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return xml;
            }

            try
            {
                XDocument doc = XDocument.Parse(xml);

                if (!includeAllProperty)
                {
                    var element = doc.Root.Elements();
                    foreach (var node in element)
                    {
                        if (node.Name == "DisplayConditions")
                        {
                            node.Remove();
                        }
                    }
                }

                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        public static string SerializeJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonConverter[]
            {
                new OptionSetValueJsonConverter(),
                new EntityReferenceJsonConverter(),
            });
        }

        public static T DeserializeJson<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str, new JsonConverter[]
            {
                new OptionSetValueJsonConverter(),
                new EntityReferenceJsonConverter(),
            });
        }
    }

    class OptionSetValueJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(OptionSetValue) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return new OptionSetValue(Convert.ToInt32(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((value as OptionSetValue).Value);
        }
    }

    class EntityReferenceJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(EntityReference) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return new EntityReference("json", Guid.Parse((string)reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((value as EntityReference).Id);
        }
    }
}
