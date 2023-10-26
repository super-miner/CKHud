using System.Collections.Generic;

namespace CKHud.Common {
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
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="defaultValue">The default value for the variable.</param>
        public void CreateVariable(string section, string key, object defaultValue) {
            configVariables.Add(new ConfigVariable(modId, section, key, defaultValue));
        }

        /// <summary>
        /// Gets a config variable by section and key.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <returns>The config variable.</returns>
        public ConfigVariable GetVariable(string section, string key) {
            foreach (ConfigVariable configVariable in configVariables) {
                if (configVariable.section == section && configVariable.key == key) {
                    return configVariable;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the value associated with a config variable specified by section and key.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value associated with the config variable.</returns>
        public object GetValue(string section, string key, bool forceFetch = false) {
            return GetValue(section, key, out bool success, forceFetch);
        }

        /// <summary>
        /// Gets the value associated with a config variable specified by section and key.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="success">Whether the operation was successful.</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value associated with the config variable.</returns>
        public object GetValue(string section, string key, out bool success, bool forceFetch = false) {
            ConfigVariable configVariable = GetVariable(section, key);

            if (configVariable == null) {
                success = false;
                return null;
            }

            return configVariable.GetValue(out success, forceFetch);
        }

        /// <summary>
        /// Sets the value associated with a config variable specified by section and key.
        /// </summary>
        /// <param name="section">The section of the config variable.</param>
        /// <param name="key">The key of the config variable.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(string section, string key, object value) {
            ConfigVariable configVariable = GetVariable(section, key);

            if (configVariable == null) {
                return;
            }

            configVariable.SetValue(value);
        }
    }
}