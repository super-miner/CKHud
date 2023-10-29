using UnityEngine.Device;

namespace CKHud.HudComponents {
    public class FPSCapHudComponent : HudComponent {
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > 1.0f;
	    }
	    
        public override string CreateString() {
            return $"FPS Cap: {Application.targetFrameRate}";
        }
    }
}