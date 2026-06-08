using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;




[Pool(typeof(BugCatcherCardPool))]
public sealed class Lunge() : PokeModCard(2,CardType.Attack,
    CardRarity.Uncommon, TargetType.AnyEnemy)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

    ];    
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<StrengthPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(18m, ValueProp.Move),
        new DynamicVar("StrengthLoss", 1m)
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {

        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await PowerCmd.Apply<StrengthPower>(choiceContext, play.Target, -base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["StrengthLoss"].UpgradeValueBy(2m);
    }
}