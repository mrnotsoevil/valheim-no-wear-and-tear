using HarmonyLib;
using UnityEngine;

namespace valheim_no_wear_and_tear
{
    [HarmonyPatch(typeof(WearNTear), "UpdateWear")]
    public class WearVisualizationPatch
    {
        static void Postfix(float health, bool triggerEffects, GameObject ___m_worn, GameObject ___m_new, GameObject ___m_broken)
        {
            if (health >= 0.5f && health <= 0.75)
            {
                if (___m_worn != ___m_new)
                    ___m_worn.SetActive(false);
                if (___m_broken != ___m_new)
                    ___m_broken.SetActive(false);
                ___m_new.SetActive(true);
            }
        }
    }
}