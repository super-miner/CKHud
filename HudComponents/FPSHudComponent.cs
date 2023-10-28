using UnityEngine;

namespace CKHud.HudComponents {
    public class FPSHudComponent : HudComponent {
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > 1.0f;
	    }
	    
        public override string CreateString() {
            return "FPS: " + (1 / Time.deltaTime).ToString("N0");
        }
    }
}
