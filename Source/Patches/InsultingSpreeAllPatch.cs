using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SirRandoo.PauseEvents.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(MentalState_InsultingSpreeAll), "PostStart")]
    public static class InsultingSpreeAllPatch
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void PostStart(MentalState_InsultingSpreeAll __instance)
        {
            if (!Settings.InsultEnabled)
            {
                return;
            }

            if (__instance?.pawn?.Spawned != true)
            {
                return;
            }

            if (__instance.Age > 150)
            {
                return;
            }

            if (Mpe.Cache.Insulting.Contains(__instance.pawn.GetUniqueLoadID()))
            {
                return;
            }

            Mpe.Cache.Insulting.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }
}
