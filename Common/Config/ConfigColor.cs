using PugMod;
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
            string valueString = base.GetValue(out success, forceFetch);

            if (valueString != null) {
                string[] splitValueStrings = valueString.Split(" ");

                if (splitValueStrings.Length == 4) {
                    float[] valueFloats = new float[4];

                    for (int i = 0; i < 4; i++) {
                        if (!float.TryParse(splitValueStrings[i], out valueFloats[i])) {
	                        CKHudMod.logger.LogError($"Could not parse value {i} for config variable {section}-{key}.");
                            
                            success = false;
                            return Color.black;
                        }
                    }

                    return new Color(valueFloats[0], valueFloats[1], valueFloats[2], valueFloats[3]);
                }
                else {
	                CKHudMod.logger.LogError($"Improper number of arguments for config variable {section}-{key}. Expected 4.");
                }
            }
            else {
	            CKHudMod.logger.LogError($"Value for config variable {section}-{key} is null.");
            }
            
            success = false;
            return Color.black;
        }
        
        /// <summary>
        /// Sets the value associated the this config variable.
        /// </summary>
        /// <param name="_value">The value to set the variable to.</param>
        public void SetValue(Color _value) {
	        this.value = ValueToString(_value);
	        
	        API.Config.Set(modId, section, key, this.value);
        }
        
        /// <summary>
        /// Converts the value in the normal data type to a string.
        /// </summary>
        /// <param name="_value">The value to convert</param>
        /// <returns>The converted string.</returns>
        public override string ValueToString(object _value) {
	        if (_value is Color colorValue) {
		        return $"{colorValue.r} {colorValue.g} {colorValue.b} {colorValue.a}";
	        }
	        
	        return _value.ToString();
        }
    }
}