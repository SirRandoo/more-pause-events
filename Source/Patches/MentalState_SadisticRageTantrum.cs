using Harmony;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_SadisticRageTantrum), "PostStart")]
    public static class MentalState_SadisticRageTantrum__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState_SadisticRageTantrum __instance)
        {
            if (!Settings.SadisticRageEnabled) return;
            if (__instance == null) return;
            if (__instance.target == null || __instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!(__instance.pawn.MentalState is MentalState_SadisticRageTantrum)) return;
            if (__instance.Age > 150) return;
            if (MPE.Cache.SadisticRage.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Info(string.Format("Pawn {0} is on a sadistic rage; pausing the game...", __instance.pawn.LabelDefinite()));

            MPE.Cache.SadisticRage.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }

    [HarmonyPatch(typeof(MentalState_SadisticRageTantrum), "PostEnd")]
    public static class MentalState_SadisticRageTantrum__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState_SadisticRageTantrum __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!MPE.Cache.SadisticRage.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Cache.SadisticRage.Remove(__instance.pawn.GetUniqueLoadID());
        }
    }
}
