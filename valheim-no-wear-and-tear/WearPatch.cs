using System.Reflection;
using HarmonyLib;

namespace valheim_no_wear_and_tear
{
    [HarmonyPatch(typeof(WearNTear), "UpdateWear")]
    public class WearPatch
    {
        static bool Prefix()
        {
            return false;
        }
    }
}