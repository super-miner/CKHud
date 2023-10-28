using CKHud.Config;
using UnityEngine;

namespace CKHud.HudComponents {
    public class PositionHudComponent : HudComponent {
        public override string CreateString() {
            Vector3 playerPosition = Manager.main.player.WorldPosition;
            if (ConfigManager.instance.compactMode.GetValue()) {
                return "X: " + playerPosition.x.ToString("N0") + " Z: " + playerPosition.z.ToString("N0");
            }
            else {
                return "X: " + playerPosition.x.ToString("N2") + "   Z: " + playerPosition.z.ToString("N2");
            }
        }
    }
}