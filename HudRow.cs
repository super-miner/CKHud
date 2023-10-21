using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
    public class HudRow {
        public PugText text = null;
        public List<HudComponent> components = new List<HudComponent>();
        
        public void Render() {
            if (components.Count == 0 || !text) {
                return;
            }
            
            text.transform.localScale = Manager.ui.isAnyInventoryShowing || HudManager.instance.mapUI.isShowingBigMap ? Vector3.zero : Manager.ui.CalcGameplayUITargetScaleMultiplier();

            if (text.transform.localScale != Vector3.zero) {
                if (HudManager.instance.hudEnabled) {
                    text.Render(GetString(), false);
                    text.SetTempColor(HudManager.instance.textColor);
                }
                else {
                    text.Render("", false);
                }
            }
        }

        public string GetString() {
            string output = "";
            
            for (int i = 0; i < components.Count; i++) {
                if (i > 0) {
                    output += ", ";
                }
                
                output += components[i].GetString();
            }

            return output;
        }
    }
}