using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches;

[UsedImplicitly]
[HarmonyPatch(typeof(MentalState_TantrumRandom), methodName: "PostStart")]
public static class SadisticRagePatch
{
    [UsedImplicitly]
    [HarmonyPostfix]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void PostStart(MentalState_TantrumRandom __instance)
    {
        if (!Settings.SadisticRageEnabled) return;
        if (__instance?.target == null || __instance.pawn == null) return;
        if (!__instance.pawn.Spawned) return;
        if (__instance.pawn.MentalState is not MentalState_SadisticRageTantrum) return;
        if (__instance.Age > 150) return;
        if (Mpe.Cache.SadisticRage.Contains(__instance.pawn.GetUniqueLoadID())) return;

        Mpe.Cache.SadisticRage.Add(__instance.pawn.GetUniqueLoadID());
        Find.TickManager.Pause();
    }
}
