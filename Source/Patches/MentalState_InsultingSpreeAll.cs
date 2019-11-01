using Harmony;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState_InsultingSpreeAll), "PostStart")]
    public static class MentalState_InsultingSpreeAll__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState_InsultingSpreeAll __instance)
        {
            if (!Settings.InsultEnabled) return;
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (__instance.Age > 150) return;
            if (MPE.Cache.Insulting.Contains(__instance.pawn.GetUniqueLoadID())) return;


            MPE.Info(string.Format("Pawn {0} is on an insulting spree; pausing the game...", __instance.pawn.LabelDefinite()));

            MPE.Cache.Insulting.Add(__instance.pawn.GetUniqueLoadID());
            Find.TickManager.Pause();
        }
    }
}
