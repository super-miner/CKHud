using System;
using UnityEngine;

namespace CKHud.HudComponents {
    public class SpawnCellHudComponent : HudComponent {
		private static int SPAWN_CELL_SIZE = 16;

		public override string GetString() {
			Vector3 playerPosition = Manager.main.player.WorldPosition;

			// Math.Floor because even at Pos 8 / 16 = 0.5 should still be 0
			var spawnCellX = Math.Floor(playerPosition.x / SPAWN_CELL_SIZE);
			// Using Y for less headache, internally Z because of the 3D
			var spawnCellY = Math.Floor(playerPosition.z / SPAWN_CELL_SIZE);

			var posInCellX = Math.Abs(playerPosition.x) % SPAWN_CELL_SIZE;
			var posInCellY = Math.Abs(playerPosition.z) % SPAWN_CELL_SIZE;

			// Spawn [Cell X | Cell Y] <PosX in C | PosY in C>s

			return $"Cell {spawnCellX:00}/{spawnCellY:00}  Pos {posInCellX:00}/{posInCellY:00}";
		}
    }
}
 