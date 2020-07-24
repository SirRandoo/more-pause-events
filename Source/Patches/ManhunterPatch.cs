using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(MentalState_Manhunter), "PostStart")]
    public static class ManhunterPatch
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void PostStart(MentalState_Manhunter __instance)
        {
            if (!Settings.MadAnimalEnabled)
            {
                return;
            }

            if (__instance?.pawn?.Spawned != true)
            {
                return;
            }

            if (Mpe.Cache.Manhunter.Contains(__instance.pawn.GetUniqueLoadID()))
            {
                return;
            }

            Mpe.Cache.Manhunter.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }
}
