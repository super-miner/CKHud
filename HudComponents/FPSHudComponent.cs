using CKHud.Common.Config;
using UnityEngine;

namespace CKHud.HudComponents {
    public class FPSHudComponent : HudComponent {
	    private static readonly float DEFAULT_REFRESH_RATE = 1.0f;

	    public static ConfigFloat refreshRate = null;
	    
	    public override void InitConfigs() {
		    refreshRate = ConfigSystem.AddFloat(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "RefreshRate", 1.0f);
	    }
	    
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > refreshRate.GetValue();
	    }
	    
        public override string CreateString() {
	        float FPS = 1 / Time.deltaTime;
	        string FPSString = FPS.ToString("N0");
	        
            return $"FPS: {FPSString}";
        }
    }
}
