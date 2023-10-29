using System;
using CKHud.Common.Config;
using CKHud.Config;
using UnityEngine;

namespace CKHud.HudComponents {
    public class SpawnCellHudComponent : HudComponent {
		private static readonly int DEFAULT_DECIMAL_PLACES = 2;
		
		private static readonly int SPAWN_CELL_SIZE = 16;

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
			
			Vector2 spawnCell = new Vector2(Mathf.Floor(playerPosition.x / SPAWN_CELL_SIZE), Mathf.Floor(playerPosition.z / SPAWN_CELL_SIZE));
			Vector2 positionInCell = new Vector2(Mathf.Abs(playerPosition.x) % SPAWN_CELL_SIZE, Mathf.Abs(playerPosition.z) % SPAWN_CELL_SIZE);
			
			string xCellString = spawnCell.x.ToString("N0");
			string zCellString = spawnCell.y.ToString("N0");
			
			string xPosString = positionInCell.x.ToString("N" + decimalPlaces.GetValue());
			string zPosString = positionInCell.y.ToString("N" + decimalPlaces.GetValue());

			if (ConfigManager.instance.compactMode.GetValue()) {
				return $"Cell: {xCellString}/{zCellString} Pos: {xPosString}/{zPosString}";
			}
			else {
				return $"Cell: {xCellString}/{zCellString}   Position: {xPosString}/{zPosString}";
			}
		}
    }
}
 