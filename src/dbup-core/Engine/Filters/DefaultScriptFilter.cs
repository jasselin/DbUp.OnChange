using System;
using System.Collections.Generic;
using System.Linq;
using DbUp.Helpers;
using DbUp.Support;

namespace DbUp.Engine.Filters
{
    public class DefaultScriptFilter : IScriptFilter
    {
        public IEnumerable<SqlScript> Filter(IEnumerable<SqlScript> sorted, HashSet<string> executedScriptNames, ScriptNameComparer comparer)
             =>  sorted.Where(s => !executedScriptNames.Contains(s.Name, comparer));

        public IEnumerable<SqlScript> Filter
        (
            IOrderedEnumerable<SqlScript> sorted, 
            IEnumerable<ExecutedSqlScript> executedScripts, 
            ScriptNameComparer scriptNameComparer,
            IHasher hasher
        )
        {
            // check if script has been already executed based on name and hash
            return sorted.Where(s => !executedScripts.Any(y => scriptNameComparer.Equals(y.Name, s.Name) && (y.Hash == null || y.Hash == hasher.GetHash(s.Contents))));
        }
    }
}