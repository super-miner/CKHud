using System.Linq;
using CKHud.Common;
using CKHud.Common.Config;
using CKHud.Config;
using CKHud.HudComponents;
using CoreLib;
using CoreLib.RewiredExtension;
using PugMod;
using Rewired;
using UnityEngine;
using Logger = CoreLib.Util.Logger;

namespace CKHud {
	public class CKHudMod : IMod {
		public static string MOD_VERSION = "1.1.4";
		public static string MOD_NAME = "CK Hud";
		public static string MOD_ID = "CKHUD";

		public static Logger logger = new Logger(MOD_NAME);

		public static string KEYBIND_TOGGLE_HUD = MOD_ID + "_TOGGLE_HUD";
		
		public static LoadedMod modInfo = null;
		public static AssetBundle assetBundle;
		public static Player rewiredPlayer;

        public void EarlyInit() {
	        modInfo = GetModInfo();

			if (modInfo != null) {
				assetBundle = modInfo.AssetBundles[0];
				CKHudMod.logger.LogInfo("Found mod info.");
			}
			else {
				CKHudMod.logger.LogError("Could not find mod info.");
			}
			
			CoreLibMod.LoadModules(typeof(RewiredExtensionModule));

			RewiredExtensionModule.rewiredStart += () => {
				rewiredPlayer = ReInput.players.GetPlayer(0);
			};
			RewiredExtensionModule.AddKeybind(KEYBIND_TOGGLE_HUD, $"{MOD_NAME}: Toggle HUD", KeyboardKeyCode.F1);
		}

		public void Init() {
			ConfigSystem.Init(MOD_ID);
			HudComponentsRegistry.InitConfigs();
			ConfigManager.Create();
			
			new GameObject("CKHud_HudManager", typeof(HudManager));
			
			CKHudMod.logger.LogInfo("Loaded " + CKHudMod.MOD_NAME + " version " + CKHudMod.MOD_VERSION + ".");
		}

		public void Shutdown() {
			CKHudMod.logger.LogInfo("Unloaded " + CKHudMod.MOD_NAME + " version " + CKHudMod.MOD_VERSION + ".");
		}

		public void ModObjectLoaded(UnityEngine.Object obj) {
			
		}

		public void Update() {
			
		}
		
		private LoadedMod GetModInfo() { // Code taken from Better Chat
			return API.ModLoader.LoadedMods.FirstOrDefault(modInfo => modInfo.Handlers.Contains(this));
		}
	}	
}
