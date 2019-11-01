using UnityEngine;

using Verse;

namespace SirRandoo.MPE
{
    public class Settings : ModSettings
    {
        private static Vector2 ScrollPos = Vector2.zero;

        public static bool CacheEnabled = true;

        public static bool IdleLettersEnabled = false;
        public static bool PredatorLettersEnabled = false;
        public static bool SocialFightLettersEnabled = false;


        public static bool BerserkEnabled = false;
        public static bool CatatoniaEnabled = false;
        public static bool ConfusionEnabled = false;
        public static bool CorpseObsessionEnabled = false;
        public static bool EdibleBingesEnabled = false;
        public static bool GiveUpEnabled = true;
        public static bool IdleEnabled = false;
        public static bool InsultEnabled = false;
        public static bool JailBreakEnabled = false;
        public static bool MadAnimalEnabled = false;
        public static bool MurderousRageEnabled = true;
        public static bool PredatorEnabled = false;
        public static bool RunWildEnabled = false;
        public static bool SadisticRageEnabled = false;
        public static bool SlaughterEnabled = false;
        public static bool SocialFightEnabled = false;
        public static bool TransportCrashEnabled = true;




        public static void DoWindowContents(Rect canvas)
        {
            var panel = new Listing_Standard();

            Rect view = new Rect(0f, 0f, canvas.width, 36f * 26f);
            view.xMax *= 0.9f;

            panel.BeginScrollView(canvas, ref ScrollPos, ref view);


            panel.Label("MPE.Settings.Groups.Cache".Translate());
            panel.GapLine();
            panel.Gap();
            panel.CheckboxLabeled("MPE.Settings.Cache.Label".Translate(), ref CacheEnabled, "MPE.Settings.Cache.Tooltip".Translate());


            panel.Gap();
            panel.Label("MPE.Settings.Groups.Letters".Translate());
            panel.GapLine();
            panel.CheckboxLabeled("MPE.Settings.Letters.Idle.Label".Translate(), ref IdleLettersEnabled, "MPE.Settings.Letters.Idle.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Letters.Predator.Label".Translate(), ref PredatorLettersEnabled, "MPE.Settings.Letters.Predator.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Letters.SocialFight.Label".Translate(), ref SocialFightLettersEnabled, "MPE.Settings.Letters.SocialFight.Tooltip".Translate());


            panel.Gap();
            panel.Label("MPE.Settings.Groups.Events".Translate());
            panel.GapLine();
            panel.CheckboxLabeled("MPE.Settings.Events.Berserk.Label".Translate(), ref BerserkEnabled, "MPE.Settings.Events.Berserk.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.Catatonia.Label".Translate(), ref CatatoniaEnabled, "MPE.Settings.Events.Catatonia.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.Confusion.Label".Translate(), ref ConfusionEnabled, "MPE.Settings.Events.Confusion.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.CorpseObsession.Label".Translate(), ref CorpseObsessionEnabled, "MPE.Settings.Events.CorpseObsession.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.EdibleBinges.Label".Translate(), ref EdibleBingesEnabled, "MPE.Settings.Events.EdibleBinges.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.GiveUp.Label".Translate(), ref GiveUpEnabled, "MPE.Settings.Events.GiveUp.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.Idle.Label".Translate(), ref IdleEnabled, "MPE.Settings.Events.Idle.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.Insult.Label".Translate(), ref InsultEnabled, "MPE.Settings.Events.Insult.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.JailBreak.Label".Translate(), ref JailBreakEnabled, "MPE.Settings.Events.JailBreak.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.MadAnimal.Label".Translate(), ref MadAnimalEnabled, "MPE.Settings.Events.MadAnimal.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.MurderousRage.Label".Translate(), ref MurderousRageEnabled, "MPE.Settings.Events.MurderousRage.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.Predator.Label".Translate(), ref PredatorEnabled, "MPE.Settings.Events.Predator.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.RunWild.Label".Translate(), ref RunWildEnabled, "MPE.Settings.Events.RunWild.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.SadisticRage.Label".Translate(), ref SadisticRageEnabled, "MPE.Settings.Events.SadisticRage.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.Slaughter.Label".Translate(), ref SlaughterEnabled, "MPE.Settings.Events.Slaughter.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.SocialFight.Label".Translate(), ref SocialFightEnabled, "MPE.Settings.Events.SocialFight.Tooltip".Translate());
            panel.CheckboxLabeled("MPE.Settings.Events.TransportCrash.Label".Translate(), ref TransportCrashEnabled, "MPE.Settings.Events.TransportCrash.Tooltip".Translate());


            panel.EndScrollView(ref view);
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look(ref CacheEnabled, "cache", true);

            Scribe_Values.Look(ref IdleLettersEnabled, "idleLetters", false);
            Scribe_Values.Look(ref PredatorLettersEnabled, "predatorLetters", false);
            Scribe_Values.Look(ref SocialFightEnabled, "socialFightLetters", false);

            Scribe_Values.Look(ref BerserkEnabled, "berserk", false);
            Scribe_Values.Look(ref CatatoniaEnabled, "catatonia", false);
            Scribe_Values.Look(ref ConfusionEnabled, "confusion", false);
            Scribe_Values.Look(ref CorpseObsessionEnabled, "corpseObsession", false);
            Scribe_Values.Look(ref EdibleBingesEnabled, "edibleBinge", false);
            Scribe_Values.Look(ref GiveUpEnabled, "giveUp", true);
            Scribe_Values.Look(ref IdleEnabled, "idle", false);
            Scribe_Values.Look(ref InsultEnabled, "insult", false);
            Scribe_Values.Look(ref JailBreakEnabled, "jailBreak", false);
            Scribe_Values.Look(ref MadAnimalEnabled, "madAnimal", true);
            Scribe_Values.Look(ref MurderousRageEnabled, "murderousRage", true);
            Scribe_Values.Look(ref PredatorEnabled, "predator", false);
            Scribe_Values.Look(ref RunWildEnabled, "runWild", true);
            Scribe_Values.Look(ref SadisticRageEnabled, "sadisticRage", false);
            Scribe_Values.Look(ref SlaughterEnabled, "slaughterer", false);
            Scribe_Values.Look(ref SocialFightEnabled, "socialFight", false);
            Scribe_Values.Look(ref TransportCrashEnabled, "transportPodCrash", true);
        }
    }
}
