using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace PokeMod.PokeModCode;

//You're recommended but not required to keep all your code in this package and all your assets in the PokeMod folder.
[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "PokeMod"; //At the moment, this is used only for the Logger and harmony names.
    public const string ResPath = $"res://{ModId}";

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        //If you want to use scripts defined in your mod for Godot scenes, uncomment the following line.
        //Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly());
     
        Harmony harmony = new(ModId);

        harmony.PatchAll();
    }
}
