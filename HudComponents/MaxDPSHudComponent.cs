using CKHud.Common.Config;
using CKHud.Config;
using CKHud.HarmonyPatches;

namespace CKHud.HudComponents {
    public class MaxDPSHudComponent : HudComponent {
	    private static readonly float DEFAULT_REFRESH_RATE = 0.25f;

	    public static ConfigFloat refreshRate = null;
	    
	    public override void InitConfigs() {
		    refreshRate = ConfigSystem.AddFloat(HudComponentsRegistry.GetHudComponentByType(typeof(FPSHudComponent)), "RefreshRate", 1.0f);
	    }
	    
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > refreshRate.GetValue();
	    }
	    
        public override string CreateString() {
            //DPSPatches.GetDPS(1.0f, 1.5f, 2.0f); // Makes sure the maxDPS is updated TODO: Look at this
            if (ConfigManager.instance.compactMode.GetValue()) {
                return "Max DPS: " + DPSPatches.maxDPS;
            }
            else {
                return "Max: " + DPSPatches.maxDPS;
            }
        }
    }
}