using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches;

[UsedImplicitly]
[HarmonyPatch(typeof(JobGiver_Binge), methodName: "TryGiveJob")]
internal class FoodBingePatch
{
    private static int _snapshot = -1;

    [UsedImplicitly]
    [HarmonyPostfix]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void TryGiveJob(Pawn pawn, Job __result, JobGiver_Binge __instance)
    {
        if (!Settings.EdibleBingesEnabled || Find.TickManager.TicksGame - _snapshot < 60) return;
        if (__instance is not JobGiver_BingeFood) return;
        if (pawn?.Spawned != true) return;
        if (!__result?.CanBeginNow(pawn) ?? true) return;
        if (__result.def != JobDefOf.Ingest) return;

        Find.TickManager.Pause();
        _snapshot = Find.TickManager.TicksGame;
    }
}
