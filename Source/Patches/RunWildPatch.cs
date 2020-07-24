using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(MentalBreakWorker_RunWild), "TryStart")]
    public static class RunWildPatch
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void TryStart(Pawn pawn)
        {
            if (!Settings.RunWildEnabled)
            {
                return;
            }

            if (pawn?.Spawned != true)
            {
                return;
            }

            if (pawn.kindDef != PawnKindDefOf.WildMan)
            {
                return;
            }

            if (pawn.Faction != null)
            {
                return;
            }

            Find.TickManager.Pause();
        }
    }
}
