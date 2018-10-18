using System;
using System.Collections.Generic;
using System.Linq;
using DbUp.Engine;
using DbUp.Engine.Transactions;

namespace DbUp.ScriptProviders
{
    public class ScriptInstanceProvider : IScriptProvider
    {
        private readonly IScript[] scripts;
        private readonly Func<IScript, string> namer;

        /// <summary>
        /// Script options
        /// </summary>
        public ScriptOptions ScriptOptions { get; private set; }

        /// <summary>
        /// Provider used to directly include an IScript instance during migrations
        /// </summary>
        /// <param name="scripts">The IScript instances to include</param>
        public ScriptInstanceProvider(params IScript[] scripts)
            : this(s => s.GetType().FullName + ".cs", scripts)
        {
        }

        /// <summary>
        /// Provider used to directly include an IScript instance during migrations
        /// </summary>
        /// <param name="scripts">The IScript instances to include</param>
        /// <param name="namer">A function that returns the name of the script</param>
        public ScriptInstanceProvider(Func<IScript, string> namer, params IScript[] scripts)
        {
            this.scripts = scripts;
            this.namer = namer;
            this.ScriptOptions = new ScriptOptions();
        }

        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            return connectionManager.ExecuteCommandsWithManagedConnection(
                dbCommandFactory => scripts
                    .Select(s => new LazySqlScript(namer(s), () => s.ProvideScript(dbCommandFactory)))
                    .ToArray()
            );
        }
    }
}