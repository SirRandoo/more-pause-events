using Harmony;

using HugsLib;
using HugsLib.Settings;

using Verse;

namespace MorePauseEvents
{
    public class MorePauseEvents : ModBase
    {
        public static string ID = "MorePauseEvents";
        public override string ModIdentifier => ID;

        public override void DefsLoaded()
        {
            // Mental breaks
            Settings.GetHandle<bool>("PausePredator", "PausePredator.DisplayName".Translate(), "PausePredator.Description".Translate(), false);

            Settings.GetHandle<bool>("PauseFoodBinge", "PauseFoodBinge.DisplayName".Translate(), "PauseFoodBinge.Description".Translate(), false);
            Settings.GetHandle<bool>("PauseInsulting", "PauseInsulting.DisplayName".Translate(), "PauseInsulting.Description".Translate(), false);

            Settings.GetHandle<bool>("PauseCorpseObsession", "PauseCorpseObsession.DisplayName".Translate(), "PauseCorpseObsession.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseSadisticRage", "PauseSadisticRage.DisplayName".Translate(), "PauseSadisticRage.Description".Translate(), true);

            Settings.GetHandle<bool>("PauseBerserk", "PauseBerserk.DisplayName".Translate(), "PauseBerserk.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseCatatonia", "PauseCatatonia.DisplayName".Translate(), "PauseCatatonia.Description".Translate(), false);
            Settings.GetHandle<bool>("PauseGaveUp", "PauseGaveUp.DisplayName".Translate(), "PauseGaveUp.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseJailBreak", "PauseJailBreak.DisplayName".Translate(), "PauseJailBreak.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseMurderousRage", "PauseMurderousRage.DisplayName".Translate(), "PauseMurderousRage.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseRunWild", "PauseRunWild.DisplayName".Translate(), "PauseRunWild.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseSlaughter", "PauseSlaughter.DisplayName".Translate(), "PauseSlaughter.Description".Translate(), true);

            Settings.GetHandle<bool>("PauseConfusion", "PauseConfusion.DisplayName".Translate(), "PauseConfusion.Description".Translate(), false);
            Settings.GetHandle<bool>("PauseSocialFight", "PauseSocialFight.DisplayName".Translate(), "PauseSocialFight.Description".Translate(), false);

            // Events
            Settings.GetHandle<bool>("PauseTransportCrash", "PauseTransportCrash.DisplayName".Translate(), "PauseTransportCrash.Description".Translate(), true);
            Settings.GetHandle<bool>("PauseMaddened", "PauseMaddened.DisplayName".Translate(), "PauseMaddened.Description".Translate(), true);
        }

        public static void LogMessage(string message) => Log.Message(string.Format("[{0}] {1}", ID, message));
        public static SettingHandle<bool> GetBoolSetting(string setting) => HugsLibController.SettingsManager.GetModSettings(ID).GetHandle<bool>(setting);

