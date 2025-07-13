using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SirRandoo.MPE.Patches;

[UsedImplicitly]
[HarmonyPatch(typeof(JobDriver_PredatorHunt), methodName: "CheckWarnPlayerInterval")]
public static class PredatorHuntPatch
{
    [UsedImplicitly]
    [HarmonyFinalizer]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void CheckWarnPlayerInterval(JobDriver_PredatorHunt __instance, bool ___notifiedPlayerAttacking)
    {
        if (!Settings.PredatorLettersEnabled || Mpe.IsPhaLoaded) return;

        if (!___notifiedPlayerAttacking) return;
        if (__instance.pawn.mindState.lastPredatorHuntingPlayerNotificationTick < Find.TickManager.TicksGame) return;
        if (!__instance.Prey.RaceProps.Animal) return;

        Find.TickManager.Pause();

        if (PawnUtility.ShouldSendNotificationAbout(__instance.Prey))
            Find.LetterStack.ReceiveLetter(
                "Letters.Predator.Label".Translate(__instance.pawn.LabelShort, __instance.Prey.LabelDefinite(), __instance.pawn.Named("PREDATOR"), __instance.Prey.Named("PREY")).CapitalizeFirst(),
                "Letters.Predator.Body".Translate(__instance.pawn.LabelIndefinite(), __instance.Prey.LabelDefinite(), __instance.pawn.Named("PREDATOR"), __instance.Prey.Named("PREY")).CapitalizeFirst(),
                LetterDefOf.NegativeEvent,
                new LookTargets(__instance.pawn)
            );
    }
}
