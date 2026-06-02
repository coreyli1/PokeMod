using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;


[Pool(typeof(BugCatcherCardPool))]
public sealed class PinMissile() : PokeModCard(1,CardType.Attack,
    CardRarity.Common, TargetType.AnyEnemy)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

        // CardKeyword.Retain
    ];    
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        //HoverTipFactory.FromPower<PlatingPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(6m, ValueProp.Move)
        
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {

        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
            .Targeting(play.Target)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2m);
    }
}