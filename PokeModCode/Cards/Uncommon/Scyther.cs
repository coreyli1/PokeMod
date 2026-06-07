using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PokeMod.PokeModCode.Character;
using PokeMod.PokeModCode.Powers;

namespace PokeMod.PokeModCode.Cards;







[Pool(typeof(BugCatcherCardPool))]
public sealed class Scyther() : PokeModCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

    ];    

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Attacks", 1m)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(PokeModCode.Keywords.Evolve)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "PowerUp", base.Owner.Character.PowerUpAnimDelay);
        await PowerCmd.Apply<ScytherPower>(choiceContext, base.Owner.Creature, base.DynamicVars["Attacks"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}