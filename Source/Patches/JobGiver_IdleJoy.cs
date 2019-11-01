using Harmony;

using RimWorld;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.JobGiver_IdleJoy), "TryGiveJob")]
    public static class JobGiver_IdleJoy
    {
        private static int Snapshot = -1;

        [HarmonyPostfix]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (__result == null || pawn == null) return;
            if (!pawn.Spawned) return;
            if (!__result.CanBeginNow(pawn)) return;
            if (!Settings.IdleEnabled) return;
            if ((Find.TickManager.TicksGame - Snapshot) < 60 && Settings.CacheEnabled) return;


            MPE.Debug(string.Format("{0} ticks passed since the last idle joy.", Find.TickManager.TicksGame - Snapshot));
            MPE.Info(string.Format("Pawn {0} is idle (joy); pausing the game...", pawn.LabelDefinite()));

            Find.TickManager.Pause();
            if (Settings.CacheEnabled) Snapshot = Find.TickManager.TicksGame;

            if (Settings.IdleLettersEnabled)
            {
                Find.LetterStack.ReceiveLetter(
                    "Letters.Idle.Label".Translate(
                        pawn,
                        pawn.Named("PAWN")
                    ),

                    "Letters.Idle.Body".Translate(
                        pawn,
                        pawn.Named("PAWN")
                    ),

                    LetterDefOf.NeutralEvent,
                    new LookTargets(pawn)
                );
            }
        }
    }
}
