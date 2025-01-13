using RioCanada.Crm.ComponentExportComparer.Core.Models;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RioCanada.Crm.ComponentExportComparer.Core.Utilities
{
    static class Helper
    {
        public static string WildcardToLikeQuery(string str)
        {
            return str.Replace("_", "[_]").Replace("*", "%").Replace("?", "_");
        }

        public static bool IsContainsWildcard(string str)
        {
            return str.Contains("*") || str.Contains("?");
        }

        public static FilterExpression GetFilterByPattern(IEnumerable<string> patterns, string attributeName)
        {
            var filter = new FilterExpression
            {
                FilterOperator = LogicalOperator.Or
            };
            foreach (var pattern in patterns)
            {
                if (IsContainsWildcard(pattern))
                {
                    filter.AddCondition(attributeName, ConditionOperator.Like, WildcardToLikeQuery(pattern));
                }
                else
                {
                    filter.AddCondition(attributeName, ConditionOperator.Equal, pattern);
                }
            }

            return filter;
        }
        public static void ApplyPatternFilter(QueryExpression query, string attributeName, IEnumerable<string> patterns)
        {
            if (patterns.Where(x => x == "*").Count() == 0)
            {
                var filter = GetFilterByPattern(patterns, attributeName);
                query.Criteria.AddFilter(filter);
            }
        }

        public static void ApplySolutionFilter(QueryExpression query, string primaryAttributeName, IEnumerable<Guid> solutionIds)
        {
            if (solutionIds != null && solutionIds.Count() > 0)
            {
                var linkEntity = query.AddLink(SolutionComponent.EntityLogicalName, primaryAttributeName, "objectid");
                var condition = new ConditionExpression("solutionid", ConditionOperator.In);
                linkEntity.LinkCriteria.AddCondition(condition);
                solutionIds.ToList().ForEach(x => condition.Values.Add(x));
            }
        }

        public static bool IsWildcardStringMatch(string str, string pattern)
        {
            if (pattern == "*")
            {
                return true;
            }

            if (pattern.IndexOf("*") == -1)
            {
                return string.Compare(str, pattern, true) == 0;
            }

            var regex = new Regex("^" + pattern.Replace("*", ".*") + "$", RegexOptions.IgnoreCase);

            return regex.IsMatch(str);

            //var startWithMatch = regexStartsWith.Match(str);

            //if (startWithMatch.Success)
            //{
            //    return str.StartsWith(startWithMatch.Groups[1].Value);
            //}

            //var endWithMatch = regexEndsWith.Match(str);

            //if (endWithMatch.Success)
            //{
            //    return str.EndsWith(endWithMatch.Groups[1].Value);
            //}
        }

        public static bool IsWildcardStringMatch(string str, IEnumerable<string> patterns)
        {
            foreach (var pattern in patterns)
            {
                if (IsWildcardStringMatch(str, pattern)) return true;
            }

            return false;
        }
    }
}
