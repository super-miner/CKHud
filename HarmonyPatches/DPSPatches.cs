using System.Collections.Generic;
using HarmonyLib;
using InventoryHandlerSystem;
using Unity.Entities;
using UnityEngine;

namespace CKHud.HarmonyPatches {
    [HarmonyPatch]
    public class DPSPatches {
        public static List<(float time, int damage)> damageInstances = new List<(float, int)>();
        
        [HarmonyPatch(typeof(EntityUtility), "GetDamageInfo")]
        [HarmonyPostfix]
        public static void GetDamageInfo(Entity attacker, int damageDone) {
            damageInstances.Add((Time.time, damageDone));
        }

        public static int GetDPS(float timeFrame) {
            while (damageInstances.Count > 0 && damageInstances[0].time < Time.time - timeFrame) {
                damageInstances.RemoveAt(0);
            }

            int sum = 0;

            foreach ((float time, int damage) damageInstance in damageInstances) {
                sum += damageInstance.damage;
            }

            return Mathf.FloorToInt(sum / timeFrame);
        }
    }
}