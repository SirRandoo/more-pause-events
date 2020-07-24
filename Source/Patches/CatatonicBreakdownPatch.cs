using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.PauseEvents.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(MentalBreakWorker_Catatonic), "TryStart")]
    public static class CatatonicBreakdownPatch
    {
        private static int _snapshot = -1;

        [UsedImplicitly]
        [HarmonyPostfix]
        public static void TryStart(Pawn pawn)
        {
            if (!Settings.CatatoniaEnabled)
            {
                return;
            }

            if (pawn?.Spawned != true)
            {
                return;
            }

            if (!pawn.health.hediffSet.HasHediff(HediffDefOf.CatatonicBreakdown))
            {
                return;
            }

            if (Find.TickManager.TicksGame - _snapshot < 60)
            {
                return;
            }

            Find.TickManager.Pause();
            _snapshot = Find.TickManager.TicksGame;
        }
    }
}
