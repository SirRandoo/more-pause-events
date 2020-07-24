using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(JobGiver_HaulCorpseToPublicPlace), "TryGiveJob")]
    public static class CorpseObsessionPatch
    {
        private static int _snapshot = -1;

        [UsedImplicitly]
        [HarmonyPostfix]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (!Settings.CorpseObsessionEnabled || Find.TickManager.TicksGame - _snapshot < 60)
            {
                return;
            }

            if (!pawn?.Spawned ?? true)
            {
                return;
            }

            if (!__result?.CanBeginNow(pawn) ?? true)
            {
                return;
            }

            if (__result.def != JobDefOf.HaulCorpseToPublicPlace)
            {
                return;
            }

            Find.TickManager.Pause();
            _snapshot = Find.TickManager.TicksGame;
        }
    }
}
