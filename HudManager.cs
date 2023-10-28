using UnityEngine;
using System.Collections.Generic;
using CKHud.Common;
using CKHud.Common.Config;
using CKHud.Config;

namespace CKHud {
	public class HudManager : MonoBehaviour {
		public static HudManager instance = null;

		private const string TEXT_PREFAB_PATH = "Assets/CKHud/Prefabs/CKHud_Text.prefab";
		private GameObject textPrefab = null;

		public MapUI mapUI = null;

		public List<HudRow> hudRows = new List<HudRow>();

		private bool foundContainers = false;

		void Awake() {
			if (instance == null) {
				instance = this;
			}
			else {
				Destroy(transform.gameObject);
				return;
			}

			DontDestroyOnLoad(transform.gameObject);

			textPrefab = CKHudMod.assetBundle.LoadAsset<GameObject>(TEXT_PREFAB_PATH);

			if (textPrefab == null) {
				LogSystem.Log("Error loading the text prefab (null).");
				return;
			}

			LogSystem.Log("Successfully initialized the Hud Manager object.");
		}

		void Update() {
			if ((CKHudMod.rewiredPlayer?.GetButtonDown(CKHudMod.KEYBIND_TOGGLE_HUD) ?? false) && !Manager.menu.IsAnyMenuActive()) {
				ConfigManager.instance.hudEnabled.SetValue(!ConfigManager.instance.hudEnabled.GetValue());
			}

			if (!foundContainers) {
				Transform ingameUI = FindInGameUI();
				
				if (ingameUI == null) {
					LogSystem.Log("Could not find IngameUI.");
					return;
				}
				
				Transform mapUITransform = FindMapUI(ingameUI);

				mapUI = mapUITransform.GetComponent<MapUI>();

				GameObject UIContainer = new GameObject("CKHud_UIContainer");
				UIContainer.transform.parent = ingameUI;

				InitHudRowsText(UIContainer.transform);

				foundContainers = true;
				LogSystem.Log("Created hud text game objects.");
			}
		}

		Transform FindInGameUI() {
			if (!Manager.ui) {
				LogSystem.Log("Could not find UI Manager.");
				return null;
			}

			Transform renderingParent = Manager.ui.transform.parent.parent.GetChild(2); // GlobalObjects (Main Manager)(Clone)/Rendering

			Transform UICamera = Util.GetChildByName(renderingParent, "UI Camera");

			if (UICamera == null) {
				LogSystem.Log("Could not find UI Camera.");
				return null;
			}

			return Util.GetChildByName(UICamera, "IngameUI");
		}

		Transform FindMapUI(Transform ingameUI) {
			if (ingameUI == null) {
				return null;
			}
			
			return Util.GetChildByName(ingameUI, "MapUI");
		}

		void CreateHudRows(int amount) {
			for (int i = 0; i < amount; i++) {
				hudRows.Add(new HudRow());
			}
		}

		void InitHudRowsText(Transform container) {
			float textYPosition = ConfigManager.instance.startHudPosition.GetValue();
			float lineStep = ConfigManager.instance.hudLineStep.GetValue();

			foreach (HudRow hudRow in hudRows) {
				GameObject textGO = UnityEngine.Object.Instantiate(textPrefab, container, true);
				textGO.transform.position = new Vector3(14.5f, textYPosition, 0.0f);

				textYPosition += lineStep;
			}
		}
	}
}
