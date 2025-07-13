using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches;

[UsedImplicitly]
[HarmonyPatch(typeof(JobGiver_IdleJoy), methodName: "TryGiveJob")]
public static class IdleJoyPatch
{
    private static int _snapshot = -1;

    [UsedImplicitly]
    [HarmonyPostfix]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void TryGiveJob(Pawn pawn, Job __result)
    {
        if (!Settings.IdleEnabled || Find.TickManager.TicksGame - _snapshot < 60) return;
        if (!__result?.CanBeginNow(pawn) ?? true) return;
        if (!pawn?.Spawned ?? true) return;

        Find.TickManager.Pause();
        _snapshot = Find.TickManager.TicksGame;

        if (!Settings.IdleLettersEnabled) return;

        Find.LetterStack.ReceiveLetter(
            "Letters.Idle.Label".Translate(pawn, pawn.Named("PAWN")),
            "Letters.Idle.Body".Translate(pawn, pawn.Named("PAWN")),
            LetterDefOf.NeutralEvent,
            new LookTargets(pawn)
        );
    }
}
