using System.Reflection;
using HarmonyLib;

namespace valheim_no_wear_and_tear
{
    [HarmonyPatch(typeof(WearNTear), "UpdateWear")]
    public class WearPatch
    {
        static bool Prefix(WearNTear __instance, ZNetView ___m_nview, ref float ___m_support)
        {
            if (!___m_nview.IsValid())
                return false;
            var shouldUpdateMethod =
                typeof(WearNTear).GetMethod("ShouldUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
            if (___m_nview.IsOwner() && (bool) shouldUpdateMethod.Invoke(__instance, new object[0]))
            {
                if (ZNetScene.instance.OutsideActiveArea(__instance.transform.position))
                {
                    var methodInfo = typeof(WearNTear).GetMethod("GetMaterialProperties",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    object[] result = new object[4];
                    methodInfo.Invoke(__instance, result);
                    float maxSupport = (float) result[0];
                    ___m_support = maxSupport;
                    ___m_nview.GetZDO().Set("support", ___m_support);
                    return false;
                }

                float damage = 0.0f;
                if ((bool) __instance.m_wet)
                    __instance.m_wet.SetActive(false);
                if (__instance.m_noSupportWear)
                {
                    var updateSupportMethod = typeof(WearNTear).GetMethod("UpdateSupport",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    var haveSupportMethod =
                        typeof(WearNTear).GetMethod("HaveSupport", BindingFlags.NonPublic | BindingFlags.Instance);
                    updateSupportMethod.Invoke(__instance, new object[0]);
                    if (!(bool) haveSupportMethod.Invoke(__instance, new object[0]))
                        damage = 100f;
                }

                var canBeRemovedMethod =
                    typeof(WearNTear).GetMethod("CanBeRemoved", BindingFlags.NonPublic | BindingFlags.Instance);
                if (damage > 0.0 && !(bool) canBeRemovedMethod.Invoke(__instance, new object[0]))
                    damage = 0.0f;
                if (damage > 0.0)
                    __instance.ApplyDamage(damage / 100f * __instance.m_health);
            }

            var updateVisualMethod =
                typeof(WearNTear).GetMethod("UpdateVisual", BindingFlags.NonPublic | BindingFlags.Instance);
            updateVisualMethod.Invoke(__instance, new object[] {true});
            return false;
        }
    }
}