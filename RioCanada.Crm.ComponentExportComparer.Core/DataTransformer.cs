using Newtonsoft.Json;
using RioCanada.Crm.ComponentExportComparer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RibbonClasses = RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.RibbonClasses;
using ModelDrivenAppDescriptorClasses = RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.ModelDrivenAppDescriptorClasses;
using ModelDrivenAppConfigClasses = RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.ModelDrivenAppConfigClasses;
using SiteMapClasses = RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.SiteMapClasses;
using FormXmlClasses = RioCanada.Crm.ComponentExportComparer.Core.Models.Transform.FormXmlClasses;
using RioCanada.Crm.ComponentExportComparer.Core.Extensions;

namespace RioCanada.Crm.ComponentExportComparer.Core
{
    public static class DataTransformer
    {
        private static readonly System.Xml.Serialization.XmlSerializerNamespaces NS = new System.Xml.Serialization.XmlSerializerNamespaces();
        public delegate bool ExceptionHandler(Exception ex);
        public static ExceptionHandler OnExceptionHandler { get; set; }

        static DataTransformer()
        {
            NS.Add(string.Empty, string.Empty);
        }

        private static void SortRibbonElements<T, U>(T obj, string property)
        {
            if (obj == null) return;
            var type = typeof(T);
            if (!(type.GetProperty(property).GetValue(obj) is U[] data)) return;
            type.GetProperty(property).SetValue(obj, data.OrderBy(x => (x as RibbonClasses.IdProperty).Id, StringComparer.OrdinalIgnoreCase).ToArray());
        }

        private static void SortElements<T, U>(T obj, string property, Func<U, string> keySelector)
        {
            if (obj == null) return;
            var type = typeof(T);
            if (!(type.GetProperty(property).GetValue(obj) is U[] data)) return;
            type.GetProperty(property).SetValue(obj, data.OrderBy(keySelector, StringComparer.OrdinalIgnoreCase).ToArray());
        }

        private static string RemoveCommentFromXml(string xml)
        {
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument
            {
                PreserveWhitespace = false
            };
            xDoc.LoadXml(xml);

            System.Xml.XmlNodeList list = xDoc.SelectNodes("//comment()");

            foreach (System.Xml.XmlNode node in list)
            {
                node.ParentNode.RemoveChild(node);
            }

            list = xDoc.SelectNodes("//text()");

            foreach (System.Xml.XmlNode node in list)
            {
                if (node.InnerText?.Trim() == "\"")
                {
                    node.ParentNode.RemoveChild(node);
                }
            }

            list = xDoc.SelectNodes("//*[count(child::*) = 0]");

            foreach (System.Xml.XmlNode node in list)
            {
                if (node is System.Xml.XmlElement xmlNode && !xmlNode.IsEmpty && node.FirstChild == null && node.InnerText == string.Empty)
                {
                    xmlNode.IsEmpty = true;
                }
            }

            XmlUtils.SortXml(xDoc);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(sw);
            xDoc.WriteContentTo(xtw);
            xtw.Close();
            sw.Close();
            return sw.ToString();
        }

        public static string TransformXml(string xmlString)
        {
            return SerializeUtility.FormatXml(RemoveCommentFromXml(xmlString));
        }

        public static string TransformJson(string jsonString)
        {
            return SerializeUtility.FormatJson(jsonString);
        }

