using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;





[Pool(typeof(BugCatcherCardPool))]
public sealed class Ninjask() : PokeModCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        PokeModCode.Keywords.Evolve,
    ];    

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(3)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PlatingPower>(),
        HoverTipFactory.FromKeyword(PokeModCode.Keywords.Evolve)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        
        // if plating exists 
        /*if (play.Target.HasPower<PlatingPower>())
        {

        }*/
        var amt = Owner.Creature.GetPowerAmount<PlatingPower>()*2;
        await PowerCmd.Apply<PlatingPower>(choiceContext, base.Owner.Creature, amt, base.Owner.Creature, this);
        
        //need a power that takes the current plating if it exists and doubles it
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}