        // Patches
        [HarmonyPatch(typeof(RimWorld.JobDriver_PredatorHunt), "CheckWarnPlayer")]
        public static class PredatorHunt_Postfix_CheckWarnPlayer
        {
            [HarmonyPostfix]
            public static void CheckWarnPlayer(RimWorld.JobDriver_PredatorHunt __instance)
            {
                if (GetBoolSetting("PausePredator"))

                {
                    // This could probably be omitted, but better safe than sorry
                    Traverse i = Traverse.Create(__instance);
                    Pawn prey = i.Property("Prey").GetValue<Pawn>();
                    Pawn pawn = i.Field("pawn").GetValue<Pawn>();

                    if (prey.Spawned && prey.Faction == RimWorld.Faction.OfPlayer && Find.TickManager.TicksGame == pawn.mindState.lastPredatorHuntingPlayerNotificationTick && prey.Position.InHorDistOf(pawn.Position, 60f))
                    {
                        LogMessage("New predator hunt detected; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_BingeFood), "TryGiveJob")]
        public static class BingeFood_Postfix_TryGiveJob
        {
            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null)
                {
                    if (GetBoolSetting("PauseFoodBinge"))

                    {
                        LogMessage("Colonist binging on food; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_InsultingSpree), "TryGiveJob")]
        public static class InsultingSpree_Postfix_TryGiveJob
        {
            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null)
                {
                    if (GetBoolSetting("PauseInsulting"))
                    {
                        LogMessage("Colonist on insulting spree; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        // Major mental breaks
        [HarmonyPatch(typeof(RimWorld.JobGiver_HaulCorpseToPublicPlace), "TryGiveJob")]
        public static class HaulCorpseToPublicPlace_Postfix_TryGiveJob
        {
            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null)
                {
                    if (GetBoolSetting("PauseCorpseObsession"))
                    {
                        LogMessage("Colonist hauling corpse to public place; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_SadisticRageTantrum), "PostStart")]
        public static class MentalState_SadisticRageTantrum_Prefix_PostStart
        {
            [HarmonyPrefix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseSadisticRage"))
                {
                    LogMessage("Colonist on sadistic rage; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_Berserk), "TryGiveJob")]
        public static class JobGiver_Berserk_Postfix_TryGiveJob
        {
            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null)
                {
                    if (GetBoolSetting("PauseBerserk"))
                    {
                        LogMessage("Colonist going berserk; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker_Catatonic), "TrySendLetter")]
        public static class MentalBreakWorker_Catatonic_Postfix_TrySendLetter
        {
            [HarmonyPostfix]
            public static void TrySendLetter(Pawn pawn, string textKey, string reason, ref bool __result)
            {
                if (__result)
                {
                    if (GetBoolSetting("PauseCatatonia"))
                    {
                        LogMessage("Colonist entering catatonia; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_GiveUpExit), "PostStart")]
        public static class MentalState_GiveUpExit_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseGaveUp"))
                {
                    LogMessage("Colonist giving up; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_Jailbreaker), "PostStart")]
        public static class MentalState_Jailbreaker_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseJailBreak"))
                {
                    LogMessage("Colonist encouraging prison break; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_MurderousRage), "PostStart")]
        public static class MentalState_MurderousRage_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseMurderousRage"))
                {
                    LogMessage("Colonist on murderous rage; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker_RunWild), "TrySendLetter")]
        public static class MentalBreakWorker_RunWild_Postfix_TrySendLetter
        {
            [HarmonyPostfix]
            public static void TrySendLetter(Pawn pawn, string textKey, string reason, ref bool __result)
            {
                if (__result)
                {
                    if (GetBoolSetting("PauseRunWild"))
                    {
                        LogMessage("Colonist running wild; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_Slaughterer), "PostStart")]
        public static class MentalState_Slaughterer_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseSlaughter"))
                {
                    LogMessage("Colonist on slaughter spree; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_WanderConfused), "PostStart")]
        public static class MentalState_WanderConfused_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseConfusion"))
                {
                    LogMessage("Colonist confused; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_SocialFighting), "PostStart")]
        public static class MentalState_SocialFighting_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                if (GetBoolSetting("PauseSocialFight"))
                {
                    LogMessage("Colonists in social fight; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.IncidentWorker_TransportPodCrash), "TryExecuteWorker")]
        public static class IncidentWorker_TransportPodCrash_Postfix_TryExecuteWorker
        {
            [HarmonyPostfix]
            public static void TryExecuteWorker(RimWorld.IncidentParms parms, ref bool __result)
            {
                if (__result)
                {
                    if (GetBoolSetting("PauseTransportCrash"))
                        LogMessage("Transport pod crashed; pausing game...");
                        Find.TickManager.Pause();
        [HarmonyPatch(typeof(RimWorld.JobGiver_Manhunter), "TryGiveJob")]
        public static class JobGiver_Manhunter_Postfix_TryGiveJob
        {
            public static int TickCache = -1;

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null)
                {
                    if (GetBoolSetting("PauseMaddened") && (Find.TickManager.TicksGame - TickCache) >= 2500)
                    {
                        TickCache = Find.TickManager.TicksGame;

                        LogMessage("Manhunter detected; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }
                    }
                }
            }
        }
    }
}