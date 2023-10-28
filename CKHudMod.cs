using System.Linq;
using System.Runtime.CompilerServices;
using PugMod;
using UnityEngine;

namespace CKHud {
	public class CKHudMod : IMod {
		public static string MOD_VERSION = "1.1.4";
		public static string MOD_NAME = "CK Hud";
		public static string MOD_ID = "CKHUD";
		
		public static LoadedMod modInfo = null;
		public static AssetBundle assetBundle;
		
		private GameObject hudManager = null;
		
		public static void Log(object message) {
			Debug.Log(MOD_NAME + ": " + message.ToString());
		}

        public static void LogMethod(object message, [CallerMemberName] string callerMethod = "") {
            Debug.Log($"{MOD_NAME} [${callerMethod}]: {message}");
        }

        public void EarlyInit() {
			modInfo = GetModInfo();

			if (modInfo != null) {
				assetBundle = modInfo.AssetBundles[0];
				CKHudMod.Log("Found mod info.");
			}
			else {
				CKHudMod.Log("Could not find mod info.");
			}
		}

		public void Init() {
			hudManager = new GameObject("CKHud_HudManager", typeof(HudManager));
			
			CKHudMod.Log("Loaded " + CKHudMod.MOD_NAME + " version " + CKHudMod.MOD_VERSION + ".");
		}

		public void Shutdown() {
			CKHudMod.Log("Unloaded " + CKHudMod.MOD_NAME + " version " + CKHudMod.MOD_VERSION + ".");
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
