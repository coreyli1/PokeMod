using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;



[Pool(typeof(BugCatcherCardPool))]
public sealed class Forretress() : PokeModCard(1, CardType.Skill,
    CardRarity.Event, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    
    //add evolve keyword

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<PlatingPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0),
        new CalculationExtraVar(2m),
        new CalculatedVar("Plating").WithMultiplier((CardModel card, Creature? _) => card.Owner.PlayerCombatState.AllCards.Count((CardModel c) => c.Tags.Contains(PokeModCode.Tags.Evolve))),
    ];
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var value = ((CalculatedVar)DynamicVars["Plating"]).Calculate(null);
        await PowerCmd.Apply<PlatingPower>(choiceContext, base.Owner.Creature, value, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.CalculationExtra.UpgradeValueBy(1m);
    }
}