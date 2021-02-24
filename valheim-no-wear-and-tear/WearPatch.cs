using System.Reflection;
using HarmonyLib;

namespace valheim_no_wear_and_tear
{
    [HarmonyPatch(typeof(WearNTear), "UpdateWear")]
    public class WearPatch
    {
        private static readonly MethodInfo ShouldUpdateMethod =
            typeof(WearNTear).GetMethod("ShouldUpdate", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo GetMaterialPropertiesMethod = typeof(WearNTear).GetMethod("GetMaterialProperties",
        BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo HaveRoofMethod =
            typeof(WearNTear).GetMethod("HaveRoof", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo UpdateSupportMethod = typeof(WearNTear).GetMethod("UpdateSupport",
            BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo HaveSupportMethod =
            typeof(WearNTear).GetMethod("HaveSupport", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo CanRemoveMethod =
            typeof(WearNTear).GetMethod("CanBeRemoved", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly MethodInfo UpdateVisualMethod =
            typeof(WearNTear).GetMethod("UpdateVisual", BindingFlags.NonPublic | BindingFlags.Instance);

        static bool Prefix(WearNTear __instance, ZNetView ___m_nview, ref float ___m_support)
        {
            if (!___m_nview.IsValid())
                return false;
            if (___m_nview.IsOwner() && (bool) ShouldUpdateMethod.Invoke(__instance, new object[0]))
            {
                if (ZNetScene.instance.OutsideActiveArea(__instance.transform.position))
                {
                    // Max support if outside active area
                    object[] result = new object[4];
                    GetMaterialPropertiesMethod.Invoke(__instance, result);
                    float maxSupport = (float) result[0];
                    ___m_support = maxSupport;
                    ___m_nview.GetZDO().Set("support", ___m_support);
                    return false;
                }

                float damage = 0.0f;
                bool hasRoof = (bool)HaveRoofMethod.Invoke(__instance, new object[0]);
                bool isWet = EnvMan.instance.IsWet() && !hasRoof;
                if ((bool) __instance.m_wet)
                    __instance.m_wet.SetActive(isWet);
                if (__instance.m_noSupportWear)
                {
                    UpdateSupportMethod.Invoke(__instance, new object[0]);
                    if (!(bool) HaveSupportMethod.Invoke(__instance, new object[0]))
                        damage = 100f;
                }
                if (damage > 0.0 && !(bool) CanRemoveMethod.Invoke(__instance, new object[0]))
                    damage = 0.0f;
                if (damage > 0.0)
                    __instance.ApplyDamage(damage / 100f * __instance.m_health);
            }
            
            UpdateVisualMethod.Invoke(__instance, new object[] {true});
            return false;
        }
    }
}