using MegaCrit.Sts2.Core.Entities.Powers;

namespace PokeMod.PokeModCode.Powers;



public class EvolvePower() : PokeModPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    //aftercardplayed
    
    //onupgrade
}