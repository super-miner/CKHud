using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
	public class HudManager : MonoBehaviour {
		public static HudManager instance = null;
		
		private const string TEXT_PREFAB_PATH = "Assets/CKHud/Prefabs/CKHud_Text.prefab";
		private GameObject textPrefab = null;

		public MapUI mapUI = null;
		
		public List<HudRow> hudRows = new List<HudRow>();

		private bool foundText = false;

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
				CKHudMod.Log("Error loading the text prefab (null)");
			}
			
			LoadConfig();
			
			CKHudMod.Log("Successfully initialized the Hud Manager object.");
		}
		
		void Update() {
			if (Input.GetKeyDown(KeyCode.F1) && !Manager.menu.IsAnyMenuActive()) {
				SetHudEnabled(!hudEnabled);
			}
			
			if (!foundText) {
				Transform ingameUI = FindInGameUI();
				Transform mapUITransform = FindMapUI(ingameUI);

				mapUI = mapUITransform.GetComponent<MapUI>();
				
				InitHudRowsText(ingameUI);
				
				CKHudMod.Log("Created text objects.");
				
				foundText = true;
			}
		}

		Transform FindInGameUI() {
			if (!Manager.ui) {
				CKHudMod.Log("Could not find UI Manager.");
				return null;
			}

			Transform renderingParent = Manager.ui.transform.parent.parent.GetChild(2); // GlobalObjects (Main Manager)(Clone)/Rendering

			Transform uiCamera = renderingParent.transform.GetChild(1);

			return uiCamera.GetChild(0);
		}
		
		Transform FindMapUI(Transform ingameUI) {
			return ingameUI.GetChild(13);
		}

		void SetHudEnabled(bool value) {
			hudEnabled = value;
			ConfigSystem.SetBool("General", "HudEnabled", value);
		}

		void CreateHudRows(int amount) {
			float textYPosition = startHudPosition;
			for (int i = 0; i < amount; i++, textYPosition += hudLineStep) {
				hudRows.Add(new HudRow());
			}
		}
		
		void InitHudRowsText(Transform container) {
			float textYPosition = startHudPosition;
			
			foreach (HudRow hudRow in hudRows) {
				GameObject textGO = UnityEngine.Object.Instantiate(textPrefab, container, true);
				textGO.transform.position = new Vector3(14.5f, textYPosition, 0.0f);

				PugText text = textGO.GetComponent<PugText>();

				hudRow.text = text;
				
				textYPosition += hudLineStep;
			}
		}
	}
}
