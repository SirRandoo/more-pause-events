using System.Collections.Generic;
using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace SirRandoo.PauseEvents.Patches
{
    [UsedImplicitly]
    [HarmonyPatch(typeof(Game), nameof(Game.FinalizeInit))]
    public static class GameEndPatch
    {
        [UsedImplicitly]
        [HarmonyFinalizer]
        public static void Finalizer()
        {
            Mpe.Cache = new Cache();
        }
    }
}
