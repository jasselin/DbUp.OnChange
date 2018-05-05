using System.Collections.Generic;
using System.Linq;
using DbUp.Engine.Filters;
using DbUp.Helpers;
using DbUp.Support;

namespace DbUp.Engine
{
    public interface IScriptFilter
    {
        IEnumerable<SqlScript> Filter(IEnumerable<SqlScript> sorted, HashSet<string> executedScriptNames, ScriptNameComparer comparer);
        IEnumerable<SqlScript> Filter(IOrderedEnumerable<SqlScript> sorted, IEnumerable<ExecutedSqlScript> executedScripts, ScriptNameComparer scriptNameComparer, IHasher hasher);
    }
}