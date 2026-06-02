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
public sealed class Tackle() : PokeModCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {

        DynamicVars.Damage.UpgradeValueBy(3m);    

    }

}