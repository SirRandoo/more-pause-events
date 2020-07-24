using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SirRandoo.MPE.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(JobDriver_PredatorHunt), "CheckWarnPlayer")]
    public static class PredatorHuntPatch
    {
        [UsedImplicitly]
        [HarmonyFinalizer]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void CheckWarnPlayer(JobDriver_PredatorHunt __instance)
        {
            if (!Settings.PredatorLettersEnabled || Mpe.IsPhaLoaded)
            {
                return;
            }

            var i = Traverse.Create(__instance);
            var prey = i.Property("Prey").GetValue<Pawn>();
            var pawn = i.Field("pawn").GetValue<Pawn>();

            if (!prey.Spawned
                || prey.Faction != Faction.OfPlayer
                || Find.TickManager.TicksGame != pawn.mindState.lastPredatorHuntingPlayerNotificationTick
                || !prey.Position.InHorDistOf(pawn.Position, 60f))
            {
                return;
            }

            if (prey.RaceProps.Animal && PawnUtility.ShouldSendNotificationAbout(prey))
            {
                Find.LetterStack.ReceiveLetter(
                    "Letters.Predator.Label".Translate(
                            pawn.LabelShort,
                            prey.LabelDefinite(),
                            pawn.Named("PREDATOR"),
                            prey.Named("PREY")
                        )
                       .CapitalizeFirst(),
                    "Letters.Predator.Body".Translate(
                            pawn.LabelIndefinite(),
                            prey.LabelDefinite(),
                            pawn.Named("PREDATOR"),
                            prey.Named("PREY")
                        )
                       .CapitalizeFirst(),
                    LetterDefOf.NegativeEvent,
                    new LookTargets(pawn)
                );
            }
        }
    }
}
