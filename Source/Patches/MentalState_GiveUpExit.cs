using Harmony;

using Verse;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(Verse.AI.MentalState_GiveUpExit), "MentalStateTick")]
    public static class MentalState_GiveUpExit
    {
        private static int Snapshot = -1;

        [HarmonyPostfix]
        public static void MentalStateTick(Verse.AI.MentalState_GiveUpExit __instance)
        {
            if (!Settings.GiveUpEnabled) return;
            if ((Find.TickManager.TicksGame - Snapshot) < 60 && Settings.CacheEnabled) return;
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (!(__instance.pawn.MentalState is Verse.AI.MentalState_GiveUpExit)) return;
            if (__instance.Age > 150) return;


            MPE.Debug(string.Format("{0} ticks passed since the last give up.", Find.TickManager.TicksGame - Snapshot));
            MPE.Info(string.Format("Pawn {0} gave up on the colony; pausing the game...", __instance.pawn.LabelDefinite()));

            Find.TickManager.Pause();
            if (Settings.CacheEnabled) Snapshot = Find.TickManager.TicksGame;
        }
    }
}
