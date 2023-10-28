using CKHud.Config;
using CKHud.HarmonyPatches;

namespace CKHud.HudComponents {
    public class MaxDPSHudComponent : HudComponent {
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > 0.25f;
	    }
	    
        public override string CreateString() {
            DPSPatches.GetDPS(1.0f, 1.5f, 2.0f); // Makes sure the maxDPS is updated
            if (ConfigManager.instance.compactMode.GetValue()) {
                return "Max DPS: " + DPSPatches.maxDPS;
            }
            else {
                return "Max: " + DPSPatches.maxDPS;
            }
        }
    }
}