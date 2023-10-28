using UnityEngine;

namespace CKHud.Common.Config {
    public class ConfigColor : ConfigVariable {
        /// <summary>
        /// The constructor for the ConfigVariable class.
        /// </summary>
        /// <param name="modId">The id of your mod.</param>
        /// <param name="section">The section that the config goes in.</param>
        /// <param name="key">The name of the config variable.</param>
        /// <param name="defaultValue">The default value to be used if the config file does not exist.</param>
        public ConfigColor(string modId, string section, string key, object defaultValue) : base(modId, section, key, defaultValue) {
            
        }
        
        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public new Color GetValue(bool forceFetch = false) {
            return GetValue(out bool success, forceFetch);
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="success">Whether the config variable already existed or not (true if already existed).</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public new Color GetValue(out bool success, bool forceFetch = false) {
            object valueObject = base.GetValue(out success, forceFetch);

            if (valueObject is string valueString) {
                string[] splitValueStrings = valueString.Split(" ");

                if (splitValueStrings.Length == 4) {
                    float[] valueFloats = new float[4];

                    for (int i = 0; i < 4; i++) {
                        if (!float.TryParse(splitValueStrings[i], out valueFloats[i])) {
                            LogSystem.Log($"Could not parse value {i} for config variable {section}-{key}.");
                            
                            success = false;
                            return Color.black;
                        }
                    }

                    return new Color(valueFloats[0], valueFloats[1], valueFloats[2], valueFloats[3]);
                }
                else {
                    LogSystem.Log($"Improper number of arguments for config variable {section}-{key}. Expected 4.");
                }
            }
            else {
                LogSystem.Log($"Value for config variable {section}-{key} is not recognized. Expected string.");
            }
            
            success = false;
            return Color.black;
        }
    }
}