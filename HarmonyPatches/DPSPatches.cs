using System.Collections.Generic;
using HarmonyLib;
using PlayerCommand;
using UnityEngine;

namespace CKHud.HarmonyPatches {
    [HarmonyPatch]
    public class DPSPatches {
        public static List<(float time, int damage)> damageInstances = new List<(float, int)>();
        public static int smoothedDPS = 0;
        public static int maxDPS = 0;
        public static float lastMaxDPSTime = -1.0f;
        
        [HarmonyPatch(typeof(ClientSystem), "DealDamageToEntity")]
        [HarmonyPostfix]
        public static void DealDamageToEntity(int damageAfterReduction) {
            damageInstances.Add((Time.time, damageAfterReduction));
        }

        public static int GetDPS(float timeFrame, float smoothingCoefficient) {
            while (damageInstances.Count > 0 && damageInstances[0].time < Time.time - timeFrame) {
                damageInstances.RemoveAt(0);
            }

            if (lastMaxDPSTime >= 0.0f && Time.time - lastMaxDPSTime > smoothingCoefficient) {
                lastMaxDPSTime = -1.0f;
                maxDPS = 0;
            }

            int sum = 0;

            foreach ((float time, int damage) damageInstance in damageInstances) {
                sum += damageInstance.damage;
            }

            int effectiveDPS = Mathf.FloorToInt(sum / timeFrame);
            
            if (effectiveDPS > 0) {
                lastMaxDPSTime = Time.time;
            }
            maxDPS = Mathf.FloorToInt(Mathf.Max(maxDPS, effectiveDPS));
            
            int smoothingStep = Mathf.FloorToInt(maxDPS * smoothingCoefficient * Time.deltaTime);
            

            smoothedDPS = Mathf.FloorToInt(Mathf.MoveTowards(smoothedDPS, effectiveDPS, smoothingStep));
            
            return smoothedDPS;
        }
    }
}