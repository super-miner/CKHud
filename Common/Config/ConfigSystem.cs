using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CKHud.Common.Config {
    /// <summary>
    /// A system for managing config files.
    /// </summary>
    public class ConfigSystem {
        public static ConfigSystem instance = null;

        public readonly string modId = "";
        public readonly List<ConfigVariable> configVariables = new List<ConfigVariable>();

        /// <summary>
        /// The constructor for the ConfigSystem class. Use ConfigSystem.Create instead.
        /// </summary>
        /// <param name="modId">The id of your mod.</param>
        public ConfigSystem(string modId) {
            this.modId = modId;
        }
        
        /// <summary>
        /// Creates the ConfigSystem singleton if none exist.
        /// </summary>
        /// <param name="modId">The id of your mod.</param>
        public static void Create(string modId) {
            instance = instance ?? new ConfigSystem(modId);
        }

        /// <summary>
        /// Creates a config variable.
        /// </summary>
        /// <param name="variable">The config variable to add.</param>
        public void AddVariable(ConfigVariable variable) {
            configVariables.Add(variable);
        }
        
        /// <summary>
        /// Creates a config float.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public ConfigFloat AddFloat(string section, string key, float defaultValue) {
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
        public ConfigInt AddInt(string section, string key, int defaultValue) {
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
        public ConfigBool AddBool(string section, string key, bool defaultValue) {
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
        public ConfigString AddString(string section, string key, string defaultValue) {
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
        public ConfigColor AddColor(string section, string key, Color defaultValue) {
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
        public ConfigVariable GetVariable(string section, string key) {
            return configVariables.FirstOrDefault(configVariable => configVariable.section == section && configVariable.key == key);
        }
    }
}