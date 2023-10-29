using CKHud.Common.Config;
using CKHud.Config;
using UnityEngine;

namespace CKHud.HudComponents {
    public class PositionHudComponent : HudComponent {
	    private static readonly int DEFAULT_DECIMAL_PLACES = 2;

	    public static ConfigInt decimalPlaces = null;
	    
	    public override void InitConfigs() {
		    decimalPlaces = ConfigSystem.AddInt(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "DecimalPlaces", DEFAULT_DECIMAL_PLACES);
	    }
	    
        public override string CreateString() {
	        PlayerController player = Manager.main.player;

	        if (player == null) {
		        return "";
	        }
	        
	        Vector3 playerPosition = player.WorldPosition;
            
            string xString = playerPosition.x.ToString("N" + decimalPlaces.GetValue());
            string zString = playerPosition.z.ToString("N" + decimalPlaces.GetValue());
            
            if (ConfigManager.instance.compactMode.GetValue()) {
                return $"Pos: {xString}/{zString}";
            }
            else {
                return $"Position: {xString}/{zString}";
            }
        }
    }
}