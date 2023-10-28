using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
    public class HudRow {
        public List<HudComponent> components = new List<HudComponent>();

        public bool GetString(out string output) { 
	        output = "";
	        bool textChanged = false;
            
            for (int i = 0; i < components.Count; i++) {
                if (i > 0) {
                    output += ", ";
                }
                
                bool textChangedTemp = components[i].GetString(out string newText);
                
                if (textChangedTemp) {
	                textChanged = true;
                }
                
                output += newText;
            }

            return textChanged;
        }
    }
}