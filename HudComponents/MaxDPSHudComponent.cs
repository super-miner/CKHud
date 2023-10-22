using CKHud.HarmonyPatches;

namespace CKHud.HudComponents {
    public class MaxDPSHudComponent : HudComponent {
        public override string GetString() {
            DPSPatches.GetDPS(1.0f, 1.5f, 2.0f); // Makes sure the maxDPS is updated
            if (HudManager.instance.compactMode) {
                return "Max DPS: " + DPSPatches.maxDPS;
            }
            else {
                return "Max: " + DPSPatches.maxDPS;
            }
        }
    }
}