using BaseLib.Utils;
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
public sealed class Beautifly() : PokeModCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{

    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromKeyword(PokeModCode.Keywords.Evolve)
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(4, ValueProp.Move),
        new PowerVar<WeakPower>("WeakPower", 2)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target, DynamicVars.Weak.BaseValue,
            Owner.Creature, this);



    }
    
    


    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(2m);       

    }

}