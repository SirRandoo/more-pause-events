using Harmony;

using RimWorld;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.JobGiver_SocialFighting), "TryGiveJob")]
    public static class JobGiver_SocialFighting
    {
        private static int Snapshot = -1;

        [HarmonyPostfix]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (__result == null || pawn == null) return;
            if (!pawn.Spawned) return;
            if (__result.def != JobDefOf.SocialFight) return;
            if (!__result.CanBeginNow(pawn)) return;
            if (!Settings.SocialFightEnabled) return;
            if ((Find.TickManager.TicksGame - Snapshot) < 60 && Settings.CacheEnabled) return;
            if (__result.targetA == null) return;
            if (__result.targetA.Thing == null) return;
            if (!(__result.targetA.Thing is Pawn victim)) return;


            MPE.Debug(string.Format("{0} ticks passed since the last social fight.", Find.TickManager.TicksGame - Snapshot));
            MPE.Info(string.Format("Pawn {0} started a social fight; pausing the game...", pawn.LabelDefinite()));

            Find.TickManager.Pause();
            if (Settings.CacheEnabled) Snapshot = Find.TickManager.TicksGame;

            if (Settings.SocialFightLettersEnabled)
            {
                Find.LetterStack.ReceiveLetter(
                    "Letters.SocialFight.Label".Translate(
                        pawn,
                        victim,
                        pawn.Named("ASSAILANT"),
                        victim.Named("VICTIM")
                    ),

                    "Letters.SocialFight.Body".Translate(
                        pawn,
                        victim,
                        pawn.Named("ASSAILANT"),
                        victim.Named("VICTIM")
                    ),

                    LetterDefOf.NeutralEvent,
                    new LookTargets(pawn)
                );
            }
        }
    }
}
