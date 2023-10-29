using CKHud.Common.Config;
using CKHud.HarmonyPatches;

namespace CKHud.HudComponents {
    public class DPSHudComponent : HudComponent {
	    private static readonly float DEFAULT_TIME_FRAME = 1.0f;
	    private static readonly float DEFAULT_SMOOTHING_COEFFICIENT = 1.5f;
	    private static readonly float DEFAULT_HOLD_TIME = 2.0f;

	    public static ConfigFloat timeFrame = null;
	    public static ConfigFloat smoothingCoefficient = null;
	    public static ConfigFloat holdTime = null;
	    
	    public override void InitConfigs() {
		    timeFrame = ConfigSystem.AddFloat(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "TimeFrame", DEFAULT_TIME_FRAME);
		    smoothingCoefficient = ConfigSystem.AddFloat(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "SmoothingCoefficient", DEFAULT_SMOOTHING_COEFFICIENT);
		    holdTime = ConfigSystem.AddFloat(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "HoldTime", DEFAULT_HOLD_TIME);
	    }
	    
        public override string CreateString() {
	        int DPS = DPSPatches.GetDPS(timeFrame.GetValue(), smoothingCoefficient.GetValue(), holdTime.GetValue());
	        
            return $"DPS: {DPS}";
        }
    }
}