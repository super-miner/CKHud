using UnityEngine;

namespace CKHud.HudComponents {
    public class HudComponent {
	    public string cachedString = "";
	    public float lastUpdateTime = float.MinValue;
	    
        public bool GetString(out string newText) {
	        if (ShouldRegenerateString()) {
		        cachedString = CreateString();
		        newText = cachedString;
		        return true;
	        }

	        newText = cachedString;
	        return false;
        }

        public virtual bool ShouldRegenerateString() {
	        return true;
        }

        public virtual string CreateString() {
	        lastUpdateTime = Time.time;

	        return "";
        }

        protected float TimeSinceLastUpdate() {
	        return Time.time - lastUpdateTime;
        }
    }
}
