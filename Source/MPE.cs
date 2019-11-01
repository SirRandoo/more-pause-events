using System.Reflection;

using Harmony;

using UnityEngine;

using Verse;

namespace SirRandoo.MPE
{
    public class MPE : Mod
    {
        public static string ID => "MorePauseEvents";
        public static Settings Settings;
        internal static HarmonyInstance Harmony;
        internal static Cache Cache = new Cache();

        public MPE(ModContentPack content) : base(content)
        {
            Harmony = HarmonyInstance.Create("com.sirrandoo.mpe");
            Harmony.PatchAll(Assembly.GetExecutingAssembly());

            Settings = GetSettings<Settings>();

            Info("Initialized!");
        }

        public override string SettingsCategory() => ID;
        public override void DoSettingsWindowContents(Rect inRect) => Settings.DoWindowContents(inRect);

        public static void Log(string Level, string Message) => Verse.Log.Message(string.Format("{0} {1} :: {2}", Level, ID, Message));
        public static void Info(string Message) => Log("INFO", Message);
        public static void Warn(string Message) => Log("WARN", Message);
        public static void Debug(string Message) => Log("DEBUG", Message);
        public static void Error(string Message) => Log("ERROR", Message);
    }
}
