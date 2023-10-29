using System;
using System.Collections.Generic;
using System.Linq;
using CKHud.Common;
using CoreLib.Util.Extensions;
using PugMod;

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

		public static void InitConfigs() {
			foreach (Type type in registry.Values) {
				HudComponent hudComponent = Activator.CreateInstance(type) as HudComponent;
				hudComponent?.InitConfigs();
			}
		}

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
				CKHudMod.logger.LogError("Empty type parameter. Skipping Register");
				return;
			}

			if (registry.ContainsKey(type)) {
				CKHudMod.logger.LogError($"{type} already registered. Skipping Register");
				return;
			}

			if (!hudComponent.IsSubclassOf(typeof(HudComponent))) {
				CKHudMod.logger.LogError($"{type} not a subclass of HudComponent. Skipping Register");
				return;
			}

			registry[type] = hudComponent;
		}
	}
}
