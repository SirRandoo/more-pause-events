using Harmony;

using RimWorld;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.JobGiver_MurderousRage), "TryGiveJob")]
    public static class JobGiver_MurderousRage
    {
        [HarmonyPostfix]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (__result == null || pawn == null) return;
            if (!pawn.Spawned) return;
            if (__result.def != JobDefOf.AttackMelee) return;
            if (!Settings.MurderousRageEnabled) return;
            if (!PawnUtility.ShouldSendNotificationAbout(pawn)) return;
            if (!(pawn.MentalState is MentalState_MurderousRage)) return;

            Pawn victim = (pawn.MentalState as MentalState_MurderousRage).target;

            if (victim == null) return;
            if (victim != (__result.targetA.Thing as Pawn)) return;


            MPE.Info(string.Format("Pawn {0} is on a murderous rage; pausing the game...", pawn.LabelDefinite()));
            Find.TickManager.Pause();
        }
    }
}
