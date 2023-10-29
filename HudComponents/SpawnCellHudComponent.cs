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
			decimalPlaces = ConfigSystem.AddInt(HudComponentsRegistry.GetHudComponentByType(this.GetType()), "RefreshRate", DEFAULT_DECIMAL_PLACES);
		}

		public override string CreateString() {
			Vector3 playerPosition = Manager.main.player.WorldPosition;
			
			Vector2 spawnCell = new Vector2(Mathf.Floor(playerPosition.x / SPAWN_CELL_SIZE), Mathf.Floor(playerPosition.z / SPAWN_CELL_SIZE));
			Vector2 positionInCell = new Vector2(Mathf.Abs(playerPosition.x) % SPAWN_CELL_SIZE, Mathf.Abs(playerPosition.z) % SPAWN_CELL_SIZE);
			
			string xCellString = spawnCell.x.ToString("N" + decimalPlaces.GetValue());
			string zCellString = spawnCell.y.ToString("N" + decimalPlaces.GetValue());
			
			string xPosString = positionInCell.x.ToString("N" + decimalPlaces.GetValue());
			string zPosString = positionInCell.y.ToString("N" + decimalPlaces.GetValue());

			if (ConfigManager.instance.compactMode.GetValue()) {
				return $"Cell: {xCellString}/{zCellString} Cell Pos: {xPosString}/{zPosString}";
			}
			else {
				return $"Cell: {xCellString}/{zCellString} Cell Position: {xPosString}/{zPosString}";
			}
		}
    }
}
 