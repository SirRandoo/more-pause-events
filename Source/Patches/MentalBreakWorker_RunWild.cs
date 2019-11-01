using Harmony;

using RimWorld;

using Verse;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker_RunWild), "TryStart")]
    public static class MentalBreakWorker_RunWild
    {
        [HarmonyPostfix]
        public static void TryStart(Pawn pawn)
        {
            if (pawn == null) return;
            if (pawn.kindDef != PawnKindDefOf.WildMan) return;
            if (pawn.Faction != null) return;
            if (!pawn.Spawned) return;
            if (!Settings.RunWildEnabled) return;


            MPE.Info(string.Format("Pawn {0} ran wild; pausing the game...", pawn.LabelDefinite()));
            Find.TickManager.Pause();
        }
    }
}
