using DbUp.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace DbUp.Helpers
{
    /// <summary>
    /// Orders scripts based on dependencies provided in a file
    /// </summary>
    public class ScriptDependencyOrderer
    {
        /// <summary>
        /// Returns scripts ordered by dependencies provided in a file
        /// </summary>
        /// <param name="scripts">List of scripts</param>
        /// <param name="dependencyOrderFilePath">Path of dependency file</param>
        /// <returns></returns>
        public List<SqlScript> GetScriptsOrderedByDependencies(List<SqlScript> scripts, string dependencyOrderFilePath)
        {
            if (!File.Exists(dependencyOrderFilePath))
            {
                throw new ArgumentException(string.Format("Dependency file {0} doesn't exist", dependencyOrderFilePath));
            }

            var dependencies = File.ReadAllLines(dependencyOrderFilePath).ToList();
            var sortedScripts = new List<SqlScript>();
            var nonDependencyScripts = new List<SqlScript>(scripts);
            var invalidDependencies = new List<string>();

            // add dependency scripts as first elements
            dependencies.ForEach(dep =>
            {
                var script = scripts.Find(e => e.Name == dep);

                if (script == null)
                {
                    invalidDependencies.Add(dep);
                }
                    
                sortedScripts.Add(script);
            });

            if (invalidDependencies.Count > 0)
            {
                throw new InvalidDataException("Dependencies listed in dependency file '" + dependencyOrderFilePath + "' can't be found" + Environment.NewLine + string.Join(Environment.NewLine, invalidDependencies.ToArray()));
            }

            // keep only non-dependency scripts
            nonDependencyScripts.RemoveAll(script => dependencies.Contains(script.Name));

            // add non-dependency scripts to the end
            sortedScripts.AddRange(nonDependencyScripts);

            return sortedScripts;
        }
    }
}
