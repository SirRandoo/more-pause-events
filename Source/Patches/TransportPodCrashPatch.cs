using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld.QuestGen;
using Verse;

namespace SirRandoo.MPE.Patches;

[HarmonyPatch]
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal static class TransportPodCrashPatch
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(QuestNode_Root_WandererJoin), name: "RunInt");
    }

    private static void Postfix()
    {
        Find.TickManager.Pause();
    }
}
