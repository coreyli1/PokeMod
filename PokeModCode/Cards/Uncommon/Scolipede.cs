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
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;







[Pool(typeof(BugCatcherCardPool))]
public sealed class Scolipede() : PokeModCard(2, CardType.Attack,
    CardRarity.Event, TargetType.AllEnemies)
{

    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        PokeModCode.Keywords.Evolve,
    ];    
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<PoisonPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(12m, ValueProp.Move),
        new PowerVar<PoisonPower>(3m)
    ];
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);
        await PowerCmd.Apply<PoisonPower>(choiceContext, base.CombatState.HittableEnemies, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(4m);
    }
}