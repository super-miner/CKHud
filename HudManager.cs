using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
	public class HudManager : MonoBehaviour {
		public static HudManager instance = null;
		
		public bool hudEnabled = true;
		public float startHudPosition = 4.25f;
		public float hudLineStep = -0.75f;
		public int hudRowsAmount = 12;
		public Color textColor = Color.white;
		public bool compactMode = false;
		
		public List<HudRow> hudRows = new List<HudRow>();

		public string DEFAULT_COMPONENTS = "FPS;Position;CenterDistance;DPS;LocalComputerTime";


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
			
			LoadConfig();
			
			CKHudMod.Log("Successfully initialized the Hud Manager object.");
		}
		
		void Update() {
			if (Input.GetKeyDown(KeyCode.F1)) {
				SetHudEnabled(!hudEnabled);
			}
			
			if (!foundText) {
				Transform ingameUI = FindInGameUI();
					
				if (!ingameUI) {
					CKHudMod.Log("Could not find Ingame UI.");
					return;
				}
				
				GameObject textPrefab = FindTextPrefab(ingameUI);
				
				if (!textPrefab) {
					return;
				}
				
				CKHudMod.Log("Found the text to use as a template.");
				
				InitHudRowsText(textPrefab, ingameUI);
				
				foundText = true;
			}
			else {
				RenderHudRows();
			}
		}
		
		void LoadConfig() {
			int configVersion = -1;
			ConfigSystem.GetInt("ConfigVersion", "DoNotEdit", ref configVersion, 1);
			
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

		Transform FindInGameUI() {
			if (!Manager.ui) {
				CKHudMod.Log("Could not find UI Manager.");
				return null;
			}

			Transform renderingParent = Manager.ui.transform.parent.parent.GetChild(2); // GlobalObjects (Main Manager)(Clone)/Rendering

			if (!renderingParent) {
				CKHudMod.Log("Could not find Rendering object.");
				return null;
			}

			Transform uiCamera = renderingParent.transform.GetChild(1);

			if (!uiCamera) {
				CKHudMod.Log("Could not find UI Camera.");
				return null;
			}

			return uiCamera.GetChild(0);
		}

		GameObject FindTextPrefab(Transform ingameUI) { // TODO: Figure out how to create a prefab for this instead of using existing text.
			Transform playerHealthBar = ingameUI.GetChild(0);

			if (!playerHealthBar) {
				CKHudMod.Log("Could not find Player Health Bar.");
				return null;
			}

			Transform playerHealthContainer = playerHealthBar.GetChild(0);

			if (!playerHealthContainer) {
				CKHudMod.Log("Could not find player health Container.");
				return null;
			}

			Transform playerHealthTextContainer = playerHealthContainer.GetChild(6);

			if (!playerHealthTextContainer) {
				CKHudMod.Log("Could not find player health Text Container.");
				return null;
			}

			Transform healthTextNumber = playerHealthTextContainer.GetChild(1);

			if (!healthTextNumber) {
				CKHudMod.Log("Could not find player Health Text Number.");
				return null;
			}

			return healthTextNumber.gameObject;
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
		
		void InitHudRowsText(GameObject textPrefab, Transform container) {
			float textYPosition = startHudPosition;
			int textNum = 1;
			foreach (HudRow hudRow in hudRows) {
				GameObject textGO = Instantiate<GameObject>(textPrefab, container);
				textGO.name = "CKHud_Text_Row" + textNum;
				textGO.transform.position = new Vector3(14.5f, textYPosition, 0.0f);

				PugText text = textGO.GetComponent<PugText>();

				hudRow.text = text;

				textNum++;
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
