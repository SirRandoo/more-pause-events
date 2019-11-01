using Harmony;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_WanderConfused), "PostStart")]
    public static class MentalState_WanderConfused__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState_WanderConfused __instance)
        {
            if (!Settings.ConfusionEnabled) return;
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!(__instance.pawn.MentalState is MentalState_WanderConfused)) return;
            if (__instance.Age <= 150) return;
            if (MPE.Cache.WanderConfused.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Info(string.Format("Pawn {0} is wandering around confused; pausing the game...", __instance.pawn.LabelDefinite()));

            MPE.Cache.WanderConfused.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }

    [HarmonyPatch(typeof(MentalState_WanderConfused), "PostEnd")]
    public static class MentalState_WanderConfused__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState_WanderConfused __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!MPE.Cache.WanderConfused.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Cache.WanderConfused.Remove(__instance.pawn.GetUniqueLoadID());
        }
    }
}
