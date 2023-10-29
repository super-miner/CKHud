using UnityEngine;
using System.Collections.Generic;
using CKHud.Common;
using CKHud.Common.Config;
using CKHud.Config;
using CoreLib.Util.Extensions;

namespace CKHud {
	public class HudManager : MonoBehaviour {
		public static HudManager instance = null;

		private const string TEXT_PREFAB_PATH = "Assets/CKHud/Prefabs/CKHud_Text.prefab";
		private GameObject textPrefab = null;

		public MapUI mapUI = null;

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
				CKHudMod.logger.LogError("Error loading the text prefab (null).");
				return;
			}

			CKHudMod.logger.LogInfo("Successfully initialized the Hud Manager object.");
		}

		void Update() {
			if ((CKHudMod.rewiredPlayer?.GetButtonDown(CKHudMod.KEYBIND_TOGGLE_HUD) ?? false) && !Manager.menu.IsAnyMenuActive()) {
				ConfigManager.instance.hudEnabled.SetValue(!ConfigManager.instance.hudEnabled.GetValue());
			}

			if (!foundContainers) {
				Transform ingameUI = FindInGameUI();
				
				if (ingameUI == null) {
					CKHudMod.logger.LogError("Could not find IngameUI.");
					return;
				}
				
				Transform mapUITransform = FindMapUI(ingameUI);

				mapUI = mapUITransform.GetComponent<MapUI>();

				GameObject UIContainer = new GameObject("CKHud_UIContainer");
				UIContainer.transform.parent = ingameUI;

				CreateHudRows(ConfigManager.instance.hudRowsAmount.GetValue());
				InitHudRowsText(UIContainer.transform);

				foundContainers = true;
				CKHudMod.logger.LogInfo("Created hud text game objects.");
			}
		}

		Transform FindInGameUI() {
			if (!Manager.ui) {
				CKHudMod.logger.LogError("Could not find UI Manager.");
				return null;
			}

			Transform renderingParent = Manager.ui.transform.parent.parent.GetChild(2); // GlobalObjects (Main Manager)(Clone)/Rendering

			Transform UICamera = Util.GetChildByName(renderingParent, "UI Camera");

			if (UICamera == null) {
				CKHudMod.logger.LogError("Could not find UI Camera.");
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
			List<HudRow> hudRows = ConfigManager.instance.componentsLayout.GetValue();
			
			for (int i = 0; i < amount; i++) {
				hudRows.Add(new HudRow());
			}
			
			//ConfigManager.instance.componentsLayout.SetValue(hudRows);
		}

		void InitHudRowsText(Transform container) {
			List<HudRow> hudRows = ConfigManager.instance.componentsLayout.GetValue();
			
			float textYPosition = ConfigManager.instance.startHudPosition.GetValue();
			float lineStep = ConfigManager.instance.hudLineStep.GetValue();

			foreach (HudRow hudRow in hudRows) {
				GameObject textGO = UnityEngine.Object.Instantiate(textPrefab, container, true);
				textGO.transform.position = new Vector3(14.5f, textYPosition, 0.0f);

				HudText hudText = textGO.GetComponent<HudText>();
				hudText.hudRow = hudRow;

				textYPosition += lineStep;
			}
		}
	}
}
