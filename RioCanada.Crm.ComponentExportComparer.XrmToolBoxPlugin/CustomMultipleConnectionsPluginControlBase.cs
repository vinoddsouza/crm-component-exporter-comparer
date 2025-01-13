using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    public class CustomMultipleConnectionsPluginControlBase : MultipleConnectionsPluginControlBase
    {
        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
