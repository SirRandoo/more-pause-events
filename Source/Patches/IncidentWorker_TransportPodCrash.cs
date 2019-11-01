using Harmony;

using Verse;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.IncidentWorker_TransportPodCrash), "TryExecuteWorker")]
    public static class IncidentWorker_TransportPodCrash
    {
        [HarmonyPostfix]
        public static void TryExecuteWorker()
        {
            if (!Settings.TransportCrashEnabled) return;


            MPE.Info("A transport pod crashed; pausing the game...");
            Find.TickManager.Pause();
        }
    }
}
