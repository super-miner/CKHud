using CKHud.Common.Config;
using CKHud.Config;
using CKHud.HarmonyPatches;

namespace CKHud.HudComponents {
    public class MaxDPSHudComponent : HudComponent {
	    private static readonly float DEFAULT_REFRESH_RATE = 0.25f;

	    public static ConfigFloat refreshRate = null;
	    
	    public override void InitConfigs() {
		    refreshRate = ConfigSystem.AddFloat(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "RefreshRate", 1.0f);
	    }
	    
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > refreshRate.GetValue();
	    }
	    
        public override string CreateString() {
            if (ConfigManager.instance.compactMode.GetValue()) {
                return $"Max DPS: {DPSPatches.maxDPS}";
            }
            else {
                return $"Max: {DPSPatches.maxDPS}";
            }
        }
    }
}