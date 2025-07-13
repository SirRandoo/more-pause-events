using UnityEngine;
using Verse;

namespace SirRandoo.MPE;

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
    public static bool MurderousRageEnabled;
    public static bool PredatorEnabled;
    public static bool PredatorLettersEnabled;
    public static bool RunWildEnabled;
    public static bool SadisticRageEnabled;
    public static bool SlaughterEnabled;
    public static bool SocialFightEnabled;
    public static bool SocialFightLettersEnabled;
    public static bool TransportCrashEnabled;
    private static Vector2 _scrollPos = Vector2.zero;

    public static void DoWindowContents(Rect canvas)
    {
        GUI.BeginGroup(canvas);
        var panel = new Listing_Standard();
        var view = new Rect(x: 0f, y: 0f, canvas.width - 16f, Text.LineHeight * 19f);

        panel.Begin(canvas);
        _scrollPos = GUI.BeginScrollView(new Rect(x: 0f, y: 0f, canvas.width, canvas.height), _scrollPos, view);

        panel.Gap();
        panel.Label("MPE.Settings.Groups.Letters".TranslateSimple());
        panel.GapLine();
        panel.CheckboxLabeled("MPE.Settings.Letters.Idle.Label".TranslateSimple(), ref IdleLettersEnabled, "MPE.Settings.Letters.Idle.Tooltip".TranslateSimple());

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
        panel.CheckboxLabeled("MPE.Settings.Events.Berserk.Label".TranslateSimple(), ref BerserkEnabled, "MPE.Settings.Events.Berserk.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.Catatonia.Label".TranslateSimple(), ref CatatoniaEnabled, "MPE.Settings.Events.Catatonia.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.Confusion.Label".TranslateSimple(), ref ConfusionEnabled, "MPE.Settings.Events.Confusion.Tooltip".TranslateSimple());

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

        panel.CheckboxLabeled("MPE.Settings.Events.GiveUp.Label".TranslateSimple(), ref GiveUpEnabled, "MPE.Settings.Events.GiveUp.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.Idle.Label".TranslateSimple(), ref IdleEnabled, "MPE.Settings.Events.Idle.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.Insult.Label".TranslateSimple(), ref InsultEnabled, "MPE.Settings.Events.Insult.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.JailBreak.Label".TranslateSimple(), ref JailBreakEnabled, "MPE.Settings.Events.JailBreak.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.MadAnimal.Label".TranslateSimple(), ref MadAnimalEnabled, "MPE.Settings.Events.MadAnimal.Tooltip".TranslateSimple());

        panel.CheckboxLabeled(
            "MPE.Settings.Events.MurderousRage.Label".TranslateSimple(),
            ref MurderousRageEnabled,
            "MPE.Settings.Events.MurderousRage.Tooltip".TranslateSimple()
        );

        panel.CheckboxLabeled("MPE.Settings.Events.Predator.Label".TranslateSimple(), ref PredatorEnabled, "MPE.Settings.Events.Predator.Tooltip".TranslateSimple());
        panel.CheckboxLabeled("MPE.Settings.Events.RunWild.Label".TranslateSimple(), ref RunWildEnabled, "MPE.Settings.Events.RunWild.Tooltip".TranslateSimple());

        panel.CheckboxLabeled(
            "MPE.Settings.Events.SadisticRage.Label".TranslateSimple(),
            ref SadisticRageEnabled,
            "MPE.Settings.Events.SadisticRage.Tooltip".TranslateSimple()
        );

        panel.CheckboxLabeled("MPE.Settings.Events.Slaughter.Label".TranslateSimple(), ref SlaughterEnabled, "MPE.Settings.Events.Slaughter.Tooltip".TranslateSimple());

        panel.CheckboxLabeled(
            "MPE.Settings.Events.SocialFight.Label".TranslateSimple(),
            ref SocialFightEnabled,
            "MPE.Settings.Events.SocialFight.Tooltip".TranslateSimple()
        );

        GUI.EndGroup();
        panel.End();
        GUI.EndScrollView();
    }

    public override void ExposeData()
    {
        base.ExposeData();

        Scribe_Values.Look(ref IdleLettersEnabled, label: "idleLetters");
        Scribe_Values.Look(ref PredatorLettersEnabled, label: "predatorLetters");
        Scribe_Values.Look(ref SocialFightLettersEnabled, label: "socialFightLetters2");

        Scribe_Values.Look(ref BerserkEnabled, label: "berserk");
        Scribe_Values.Look(ref CatatoniaEnabled, label: "catatonia");
        Scribe_Values.Look(ref ConfusionEnabled, label: "confusion");
        Scribe_Values.Look(ref CorpseObsessionEnabled, label: "corpseObsession");
        Scribe_Values.Look(ref EdibleBingesEnabled, label: "edibleBinge");
        Scribe_Values.Look(ref GiveUpEnabled, label: "giveUp", defaultValue: true);
        Scribe_Values.Look(ref IdleEnabled, label: "idle");
        Scribe_Values.Look(ref InsultEnabled, label: "insult");
        Scribe_Values.Look(ref JailBreakEnabled, label: "jailBreak");
        Scribe_Values.Look(ref MadAnimalEnabled, label: "madAnimal", defaultValue: true);
        Scribe_Values.Look(ref MurderousRageEnabled, label: "murderousRage", defaultValue: true);
        Scribe_Values.Look(ref PredatorEnabled, label: "predator");
        Scribe_Values.Look(ref RunWildEnabled, label: "runWild", defaultValue: true);
        Scribe_Values.Look(ref SadisticRageEnabled, label: "sadisticRage");
        Scribe_Values.Look(ref SlaughterEnabled, label: "slaughterer");
        Scribe_Values.Look(ref SocialFightEnabled, label: "socialFight");
        Scribe_Values.Look(ref TransportCrashEnabled, label: "transportPodCrash", defaultValue: true);
    }
}
