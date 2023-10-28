using UnityEngine;
using System.Collections.Generic;
using CKHud.HudComponents;

namespace CKHud {
    public class HudRow {
        public List<HudComponent> components = new List<HudComponent>();

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