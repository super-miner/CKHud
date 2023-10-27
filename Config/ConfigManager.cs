using CKHud.Common;
using CKHud.Common.Config;
using CKHud.HudComponents;
using UnityEngine;

namespace CKHud.Config {
    public class ConfigManager {
	    public static ConfigManager instance = null;
	    
	    public readonly int CONFIG_VERSION = 3;
	    
	    private readonly string DEFAULT_COMPONENTS = "FPS;Position;CenterDistance;DPS,MaxDPS;LocalComputerTime";
	    private readonly bool DEFAULT_HUD_ENABLED = true;
	    private readonly float DEFAULT_START_HUD_POSITION = 4.25f;
	    private readonly float DEFAULT_HUD_LINE_STEP = -0.75f;
	    private readonly int DEFAULT_HUD_ROWS_AMOUNT = 12;
	    private readonly Color DEFAULT_TEXT_COLOR = Color.white;
	    private readonly bool DEFAULT_COMPACT_MODE = false;

	    public ConfigInt configVersion = null;

	    public ConfigBool hudEnabled = null;
	    public ConfigFloat startHudPosition = null;
	    public ConfigFloat hudLineStep = null;
	    public ConfigInt hudRowsAmount = null;
	    public ConfigColor textColor = null;
	    public ConfigBool compactMode = null;

	    public ConfigManager() {
		    configVersion = ConfigSystem.instance.AddInt("ConfigVersion", "DoNotEdit", CONFIG_VERSION);

		    if (configVersion.GetValue() < CONFIG_VERSION) {
			    UpdateConfig();
		    }
		    
		    hudEnabled = ConfigSystem.instance.AddBool("General", "HudEnabled", DEFAULT_HUD_ENABLED);
		    startHudPosition = ConfigSystem.instance.AddFloat("General", "HudStart", DEFAULT_START_HUD_POSITION);
		    hudLineStep = ConfigSystem.instance.AddFloat("General", "LineSpacing", DEFAULT_HUD_LINE_STEP);
		    hudRowsAmount = ConfigSystem.instance.AddInt("General", "NumRows", DEFAULT_HUD_ROWS_AMOUNT);
		    textColor = ConfigSystem.instance.AddColor("General", "HudEnabled", DEFAULT_TEXT_COLOR);
		    compactMode = ConfigSystem.instance.AddBool("General", "CompactMode", DEFAULT_COMPACT_MODE);
		    
		    LogSystem.instance.Log("Loaded config manager.");
	    }
	    
	    public static void Create() {
		    if (ConfigSystem.instance == null) {
			    LogSystem.instance.Log("Config Manager created before the config system.");
			    return;
		    }
		    
		    instance = instance ?? new ConfigManager();
	    }

		void LoadLayoutConfig() {
			string componentLayout = "";
			ConfigSystem.GetString("Components", "Layout", ref componentLayout, DEFAULT_COMPONENTS);
			string[] rowStrings = componentLayout.Replace(" ", "").Split(';');
			int rowsUsed = (int)Mathf.Min(rowStrings.Length, hudRows.Count);
			bool addedComponents = false;
			
			for (int i = 0; i < rowsUsed; i++) {
				string[] componentStrings = rowStrings[i].Split(',');
				
				foreach (string componentString in componentStrings) {
					HudComponent component = HudComponent.Parse(componentString);

					if (component != null) {
						hudRows[i].components.Add(component);
						addedComponents = true;
					}
				}
			}

			if (!addedComponents) {
				CKHudMod.Log("No components were found to add.");
			}
			else {
				CKHudMod.Log("Components added.");
			}
		}

		void UpdateConfig() {
			int configVersion = 999999;
			ConfigSystem.GetInt("ConfigVersion", "DoNotEdit", ref configVersion, CONFIG_VERSION);
			
			if (configVersion < 2) {
				ConfigSystem.SetInt("ConfigVersion", "DoNotEdit", CONFIG_VERSION);
				
				string componentLayout = "";
				ConfigSystem.GetString("Components", "Layout", ref componentLayout, DEFAULT_COMPONENTS);
				ConfigSystem.SetString("Components", "Layout", componentLayout + ";LocalComputerTime");
			}
			
			if (configVersion < 3) {
				ConfigSystem.SetInt("ConfigVersion", "DoNotEdit", CONFIG_VERSION);
				
				string componentLayout = "";
				ConfigSystem.GetString("Components", "Layout", ref componentLayout, DEFAULT_COMPONENTS);
				ConfigSystem.SetString("Components", "Layout", componentLayout.Replace("DPS", "DPS,MaxDPS"));
			}
		}
    }
}