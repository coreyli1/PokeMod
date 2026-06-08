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
public sealed class LeechierLife() : PokeModCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{

    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<DexterityPower>(),
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(14m, ValueProp.Move),
        new PowerVar<DexterityPower>(4m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        await PowerCmd.Apply<AnticipatePower>(choiceContext, base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this);



    }
    
    


    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(4m);

    }

}