using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;





[Pool(typeof(BugCatcherCardPool))]
public sealed class AttackOrder() : PokeModCard(2,CardType.Attack,
    CardRarity.Uncommon, TargetType.AllEnemies)
{
    
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(4m),
        new ExtraDamageVar(2m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, Creature? target) => target?.Powers.Count(ShouldCountPower) ?? 0)
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        
        await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
    }
    
    private static bool ShouldCountPower(PowerModel power)
    {
        if (power.TypeForCurrentAmount == PowerType.Debuff)
        {
            return !(power is ITemporaryPower);
        }
        return false;
    }
}