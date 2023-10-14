using UnityEngine;

namespace CKHud.HudComponents {
    public class FPSHudComponent : HudComponent {
        public override string GetString() {
            return "FPS: " + (1 / Time.deltaTime).ToString("N0");
        }
    }
}
