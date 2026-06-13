using BaseLib.Utils;
using System;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;


[Pool(typeof(BugCatcherCardPool))]
public sealed class Swarm() : PokeModCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    private static readonly Random _random = new Random();
    private int? _debuffIndex = null;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8m, ValueProp.Move),
        new PowerVar<StrengthPower>(2m),
        new PowerVar<PoisonPower>(5m),
        new PowerVar<WeakPower>(3m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<PoisonPower>(),
        HoverTipFactory.FromPower<WeakPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        _debuffIndex ??= _random.Next(0, 3);

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .Execute(choiceContext);

        switch (_debuffIndex)
        {
            case 0:
                await PowerCmd.Apply<StrengthPower>(choiceContext, base.CombatState.HittableEnemies, -base.DynamicVars.Strength.BaseValue, base.Owner.Creature, this);
                break;
            case 1:
                await PowerCmd.Apply<PoisonPower>(choiceContext, base.CombatState.HittableEnemies, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this);
                break;
            case 2:
                await PowerCmd.Apply<WeakPower>(choiceContext, base.CombatState.HittableEnemies, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this);
                break;
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(3m);
    }
}