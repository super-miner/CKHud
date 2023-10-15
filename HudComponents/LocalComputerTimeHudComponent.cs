using System;

namespace CKHud.HudComponents {
    public class LocalComputerTimeHudComponent : HudComponent {
        public override string GetString() {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
