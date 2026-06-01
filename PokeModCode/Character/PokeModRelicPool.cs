using BaseLib.Abstracts;
using PokeMod.PokeModCode.Extensions;
using Godot;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;

public class PokeModRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => BugCatcher.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}