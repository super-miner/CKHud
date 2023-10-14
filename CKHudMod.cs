using PugMod;
using UnityEngine;

namespace CKHud {
	public class CKHudMod : IMod {
		public static string MOD_VERSION = "1.1.2";
		public static string MOD_NAME = "CK Hud";
		public static string MOD_ID = "CKHUD";
		
		private GameObject hudManager = null;
		
		public static void Log(object message) {
			Debug.Log(MOD_NAME + ": " + message.ToString());
		}
		
		public void EarlyInit() {

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
	}	
}
