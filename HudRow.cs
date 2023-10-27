using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
    public class HudRow {
        public PugText text = null;
        public List<HudComponent> components = new List<HudComponent>();

        public void Update() {
            Render();
        }
        
        public void Render() {
            if (text == null) {
                CKHudMod.Log("Could not find text.");
                return;
            }
            
            if (components.Count == 0) {
                return;
            }
            
            text.transform.localScale = Manager.ui.isAnyInventoryShowing || HudManager.instance.mapUI.isShowingBigMap ? Vector3.zero : Manager.ui.CalcGameplayUITargetScaleMultiplier();

            if (text.transform.localScale != Vector3.zero) {
                if (HudManager.instance.hudEnabled) {
                    string displayString = GetString();

                    if (displayString == "") {
                        CKHudMod.Log("The displayed string was empty.");
                    }
                    
                    text.Render(displayString, false);
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