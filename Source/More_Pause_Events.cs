using System.Collections.Generic;

using Harmony;

using HugsLib;
using HugsLib.Settings;

using Verse;

namespace MorePauseEvents
{
    public class MorePauseEvents : ModBase
    {
        public static string ID = "MorePauseEvents";
        public static int DaysInTicks = 60000;

        public override string ModIdentifier => ID;

        public override void DefsLoaded()
        {
            Settings.GetHandle("PauseDebug", "PauseDebug.DisplayName".Translate(), "PauseDebug.Description".Translate(), false);

            // Mental breaks
            var PausePredator = Settings.GetHandle("PausePredator", "PausePredator.DisplayName".Translate(), "PausePredator.Description".Translate(), false);
            var PausePredatorLetter = Settings.GetHandle("PausePredatorLetter", "PausePredatorLetter.DisplayName".Translate(), "PausePredatorLetter.Description".Translate(), false);

            Settings.GetHandle("PauseFoodBinge", "PauseFoodBinge.DisplayName".Translate(), "PauseFoodBinge.Description".Translate(), false);
            Settings.GetHandle("PauseInsulting", "PauseInsulting.DisplayName".Translate(), "PauseInsulting.Description".Translate(), false);

            Settings.GetHandle("PauseCorpseObsession", "PauseCorpseObsession.DisplayName".Translate(), "PauseCorpseObsession.Description".Translate(), true);
            Settings.GetHandle("PauseBerserk", "PauseBerserk.DisplayName".Translate(), "PauseBerserk.Description".Translate(), false);
            Settings.GetHandle("PauseSadisticRage", "PauseSadisticRage.DisplayName".Translate(), "PauseSadisticRage.Description".Translate(), true);

            Settings.GetHandle("PauseCatatonia", "PauseCatatonia.DisplayName".Translate(), "PauseCatatonia.Description".Translate(), false);
            Settings.GetHandle("PauseGaveUp", "PauseGaveUp.DisplayName".Translate(), "PauseGaveUp.Description".Translate(), true);
            Settings.GetHandle("PauseJailBreak", "PauseJailBreak.DisplayName".Translate(), "PauseJailBreak.Description".Translate(), true);
            Settings.GetHandle("PauseMurderousRage", "PauseMurderousRage.DisplayName".Translate(), "PauseMurderousRage.Description".Translate(), true);
            Settings.GetHandle("PauseRunWild", "PauseRunWild.DisplayName".Translate(), "PauseRunWild.Description".Translate(), true);
            Settings.GetHandle("PauseSlaughter", "PauseSlaughter.DisplayName".Translate(), "PauseSlaughter.Description".Translate(), true);

            Settings.GetHandle("PauseConfusion", "PauseConfusion.DisplayName".Translate(), "PauseConfusion.Description".Translate(), false);
            Settings.GetHandle("PauseSocialFight", "PauseSocialFight.DisplayName".Translate(), "PauseSocialFight.Description".Translate(), false);

            // Events
            Settings.GetHandle("PauseTransportCrash", "PauseTransportCrash.DisplayName".Translate(), "PauseTransportCrash.Description".Translate(), true);
            Settings.GetHandle("PauseMaddened", "PauseMaddened.DisplayName".Translate(), "PauseMaddened.Description".Translate(), true);

            var PauseIdle = Settings.GetHandle("PauseIdle", "PauseIdle.DisplayName".Translate(), "PauseIdle.Description".Translate(), false);
            var PauseIdleDelay = Settings.GetHandle("PauseIdleDelay", "PauseIdleDelay.DisplayName".Translate(), "PauseIdleDelay.Description".Translate(), 8, Validators.IntRangeValidator(1, 24));
            var PauseIdleJoy = Settings.GetHandle("PauseIdleJoy", "PauseIdleJoy.DisplayName".Translate(), "PauseIdleJoy.Description".Translate(), false);
            var PauseIdleJoyDelay = Settings.GetHandle("PauseIdeJoyDelay", "PauseIdleJoyDelay.DisplayName".Translate(), "PauseIdleJoyDelay.Description".Translate(), 8, Validators.IntRangeValidator(1, 24));


            PausePredatorLetter.VisibilityPredicate = () => PausePredator.Value;
            PauseIdleDelay.VisibilityPredicate = () => PauseIdle.Value;
            PauseIdleJoyDelay.VisibilityPredicate = () => PauseIdleJoy.Value;
        }

        public static void LogMessage(string message) => Log.Message(string.Format("[{0}] {1}", ID, message));
        public static void LogDebugMessage(string message) => Log.Message(string.Format("[{0}][DEBUG] {1}", ID, message));
        public static SettingHandle<bool> GetBoolSetting(string setting) => HugsLibController.SettingsManager.GetModSettings(ID).GetHandle<bool>(setting);
        public static SettingHandle<int> GetIntSetting(string setting) => HugsLibController.SettingsManager.GetModSettings(ID).GetHandle<int>(setting);


