using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using PokeMod.PokeModCode.Character;
using PokeMod.PokeModCode.Powers;

namespace PokeMod.PokeModCode.Cards;






[Pool(typeof(BugCatcherCardPool))]
public sealed class TailGlow() : PokeModCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    private const string _powers = "Powers";
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        PokeModCode.Keywords.Evolve,
    ];    

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Powers", 1m),
        new DamageVar(13, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await PowerCmd.Apply<TailGlowPower>(choiceContext, base.Owner.Creature, base.DynamicVars["Powers"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}