using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.HoverTips;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;



[Pool(typeof(BugCatcherCardPool))]
public sealed class Kricketune() : PokeModCard(1, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        PokeModCode.Keywords.Evolve,
    ];    
    
    public override bool GainsBlock => true;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        //HoverTipFactory.FromPower<PlatingPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("PlayerStrengthLoss", 2m),
        new EnergyVar(2)
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<StrengthPower>(choiceContext, base.Owner.Creature, -base.DynamicVars["PlayerStrengthLoss"].BaseValue, base.Owner.Creature, this);
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["PlayerStrengthLoss"].UpgradeValueBy(-1m);
    }
}