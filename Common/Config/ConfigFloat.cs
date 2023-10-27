namespace CKHud.Common.Config {
    public class ConfigFloat : ConfigVariable {
        /// <summary>
        /// The constructor for the ConfigVariable class.
        /// </summary>
        /// <param name="modId">The id of your mod.</param>
        /// <param name="section">The section that the config goes in.</param>
        /// <param name="key">The name of the config variable.</param>
        /// <param name="defaultValue">The default value to be used if the config file does not exist.</param>
        public ConfigFloat(string modId, string section, string key, object defaultValue) : base(modId, section, key, defaultValue) {
            
        }
        
        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public new float GetValue(bool forceFetch = false) {
            return GetValue(out bool success, forceFetch);
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="success">Whether the config variable already existed or not (true if already existed).</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public new float GetValue(out bool success, bool forceFetch = false) {
            object valueObject = base.GetValue(out success, forceFetch);

            if (valueObject is float) {
                return (float)valueObject;
            }
            else if (valueObject is string valueString) {
                if (float.TryParse(valueString, out float valueFloat)) {
                    value = valueFloat;
                    return valueFloat;
                }
                else {
                    LogSystem.instance.Log("Could not parse value for config variable " + key + "-" + value + ".");
                }
            }
            else {
                LogSystem.instance.Log("Value for config variable " + key + "-" + value + " is not recognized. Expected float/string.");
            }
            
            success = false;
            return 0.0f;
        }
    }
}