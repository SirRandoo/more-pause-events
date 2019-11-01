
using Harmony;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_Berserk), "PostStart")]
    public static class MentalState_Berserk__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState_Berserk __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!Settings.BerserkEnabled) return;
            if (MPE.Cache.Berserk.Contains(__instance.pawn.GetUniqueLoadID())) return;
            if (__instance.Age > 10) return;


            MPE.Info(string.Format("Pawn {0} has gone berserk;  pausing the game...", __instance.pawn.LabelDefinite()));

            MPE.Cache.Berserk.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }

    [HarmonyPatch(typeof(MentalState_Berserk), "PostEnd")]
    public static class MentalState_Berserk__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState_Berserk __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!MPE.Cache.Berserk.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Cache.Berserk.Remove(__instance.pawn.GetUniqueLoadID());
        }
    }
}
