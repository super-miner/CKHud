using CKHud;
using CKHud.HudComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using CKHud.Common;

namespace CKHud.HudComponents {
	public static class HudComponentsRegistry {
		private static Dictionary<string, Type> registry = new Dictionary<string, Type> {
			{"FPS",                 typeof(FPSHudComponent)},
			{"FPSCap",              typeof(FPSCapHudComponent)},

			{"Position",            typeof(PositionHudComponent)},
			{"CenterDistance",      typeof(CenterDistanceHudComponent)},

			{"DPS",                 typeof(DPSHudComponent)},
			{"MaxDPS",              typeof(MaxDPSHudComponent)},

			{"LocalComputerTime",   typeof(LocalComputerTimeHudComponent)},

			{"SpawnCell",			typeof(SpawnCellHudComponent)}
		};

		public static HudComponent GetHudComponentByName(string name) {
			if (registry.TryGetValue(name, out Type type)) {
				return Activator.CreateInstance(type) as HudComponent;
			}

			return null;
		}

		public static string GetHudComponentByType(Type type) {
			return registry.FirstOrDefault(pair => type == pair.Value).Key;
		}

		public static void RegisterHudComponent(string type, Type hudComponent) {
			if (string.IsNullOrEmpty(type)) {
				LogSystem.Log("Empty type parameter. Skipping Register");
				return;
			}

			if (registry.ContainsKey(type)) {
				LogSystem.Log($"{type} already registered. Skipping Register");
				return;
			}

			if (!hudComponent.IsSubclassOf(typeof(HudComponent))) {
				LogSystem.Log($"{type} not a subclass of HudComponent. Skipping Register");
				return;
			}

			registry[type] = hudComponent;
		}
	}
}
