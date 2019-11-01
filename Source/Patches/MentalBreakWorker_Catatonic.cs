using Harmony;

using RimWorld;

using Verse;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker_Catatonic), "TryStart")]
    public static class MentalBreakWorker_Catatonic
    {
        private static int Snapshot = -1;

        [HarmonyPostfix]
        public static void TryStart(Pawn pawn)
        {
            if (pawn == null) return;
            if (!pawn.Spawned) return;
            if (!pawn.health.hediffSet.HasHediff(HediffDefOf.CatatonicBreakdown)) return;
            if (!Settings.CatatoniaEnabled) return;
            if ((Find.TickManager.TicksGame - Snapshot) < 60 && Settings.CacheEnabled) return;


            MPE.Debug(string.Format("{0} ticks passed since the last catatonic breakdown.", Find.TickManager.TicksGame - Snapshot));
            MPE.Info(string.Format("Pawn {0} is on a catatonic breakdown; pausing the game...", pawn.LabelDefinite()));

            Find.TickManager.Pause();
            if (Settings.CacheEnabled) Snapshot = Find.TickManager.TicksGame;
        }
    }
}
