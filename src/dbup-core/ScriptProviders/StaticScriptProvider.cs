using System;
using System.Collections.Generic;
using DbUp.Engine;
using DbUp.Engine.Transactions;

namespace DbUp.ScriptProviders
{
    /// <summary>
    /// Allows you to easily programatically supply scripts from code.
    /// </summary>
    public sealed class StaticScriptProvider : IScriptProvider
    {
        private readonly IEnumerable<SqlScript> scripts;

        /// <summary>
        /// Script options
        /// </summary>
        public ScriptOptions ScriptOptions { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticScriptProvider"/> class.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        /// <param name="scriptOptions">Script options</param>
        public StaticScriptProvider(IEnumerable<SqlScript> scripts, ScriptOptions scriptOptions)
        {
            this.scripts = scripts;

            ScriptOptions = scriptOptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticScriptProvider"/> class.
        /// </summary>
        /// <param name="scripts">The scripts.</param>
        public StaticScriptProvider(IEnumerable<SqlScript> scripts) : this(scripts, new ScriptOptions())
        {
        }

        /// <summary>
        /// Gets all scripts that should be executed.
        /// </summary>
        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            return scripts;
        }
    }
}