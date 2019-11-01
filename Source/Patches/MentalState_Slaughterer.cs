using Harmony;

using RimWorld;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_Slaughterer), "PostStart")]
    public static class MentalState_Slaughterer__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState_Slaughterer __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (__instance.pawn.CurJob.def != JobDefOf.Slaughter) return;
            if (!Settings.SlaughterEnabled) return;
            if (__instance.Age > 150) return;
            if (MPE.Cache.Slaughter.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Info(string.Format("Pawn {0} is on a slaughter spree; pausing the game...", __instance.pawn.LabelDefinite()));

            MPE.Cache.Slaughter.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }

    [HarmonyPatch(typeof(MentalState_Slaughterer), "PostEnd")]
    public static class MentalState_Slaughterer__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState_Slaughterer __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!MPE.Cache.Slaughter.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Cache.Slaughter.Remove(__instance.pawn.GetUniqueLoadID());
        }
    }
}
