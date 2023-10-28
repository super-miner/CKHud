using CKHud.HarmonyPatches;

namespace CKHud.HudComponents {
    public class DPSHudComponent : HudComponent {
        public override string CreateString() {
            return "DPS: " + DPSPatches.GetDPS(1.0f, 1.5f, 2.0f);
        }
    }
}