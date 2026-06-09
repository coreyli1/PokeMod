using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;







[Pool(typeof(BugCatcherCardPool))]
public sealed class BurmySandy() : PokeModCard(1, CardType.Attack,
    CardRarity.Uncommon, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        PokeModCode.Keywords.Evolve,
        CardKeyword.Exhaust
    ];    
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<DexterityPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(1m),
        new CalculationExtraVar(1m),
        new CalculatedVar("Dexterity").WithMultiplier((CardModel card, Creature? _) => card.Owner.PlayerCombatState.AllCards.Count((CardModel c) => c.Tags.Contains(PokeModCode.Tags.Evolve)))
    ];
    /*
    protected override IEnumerable<DynamicVar> CanonicalVars => MakeCalculatedVar("Dexterity", 0, Calc);
//or if you want to use other vars too
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(1, ValueProp.Move), ..MakeCalculatedVar("Dexterity", 0, Calc)];

    private decimal Calc(CardModel card, Creature? _)
        => card.Owner.PlayerCombatState?.AllCards.Count(c => c.Tags.Contains(PokeModCode.Tags.Evolve));

    */
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        var value = ((CalculatedVar)DynamicVars["Dexterity"]).Calculate(null);
        await PowerCmd.Apply<DexterityPower>(choiceContext, base.Owner.Creature, value, base.Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}