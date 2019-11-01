using Harmony;

using RimWorld;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.JobGiver_Manhunter), "TryGiveJob")]
    public static class JobGiver_Manhunter
    {
        private static int Snapshot = -1;

        [HarmonyPostfix]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (__result == null || pawn == null) return;
            if (!pawn.Spawned) return;
            if (__result.def != JobDefOf.AttackMelee) return;
            if (!__result.CanBeginNow(pawn)) return;
            if (!Settings.MadAnimalEnabled) return;
            if ((Find.TickManager.TicksGame - Snapshot) < 60 && Settings.CacheEnabled) return;


            MPE.Debug(string.Format("{0} ticks passed since the last mad animal.", Find.TickManager.TicksGame - Snapshot));
            MPE.Info(string.Format("Pawn {0} has gone mad; pausing the game...", pawn.LabelDefinite()));

            Find.TickManager.Pause();
            if (Settings.CacheEnabled) Snapshot = Find.TickManager.TicksGame;
        }
    }
}
