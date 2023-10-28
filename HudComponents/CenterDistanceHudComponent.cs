using CKHud.Config;
using UnityEngine;

namespace CKHud.HudComponents {
    public class CenterDistanceHudComponent : HudComponent {
        public override string CreateString() {
            Vector3 playerPosition = Manager.main.player.WorldPosition;
            if (ConfigManager.instance.compactMode.GetValue()) {
                return "C Dist: " + Vector3.Distance(Vector3.zero, playerPosition).ToString("N0");
            }
            else {
                return "Center Dist: " + Vector3.Distance(Vector3.zero, playerPosition).ToString("N2");
            }
        }
    }
}