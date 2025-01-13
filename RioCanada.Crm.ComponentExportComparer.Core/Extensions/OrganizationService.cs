using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Extensions
{
    public class OrganizationService : IOrganizationService, IDisposable
    {
        public delegate bool RetryHandler(Exception ex);

        private IOrganizationService Service { get; set; }
        private bool isDisposed;
        public RetryHandler OnRetryHandler { get; set; }
        public string EnvironmentId
        {
            get
            {
                if (this.Service is CrmServiceClient)
                {
                    return (this.Service as CrmServiceClient).EnvironmentId;
                }

                return null;
            }
        }

        public string Server
        {
            get
            {
                if (this.Service is CrmServiceClient)
                {
                    var service = (this.Service as CrmServiceClient);
                    if (service.ConnectedOrgPublishedEndpoints?.Count() == 0) return null;

                    if (service.ConnectedOrgPublishedEndpoints.ContainsKey(Microsoft.Xrm.Sdk.Discovery.EndpointType.WebApplication))
                    {
                        return service.ConnectedOrgPublishedEndpoints[Microsoft.Xrm.Sdk.Discovery.EndpointType.WebApplication];
                    }
                }

                return null;
            }
        }

        public ConnectionInformation ConnectionInfo { get; }

        public Dictionary<string, object> CacheData { get; } = new Dictionary<string, object>();

        public OrganizationService(IOrganizationService service, ConnectionInformation info = null)
        {
            this.Service = service;
            this.ConnectionInfo = info;
        }

        #region Core functions
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.Service.Associate(entityName, entityId, relationship, relatedEntities);
        }

        public Guid Create(Entity entity)
        {
            return this.Service.Create(this.GetEntity(entity));
        }

        public void Delete(string entityName, Guid id)
        {
            this.Service.Delete(entityName, id);
        }

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.Service.Disassociate(entityName, entityId, relationship, relatedEntities);
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.WithOnRetry(() => this.Service.Execute(request));
        }

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return this.WithOnRetry(() => this.Service.Retrieve(entityName, id, columnSet));
        }

        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return this.WithOnRetry(() => this.Service.RetrieveMultiple(query));
        }

        public void Update(Entity entity)
        {
            this.Service.Update(this.GetEntity(entity));
        }

        private Entity GetEntity(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.GetType() == typeof(Entity))
            {
                return entity;
            }

            return entity.ToEntity<Entity>();
        }

        public Entity Retrieve(EntityReference entityReference, ColumnSet columnSet)
        {
            return this.WithOnRetry(() => this.Service.Retrieve(entityReference.LogicalName, entityReference.Id, columnSet));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                this.CacheData.Clear();
            }

            isDisposed = true;
        }

        #endregion

        #region Extended overrides

        private T WithOnRetry<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                if (this.OnRetryHandler?.Invoke(ex) == true)
                {
                    return this.WithOnRetry(action);
                }

                throw ex;
            }
        }

        public Entity Retrieve(string entityName, Guid id)
        {
            return this.Retrieve(entityName, id, new ColumnSet(true));
        }

        public T Retrieve<T>(string entityName, Guid id) where T : Entity
        {
            return this.Retrieve(entityName, id).ToEntity<T>();
        }

        public T Retrieve<T>(string entityName, Guid id, ColumnSet columnSet) where T : Entity
        {
            return this.Retrieve(entityName, id, columnSet).ToEntity<T>();
        }

        public T Retrieve<T>(EntityReference entityReference) where T : Entity
        {
            return this.Retrieve(entityReference.LogicalName, entityReference.Id).ToEntity<T>();
        }

        public T Retrieve<T>(EntityReference entityReference, ColumnSet columnSet) where T : Entity
        {
            return this.Retrieve(entityReference.LogicalName, entityReference.Id, columnSet).ToEntity<T>();
        }

        #endregion

        #region Extended methods

        /// <summary>
        /// Extended method of RetriveMultiple and convert to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<T> GetData<T>(QueryExpression query) where T : Entity
        {
            return this.RetrieveMultiple(query).Entities.Select(x => x.ToEntity<T>()).ToList();
        }

        /// <summary>
        /// Retrive data with in query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<T> GetData<T, U>(QueryExpression query, ConditionExpression condition, IEnumerable<U> values) where T : Entity
        {
            List<T> data = new List<T>();

            var count = values.Count();
            for (var i = 0; i < count; i += 100)
            {
                condition.Values.Clear();
                values.Skip(i).Take(100).ToList().ForEach(x => condition.Values.Add(x));
                data.AddRange(this.RetrieveMultiple(query).Entities.Select(x => x.ToEntity<T>()));
            }

            return data;
        }

        public List<T> GetData<T, U>(QueryExpression query, string attributeName, IEnumerable<U> values) where T : Entity
        {
            var condition = new ConditionExpression(attributeName, ConditionOperator.In);
            query.Criteria.AddCondition(condition);
            return this.GetData<T, U>(query, condition, values);
        }

        public List<T> GetData<T, U>(string entityName, string attributeName, IEnumerable<U> values) where T : Entity
        {
            QueryExpression query = new QueryExpression(entityName);
            query.ColumnSet.AllColumns = true;
            return this.GetData<T, U>(query, attributeName, values);
        }

        public List<T> GetData<T>(string entityName, IEnumerable<Guid> values) where T : Entity
        {
            QueryExpression query = new QueryExpression(entityName);
            query.ColumnSet.AllColumns = true;
            return this.GetData<T, Guid>(query, $"{entityName}id", values);
        }

        public List<T> GetData<T>(QueryExpression query, ConditionExpression condition, IEnumerable<Guid> values) where T : Entity
        {
            return this.GetData<T, Guid>(query, condition, values);
        }

        public List<T> GetData<T>(QueryExpression query, string attributeName, IEnumerable<Guid> values) where T : Entity
        {
            return this.GetData<T, Guid>(query, attributeName, values);
        }

        public List<T> GetBigData<T>(QueryExpression query) where T : Entity
        {
            if (query == null)
                throw new ArgumentNullException();

            if (query.TopCount != null)
                query.TopCount = null;

            query.PageInfo = new PagingInfo { Count = 5000, PageNumber = 1 };

            List<T> entities = new List<T>();
            EntityCollection result = null;

            while ((result = this.RetrieveMultiple(query)).Entities.Count != 0)
            {
                entities.AddRange(result.Entities.Select(x => x.ToEntity<T>()).ToList());
                query.PageInfo.PageNumber++;
                if (!result.MoreRecords) break;
            }

            return entities;
        }

        //public List<T> FindByPatterns<T>(QueryExpression query, string attributeName, params string[] patterns) where T : Entity
        //{
        //    if (patterns.Length == 0)
        //    {
        //        return new List<T>();
        //    }

        //    ConditionExpression condition = new ConditionExpression();
        //    query.Criteria.AddCondition(condition);
        //    condition.AttributeName = attributeName;

        //    var patternList = patterns.Where(x => Utilities.Helper.IsContainsWildcard(x)).ToList();
        //    var exactPatternList = patterns.Where(x => !string.IsNullOrWhiteSpace(x) && !Utilities.Helper.IsContainsWildcard(x)).ToList();

        //    List<T> records = new List<T>();
        //    foreach (var pattern in patternList)
        //    {
        //        condition.Operator = ConditionOperator.Like;
        //        condition.Values.Clear();
        //        condition.Values.Add(pattern.Replace("*", "%"));

        //        var result = this.GetData<T>(query);

        //        records.AddRange(result.FindAll(x => records.FindIndex(y => y.Id == x.Id) == -1));
        //    }

        //    if (exactPatternList.Count > 0)
        //    {
        //        condition.Operator = ConditionOperator.In;
        //        condition.Values.Clear();
        //        condition.Values.AddRange(exactPatternList);

        //        var result = this.GetData<T>(query);

        //        records.AddRange(result.FindAll(x => records.FindIndex(y => y.Id == x.Id) == -1));
        //    }

        //    return records;
        //}

        //public List<T> FindByPatterns<T>(string entityName, ColumnSet columnSet, string attributeName, params string[] patterns) where T : Entity
        //{
        //    QueryExpression query = new QueryExpression(entityName)
        //    {
        //        Distinct = true,
        //        ColumnSet = columnSet,
        //    };

        //    return this.FindByPatterns<T>(query, attributeName, patterns);
        //}

        //public List<T> FindByPatterns<T>(string entityName, string attributeName, params string[] patterns) where T : Entity
        //{
        //    return this.FindByPatterns<T>(entityName, new ColumnSet(true), attributeName, patterns);
        //}

        #endregion

        public class ConnectionInformation
        {
            public string ConnectionName { get; set; }
            public string EnvironmentId { get; set; }
            public string ServerName { get; set; }
            public string OrganizationUniqueName { get; set; }
            public string OrganizationFriendlyName { get; set; }
            public string OrganizationVersion { get; set; }
            public int OrganizationMinorVersion { get; set; }
            public int OrganizationMajorVersion { get; set; }
        }
    }
}
