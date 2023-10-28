using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CKHud.Common.Config {
    /// <summary>
    /// A system for managing config files.
    /// </summary>
    public static class ConfigSystem {
        public static string modId = "";
        public static readonly List<ConfigVariable> configVariables = new List<ConfigVariable>();

        /// <summary>
        /// The initialization function for the ConfigSystem class.
        /// </summary>
        /// <param name="_modId">The id of your mod.</param>
        public static void Init(string _modId) {
            modId = _modId;
        }

        /// <summary>
        /// Creates a config variable.
        /// </summary>
        /// <param name="variable">The config variable to add.</param>
        public static ConfigVariable AddVariable(ConfigVariable variable) {
            configVariables.Add(variable);

            return variable;
        }
        
        /// <summary>
        /// Creates a config float.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public static ConfigFloat AddFloat(string section, string key, float defaultValue) {
	        if (modId == null) {
		        LogSystem.Log("Trying to create a config variable before the ConfigSystem was initialized.");
		        return null;
	        }
	        
            ConfigFloat variable = new ConfigFloat(modId, section, key, defaultValue);
            
            configVariables.Add(variable);
            return variable;
        }
        
        /// <summary>
        /// Creates a config int.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public static ConfigInt AddInt(string section, string key, int defaultValue) {
	        if (modId == null) {
		        LogSystem.Log("Trying to create a config variable before the ConfigSystem was initialized.");
		        return null;
	        }
	        
            ConfigInt variable = new ConfigInt(modId, section, key, defaultValue);
            
            configVariables.Add(variable);
            return variable;
        }
        
        /// <summary>
        /// Creates a config bool.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public static ConfigBool AddBool(string section, string key, bool defaultValue) {
	        if (modId == null) {
		        LogSystem.Log("Trying to create a config variable before the ConfigSystem was initialized.");
		        return null;
	        }
	        
            ConfigBool variable = new ConfigBool(modId, section, key, defaultValue);
            
            configVariables.Add(variable);
            return variable;
        }
        
        /// <summary>
        /// Creates a config string.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public static ConfigString AddString(string section, string key, string defaultValue) {
	        if (modId == null) {
		        LogSystem.Log("Trying to create a config variable before the ConfigSystem was initialized.");
		        return null;
	        }
	        
            ConfigString variable = new ConfigString(modId, section, key, defaultValue);
            
            configVariables.Add(variable);
            return variable;
        }
        
        /// <summary>
        /// Creates a config color.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public static ConfigColor AddColor(string section, string key, Color defaultValue) {
	        if (modId == null) {
		        LogSystem.Log("Trying to create a config variable before the ConfigSystem was initialized.");
		        return null;
	        }
	        
            ConfigColor variable = new ConfigColor(modId, section, key, defaultValue);
            
            configVariables.Add(variable);
            return variable;
        }

        /// <summary>
        /// Gets a config variable by section and key.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <returns>The config variable.</returns>
        public static ConfigVariable GetVariable(string section, string key) {
            return configVariables.FirstOrDefault(configVariable => configVariable.section == section && configVariable.key == key);
        }
    }
}