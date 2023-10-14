using UnityEngine.Device;

namespace CKHud.HudComponents {
    public class FPSCapHudComponent : HudComponent {
        public override string GetString() {
            return "FPS Cap: " + Application.targetFrameRate;
        }
    }
}