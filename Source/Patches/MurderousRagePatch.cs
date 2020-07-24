using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(JobGiver_MurderousRage), "TryGiveJob")]
    public static class MurderousRagePatch
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void TryGiveJob(Pawn pawn, Job __result)
        {
            if (!Settings.MurderousRageEnabled)
            {
                return;
            }

            if (!pawn?.Spawned != true)
            {
                return;
            }

            if (__result?.targetA.Thing != null)
            {
                return;
            }

            if (__result?.def != JobDefOf.AttackMelee)
            {
                return;
            }

            if (!PawnUtility.ShouldSendNotificationAbout(pawn))
            {
                return;
            }

            if (!(pawn.MentalState is MentalState_MurderousRage murderousRage))
            {
                return;
            }

            if (murderousRage.target != null && murderousRage.target != __result?.targetA.Thing as Pawn)
            {
                return;
            }

            Find.TickManager.Pause();
        }
    }
}
