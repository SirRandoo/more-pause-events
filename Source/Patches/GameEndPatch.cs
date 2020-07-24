using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace SirRandoo.MPE.Patches
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
