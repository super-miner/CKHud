using PugMod;

namespace CKHud.Common.Config {
    /// <summary>
    /// Manages a single config variable.
    /// </summary>
    public class ConfigVariable {
        public readonly string modId = "";
        public readonly string section = "";
        public readonly string key = "";
        public object value = null;
        public object defaultValue = null;

        /// <summary>
        /// The constructor for the ConfigVariable class.
        /// </summary>
        /// <param name="modId">The id of your mod.</param>
        /// <param name="section">The section that the config goes in.</param>
        /// <param name="key">The name of the config variable.</param>
        /// <param name="defaultValue">The default value to be used if the config file does not exist.</param>
        public ConfigVariable(string modId, string section, string key, object defaultValue) {
            this.modId = modId;
            this.section = section;
            this.key = key;
            this.defaultValue = defaultValue;
            
            LogSystem.instance.Log("Created the template for the config value " + section + "-" + key + " with a default value of " + defaultValue + ".");

            bool foundValue = false;
            value = GetValue(out foundValue);

            if (foundValue) {
                LogSystem.instance.Log("Found value " + value + " for " + section + "-" + key + ".");
            }
            else {
                LogSystem.instance.Log("Could not find value for " + section + "-" + key + ", initialized with " + this.defaultValue + ".");
            }
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public object GetValue(bool forceFetch = false) {
            return GetValue(out bool success, forceFetch);
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="success">Whether the config variable already existed or not (true if already existed).</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public object GetValue(out bool success, bool forceFetch = false) {
            if (forceFetch || value == null) {
                if (API.Config.TryGet(modId, section, key, out object output)) {
                    success = true;
                    return output;
                }
                else {
                    API.Config.Set(CKHudMod.MOD_ID, section, key, defaultValue);
                    success = false;
                    return defaultValue;
                }
            }
            else {
                success = true;
                return value;
            }
        }

        /// <summary>
        /// Sets the value associated the this config variable.
        /// </summary>
        /// <param name="value">The value to set the variable to.</param>
        public void SetValue(object value) {
            this.value = value;
            
            API.Config.Set(modId, section, key, value);
        }
    }
}