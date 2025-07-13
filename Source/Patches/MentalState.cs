using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SirRandoo.MPE.Patches;

[UsedImplicitly]
[HarmonyPatch(typeof(MentalState), methodName: "PostEnd")]
public static class MentalStatePostEndPatch
{
    [UsedImplicitly]
    [HarmonyPostfix]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void PostEnd(MentalState __instance)
    {
        if (__instance?.pawn?.Spawned != true) return;

        string PID = __instance.pawn.GetUniqueLoadID();

        switch (__instance)
        {
            case MentalState_Slaughterer _ when Mpe.Cache.Slaughter.Contains(PID):
                Mpe.Cache.Slaughter.Remove(PID);

                break;
            case MentalState_WanderConfused _ when Mpe.Cache.WanderConfused.Contains(PID):
                Mpe.Cache.WanderConfused.Remove(PID);

                break;
            case MentalState_Berserk _ when Mpe.Cache.Berserk.Contains(PID):
                Mpe.Cache.Berserk.Remove(PID);

                break;
            case MentalState_InsultingSpree _ when Mpe.Cache.Insulting.Contains(PID):
                Mpe.Cache.Insulting.Remove(PID);

                break;
            case MentalState_Manhunter _ when Mpe.Cache.Manhunter.Contains(PID):
                Mpe.Cache.Manhunter.Remove(PID);

                break;
            case MentalState_SadisticRageTantrum _ when Mpe.Cache.SadisticRage.Contains(PID):
                Mpe.Cache.SadisticRage.Remove(PID);

                break;
            case MentalState_Jailbreaker _ when Mpe.Cache.Jailbreaker.Contains(PID):
            {
                Mpe.Cache.Jailbreaker.Remove(PID);

                break;
            }
        }
    }
}

[UsedImplicitly]
[HarmonyPatch(typeof(MentalState), methodName: "PostStart")]
public static class MentalStatePostStartPatch
{
    [UsedImplicitly]
    [HarmonyPostfix]
    [SuppressMessage(category: "ReSharper", checkId: "InconsistentNaming")]
    public static void PostStart(MentalState __instance)
    {
        if (__instance?.pawn?.Spawned != true) return;

        if (__instance.Age > 150) return;

        string PID = __instance.pawn.GetUniqueLoadID();
        var shouldPause = false;

        if (Settings.SlaughterEnabled && __instance is MentalState_Slaughterer && !Mpe.Cache.Slaughter.Contains(PID))
        {
            Mpe.Cache.Slaughter.Add(PID);
            shouldPause = true;
        }
        else if (Settings.ConfusionEnabled && __instance is MentalState_WanderConfused && !Mpe.Cache.WanderConfused.Contains(PID))
        {
            Mpe.Cache.WanderConfused.Add(PID);
            shouldPause = true;
        }
        else if (Settings.BerserkEnabled && __instance is MentalState_Berserk && !Mpe.Cache.Berserk.Contains(PID))
        {
            Mpe.Cache.Berserk.Add(PID);
            shouldPause = true;
        }
        else if (Settings.GiveUpEnabled && __instance is MentalState_GiveUpExit)
        {
            shouldPause = true;
        }
        else if (Settings.InsultEnabled && __instance is MentalState_InsultingSpree && !Mpe.Cache.Insulting.Contains(PID))
        {
            Mpe.Cache.Insulting.Add(PID);
            shouldPause = true;
        }
        else if (Settings.SocialFightEnabled && __instance is MentalState_SocialFighting conv && !Mpe.Cache.SocialFight.Contains(PID))
        {
            Mpe.Cache.SocialFight.Add(PID);
            shouldPause = true;

            if (Settings.SocialFightLettersEnabled)
            {
                var assailant = conv.pawn.Named("ASSAILANT");
                var victim = conv.otherPawn.Named("VICTIM");

                Find.LetterStack.ReceiveLetter(
                    "Letters.SocialFight.Label".Translate(assailant, victim),
                    "Letters.SocialFight.Body".Translate(assailant, victim),
                    LetterDefOf.ThreatSmall,
                    new LookTargets(conv.pawn, conv.otherPawn)
                );
            }

            if (!Mpe.Cache.SocialFight.Contains(conv.otherPawn.GetUniqueLoadID())) Mpe.Cache.SocialFight.Add(conv.otherPawn.GetUniqueLoadID());
        }
        else if (Settings.JailBreakEnabled && __instance is MentalState_Jailbreaker && !Mpe.Cache.Jailbreaker.Contains(PID))
        {
            Mpe.Cache.Jailbreaker.Add(PID);
            shouldPause = true;
        }


        if (!shouldPause) return;

        Find.TickManager.Pause();
    }
}