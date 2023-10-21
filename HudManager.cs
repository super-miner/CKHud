using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
	public class HudManager : MonoBehaviour {
		public static HudManager instance = null;
		
		public const string DEFAULT_COMPONENTS = "FPS;Position;CenterDistance;DPS;LocalComputerTime";
		public const int CONFIG_VERSION = 2;
		
		private const string TEXT_PREFAB_PATH = "Assets/CKHud/CKHud_Text.prefab";
		private GameObject textPrefab = null;

		public MapUI mapUI = null;
		
		public bool hudEnabled = true;
		public float startHudPosition = 4.25f;
		public float hudLineStep = -0.75f;
		public int hudRowsAmount = 12;
		public Color textColor = Color.white;
		public bool compactMode = false;
		
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
			else {
				RenderHudRows();
			}
		}
		
		void LoadConfig() {
			UpdateConfig();
			
			ConfigSystem.GetBool("General", "HudEnabled", ref hudEnabled, hudEnabled);
			ConfigSystem.GetFloat("General", "HudStart", ref startHudPosition, startHudPosition);
			ConfigSystem.GetFloat("General", "LineSpacing", ref hudLineStep, hudLineStep);
			ConfigSystem.GetInt("General", "NumRows", ref hudRowsAmount, hudRowsAmount);
			ConfigSystem.GetColor("General", "TextColor", ref textColor, textColor);
			ConfigSystem.GetBool("General", "CompactMode", ref compactMode, compactMode);
			
			CreateHudRows(hudRowsAmount);

			LoadLayoutConfig();
			
			CKHudMod.Log("Loaded config.");
		}

		void LoadLayoutConfig() {
			string componentLayout = "";
			ConfigSystem.GetString("Components", "Layout", ref componentLayout, DEFAULT_COMPONENTS);
			string[] rowStrings = componentLayout.Replace(" ", "").Split(';');
			int rowsUsed = (int)Mathf.Min(rowStrings.Length, hudRows.Count);
			
			for (int i = 0; i < rowsUsed; i++) {
				string[] componentStrings = rowStrings[i].Split(',');
				
				foreach (string componentString in componentStrings) {
					HudComponent component = HudComponent.Parse(componentString);

					if (component != null) {
						hudRows[i].components.Add(component);
					}
				}
			}
		}

		void UpdateConfig() {
			int configVersion = -1;
			ConfigSystem.GetInt("ConfigVersion", "DoNotEdit", ref configVersion, CONFIG_VERSION);
			
			if (configVersion < 2) {
				ConfigSystem.SetInt("ConfigVersion", "DoNotEdit", CONFIG_VERSION);
				
				string componentLayout = "";
				ConfigSystem.GetString("Components", "Layout", ref componentLayout, DEFAULT_COMPONENTS);
				ConfigSystem.SetString("Components", "Layout", componentLayout + ";LocalComputerTime");
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

		void RenderHudRows() {
			foreach (HudRow hudRow in hudRows) {
				hudRow.Render();
			}
		}
	}
}
