using System;

namespace CKHud.HudComponents {
    public class LocalComputerTimeComponent : HudComponent {
        public override string GetString() {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
