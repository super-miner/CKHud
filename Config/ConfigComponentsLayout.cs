using System.Collections.Generic;
using CKHud.Common;
using CKHud.Common.Config;
using CKHud.HudComponents;
using PugMod;

namespace CKHud.Config {
    public class ConfigComponentsLayout : ConfigVariable {
        /// <summary>
        /// The constructor for the ConfigVariable class.
        /// </summary>
        /// <param name="modId">The id of your mod.</param>
        /// <param name="section">The section that the config goes in.</param>
        /// <param name="key">The name of the config variable.</param>
        /// <param name="defaultValue">The default value to be used if the config file does not exist.</param>
        public ConfigComponentsLayout(string modId, string section, string key, object defaultValue) : base(modId, section, key, defaultValue) {
            
        }
        
        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public new List<HudRow> GetValue(bool forceFetch = false) {
            return GetValue(out bool success, forceFetch);
        }

        /// <summary>
        /// Gets the value associated with this config variable.
        /// </summary>
        /// <param name="success">Whether the config variable already existed or not (true if already existed).</param>
        /// <param name="forceFetch">Forces the function to re-read to config file instead of using cached values.</param>
        /// <returns>The value found or the default value if none found.</returns>
        public new List<HudRow> GetValue(out bool success, bool forceFetch = false) {
            string valueString = base.GetValue(out success, forceFetch);

            if (valueString != null) {
                string[] rowStrings = valueString.Split(";");

                if (rowStrings.Length > 0) {
                    List<HudRow> hudRows = new List<HudRow>();

                    foreach (string rowString in rowStrings) {
	                    HudRow hudRow = new HudRow();
	                    
	                    string[] componentStrings = rowString.Split(",");
	                    if (componentStrings.Length > 0) {
		                    foreach (string componentString in componentStrings) {
			                    HudComponent hudComponent = HudComponentsRegistry.GetHudComponentByName(componentString);

			                    if (hudComponent != null) {
				                    hudRow.components.Add(hudComponent);
			                    }
		                    }
	                    }
	                    
	                    hudRows.Add(hudRow);
                    }

                    return hudRows;
                }
                else {
	                CKHudMod.logger.LogError($"The {section}-{key} value is empty or in the incorrect format");
                }
            }
            else {
	            CKHudMod.logger.LogError($"Value for config variable {section}-{key} is null.");
            }
            
            success = false;
            return new List<HudRow>();
        }
        
        /// <summary>
        /// Sets the value associated the this config variable.
        /// </summary>
        /// <param name="value">The value to set the variable to.</param>
        public void SetValue(List<HudRow> value) {
	        this.value = ValueToString(value);
	        
	        API.Config.Set(modId, section, key, this.value);
        }
        
        /// <summary>
        /// Converts the value in the normal data type to a string.
        /// </summary>
        /// <param name="_value">The value to convert</param>
        /// <returns>The converted string.</returns>
        public override string ValueToString(object _value) {
	        if (_value is List<HudRow>) {
		        List<HudRow> valueHudRows = (List<HudRow>)_value;
		        
		        string valueString = "";
		        
		        for (int i = 0; i < valueHudRows.Count; i++) {
			        HudRow hudRow = valueHudRows[i];
		        
			        if (i > 0) {
				        valueString += ";";
			        }

			        for (int j = 0; j < hudRow.components.Count; j++) {
				        HudComponent hudComponent = hudRow.components[j];

				        if (j > 0) {
					        valueString += ",";
				        }

				        valueString += HudComponentsRegistry.GetHudComponentByType(hudComponent.GetType());
			        }
		        }

		        return valueString;
	        }
	        
	        return _value.ToString();
        }
    }
}