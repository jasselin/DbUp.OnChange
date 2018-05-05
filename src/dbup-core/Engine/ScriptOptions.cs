using System;

namespace DbUp.Engine
{
    /// <summary>
    /// Configuration options for scripts
    /// </summary>
    public class ScriptOptions
    {
        /// <summary>
        /// Gets or sets whether the scripts should be redeployed on content change
        /// </summary>
        public bool RedeployOnChange { get; set; }

        /// <summary>
        /// Gets or sets whether the first deploy in redeploy mode should be used as a starting point. If this is set to true, the script won't be deployed
        /// into the database, they will just be marked as processed. Used when activating DbUp for an existing database
        /// </summary>
        public bool FirstDeploymentAsStartingPoint { get; set; }

        /// <summary>
        /// Path to a file containing script dependencies
        /// For example by providing a dependencies.txt with contents:
        /// 
        /// -- start of dependencies.txt file
        /// first.sql
        /// second.sql
        /// -- end of dependencies.txt file
        /// 
        /// Means that first.sql should be executed before second.sql. 
        /// 
        /// A usage scenario is when having nested SQL views and the parent view references a child view which is not available at that time.
        /// With this approach it can be ensured that the child view is created before the parent
        /// 
        /// </summary>
        public string DependencyOrderFilePath { get; set; }

        /// <summary>
        /// Gets or sets whether subdirectory name should be included in the name of the script
        /// </summary>
        public bool IncludeSubDirectoryInName { get; set; }
    }
}
