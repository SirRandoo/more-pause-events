using System;

using Harmony;

using Verse;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(RimWorld.PrisonBreakUtility), "StartPrisonBreak", new Type[] { typeof(Pawn) })]
    public static class PrisonBreakUtility
    {
        [HarmonyPostfix]
        public static void StartPrisonBreak(Pawn initiator)
        {
            if (!Settings.JailBreakEnabled) return;
            if (initiator == null) return;

            MPE.Info(string.Format("Pawn {0} induced a prison break; pausing the game...", initiator.LabelDefinite()));
            Find.TickManager.Pause();
        }
    }
}
