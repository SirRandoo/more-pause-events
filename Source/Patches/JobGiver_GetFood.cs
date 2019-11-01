
using Harmony;

using RimWorld;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.JobGiver_GetFood), "TryGiveJob")]
    public static class JobGiver_GetFood
    {
        [HarmonyPostfix]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (__result == null || pawn == null) return;
            if (!pawn.Spawned) return;
            if (__result.def != JobDefOf.PredatorHunt) return;
            if (!__result.CanBeginNow(pawn)) return;
            if (!Settings.PredatorEnabled) return;
            if (__result.targetA == null) return;
            if (__result.targetA.Thing == null) return;
            if (!(__result.targetA.Thing is Pawn prey)) return;
            if (!PawnUtility.ShouldSendNotificationAbout(prey)) return;

            MPE.Info(string.Format("Predator {0} is hunting {1}; pausing the game...", pawn.LabelDefinite(), prey.LabelDefinite()));
            Find.TickManager.Pause();

            if (Settings.PredatorLettersEnabled && prey.RaceProps.Animal)
            {
                Find.LetterStack.ReceiveLetter(
                    "Letters.Predator.Label".Translate(
                        pawn.LabelShort,
                        prey.LabelDefinite(),
                        pawn.Named("PREDATOR"),
                        prey.Named("PREY")
                    ).CapitalizeFirst(),

                    "Letters.Predator.Body".Translate(
                        pawn.LabelIndefinite(),
                        prey.LabelDefinite(),
                        pawn.Named("PREDATOR"),
                        prey.Named("PREY")
                    ).CapitalizeFirst(),

                    LetterDefOf.NegativeEvent,
                    new LookTargets(pawn)
                );
            }
        }
    }
}
