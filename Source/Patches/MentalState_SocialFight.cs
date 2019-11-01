using Harmony;

using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_SocialFighting), "PostEnd")]
    public static class MentalState_SocialFight__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState_SocialFighting __instance)
        {
            if (!Settings.SocialFightEnabled) return;
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!MPE.Cache.SocialFight.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Cache.SocialFight.Remove(__instance.pawn.GetUniqueLoadID());
        }
    }
}
