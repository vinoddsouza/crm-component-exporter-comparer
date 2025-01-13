using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
    ExportMetadata("Name", "Component Comparer and Exporter"),
    ExportMetadata("Description", "Export compare or export components between two enviornment."),
    // Please specify the base64 content of a 32x32 pixels image
    ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAA7AAAAOwBeShxvQAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAARHSURBVFiF7ZfLbxtFHMc/szvrRxI7DknrJnGTpgSSIAItVKituCCEyqWVKiTEAcEF7nDiwJ2/AIkjHKCoh6rAoQd6AKFKpVStKqAtaR23SVM7TpzYeXj92Mdw2MRp7HVio/I48JVWmsfvN7/PzPx2Zhf+ZYn6huvnPh62DKG1PEJhqmVTo2q4L7x3ZsYX4OezH72dvD39ubluSgUIBQpQQiCUajnIburq0OyRoeA7x98/9zWA3OyYm8l8Wlwz5Wsvd2E7iuClTkzh8NVYgkC5RPzBnccCsG66MpO1PgO2A5jrxQjAVKqK4yqkprBwiC08RDrWYwm+qWLJjWyWawCvnn4lr5TqrTc+vNtomUttAwgh8g0AA8P91bZHAnCNv+JVi9V6tv9N+h+glgMX5o0bjiLV7gAnL4bbj6qx2gAwU9YOA/vaHmxe7m7j49UAUC/dcRlPPiCRziFQpON93BobwtZ33jVb6tx6ej/peB8CRSK9yMTdOTTX9bX3BTAsm1PfX2FvrgCjfWBojFy9yXhylm9PHKUSDPgOVg4afPf6MR6GJJVyGV3Tmd7/LFNPJjh58QqG5TT4+E7npRtTXvCghLeehzcmoSdMb36NY9f+aDr7y0eeYSkWIZfNUlheZj6TJr+UI7unh6uHxnx9fAFG72W8wkAUyhaslGCw2+u7nwafy0kJmD7QX6sHQyEMubXAyQMDrQOEKhtn//5uqLpguzAYBcCwHKTbCODoOpbUa/VSycS2bYxg0KuH/E9MX4ClWJdXGIyCLsCQ3moAK9FO30SUtkN0rVirRyLdhDs6WF/13rje/GqDT1OA68+NeoWAhEv34YdpkJ7ptclR34EAXvw16cHokvxSjpJpEolEt/U1gPs1pob7+enoJMfO3Khlrn17gV+OTDA1mmgKMJ6coxwKcPXQGHbCszMsm+OXf2NkNts6AMDNsSHuHhwgvphHKEW2r4dKcPeb79DvKSbuPiDb14PAZV+2gGHbTe1rAB2SO8pVi9t6gzq5RB/g7VUYn0+zYOMBE6TCUG7jsNM3nm1Rfb4H3k1UngL6aVdvFne3aVSGT7zCf+c2rNeMqfHjksRRgsGwy4k9FnlL48KCpOIIoobLqbiFrEL2rI29AlpIsPe0htErWPzGoTynEDr0ntAJH2z4AwB2WIHZsobpCCoupIoajoJMBVYtr22xolGwBdYyVLPglsEuKCoPQdkKM6lwy+AUoZTyv4h2BOjWtxIuIhW6gO5HkkkK6NQVslMhNtdRgIyB0AUyujVjo8d/9hsuntTyuTR1SViwBJYLsYDC2LBcswVlB7okhHUFqfM4JjhroIVBeucOblVh50EYAuOJhrgZceTLAdiWAyIJahtAzGh87SJSEanLHL3Dex6VFhAE4k2nXfvL2doCTX0A3Gvi8jiVwtU+3GKpk8qfjyGM5ptWr9kzrYd2Akoc/qLQusM/oD8B+uaLcc0sW7MAAAAASUVORK5CYII="),
    // Please specify the base64 content of a 80x80 pixels image
    ExportMetadata("BigImageBase64", "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"),
    ExportMetadata("BackgroundColor", "Lavender"),
    ExportMetadata("PrimaryFontColor", "Black"),
    ExportMetadata("SecondaryFontColor", "Gray")]
    public class MyPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new MyPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MyPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}