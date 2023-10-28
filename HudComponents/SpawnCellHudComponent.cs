using System;
using UnityEngine;

namespace CKHud.HudComponents {
    public class SpawnCellHudComponent : HudComponent {
		private static readonly int SPAWN_CELL_SIZE = 16;

		public override string CreateString() {
			Vector3 playerPosition = Manager.main.player.WorldPosition;
			
			var spawnCellX = Math.Floor(playerPosition.x / SPAWN_CELL_SIZE);
			var spawnCellY = Math.Floor(playerPosition.z / SPAWN_CELL_SIZE);

			var posInCellX = Math.Abs(playerPosition.x) % SPAWN_CELL_SIZE;
			var posInCellY = Math.Abs(playerPosition.z) % SPAWN_CELL_SIZE;

			return $"Cell {spawnCellX:00}/{spawnCellY:00}  Pos {posInCellX:00}/{posInCellY:00}";
		}
    }
}
 