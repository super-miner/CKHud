using CKHud.Config;
using UnityEngine;

namespace CKHud {
	[RequireComponent(typeof(PugText))]
	public class HudText : MonoBehaviour {
		public HudRow hudRow = null;

		private PugText text = null;

		private void Awake() {
			text = GetComponent<PugText>();
		}
		
		private void Update() {
			if (hudRow.components.Count == 0) {
				return;
			}
            
			transform.localScale = Manager.ui.isAnyInventoryShowing || HudManager.instance.mapUI.isShowingBigMap || !ConfigManager.instance.hudEnabled.GetValue() ? Vector3.zero : Manager.ui.CalcGameplayUITargetScaleMultiplier();

			if (text.transform.localScale != Vector3.zero) {
				bool shouldRerender = hudRow.GetString(out string newText);

				if (shouldRerender) {
					text.Render(newText, false);
					text.SetTempColor(ConfigManager.instance.textColor.GetValue());
				}
			}
		}
	}
}