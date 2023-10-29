using System;

namespace CKHud.HudComponents {
    public class LocalComputerTimeHudComponent : HudComponent {
	    public override bool ShouldRegenerateString() {
		    return TimeSinceLastUpdate() > 5.0f;
	    }
	    
        public override string CreateString() {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
