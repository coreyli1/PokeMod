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
public sealed class FuryCutter() : PokeModCard(1,CardType.Attack,
    CardRarity.Uncommon, TargetType.AnyEnemy)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

        // CardKeyword.Retain
    ];    
    
    private const string _increaseKey = "Increase";
    
    private decimal _extraDamageFromPlays;
    
    private decimal ExtraDamageFromPlays
    {
        get
        {
            return _extraDamageFromPlays;
        }
        set
        {
            AssertMutable();
            _extraDamageFromPlays = value;
        }
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(3m, ValueProp.Move),
        new DynamicVar("Increase", 2m)
        
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {

        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(2).FromCard(this)
            .Targeting(play.Target)
            .Execute(choiceContext);
        
        base.DynamicVars.Damage.BaseValue += base.DynamicVars["Increase"].BaseValue;
        ExtraDamageFromPlays += base.DynamicVars["Increase"].BaseValue;
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["Increase"].UpgradeValueBy(4m);
    }
}