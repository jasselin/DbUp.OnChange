using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DbUp.Engine;
using DbUp.Engine.Transactions;

namespace DbUp.ScriptProviders
{
    ///<summary>
    /// Alternate <see cref="IScriptProvider"/> implementation which retrieves upgrade scripts via a directory
    ///</summary>
    public class FileSystemScriptProvider : IScriptProvider
    {
        private readonly string directoryPath;
        private readonly Func<string, bool> filter;
        private readonly Encoding encoding;
        private FileSystemScriptOptions options;

        /// <summary>
        /// Script options
        /// </summary>
        public ScriptOptions ScriptOptions { get; private set; } 

        ///<summary>
        ///</summary>
        ///<param name="directoryPath">Path to SQL upgrade scripts</param>
        public FileSystemScriptProvider(string directoryPath):this(directoryPath, new FileSystemScriptOptions(), new ScriptOptions())
        {
        }

        ///<summary>
        ///</summary>
        ///<param name="directoryPath">Path to SQL upgrade scripts</param>
        ///<param name="options">Different options for the file system script provider</param>
        public FileSystemScriptProvider(string directoryPath, FileSystemScriptOptions options, ScriptOptions scriptOptions)
        {
            if (options==null)
                throw new ArgumentNullException("options");
            this.directoryPath = directoryPath;
            this.filter = options.Filter;
            this.encoding = options.Encoding;
            this.options = options;

            ScriptOptions = scriptOptions;
        }

        /// <summary>
        /// Gets all scripts that should be executed.
        /// </summary>
        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            var files = Directory.GetFiles(directoryPath, "*.sql", ShouldSearchSubDirectories()).AsEnumerable();
            if (filter != null)
            {
                files = files.Where(filter);
            }

            return files.Select(x => {

                // if subdirectory name is needed, remove root folder from file path to obtain the subdirectory name and file name
                var scriptName = ScriptOptions.IncludeSubDirectoryInName ? new Regex(Regex.Escape(directoryPath)).Replace(x, string.Empty, 1).TrimStart('\\') : null;

                return SqlScript.FromFile(x, encoding, scriptName);
            }).OrderBy(x => x.Name).ToList();
        }

        private SearchOption ShouldSearchSubDirectories()
        {
            return options.IncludeSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        }
    }
}
