using UnityEngine;
using Verse;

namespace SirRandoo.MPE
{
    public class Settings : ModSettings
    {
        // Events
        public static bool BerserkEnabled;

        public static bool CatatoniaEnabled;
        public static bool ConfusionEnabled;
        public static bool CorpseObsessionEnabled;
        public static bool EdibleBingesEnabled;
        public static bool GiveUpEnabled = true;
        public static bool IdleEnabled;

        // Letters
        public static bool IdleLettersEnabled;

        public static bool InsultEnabled;
        public static bool JailBreakEnabled;
        public static bool MadAnimalEnabled;
        public static bool MurderousRageEnabled = true;
        public static bool PredatorEnabled;
        public static bool PredatorLettersEnabled;
        public static bool RunWildEnabled;
        public static bool SadisticRageEnabled;
        public static bool SlaughterEnabled;
        public static bool SocialFightEnabled;
        public static bool SocialFightLettersEnabled;
        public static bool TransportCrashEnabled = true;
        private static Vector2 _scrollPos = Vector2.zero;

        public static void DoWindowContents(Rect canvas)
        {
            GUI.BeginGroup(canvas);
            var panel = new Listing_Standard();
            var view = new Rect(0f, 0f, canvas.width - 16f, Text.LineHeight * 19f);

            panel.BeginScrollView(new Rect(0f, 0f, canvas.width, canvas.height), ref _scrollPos, ref view);

            panel.Gap();
            panel.Label("MPE.Settings.Groups.Letters".TranslateSimple());
            panel.GapLine();
            panel.CheckboxLabeled(
                "MPE.Settings.Letters.Idle.Label".TranslateSimple(),
                ref IdleLettersEnabled,
                "MPE.Settings.Letters.Idle.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Letters.Predator.Label".TranslateSimple(),
                ref PredatorLettersEnabled,
                "MPE.Settings.Letters.Predator.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Letters.SocialFight.Label".TranslateSimple(),
                ref SocialFightLettersEnabled,
                "MPE.Settings.Letters.SocialFight.Tooltip".TranslateSimple()
            );

            panel.Gap();
            panel.Label("MPE.Settings.Groups.Events".TranslateSimple());
            panel.GapLine();
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Berserk.Label".TranslateSimple(),
                ref BerserkEnabled,
                "MPE.Settings.Events.Berserk.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Catatonia.Label".TranslateSimple(),
                ref CatatoniaEnabled,
                "MPE.Settings.Events.Catatonia.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Confusion.Label".TranslateSimple(),
                ref ConfusionEnabled,
                "MPE.Settings.Events.Confusion.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.CorpseObsession.Label".TranslateSimple(),
                ref CorpseObsessionEnabled,
                "MPE.Settings.Events.CorpseObsession.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.EdibleBinges.Label".TranslateSimple(),
                ref EdibleBingesEnabled,
                "MPE.Settings.Events.EdibleBinges.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.GiveUp.Label".TranslateSimple(),
                ref GiveUpEnabled,
                "MPE.Settings.Events.GiveUp.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Idle.Label".TranslateSimple(),
                ref IdleEnabled,
                "MPE.Settings.Events.Idle.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Insult.Label".TranslateSimple(),
                ref InsultEnabled,
                "MPE.Settings.Events.Insult.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.JailBreak.Label".TranslateSimple(),
                ref JailBreakEnabled,
                "MPE.Settings.Events.JailBreak.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.MadAnimal.Label".TranslateSimple(),
                ref MadAnimalEnabled,
                "MPE.Settings.Events.MadAnimal.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.MurderousRage.Label".TranslateSimple(),
                ref MurderousRageEnabled,
                "MPE.Settings.Events.MurderousRage.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Predator.Label".TranslateSimple(),
                ref PredatorEnabled,
                "MPE.Settings.Events.Predator.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.RunWild.Label".TranslateSimple(),
                ref RunWildEnabled,
                "MPE.Settings.Events.RunWild.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.SadisticRage.Label".TranslateSimple(),
                ref SadisticRageEnabled,
                "MPE.Settings.Events.SadisticRage.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.Slaughter.Label".TranslateSimple(),
                ref SlaughterEnabled,
                "MPE.Settings.Events.Slaughter.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.SocialFight.Label".TranslateSimple(),
                ref SocialFightEnabled,
                "MPE.Settings.Events.SocialFight.Tooltip".TranslateSimple()
            );
            panel.CheckboxLabeled(
                "MPE.Settings.Events.TransportCrash.Label".TranslateSimple(),
                ref TransportCrashEnabled,
                "MPE.Settings.Events.TransportCrash.Tooltip".TranslateSimple()
            );

            GUI.EndGroup();
            panel.EndScrollView(ref view);
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref IdleLettersEnabled, "idleLetters");
            Scribe_Values.Look(ref PredatorLettersEnabled, "predatorLetters");
            Scribe_Values.Look(ref SocialFightEnabled, "socialFightLetters");

            Scribe_Values.Look(ref BerserkEnabled, "berserk");
            Scribe_Values.Look(ref CatatoniaEnabled, "catatonia");
            Scribe_Values.Look(ref ConfusionEnabled, "confusion");
            Scribe_Values.Look(ref CorpseObsessionEnabled, "corpseObsession");
            Scribe_Values.Look(ref EdibleBingesEnabled, "edibleBinge");
            Scribe_Values.Look(ref GiveUpEnabled, "giveUp", true);
            Scribe_Values.Look(ref IdleEnabled, "idle");
            Scribe_Values.Look(ref InsultEnabled, "insult");
            Scribe_Values.Look(ref JailBreakEnabled, "jailBreak");
            Scribe_Values.Look(ref MadAnimalEnabled, "madAnimal", true);
            Scribe_Values.Look(ref MurderousRageEnabled, "murderousRage", true);
            Scribe_Values.Look(ref PredatorEnabled, "predator");
            Scribe_Values.Look(ref RunWildEnabled, "runWild", true);
            Scribe_Values.Look(ref SadisticRageEnabled, "sadisticRage");
            Scribe_Values.Look(ref SlaughterEnabled, "slaughterer");
            Scribe_Values.Look(ref SocialFightEnabled, "socialFight");
            Scribe_Values.Look(ref TransportCrashEnabled, "transportPodCrash", true);
        }
    }
}