        public static string TransformRibbonXml(byte[] data, bool verifyTransformedData, Logger logger)
        {
            try
            {
                logger?.Log("Transforming ribbon xml");
                string xmlString = Encoding.UTF8.GetString(data);

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(RibbonClasses.RibbonDefinitions));
                System.IO.StringReader stringReader = new System.IO.StringReader(xmlString);
                RibbonClasses.RibbonDefinitions ribbon = (RibbonClasses.RibbonDefinitions)serializer.Deserialize(stringReader);

                if (verifyTransformedData)
                {
                    VerifyTransformedDataXml(serializer, ribbon, xmlString, "Ribbon");
                }

                SortRibbonElements<RibbonClasses.Tabs, RibbonClasses.Tab>(ribbon.RibbonDefinition.UI?.Ribbon.Tabs, "Tab");
                SortRibbonElements<RibbonClasses.RibbonTemplates, RibbonClasses.GroupTemplate>(ribbon.RibbonDefinition.Templates?.RibbonTemplates, "GroupTemplate");
                SortRibbonElements<RibbonClasses.RibbonDefinition, RibbonClasses.CommandDefinition>(ribbon.RibbonDefinition, "CommandDefinitions");
                SortRibbonElements<RibbonClasses.RuleDefinitions, RibbonClasses.DisplayRule>(ribbon.RibbonDefinition.RuleDefinitions, "DisplayRules");
                SortRibbonElements<RibbonClasses.RuleDefinitions, RibbonClasses.EnableRule>(ribbon.RibbonDefinition.RuleDefinitions, "EnableRules");

                if (ribbon.RibbonDefinition.UI?.Ribbon?.Tabs?.Tab != null)
                {
                    foreach (var tab in ribbon.RibbonDefinition.UI.Ribbon.Tabs.Tab)
                    {
                        if (tab.Groups == null) continue;
                        SortRibbonElements<RibbonClasses.TabGroups, RibbonClasses.TabGroup>(tab.Groups, "Group");

                        foreach (var group in tab.Groups.Group)
                        {
                            if (group.Controls == null) continue;
                            SortRibbonElements<RibbonClasses.Controls, object>(group.Controls, "Items");
                        }
                    }
                }

                System.IO.StringWriter stringOut = new System.IO.StringWriter();
                serializer.Serialize(stringOut, ribbon, NS);

                return stringOut.ToString();
            }
            catch (Exception ex)
            {
                logger?.Error(ex);
                logger?.WriteFile("ribbon.xml", data);

                ex = new Exception($"Ribbon Xml transformation failed due to \"{ex.Message}\"", ex);
                if (OnExceptionHandler != null)
                {
                    OnExceptionHandler.Invoke(ex);
                    return Encoding.UTF8.GetString(data);
                }
                throw ex;
            }
        }

        public static string TransformModelDrivenAppDescriptor(string data, bool verifyTransformedData, OrganizationService service, Logger logger)
        {
            if (string.IsNullOrWhiteSpace(data)) return data;

            try
            {
                logger?.Log("Transforming Model Driven App Descriptor");
                ModelDrivenAppDescriptorClasses.Descriptor descriptor = JsonConvert.DeserializeObject<ModelDrivenAppDescriptorClasses.Descriptor>(data);

                if (verifyTransformedData)
                {
                    var jsonVerificationOut = JsonConvert.SerializeObject(descriptor, Formatting.Indented);

                    if (TransformJson(data).Length != TransformJson(jsonVerificationOut).Length)
                    {
                        throw new Exception(ExtendMessageWithLineDetail("Model Driven App descriptor.json transformation verification failed", TransformJson(data), TransformJson(jsonVerificationOut)));
                    }
                }

                SortElements<ModelDrivenAppDescriptorClasses.Appcomponents, ModelDrivenAppDescriptorClasses.Entity>(descriptor.appInfo.AppComponents, "Entities", x => x.LogicalName);

                foreach (var item in descriptor.appInfo.Components)
                {
                    if (item.Type == (int)Models.ComponentType.Entity)
                    {
                        var entityItem = descriptor.appInfo.AppComponents.Entities.FirstOrDefault(x => x.Id == item.Id);

                        if (entityItem != null)
                        {
                            item.IdSpecified = false;
                            item.Name = entityItem.LogicalName;
                            item.NameSpecified = true;
                        }
                    }
                    else if (item.Type == (int)Models.ComponentType.SiteMap && service != null)
                    {
                        item.IdSpecified = false;
                        item.Name = Models.SiteMap.GetUniqueKeyById(service, new Guid(item.Id));
                        item.NameSpecified = true;
                    }
                }

                foreach (var item in descriptor.appInfo.AppComponents.Entities) item.IdSpecified = false;

                descriptor.appInfo.Components = descriptor.appInfo.Components.OrderBy(x => x.Type.ToString()).ThenBy(x => x.Name ?? x.Id).ToArray();

                descriptor.appInfo.SolutionIdSpecified = false;
                descriptor.appInfo.PublishedOnSpecified = false;
                descriptor.appInfo.AppIdSpecified = false;

                descriptor.publisherInfoSpecified = false;
                //if (service != null)
                //{
                //    descriptor.publisherInfo.IdSpecified = false;
                //    descriptor.publisherInfo.Name = Models.Publisher.GetUniqueNameById(service, new Guid(descriptor.publisherInfo.Id));
                //    descriptor.publisherInfo.NameSpecified = true;
                //}

                var jsonOut = JsonConvert.SerializeObject(descriptor, Formatting.Indented);

                return jsonOut;
            }
            catch (Exception ex)
            {
                logger?.Error(ex);
                logger?.WriteFile("descriptor.json", data);

                ex = new Exception($"Model Driven App descriptor.json transformation failed due to \"{ex.Message}\"", ex);
                if (OnExceptionHandler != null)
                {
                    OnExceptionHandler.Invoke(ex);
                    return data;
                }
                throw ex;
            }
        }

