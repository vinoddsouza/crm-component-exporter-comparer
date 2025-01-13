using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Extensions
{
    static class EntityExtension
    {
        public static T Get<T>(this Entity entity, string attributeLogicalName)
        {
            return entity.GetAttributeValue<T>(attributeLogicalName);
        }

        public static void Set(this Entity entity, string attributeLogicalName, object value)
        {
            entity[attributeLogicalName] = value;
        }
    }
}
