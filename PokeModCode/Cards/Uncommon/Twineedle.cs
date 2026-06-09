using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
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
public sealed class Twineedle() : PokeModCard(1, CardType.Skill,
    CardRarity.Uncommon, TargetType.Self)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [


    ];    
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [

    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(1),
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        CardModel selection = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 1), context: choiceContext, player: base.Owner, filter: delegate(CardModel c)
        {
            CardType type = c.Type;
            return (type == CardType.Attack || type == CardType.Power) ? true : false;
        }, source: this)).FirstOrDefault();
        if (selection != null)
        {
            for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
            {
                CardModel card = selection.CreateClone();
                await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, base.Owner);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1);
    }
}