        // Patches
        [HarmonyPatch(typeof(RimWorld.JobDriver_PredatorHunt), "CheckWarnPlayer")]
        public static class JobDriver_PredatorHunt__CheckWarnPlayer_Postfix
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

                    if (GetBoolSetting("PauseDebug"))
                    {
                        LogDebugMessage(pawn.Label);
                        LogDebugMessage(pawn.Spawned.ToString());

                        LogDebugMessage("Detected predator event!");
                        LogDebugMessage(string.Format("Predator > Label:{0},Spawned:{1}", pawn.Label, pawn.Spawned.ToString()));
                        LogDebugMessage(string.Format("Prey > Label:{0},IsAnimal:{1},Spawned:{2}", prey.Label, prey.RaceProps.Animal, prey.Spawned.ToString()));
                        LogDebugMessage(string.Format("Ticks > TicksGame:{0},LastPredatorTick:{1}", Find.TickManager.TicksGame, pawn.mindState.lastPredatorHuntingPlayerNotificationTick));
                        LogDebugMessage(string.Format("Is in horizontal range: {0}", prey.Position.InHorDistOf(pawn.Position, 60f)));
                    }

                    if (prey.Spawned && prey.Faction == RimWorld.Faction.OfPlayer && Find.TickManager.TicksGame == pawn.mindState.lastPredatorHuntingPlayerNotificationTick && prey.Position.InHorDistOf(pawn.Position, 60f))
                    {
                        LogMessage("New predator hunt detected; pausing game...");
                        Find.TickManager.Pause();

                        if (prey.RaceProps.Animal && RimWorld.PawnUtility.ShouldSendNotificationAbout(prey) && GetBoolSetting("PausePredatorLetter").Value)
                        {
                            Find.LetterStack.ReceiveLetter("PausePredator.LetterLabel".Translate(pawn.LabelShort, prey.LabelDefinite(), pawn.Named("PREDATOR"), prey.Named("PREY")).CapitalizeFirst(), "PausePredator.LetterText".Translate(pawn.LabelIndefinite(), prey.LabelDefinite(), pawn.Named("PREDATOR"), prey.Named("PREY")).CapitalizeFirst(), RimWorld.LetterDefOf.NegativeEvent, new LookTargets(pawn));
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_BingeFood), "TryGiveJob")]
        public static class JobGiver_BingeFood__TryGiveJob_Postfix
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                if (__result != null && __result.CanBeginNow(pawn))
                {
                    if (GetBoolSetting("PauseFoodBinge") && Find.TickManager.TicksGame >= (cached + DaysInTicks))
                    {
                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Colonist binging on food; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_InsultingSpree), "TryGiveJob")]
        public static class JobGiver_InsultingSpree__TryGiveJob_Postfix
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null && __result.CanBeginNow(pawn))
                {
                    TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                    if (GetBoolSetting("PauseInsulting") && Find.TickManager.TicksGame >= (cached + DaysInTicks))
                    {
                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Colonist on insulting spree; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        // Major mental breaks
        [HarmonyPatch(typeof(RimWorld.JobGiver_HaulCorpseToPublicPlace), "TryGiveJob")]
        public static class JobGiver_HaulCorpseTo_PublicPlace__TryGiveJob_Postfix
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null && __result.CanBeginNow(pawn))
                {
                    TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                    if (GetBoolSetting("PauseCorpseObsession") && Find.TickManager.TicksGame >= (cached + DaysInTicks))
                    {
                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Colonist hauling corpse to public place; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_SadisticRageTantrum), "PostStart")]
        public static class MentalState_SadisticRageTantrum__PostStart_Postfix
        {
            [HarmonyPostfix]
            public static void PostStart(Verse.AI.MentalState_SadisticRageTantrum __instance)
            {
                if (GetBoolSetting("PauseSadisticRage") && __instance.Age <= 150)
                {
                    LogMessage("Colonist on sadistic rage; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_Berserk), "TryGiveJob")]
        public static class JobGiver_Berserk__TryGiveJob_Postfix
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null && __result.CanBeginNow(pawn))
                {
                    TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                    if (GetBoolSetting("PauseBerserk") && Find.TickManager.TicksGame >= (cached + DaysInTicks))
                    {
                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Colonist going berserk; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker_Catatonic), "TrySendLetter")]
        public static class MentalBreakWorker_Catatonic__TrySendLetter_Postfix
        {
            [HarmonyPostfix]
            public static void TrySendLetter(ref bool __result)
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
        public static class MentalState_GiveUpExit__PostStart_Postfix
        {
            [HarmonyPostfix]
            public static void PostStart(ref Verse.AI.MentalState_GiveUpExit __instance)
            {
                if (GetBoolSetting("PauseGaveUp") && __instance.Age <= 150)
                {
                    LogMessage("Colonist giving up; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_Jailbreaker), "PostStart")]
        public static class MentalState_Jailbreaker__PostStart_Postfix
        {
            [HarmonyPostfix]
            public static void PostStart(ref Verse.AI.MentalState_Jailbreaker __instance)
            {
                if (GetBoolSetting("PauseJailBreak") && __instance.Age <= 150)
                {
                    LogMessage("Colonist encouraging prison break; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_MurderousRage), "PostStart")]
        public static class MentalState_MurderousRage__PostStart_Postfix
        {
            [HarmonyPostfix]
            public static void PostStart(ref Verse.AI.MentalState_MurderousRage __instance)
            {
                if (GetBoolSetting("PauseMurderousRage") && __instance.Age <= 150)
                {
                    LogMessage("Colonist on murderous rage; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalBreakWorker_RunWild), "TrySendLetter")]
        public static class MentalBreakWorker_RunWild__TrySendLetter_Postfix
        {
            [HarmonyPostfix]
            public static void TrySendLetter(ref bool __result)
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
        public static class MentalState_Slaughterer__PostStart_Postfix
        {
            [HarmonyPostfix]
            public static void PostStart(ref Verse.AI.MentalState_Slaughterer __instance)
            {
                if (GetBoolSetting("PauseSlaughter") && __instance.Age <= 150)
                {
                    LogMessage("Colonist on slaughter spree; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.MentalState_WanderConfused), "PostStart")]
        public static class MentalState_WanderConfused__PostStart_Postfix
        {
            [HarmonyPostfix]
            public static void PostStart(ref Verse.AI.MentalState_WanderConfused __instance)
            {
                if (GetBoolSetting("PauseConfusion") && __instance.Age <= 150)
                {
                    LogMessage("Colonist confused; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_SocialFighting), "TryGiveJob")]
        public static class JobGiver_SocialFighting__TryGiveJob_Postfix
        {
            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (GetBoolSetting("PauseSocialFight") && __result.CanBeginNow(pawn))
                {
                    LogMessage("Colonists in social fight; pausing game...");
                    Find.TickManager.Pause();
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.IncidentWorker_TransportPodCrash), "TryExecuteWorker")]
        public static class IncidentWorker_TransportPodCrash__TryExecuteWorker_Postfix
        {
            [HarmonyPostfix]
            public static void TryExecuteWorker(ref bool __result)
            {
                if (__result)
                {
                    if (GetBoolSetting("PauseTransportCrash"))
                    {
                        LogMessage("Transport pod crashed; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_Manhunter), "TryGiveJob")]
        public static class JobGiver_Manhunter__TryGiveJob_Postfix
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null && GetBoolSetting("PauseMaddened"))
                {
                    TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                    if ((Find.TickManager.TicksGame - cached) >= 2500)
                    {

                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Manhunter detected; pausing game...");
                        Find.TickManager.Pause();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Verse.AI.JobGiver_Idle), "TryGiveJob")]
        public static class JobGiver_Idle_Postfix_TryGiveJob
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null && GetBoolSetting("PauseIdle"))
                {
                    TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                    if ((Find.TickManager.TicksGame - cached) >= (GetIntSetting("PauseIdleDelay").Value * 2500))
                    {
                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Idle pawn detected; pausing game...");
                        Find.TickManager.Pause();

                        LogMessage("Sending idle pawn notification...");
                        Find.LetterStack.ReceiveLetter("PauseIdle.LetterLabel".Translate(), "PauseIdle.LetterText".Translate(pawn.LabelShort).CapitalizeFirst(), RimWorld.LetterDefOf.NeutralEvent, new LookTargets(pawn));
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RimWorld.JobGiver_IdleJoy), "TryGiveJob")]
        public static class JobGiver_IdleJoy_Postfix_TryGiveJob
        {
            private static readonly Dictionary<string, int> TickCache = new Dictionary<string, int>();

            [HarmonyPostfix]
            public static void TryGiveJob(Pawn pawn, ref Verse.AI.Job __result)
            {
                if (__result != null && __result.CanBeginNow(pawn))
                {
                    TickCache.TryGetValue(pawn.GetUniqueLoadID(), out int cached);

                    if (GetBoolSetting("PauseIdleJoy") && (Find.TickManager.TicksGame - cached) >= (GetIntSetting("PauseIdleJoyDelay").Value * 2500))
                    {
                        TickCache[pawn.GetUniqueLoadID()] = Find.TickManager.TicksGame;

                        LogMessage("Idle joy pawn detected; pausing game...");
                        Find.TickManager.Pause();

                        LogMessage("Send idle joy pawn notification...");
                        Find.LetterStack.ReceiveLetter("PauseIdle.LetterLabel".Translate(), "PauseIdle.LetterText".Translate(pawn.LabelShort).CapitalizeFirst(), RimWorld.LetterDefOf.NeutralEvent, new LookTargets(pawn));
                    }
                }
            }
        }
    }
}