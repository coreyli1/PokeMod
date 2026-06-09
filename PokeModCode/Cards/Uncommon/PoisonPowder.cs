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
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;




[Pool(typeof(BugCatcherCardPool))]
public sealed class PoisonPowder() : PokeModCard(2,CardType.Skill,
    CardRarity.Uncommon, TargetType.AllEnemies)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

        CardKeyword.Exhaust
    ];    
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [

    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PoisonPower>(4m),
        new PowerVar<WeakPower>(2)
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, hittableEnemy, DynamicVars.Weak.BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<PoisonPower>(choiceContext, hittableEnemy, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}