using System.Reflection;
using HarmonyLib;

namespace valheim_no_wear_and_tear
{
    [HarmonyPatch(typeof(WearNTear), "UpdateSupport")]
    public class StructuralIntegrityPatch
    {
        private static readonly MethodInfo GetMaterialPropertiesMethod = typeof(WearNTear).GetMethod("GetMaterialProperties",
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        static bool Prefix(WearNTear __instance, ref float ___m_support, ZNetView ___m_nview)
        {
            object[] result = new object[4];
            GetMaterialPropertiesMethod.Invoke(__instance, result);
            float maxSupport = (float) result[0];
            ___m_support = maxSupport;
            ___m_nview.GetZDO().Set("support", ___m_support);
            return false;
        }
    }
}