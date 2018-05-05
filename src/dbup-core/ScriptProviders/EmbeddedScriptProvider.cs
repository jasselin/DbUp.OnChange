using System;
using System.Reflection;
using System.Text;
using DbUp.Engine;

namespace DbUp.ScriptProviders
{
    /// <summary>
    /// The default <see cref="IScriptProvider"/> implementation which retrieves upgrade scripts embedded in an assembly.
    /// </summary>
    public class EmbeddedScriptProvider : EmbeddedScriptsProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedScriptProvider"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="scriptOptions">Script options</param>
        public EmbeddedScriptProvider(Assembly assembly, Func<string, bool> filter, ScriptOptions scriptOptions) : this(assembly, filter, DbUpDefaults.DefaultEncoding, scriptOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedScriptProvider"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="filter">The filter.</param>
        public EmbeddedScriptProvider(Assembly assembly, Func<string, bool> filter) : this(assembly, filter, Encoding.UTF8, new ScriptOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedScriptProvider"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="encoding">The encoding.</param>
        public EmbeddedScriptProvider(Assembly assembly, Func<string, bool> filter, Encoding encoding) : base(new[] { assembly }, filter, encoding, new ScriptOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedScriptProvider"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="scriptOptions">Script options</param>
        public EmbeddedScriptProvider(Assembly assembly, Func<string, bool> filter, Encoding encoding, ScriptOptions scriptOptions) : base(new[] { assembly }, filter, encoding, scriptOptions)
        {
        }
    }
}