using Newtonsoft.Json;
using RioCanada.Crm.ComponentExportComparer.Core.Models;
using RioCanada.Crm.ComponentExportComparer.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core
{
    public static class IndexLineItemGenerator
    {
        #region Privileges Mapping
        private static readonly List<string> ConfigurationPrivileges = new List<string>
            {
                "prvActOnBehalfOfAnotherUser",
                "prvActOnBehalfOfExternalParty",
                "prvActivate",
                "prvAddressBook",
                "prvAdminFilter",
                "prvAllowQuickCampaign",
                "prvApprove",
                "prvBrowseAvailability",
                "prvBulk",
                "prvBypassCustomPlugins",
                "prvChangeSqlEncryptionKey",
                "prvConfigure",
                "prvControlDecrementTerm",
                "prvDisable",
                "prvDocumentGeneration",
                "prvExport",
                "prvFlow",
                "prvGoOffline",
                "prvISVExtensions",
                "prvImport",
                "prvLanguageSettings",
                "prvLearningPath",
                "prvMailMerge",
                "prvMerge",
                "prvOneDrive",
                "prvOverride",
                "prvPostRuntimeIntegrationExternalEvent",
                "prvPrint",
                "prvPromoteToAdmin",
                "prvPublish",
                "prvQOIOverrideDelete",
                "prvRefreshRuntimeIntegrationComponents",
                "prvReparent",
                "prvRestoreSqlEncryptionKey",
                "prvRetrieveMultiple",
                "prvRollup",
                "prvSearchAvailability",
                "prvSendAsUser",
                "prvSendInviteForLive",
                "prvSyncToOutlook",
                "prvTurnDevErrorsOnOff",
                "prvUse",
                "prvWebMailMerge",
                "prvWorkflowExecution",
            };

        private static readonly List<string> TablePrivileges = new List<string>
            {
                "prvAppend",
                "prvAssign",
                "prvCreate",
                "prvDelete",
                "prvRead",
                "prvShare",
                "prvWrite",
            };
        #endregion

        private static string GetChecksum(string content)
        {
            MD5 md5Hash = MD5.Create();

            if (string.IsNullOrEmpty(content))
            {
                return "00000000000000000000000000000000";
            }

            var data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(content));

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private static void CalculateIndexChecksum(IndexLineItem item)
        {
            if (item.Children != null && item.Children.Count > 0)
            {
                item.Children.ForEach(x => CalculateIndexChecksum(x));
                item.Checksum = GetChecksum(string.Join("-", item.Children.OrderBy(x => x.Checksum).Select(x => x.Checksum)));
            }
        }

        private static void AddIndexItemWithContent<T>(string content, List<T> listIndexItems, T indexLineItem) where T : IndexLineItem
        {
            bool isEmpty = string.IsNullOrEmpty(content);
            if (!isEmpty)
            {
                indexLineItem.Content = content;
                indexLineItem.Checksum = GetChecksum(content);
                listIndexItems.Add(indexLineItem);
            }
        }

        public static Tuple<SecurityRoleIndexLineItem, List<SecurityRoleIndexLineItem>> GenerateForSecurityRole(string file)
        {
            SecurityRoleIndexLineItem treeViewIndexLineItem = new SecurityRoleIndexLineItem();
            List<SecurityRoleIndexLineItem> listViewIndexLineItems = new List<SecurityRoleIndexLineItem>();
            var result = new Tuple<SecurityRoleIndexLineItem, List<SecurityRoleIndexLineItem>>(treeViewIndexLineItem, listViewIndexLineItems);

            if (!System.IO.File.Exists(file))
            {
                return result;
            }

            var content = System.IO.File.ReadAllText(file);
            var securityRole = SerializeUtility.DeserializeJson<Models.Transform.Metadata.SecurityRoleMergedData>(content);

            string fileName = System.IO.Path.GetFileName(file);
            treeViewIndexLineItem.Key = fileName;
            treeViewIndexLineItem.Name = securityRole.Metadata.Name;
            treeViewIndexLineItem.Type = IndexItemType.Folder;
            treeViewIndexLineItem.Children = new List<IndexLineItem>();
            treeViewIndexLineItem.IsGroup = true;

            AddIndexItemWithContent(SerializeUtility.SerializeJson(securityRole.Metadata), treeViewIndexLineItem.Children, new SecurityRoleIndexLineItem { Key = "metadata", Name = "Metadata", Type = IndexItemType.FileJson, Order = 1, RoleName = securityRole.Metadata.Name, Category = "Metadata", PrivilegeType = null, Component = null, IsMetadata = true });
            AddIndexItemWithContent(SerializeUtility.SerializeJson(securityRole.Metadata), listViewIndexLineItems, new SecurityRoleIndexLineItem { Key = $"{fileName} - metadata", Name = "Metadata", Type = IndexItemType.FileJson, Order = 1, RoleName = securityRole.Metadata.Name, Category = "Metadata", PrivilegeType = null, Component = null, IsMetadata = true });

            var configurationPrvIndexItem = new SecurityRoleIndexLineItem
            {
                Key = "config-privilege",
                Name = "Configuration Privilege",
                Type = IndexItemType.Folder,
                Children = new List<IndexLineItem>(),
                Order = 2,
                IsGroup = true,
            };

            var tablePrvIndexItem = new SecurityRoleIndexLineItem
            {
                Key = "table-privilege",
                Name = "Table Privilege",
                Type = IndexItemType.Folder,
                Children = new List<IndexLineItem>(),
                Order = 3,
                IsGroup = true,
            };

            securityRole.Privilege.ForEach(x =>
            {
                string config = null;
                string category = null;
                string prvType = null;
                string prvComponent = null;
                string displayName = null;
                IndexLineItem indexItem;
                if ((config = ConfigurationPrivileges.Find(y => x.Name.StartsWith(y))) != null)
                {
                    indexItem = configurationPrvIndexItem;
                    prvType = config.Substring(3);
                    prvComponent = x.Name.Substring(config.Length);
                    displayName = $"{prvType} {prvComponent}";
                    category = "Configuration";
                }
                else if ((config = TablePrivileges.Find(y => x.Name.StartsWith(y))) != null)
                {
                    indexItem = tablePrvIndexItem;
                    prvType = config.Substring(3);
                    prvComponent = x.Name.Substring(config.Length);
                    displayName = $"{prvComponent} ({prvType})";
                    category = "Table";
                }
                else
                {
                    indexItem = configurationPrvIndexItem;
                    prvType = x.Name;
                    displayName = x.Name;
                }

                AddIndexItemWithContent(SerializeUtility.SerializeJson(x), indexItem.Children, new SecurityRoleIndexLineItem { Key = x.Name, Name = displayName, Type = IndexItemType.FileJson, RoleName = securityRole.Metadata.Name, Category = category, PrivilegeType = prvType, Component = prvComponent, IsPrivilege = true });
                AddIndexItemWithContent(SerializeUtility.SerializeJson(x), listViewIndexLineItems, new SecurityRoleIndexLineItem { Key = $"{fileName} - {x.Name}", Name = displayName, Type = IndexItemType.FileJson, RoleName = securityRole.Metadata.Name, Category = category, PrivilegeType = prvType, Component = prvComponent, IsPrivilege = true });
            });

            if (configurationPrvIndexItem.Children.Count > 0)
            {
                treeViewIndexLineItem.Children.Add(configurationPrvIndexItem);
            }

            if (tablePrvIndexItem.Children.Count > 0)
            {
                treeViewIndexLineItem.Children.Add(tablePrvIndexItem);
            }

            CalculateIndexChecksum(treeViewIndexLineItem);

            return result;
        }

        public static Tuple<List<IndexLineItem>, List<IndexLineItem>> GenerateForSecurityRoles(string directory)
        {
            var files = System.IO.Directory.GetFiles(directory);

            List<IndexLineItem> treeViewIndexLineItems = new List<IndexLineItem>();
            List<IndexLineItem> listViewIndexLineItems = new List<IndexLineItem>();
            var result = new Tuple<List<IndexLineItem>, List<IndexLineItem>>(treeViewIndexLineItems, listViewIndexLineItems);
            foreach (var file in files)
            {
                var r = GenerateForSecurityRole(file);
                treeViewIndexLineItems.Add(r.Item1);
                listViewIndexLineItems.AddRange(r.Item2);
            }

            return result;
        }

        public static List<IndexLineItem> GenerateForLists<T>(string file, Func<T, string> keyExtractor, Func<T, string> nameExtractor)
        {
            List<IndexLineItem> listViewIndexLineItems = new List<IndexLineItem>();

            if (!System.IO.File.Exists(file))
            {
                return listViewIndexLineItems;
            }

            var content = System.IO.File.ReadAllText(file);
            var items = SerializeUtility.DeserializeJson<List<T>>(content);
            var itemsAsObjectList = SerializeUtility.DeserializeJson<List<object>>(content);

            items.Select(x => x);

            for(var i = 0; i < items.Count; i++)
            {
                var key = keyExtractor(items[i]);
                var name = nameExtractor(items[i]);

                AddIndexItemWithContent(
                    SerializeUtility.SerializeJson(itemsAsObjectList[i]),
                    listViewIndexLineItems,
                    new IndexLineItem { Key = key, Name = name, Type = IndexItemType.FileJson }
                );
            }

            return listViewIndexLineItems;
        }

        public static List<IndexLineItem> GenerateForColumns(string file)
        {
            return GenerateForLists<Models.Transform.Metadata.AttributeMetadata>(
                file,
                x => x.LogicalName,
                x => x.DisplayName?.UserLocalizedLabel?.Label ?? x.LogicalName
            );
        }

        public static List<IndexLineItem> GenerateForManyToMany(string file)
        {
            return GenerateForLists<Microsoft.Xrm.Sdk.Metadata.ManyToManyRelationshipMetadata>(
                file,
                x => x.SchemaName,
                x => x.SchemaName
            );
        }

        public static List<IndexLineItem> GenerateForOneToMany(string file)
        {
            return GenerateForLists<Microsoft.Xrm.Sdk.Metadata.OneToManyRelationshipMetadata>(
                file,
                x => x.SchemaName,
                x => x.SchemaName
            );
        }

        public static RibbonIndexLineItems.Element GenerateForRibbon(string file)
        {
            RibbonIndexLineItems.Element treeViewIndexLineItem = new RibbonIndexLineItems.Element();
            var result = treeViewIndexLineItem;

            if (!System.IO.File.Exists(file))
            {
                return result;
            }

            var content = System.IO.File.ReadAllText(file);

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Models.Transform.RibbonClasses.RibbonDefinitions));
            System.IO.StringReader stringReader = new System.IO.StringReader(content);
            Models.Transform.RibbonClasses.RibbonDefinitions ribbon = (Models.Transform.RibbonClasses.RibbonDefinitions)serializer.Deserialize(stringReader);

            string fileName = System.IO.Path.GetFileName(file);
            treeViewIndexLineItem.Key = fileName;
            treeViewIndexLineItem.Name = "Root"; // securityRole.Metadata.Name;
            treeViewIndexLineItem.Type = IndexItemType.Folder;
            treeViewIndexLineItem.Children = new List<IndexLineItem>();
            treeViewIndexLineItem.IsGroup = true;

            {
                var commandIndexLineItem = new RibbonIndexLineItems.Element
                {
                    Key = "commands",
                    Name = "Commands",
                    Type = IndexItemType.FileXml,
                    IsGroup = true,
                    Children = new List<IndexLineItem>(),
                };

                AddIndexItemWithContent(SerializeUtility.SerializeJson(ribbon.RibbonDefinition.CommandDefinitions), treeViewIndexLineItem.Children, commandIndexLineItem);

                Dictionary<string, bool> usedKeys = new Dictionary<string, bool>();
                for (var i = 0; i < ribbon.RibbonDefinition.CommandDefinitions.Count(); i++)
                {
                    var command = ribbon.RibbonDefinition.CommandDefinitions[i];
                    var key = command.Id;
                    var keyIndex = 1;

                    while (usedKeys.ContainsKey(key))
                    {
                        key = command.Id + "$" + (++keyIndex);
                    }

                    usedKeys[key] = true;

                    AddIndexItemWithContent(SerializeUtility.SerializeJson(command), commandIndexLineItem.Children, new RibbonIndexLineItems.Element
                    {
                        Key = key,
                        Name = command.Id,
                        Type = IndexItemType.FileXml,
                    });
                }
            }

            {
                var displayRuleIndexLineItem = new RibbonIndexLineItems.Element
                {
                    Key = "display-rules",
                    Name = "Display Rules",
                    Type = IndexItemType.FileXml,
                    IsGroup = true,
                    Children = new List<IndexLineItem>(),
                };

                AddIndexItemWithContent(SerializeUtility.SerializeJson(ribbon.RibbonDefinition.RuleDefinitions.DisplayRules), treeViewIndexLineItem.Children, displayRuleIndexLineItem);

                Dictionary<string, bool> usedKeys = new Dictionary<string, bool>();
                for (var i = 0; i < ribbon.RibbonDefinition.RuleDefinitions.DisplayRules.Count(); i++)
                {
                    var rule = ribbon.RibbonDefinition.RuleDefinitions.DisplayRules[i];
                    var key = rule.Id;
                    var keyIndex = 1;

                    while (usedKeys.ContainsKey(key))
                    {
                        key = rule.Id + "$" + (++keyIndex);
                    }

                    usedKeys[key] = true;

                    AddIndexItemWithContent(SerializeUtility.SerializeJson(rule), displayRuleIndexLineItem.Children, new RibbonIndexLineItems.Element
                    {
                        Key = key,
                        Name = rule.Id,
                        Type = IndexItemType.FileXml,
                    });
                }
            }

            {
                var enableRuleIndexLineItem = new RibbonIndexLineItems.Element
                {
                    Key = "enable-rules",
                    Name = "Enable Rules",
                    Type = IndexItemType.FileXml,
                    IsGroup = true,
                    Children = new List<IndexLineItem>(),
                };

                AddIndexItemWithContent(SerializeUtility.SerializeJson(ribbon.RibbonDefinition.RuleDefinitions.EnableRules), treeViewIndexLineItem.Children, enableRuleIndexLineItem);

                Dictionary<string, bool> usedKeys = new Dictionary<string, bool>();
                for (var i = 0; i < ribbon.RibbonDefinition.RuleDefinitions.EnableRules.Count(); i++)
                {
                    var rule = ribbon.RibbonDefinition.RuleDefinitions.EnableRules[i];
                    var key = rule.Id;
                    var keyIndex = 1;

                    while (usedKeys.ContainsKey(key))
                    {
                        key = rule.Id + "$" + (++keyIndex);
                    }

                    usedKeys[key] = true;

                    AddIndexItemWithContent(SerializeUtility.SerializeJson(rule), enableRuleIndexLineItem.Children, new RibbonIndexLineItems.Element
                    {
                        Key = key,
                        Name = rule.Id,
                        Type = IndexItemType.FileXml,
                    });
                }
            }

            RibbonIndexLineItems.Element GenerateForTab(Models.Transform.RibbonClasses.Tab tab, RibbonIndexLineItems.Element indexItem)
            {
                var indexLineItem = new RibbonIndexLineItems.Element
                {
                    Key = tab.Id,
                    Name = tab.Id,
                    Type = IndexItemType.FileXml,
                    IsGroup = true,
                    Children = new List<IndexLineItem>(),
                };

                AddIndexItemWithContent(SerializeUtility.SerializeJson(tab), indexItem.Children, indexLineItem);

                Dictionary<string, bool> usedKeys = new Dictionary<string, bool>();
                for (var i = 0; i < tab.Groups.Group.Count(); i++)
                {
                    var group = tab.Groups.Group[i];

                    if (group.Controls.Items == null)
                    {
                        continue;
                    }

                    for (var j = 0; j < group.Controls.Items.Count(); j++)
                    {
                        var control = group.Controls.Items[j];

                        string id = string.Empty;

                        if (control is Models.Transform.RibbonClasses.Button buttonControl)
                        {
                            id = buttonControl.Id;
                        }
                        else if (control is Models.Transform.RibbonClasses.FlyoutAnchor flyoutAnchorControl)
                        {
                            id = flyoutAnchorControl.Id;
                        }
                        else if (control is Models.Transform.RibbonClasses.SplitButton splitButtonControl)
                        {
                            id = splitButtonControl.Id;
                        }
                        else if (control is Models.Transform.RibbonClasses.ToggleButton toggleButtonControl)
                        {
                            id = toggleButtonControl.Id;
                        }
                        else if (control is Models.Transform.RibbonClasses.Label labelControl)
                        {
                            id = labelControl.Id;
                        }
                        else
                        {
                            continue;
                        }

                        var key = id;
                        var keyIndex = 1;

                        while (usedKeys.ContainsKey(key))
                        {
                            key = id + "$" + (++keyIndex);
                        }

                        usedKeys[key] = true;

                        AddIndexItemWithContent(SerializeUtility.SerializeJson(control), indexLineItem.Children, new RibbonIndexLineItems.Element
                        {
                            Key = key,
                            Name = id,
                            Type = IndexItemType.FileXml,
                        });
                    }
                }

                return indexLineItem;
            }

            {
                var indexItem = new RibbonIndexLineItems.Element
                {
                    Key = "tabs",
                    Name = "Tabs",
                    Type = IndexItemType.FileXml,
                    IsGroup = true,
                    Children = new List<IndexLineItem>(),
                };

                AddIndexItemWithContent(SerializeUtility.SerializeJson(ribbon.RibbonDefinition.UI.Ribbon), treeViewIndexLineItem.Children, indexItem);

                for (var i = 0; i < ribbon.RibbonDefinition.UI.Ribbon.Tabs.Tab.Count(); i++)
                {
                    var tab = ribbon.RibbonDefinition.UI.Ribbon.Tabs.Tab[i];
                    if (!tab.Id.EndsWith(".MainTab"))
                    {
                        continue;
                    }

                    GenerateForTab(tab, indexItem);
                }

                for (var i = 0; i < ribbon.RibbonDefinition.UI.Ribbon.ContextualTabs.ContextualGroup.Count(); i++)
                {
                    var group = ribbon.RibbonDefinition.UI.Ribbon.ContextualTabs.ContextualGroup[i];
                    if (!group.Tab.Id.EndsWith(".MainTab"))
                    {
                        continue;
                    }

                    GenerateForTab(group.Tab, indexItem);
                }
            }

            CalculateIndexChecksum(treeViewIndexLineItem);

            return result;
        }

        class Ribbon
        {

        }
    }


    public class RibbonIndexLineItems
    {
        public class Element : IndexLineItem
        {
            public bool IsMetadata { get; set; }
            public bool IsGroup { get; set; }
            public bool Collapsed { get; set; }
        }
    }

    public class SecurityRoleIndexLineItem : IndexLineItem
    {
        public string RoleName { get; set; }
        public string Category { get; set; }
        public string PrivilegeType { get; set; }
        public string Component { get; set; }
        public bool IsMetadata { get; set; }
        public bool IsPrivilege { get; set; }
        public bool IsGroup { get; set; }
        public bool Collapsed { get; set; }
    }
}
