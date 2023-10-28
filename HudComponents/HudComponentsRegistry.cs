using CKHud;
using CKHud.HudComponents;
using System;
using System.Collections.Generic;

namespace Assets.CKHud.HudComponents {
	public static class HudComponentsRegistry {
		private static Dictionary<string, Type> _registry = new Dictionary<string, Type> {
			{"FPS",                 typeof(FPSHudComponent)},
			{"FPSCap",              typeof(FPSCapHudComponent) },

			{"Position",            typeof(PositionHudComponent)},
			{"CenterDistance",      typeof(CenterDistanceHudComponent)},

			{"DPS",                 typeof(DPSHudComponent)},
			{"MaxDPS",              typeof(MaxDPSHudComponent)},

			{"LocalComputerTime",   typeof(LocalComputerTimeHudComponent)},

			{"SpawnCell",			typeof(SpawnCellHudComponent)}
		};

		public static HudComponent GetHudComponentByType(string type) {
			if (_registry.ContainsKey(type)) {
				return Activator.CreateInstance(_registry[type]) as HudComponent;
			}

			return null;
		}

		public static void RegisterHudComponent(string type, Type hudComponent) {
			if (string.IsNullOrEmpty(type)) {
				CKHudMod.LogMethod("empty type param - Skipping Register");
				return;
			}

			if (_registry.ContainsKey(type)) {

				CKHudMod.LogMethod($"{type} already registered - Skipping Register");
				return;
			}

			if (!hudComponent.IsSubclassOf(typeof(HudComponent))) {
				CKHudMod.LogMethod($"{type} not a subclass of HudComponent - Skipping Register");
				return;
			}

			_registry[type] = hudComponent;
		}
	}
}
