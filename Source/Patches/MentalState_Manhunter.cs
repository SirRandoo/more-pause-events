using Harmony;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_Manhunter), "PostStart")]
    public static class MentalState_Manhunter__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState_Manhunter __instance)
        {
            if (!Settings.MadAnimalEnabled) return;
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (MPE.Cache.Manhunter.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Info(string.Format("Pawn {0} has gone mad; pausing the game...", __instance.pawn.LabelDefinite()));

            MPE.Cache.Manhunter.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }

    [HarmonyPatch(typeof(MentalState_Manhunter), "PostEnd")]
    public static class MentalState_Manhunter__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState_Manhunter __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!MPE.Cache.Manhunter.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Cache.Manhunter.Remove(__instance.pawn.GetUniqueLoadID());
            MPE.Debug(string.Format("Removed {0} from the cache.", __instance.pawn.LabelDefinite()));
        }
    }
}
