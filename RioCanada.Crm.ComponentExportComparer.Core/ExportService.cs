using Microsoft.Xrm.Tooling.Connector;
using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using RioCanada.Crm.ComponentExportComparer.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Compression;
using System.Linq;

namespace RioCanada.Crm.ComponentExportComparer.Core
{
    public static class ExportService
    {
        public delegate void OutFileHandler(string path, string content, byte[] data);

        public static OrganizationService GetService(string connectionString)
        {
            return new OrganizationService(new CrmServiceClient(connectionString));
        }

        public static ArgumentQueryResponse PrepareComponent(OrganizationService service, IEnumerable<ArgumentQueryRequest> argumentQueries, Action<int> onProgress = null, BackgroundWorker bgWorker = null)
        {
            return ArgumentQueryResponse.GetResponse(service, argumentQueries, onProgress);
        }

        public static ArgumentQueryResponse PrepareComponent(OrganizationService service, IEnumerable<string> argumentQueries, Action<int> onProgress = null, BackgroundWorker bgWorker = null)
        {
            return PrepareComponent(service, argumentQueries.Select(x => ArgumentQueryRequest.Parse(x)), onProgress, bgWorker);
        }

        public static void Export(OrganizationService service, List<string> argumentQueries, ArgumentQueryResponse argumentQueryResponse, ExportSetting setting, Logger logger, OutFileHandler outFileFunc, Action<ExportProgressSnapshot> onProgress, BackgroundWorker bgWorker = null, bool generateIndexFile = false)
        {
            var exporter = new Exporter(service, argumentQueries, argumentQueryResponse, setting, logger, outFileFunc, onProgress, bgWorker, generateIndexFile);
            exporter.Execute();
        }

        public static void WriteFile(string path, string content, byte[] data)
        {
            bool isEmpty = string.IsNullOrEmpty(content) && (data == null || data.Length == 0);
            if (isEmpty)
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return;
            }

            EnsureDirectory(System.IO.Path.GetDirectoryName(path));

            if (data != null && data.Length > 0)
            {
                System.IO.File.WriteAllBytes(path, data);
            }
            else
            {
                System.IO.File.WriteAllText(path, content);
            }
        }

        public static void EnsureDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("Invalid path");
            }

            if (!System.IO.Directory.Exists(path))
            {
                EnsureDirectory(System.IO.Path.GetDirectoryName(path));
                System.IO.Directory.CreateDirectory(path);
            }
        }

        public static ILookup<Guid, string> GetSolutionLookup(OrganizationService service)
        {
            return Solution.GetSolutionLookup(service);
        }

        public static IndexLineItem GetIndexData(string filePath)
        {
            try
            {
                using (var zip = ZipFile.OpenRead(filePath))
                {
                    var e = zip.GetEntry("index.json");

                    if (e == null)
                    {
                        return null;
                    }

                    using (var stream = e.Open())
                    using (var sr = new System.IO.StreamReader(stream))
                    {
                        var fileContent = sr.ReadToEnd();

                        var indexItem = Newtonsoft.Json.JsonConvert.DeserializeObject<IndexLineItem>(fileContent);
                        return indexItem;
                    }
                }
            }
            catch (System.IO.IOException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class ExportProgressSnapshot
    {
        public string CurrentLabel { get; set; }
        public int CurrentProgress { get; set; }
        public int OverallProgress { get; set; }

        public int CurrentTotal { get; set; }
        public int CurrentCompleted { get; set; }
    }

    public class ExportSetting
    {
        public bool IncludeAllProperty { get; set; }
        public bool IncludeSystemWebresource { get; set; }
        public bool IncludeSystemPluginStep { get; set; }
        public bool IncludeEntityColumn { get; set; }
        public bool IncludeEntityRelationship { get; set; }
        public bool IncludeEntityForm { get; set; }
        public bool IncludeEntityDashboard { get; set; }
        public bool IncludeEntityView { get; set; }
        public bool IncludeEntityRibbon { get; set; }
        public bool RoleById { get; set; }
        public bool VerifyTransformedData { get; set; }
        public bool ReplaceEmptyStringByNull { get; set; }
    }
}
