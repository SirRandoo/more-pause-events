using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SirRandoo.MPE.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(IncidentWorker_TransportPodCrash), "TryExecuteWorker")]
    public static class TransportPodCrashPatch
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void TryExecuteWorker()
        {
            if (!Settings.TransportCrashEnabled)
            {
                return;
            }

            Find.TickManager.Pause();
        }
    }
}
