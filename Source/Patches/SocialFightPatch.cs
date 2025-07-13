using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using Verse.AI;

namespace SirRandoo.MPE.Patches;

[UsedImplicitly]
[HarmonyPatch(typeof(MentalState_SocialFighting), methodName: "PostEnd")]
public static class SocialFightPatch
{
    [UsedImplicitly]
    [HarmonyPostfix]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void PostEnd(MentalState_SocialFighting __instance)
    {
        if (!Settings.SocialFightEnabled) return;
        if (__instance?.pawn is not { Spawned: true }) return;
        if (!Mpe.Cache.SocialFight.Contains(__instance.pawn.GetUniqueLoadID())) return;

        Mpe.Cache.SocialFight.Remove(__instance.pawn.GetUniqueLoadID());
    }
}
