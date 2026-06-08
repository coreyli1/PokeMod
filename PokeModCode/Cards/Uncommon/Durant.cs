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
public sealed class Durant() : PokeModCard(1, CardType.Skill,
    CardRarity.Uncommon, TargetType.AnyEnemy)
{

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<PlatingPower>(),
        HoverTipFactory.FromPower<DexterityPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0m),
        new ExtraDamageVar(1m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, Creature? _) =>
        {
            return card.Owner.Creature.GetPowerAmount<DexterityPower>() + card.Owner.Creature.GetPowerAmount<PlatingPower>();
        })
    ];
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int num = ResolveEnergyXValue();
        if (base.IsUpgraded)
        {
            num++;
        }
        await DamageCmd.Attack(base.DynamicVars.CalculatedDamage).WithHitCount(num).FromCard(this).Targeting(play.Target).Execute(choiceContext);
    }


}