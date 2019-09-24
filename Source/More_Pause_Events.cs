using Harmony;

using HugsLib;
using HugsLib.Settings;

using Verse;

namespace More_Pause_Events
{
    public class More_Pause_Events : ModBase
    {
        public override string ModIdentifier => "MorePauseEvents";

        // Settings
        // Misc
        private SettingHandle<bool> PausePredator;

        // Minor mental breaks
        private SettingHandle<bool> PauseFoodBinge;
        private SettingHandle<bool> PauseInsulting;

        // Major mental breaks
        private SettingHandle<bool> PauseCorpseObsession;
        private SettingHandle<bool> PauseSadisticRage;

        // Extreme mental breaks
        private SettingHandle<bool> PauseBerserk;
        private SettingHandle<bool> PauseCatatonia;
        private SettingHandle<bool> PauseGaveUp;
        private SettingHandle<bool> PauseJailBreak;
        private SettingHandle<bool> PauseMurderousRage;
        private SettingHandle<bool> PauseRunWild;
        private SettingHandle<bool> PauseSlaughter;

        // Special mental breaks
        private SettingHandle<bool> PauseConfusion;
        private SettingHandle<bool> PauseSocialFight;

        public override void DefsLoaded()
        {
            PausePredator = Settings.GetHandle<bool>("PausePredator", "PausePredator.DisplayName".Translate(), "PausePredator.Description".Translate(), false);

            PauseFoodBinge = Settings.GetHandle<bool>("PauseFoodBinge", "PauseFoodBinge.DisplayName".Translate(), "PauseFoodBinge.Description".Translate(), false);
            PauseInsulting = Settings.GetHandle<bool>("PauseInsulting", "PauseInsulting.DisplayName".Translate(), "PauseInsulting.Description".Translate(), false);

            PauseCorpseObsession = Settings.GetHandle<bool>("PauseCorpseObsession", "PauseCorpseObsession.DisplayName".Translate(), "PauseCorpseObsession.Description".Translate(), true);
            PauseSadisticRage = Settings.GetHandle<bool>("PauseSadisticRage", "PauseSadisticRage.DisplayName".Translate(), "PauseSadisticRage.Description".Translate(), true);

            PauseBerserk = Settings.GetHandle<bool>("PauseBerserk", "PauseBerserk.DisplayName".Translate(), "PauseBerserk.Description".Translate(), true);
            PauseCatatonia = Settings.GetHandle<bool>("PauseCatatonia", "PauseCatatonia.DisplayName".Translate(), "PauseCatatonia.Description".Translate(), false);
            PauseGaveUp = Settings.GetHandle<bool>("PauseGaveUp", "PauseGaveUp.DisplayName".Translate(), "PauseGaveUp.Description".Translate(), true);
            PauseJailBreak = Settings.GetHandle<bool>("PauseJailBreak", "PauseJailBreak.DisplayName".Translate(), "PauseJailBreak.Description".Translate(), true);
            PauseMurderousRage = Settings.GetHandle<bool>("PauseMurderousRage", "PauseMurderousRage.DisplayName".Translate(), "PauseMurderousRage.Description".Translate(), true);
            PauseRunWild = Settings.GetHandle<bool>("PauseRunWild", "PauseRunWild.DisplayName".Translate(), "PauseRunWild.Description".Translate(), true);
            PauseSlaughter = Settings.GetHandle<bool>("PauseSlaughter", "PauseSlaughter.DisplayName".Translate(), "PauseSlaughter.Description".Translate(), true);

            PauseConfusion = Settings.GetHandle<bool>("PauseConfusion", "PauseConfusion.DisplayName".Translate(), "PauseConfusion.Description".Translate(), false);
            PauseSocialFight = Settings.GetHandle<bool>("PauseSocialFight", "PauseSocialFight.DisplayName".Translate(), "PauseSocialFight.Description".Translate(), false);
        }


        // Patches
        [HarmonyPatch(typeof(RimWorld.JobDriver_PredatorHunt), "CheckWarnPlayer")]
        public static class PredatorHunt_Postfix_CheckWarnPlayer
        {
            [HarmonyPostfix]
            public static void CheckWarnPlayer(RimWorld.JobDriver_PredatorHunt __instance)
            {
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PausePredator");

                if (enabled)
                {
                    // This could probably be omitted, but better safe than sorry
                    Traverse i = Traverse.Create(__instance);
                    Pawn prey = i.GetValue<Pawn>("Prey");
                    Pawn pawn = i.GetValue<Pawn>("pawn");

                    if (prey.Spawned && prey.Faction == RimWorld.Faction.OfPlayer && Find.TickManager.TicksGame <= pawn.mindState.lastPredatorHuntingPlayerNotificationTick + 2500 && prey.Position.InHorDistOf(pawn.Position, 60f))
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseFoodBinge");

                    if (enabled)
                    {
                        HugsLibController
                            .Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseInsulting");

                    if (enabled)
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseCorpseObsession");

                    if (enabled)
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseSadisticRage");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseBerserk");

                    if (enabled)
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseCatatonia");

                    if (enabled)
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseGaveUp");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_Jailbreaker), "PostStart")]
        public static class MentalState_Jailbreaker_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseJailBreak");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_MurderousRage), "PostStart")]
        public static class MentalState_MurderousRage_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseMurderousRage");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseRunWild");

                    if (enabled)
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
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
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseSlaughter");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_WanderConfused), "PostStart")]
        public static class MentalState_WanderConfused_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseConfusion");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_SocialFighting), "PostStart")]
        public static class MentalState_SocialFighting_Postfix_PostStart
        {
            [HarmonyPostfix]
            public static void PostStart()
            {
                SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseSocialFight");

                if (enabled)
                {
                    HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_Manhunter), "TryGiveJob")]
        public static class JobGiver_Manhunter_Postfix_TryGiveJob
        {
            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null)
                {
                    SettingHandle<bool> enabled = HugsLibController.SettingsManager.GetModSettings("MorePauseEvents").GetHandle<bool>("PauseManhunter");

                    if (enabled)
                    {
                        HugsLibController.Instance.DoLater.DoNextTick(() => Find.TickManager.Pause());
                    }
                }
            }
        }
    }
}