using BaseLib.Abstracts;
using PokeMod.PokeModCode.Extensions;
using Godot;

namespace PokeMod.PokeModCode.Character;

public class PokeModPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => BugCatcher.Color;
    

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}