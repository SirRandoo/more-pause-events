using Harmony;

using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches
{
    [HarmonyPatch(typeof(MentalState), "PostStart")]
    public static class MentalState__PostStart
    {
        [HarmonyPostfix]
        public static void PostStart(MentalState __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;
            if (__instance.Age > 150) return;

            string PID = __instance.pawn.GetUniqueLoadID();
            string message = "Pawn " + __instance.pawn.LabelDefinite() + " {0}; pausing the game...";

            if (Settings.SlaughterEnabled && __instance is MentalState_Slaughterer && !MPE.Cache.Slaughter.Contains(PID))
            {
                MPE.Info(string.Format(message, "is on a slaughter spree"));

                MPE.Cache.Slaughter.Add(PID);
                Find.TickManager.Pause();
            }
            else if (Settings.ConfusionEnabled && __instance is MentalState_WanderConfused && !MPE.Cache.WanderConfused.Contains(PID))
            {
                MPE.Info(string.Format(message, "is wandering around confused"));

                MPE.Cache.WanderConfused.Add(PID);
                Find.TickManager.Pause();
            }
            else if (Settings.BerserkEnabled && __instance is MentalState_Berserk && !MPE.Cache.Berserk.Contains(PID))
            {
                MPE.Info(string.Format(message, "has gone berserk"));

                MPE.Cache.Berserk.Add(PID);
                Find.TickManager.Pause();
            }
            else if (Settings.GiveUpEnabled && __instance is MentalState_GiveUpExit)
            {
                MPE.Info(string.Format(message, "has given up on the colony"));
                Find.TickManager.Pause();
            }
            else if (Settings.InsultEnabled && (__instance is MentalState_InsultingSpree || __instance is MentalState_TargetedInsultingSpree) && !MPE.Cache.Insulting.Contains(PID))
            {
                MPE.Info(string.Format(message, "is on an insulting spree"));

                MPE.Cache.Insulting.Add(PID);
                Find.TickManager.Pause();
            }
            else if (Settings.SocialFightEnabled && __instance is MentalState_SocialFighting && !MPE.Cache.SocialFight.Contains(PID))
            {
                var conv = __instance as MentalState_SocialFighting;

                MPE.Info(string.Format(message, "started a social fight with " + conv.otherPawn.LabelDefinite()));

                MPE.Cache.Insulting.Add(PID);
                Find.TickManager.Pause();


                if (!MPE.Cache.Insulting.Contains(conv.otherPawn.GetUniqueLoadID())) MPE.Cache.Insulting.Add(conv.otherPawn.GetUniqueLoadID());
            }

            MPE.Debug(__instance.def.defName);
        }
    }

    [HarmonyPatch(typeof(MentalState), "PostEnd")]
    public static class MentalState__PostEnd
    {
        [HarmonyPostfix]
        public static void PostEnd(MentalState __instance)
        {
            if (__instance == null) return;
            if (__instance.pawn == null) return;
            if (!__instance.pawn.Spawned) return;

            string PID = __instance.pawn.GetUniqueLoadID();

            if (__instance is MentalState_Slaughterer && MPE.Cache.Slaughter.Contains(PID))
            {
                MPE.Cache.Slaughter.Remove(PID);
            }
            else if (__instance is MentalState_WanderConfused && MPE.Cache.WanderConfused.Contains(PID))
            {
                MPE.Cache.WanderConfused.Remove(PID);
            }
            else if (__instance is MentalState_Berserk && MPE.Cache.Berserk.Contains(PID))
            {
                MPE.Cache.Berserk.Remove(PID);
            }
            else if ((__instance is MentalState_InsultingSpree || __instance is MentalState_TargetedInsultingSpree || __instance is MentalState_InsultingSpreeAll) && MPE.Cache.Insulting.Contains(PID))
            {
                MPE.Cache.Insulting.Remove(PID);
            }
        }
    }
}
