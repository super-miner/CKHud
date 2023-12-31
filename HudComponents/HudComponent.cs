using UnityEngine;

namespace CKHud.HudComponents {
    public class HudComponent {
	    public string cachedString = "";
	    public float lastUpdateTime = float.MinValue;

	    public virtual void InitConfigs() {
		    
	    }
	    
        public bool GetString(out string output) {
	        if (ShouldRegenerateString()) {
		        cachedString = CreateString();
		        output = cachedString;
		        
		        lastUpdateTime = Time.time;
		        return true;
	        }

	        output = cachedString;
	        return false;
        }

        public virtual bool ShouldRegenerateString() {
	        return true;
        }

        public virtual string CreateString() {
	        return "";
        }

        protected float TimeSinceLastUpdate() {
	        return Time.time - lastUpdateTime;
        }
    }
}
