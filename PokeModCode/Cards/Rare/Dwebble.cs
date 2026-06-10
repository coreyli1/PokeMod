using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.HoverTips;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;




[Pool(typeof(BugCatcherCardPool))]
public sealed class Dwebble() : PokeModCard(1, CardType.Skill,
    CardRarity.Common, TargetType.Self)
{
    public const int incrementAmount = 5;

    private const string _incrementAmountKey = "IncrementAmount";
    
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        PokeModCode.Keywords.Evolve,
    ];    
    

    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [

    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RollingBoulderPower>(5m),
        new DynamicVar("IncrementAmount", 5m)
    ];
    
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<RollingBoulderPower>(choiceContext, base.Owner.Creature, base.DynamicVars["RollingBoulderPower"].IntValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["RollingBoulderPower"].UpgradeValueBy(5m);
    }
}