        public static string TransformModelDrivenAppConfigXml(string xmlString, bool verifyTransformedData, Logger logger)
        {
            if (string.IsNullOrWhiteSpace(xmlString)) return xmlString;

            try
            {
                logger?.Log("Transforming Model Driven App Config Xml");
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(ModelDrivenAppConfigClasses.AppModule));
                System.IO.StringReader stringReader = new System.IO.StringReader(xmlString);
                ModelDrivenAppConfigClasses.AppModule appModule = (ModelDrivenAppConfigClasses.AppModule)serializer.Deserialize(stringReader);

                if (verifyTransformedData)
                {
                    VerifyTransformedDataXml(serializer, appModule, xmlString, "Model Driven App Config Xml");
                }

                appModule.AppModuleComponents = appModule.AppModuleComponents.OrderBy(x => x.type).ThenBy(x => x.schemaName ?? x.id).ToArray();

                System.IO.StringWriter stringOut = new System.IO.StringWriter();
                serializer.Serialize(stringOut, appModule, NS);

                return stringOut.ToString();
            }
            catch (Exception ex)
            {
                logger?.Error(ex);
                logger?.WriteFile("config.xml", xmlString);

                ex = new Exception($"Model Driven App Config Xml transformation failed due to \"{ex.Message}\"", ex);
                if (OnExceptionHandler != null)
                {
                    OnExceptionHandler.Invoke(ex);
                    return xmlString;
                }
                throw ex;
            }
        }

        public static string TransformSiteMapXml(string xmlString, bool verifyTransformedData, Logger logger)
        {
            if (string.IsNullOrWhiteSpace(xmlString)) return xmlString;

            try
            {
                logger?.Log("Transforming SiteMap Xml");
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SiteMapClasses.SiteMap));
                System.IO.StringReader stringReader = new System.IO.StringReader(xmlString);
                SiteMapClasses.SiteMap sitemap = (SiteMapClasses.SiteMap)serializer.Deserialize(stringReader);

                if (verifyTransformedData)
                {
                    VerifyTransformedDataXml(serializer, sitemap, xmlString, "SiteMap Xml");
                }

                System.IO.StringWriter stringOut = new System.IO.StringWriter();
                serializer.Serialize(stringOut, sitemap, NS);

                return stringOut.ToString();
            }
            catch (Exception ex)
            {
                logger?.Error(ex);
                logger?.WriteFile("sitemap.xml", xmlString);

                ex = new Exception($"SiteMap Xml transformation failed due to \"{ex.Message}\"", ex);
                if (OnExceptionHandler != null)
                {
                    OnExceptionHandler.Invoke(ex);
                    return xmlString;
                }
                throw ex;
            }
        }

        public static string TransformFormXml(string xmlString, bool verifyTransformedData, OrganizationService service, Logger logger)
        {
            if (string.IsNullOrWhiteSpace(xmlString)) return xmlString;

            try
            {
                logger?.Log("Transforming Form Xml");
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(FormXmlClasses.Form));
                System.IO.StringReader stringReader = new System.IO.StringReader(xmlString);
                FormXmlClasses.Form form = (FormXmlClasses.Form)serializer.Deserialize(stringReader);

                if (verifyTransformedData)
                {
                    VerifyTransformedDataXml(serializer, form, xmlString, "Form Xml");
                }

                foreach (var tab in form.Tabs.TabItems)
                {
                    tab.IdSpecified = false;
                    if (tab.Columns != null)
                        foreach (var column in tab.Columns)
                        {
                            if (column.Sections != null)
                                foreach (var section in column.Sections)
                                {
                                    section.IdSpecified = false;
                                    if (section.Rows != null)
                                        foreach (var row in section.Rows)
                                        {
                                            if (row.Cells != null)
                                                foreach (var cell in row.Cells)
                                                {
                                                    cell.IdSpecified = false;
                                                }
                                        }
                                }
                        }
                }

                if (form.Header != null && form.Header.Rows != null)
                    foreach (var row in form.Header.Rows)
                    {
                        if (row.Cells != null)
                            foreach (var cell in row.Cells)
                            {
                                cell.IdSpecified = false;
                            }
                    }

                if (service != null && form.DisplayConditions?.Roles != null && form.DisplayConditions.Roles.Length > 0)
                {
                    foreach (var role in form.DisplayConditions.Roles)
                    {
                        role.IdSpecified = false;
                        role.Name = Models.SecurityRole.GetNameById(service, new Guid(role.Id));
                        role.NameSpecified = true;
                    }
                }

                System.IO.StringWriter stringOut = new System.IO.StringWriter();
                serializer.Serialize(stringOut, form, NS);

                return stringOut.ToString();
            }
            catch (Exception ex)
            {
                logger?.Error(ex);
                logger?.WriteFile("form.xml", xmlString);

                ex = new Exception($"Form Xml transformation failed due to \"{ex.Message}\"", ex);
                if (OnExceptionHandler != null)
                {
                    OnExceptionHandler.Invoke(ex);
                    return xmlString;
                }
                throw ex;
            }
        }

        private static void VerifyTransformedDataXml<T>(System.Xml.Serialization.XmlSerializer serializer, T data, string xmlString, string label)
        {
            var stringOut = new System.IO.StringWriter();
            serializer.Serialize(stringOut, data, NS);

            var xmlOut = stringOut.ToString();

            if (TransformXml(xmlString).Length != TransformXml(xmlOut).Length)
            {
                throw new Exception(ExtendXmlMessageWithLineDetail($"{label} transform verification failed", xmlString, xmlOut));
            }
        }

        private static string ExtendXmlMessageWithLineDetail(string msg, string str1, string str2)
        {
            return ExtendMessageWithLineDetail(
                msg,
                TransformXml(str1),
                TransformXml(str2)
            );
        }

        private static string ExtendMessageWithLineDetail(string msg, string str1, string str2)
        {
            var str1Lines = str1.Split('\n');
            var str2Lines = str2.Split('\n');

            List<string> messages = new List<string> { msg };

            if (str1Lines.Length != str2Lines.Length)
            {
                messages.Add($"Expected lines are {str1Lines.Length} but actual lines are {str2Lines.Length}");
            }

            var minLine = Math.Min(str1Lines.Length, str2Lines.Length);

            for (var i = 0; i < minLine; i++)
            {
                if (str1Lines[i].Length != str2Lines[i].Length)
                {
                    messages.Add($@"Line number {i + 1} have different size of content.
Input:
{str1Lines[i]}

Output:
{str2Lines[i]}");
                    break;
                }
            }

            return string.Join(Environment.NewLine, messages);
        }
    }

    public abstract class XmlUtils
    {
        // #################################################################### //
        // These code is derived from Mainsoft.com                              //
        // http://www.koders.com/csharp/fid439BB5BEF93D1AEFAF0B9206236AB0ECE49BC229.aspx
        // #################################################################### //


        /// -----------------------------------------------------------
        /// <summary>
        /// Alphabetical sorting of the XmlNodes 
        /// and their  attributes in the <see cref="System.Xml.XmlDocument"/>.
        /// </summary>
        /// <param name="document"><see cref="System.Xml.XmlDocument"/> to be sorted</param>
        /// -----------------------------------------------------------
        public static void SortXml(System.Xml.XmlDocument document)
        {
            SortXml(document.DocumentElement);
        }


        /// -----------------------------------------------------------
        /// <summary>
        /// Inplace pre-order recursive alphabetical sorting of the XmlNodes child 
        /// elements and <see cref="System.Xml.XmlAttributeCollection" />.
        /// </summary>
        /// <param name="rootNode">The root to be sorted.</param>
        /// -----------------------------------------------------------
        public static void SortXml(System.Xml.XmlNode rootNode)
        {
            SortAttributes(rootNode.Attributes);
            SortElements(rootNode);
            foreach (System.Xml.XmlNode childNode in rootNode.ChildNodes)
            {
                SortXml(childNode);
            }
        }

        /// -----------------------------------------------------------
        /// <summary>
        /// Sorts an attributes collection alphabetically.
        /// It uses the bubble sort algorithm.
        /// </summary>
        /// <param name="attribCol">The attribute collection to be sorted.</param>
        /// -----------------------------------------------------------
        public static void SortAttributes(System.Xml.XmlAttributeCollection attribCol)
        {
            if (attribCol == null)
                return;

            bool hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;
                for (int i = 1; i < attribCol.Count; i++)
                {
                    if (String.Compare(attribCol[i].Name, attribCol[i - 1].Name, true) < 0)
                    {
                        //Replace
                        attribCol.InsertBefore(attribCol[i], attribCol[i - 1]);
                        hasChanged = true;
                    }
                }
            }

        }

        /// -----------------------------------------------------------
        /// <summary>
        /// Sorts a <see cref="XmlNodeList" /> alphabetically, by the names of the elements.
        /// It uses the bubble sort algorithm.
        /// </summary>
        /// <param name="node">The node in which its childNodes are to be sorted.</param>
        /// -----------------------------------------------------------
        public static void SortElements(System.Xml.XmlNode node)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = 1; i < node.ChildNodes.Count; i++)
                {
                    if (String.Compare(node.ChildNodes[i].Name, node.ChildNodes[i - 1].Name, true) < 0)
                    {
                        //Replace:
                        node.InsertBefore(node.ChildNodes[i], node.ChildNodes[i - 1]);
                        changed = true;
                    }
                }
            }
        }
    }
}
