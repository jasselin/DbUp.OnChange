using System;
using System.Collections.Generic;
using DbUp.Engine.Transactions;

namespace DbUp.Engine
{
    /// <summary>
    /// Provides scripts to be executed.
    /// </summary>
    public interface IScriptProvider
    {
        /// <summary>
        /// Configuration options for scripts
        /// </summary>
        ScriptOptions ScriptOptions { get; }

        /// <summary>
        /// Gets all scripts that should be executed.
        /// </summary>
        IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager);
    }
}