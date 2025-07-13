using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Verse;

namespace SirRandoo.MPE;

[UsedImplicitly]
[StaticConstructorOnStartup]
public static class MpeStatic
{
    static MpeStatic()
    {
        if (LoadedModManager.RunningModsListForReading.Any(i => i.Name.Equals("PredatorHuntAlert")))
        {
            Logging.Info("PredatorHuntAlert is loaded, disabling MPE's predator alerts.");
            Mpe.IsPhaLoaded = true;
        }

        Mpe.Harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}

[UsedImplicitly]
public class Mpe : Mod
{
    public const string Id = "MorePauseEvents";
    internal static Cache Cache = new();
    internal static Harmony Harmony;

    public Mpe(ModContentPack content) : base(content)
    {
        Harmony = new Harmony("com.sirrandoo.mpe");
        GetSettings<Settings>();
    }

    internal static bool IsPhaLoaded { get; set; }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Settings.DoWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return Id;
    }
}
