using System;
using System.Collections.Generic;
using CKHud.Common;
using CKHud.Common.Config;
using CKHud.HudComponents;
using UnityEngine;

namespace CKHud.Config {
    public class ConfigManager {
	    public static ConfigManager instance = null;
	    
	    public readonly int CONFIG_VERSION = 3;
	    
	    private readonly string DEFAULT_COMPONENTS_LAYOUT = "FPS;Position;CenterDistance;DPS,MaxDPS;LocalComputerTime";
	    private readonly bool DEFAULT_HUD_ENABLED = true;
	    private readonly float DEFAULT_START_HUD_POSITION = 4.25f;
	    private readonly float DEFAULT_HUD_LINE_STEP = -0.75f;
	    private readonly int DEFAULT_HUD_ROWS_AMOUNT = 12;
	    private readonly Color DEFAULT_TEXT_COLOR = Color.white;
	    private readonly bool DEFAULT_COMPACT_MODE = false;

	    public ConfigInt configVersion = null;

	    public ConfigComponentsLayout componentsLayout = null;
	    public ConfigBool hudEnabled = null;
	    public ConfigFloat startHudPosition = null;
	    public ConfigFloat hudLineStep = null;
	    public ConfigInt hudRowsAmount = null;
	    public ConfigColor textColor = null;
	    public ConfigBool compactMode = null;

	    public ConfigManager() {
		    configVersion = ConfigSystem.AddInt("ConfigVersion", "DoNotEdit", CONFIG_VERSION);

		    if (configVersion.GetValue() < CONFIG_VERSION) {
			    UpdateConfig();
		    }
		    
		    componentsLayout = (ConfigComponentsLayout) ConfigSystem.AddVariable(new ConfigComponentsLayout(CKHudMod.MOD_ID, "Components", "Layout", DEFAULT_COMPONENTS_LAYOUT));
		    hudEnabled = ConfigSystem.AddBool("General", "HudEnabled", DEFAULT_HUD_ENABLED);
		    startHudPosition = ConfigSystem.AddFloat("General", "HudStart", DEFAULT_START_HUD_POSITION);
		    hudLineStep = ConfigSystem.AddFloat("General", "LineSpacing", DEFAULT_HUD_LINE_STEP);
		    hudRowsAmount = ConfigSystem.AddInt("General", "NumRows", DEFAULT_HUD_ROWS_AMOUNT);
		    textColor = ConfigSystem.AddColor("General", "HudEnabled", DEFAULT_TEXT_COLOR);
		    compactMode = ConfigSystem.AddBool("General", "CompactMode", DEFAULT_COMPACT_MODE);
		    
		    LogSystem.Log("Loaded config manager.");
	    }

	    public static void Create() {
		    instance = instance ?? new ConfigManager();
	    }

		private void UpdateConfig() {
			LogSystem.Log("Updating configs, do not close the game!");
			
			if (configVersion.GetValue() < 2) {
				List<HudRow> hudRows = componentsLayout.GetValue();
				
				HudRow hudRow = new HudRow();
				hudRow.components.Add(new LocalComputerTimeHudComponent());
				
				hudRows.Add(hudRow);
				
				componentsLayout.SetValue(hudRows);
				
				configVersion.SetValue(2);
			}
			
			if (configVersion.GetValue() < 3) {
				List<HudRow> hudRows = componentsLayout.GetValue();

				foreach (HudRow hudRow in hudRows) {
					for (int i = 0; i < hudRow.components.Count; i++) {
						HudComponent hudComponent = hudRow.components[i];
						
						if (hudComponent is DPSHudComponent) {
							hudRow.components.Insert(i + 1, new MaxDPSHudComponent());
						}
					}
				}
				
				componentsLayout.SetValue(hudRows);

				configVersion.SetValue(2);
			}
			
			LogSystem.Log("Finished updating configs.");
		}
    }
}