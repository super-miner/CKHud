using PugMod;

namespace CKHud.Common.Config {
    /// <summary>
    /// Manages a single config variable.
    /// </summary>
    public class ConfigVariable {
        public readonly string modId = "";
        public readonly string section = "";
        public readonly string key = "";
        public string value = null;
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
            
            CKHudMod.logger.LogInfo($"Created the template for the config value {section}-{key} with a default value of {ValueToString(defaultValue)}.");
            
            InitConfig();
        }

        public void InitConfig() {
	        value = GetValue(out bool foundValue);

	        if (foundValue) {
		        CKHudMod.logger.LogInfo($"Found value {value} for {modId}: {section}-{key}.");
	        }
	        else {
		        CKHudMod.logger.LogWarning($"Could not find value for {section}-{key}, initialized with {ValueToString(defaultValue)}.");
	        }
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public string GetValue(bool forceFetch = false) {
            return GetValue(out bool success, forceFetch);
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="success">Whether the config variable already existed or not (true if already existed).</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public string GetValue(out bool success, bool forceFetch = false) {
            if (forceFetch || value == null) {
                if (API.Config.TryGet(modId, section, key, out string output)) {
                    success = true;
                    return output;
                }
                else {
                    SetValue(defaultValue);
                    success = false;
                    return ValueToString(defaultValue);
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
        /// <param name="_value">The value to set the variable to.</param>
        public void SetValue(object _value) {
            value = ValueToString(_value);
            
            API.Config.Set(modId, section, key, value);
        }

        /// <summary>
        /// Converts the value in the normal data type to a string.
        /// </summary>
        /// <param name="_value">The value to convert</param>
        /// <returns>The converted string.</returns>
        public virtual string ValueToString(object _value) {
	        return _value.ToString();
        }
    }
}