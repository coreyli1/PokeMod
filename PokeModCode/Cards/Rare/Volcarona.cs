using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;







[Pool(typeof(BugCatcherCardPool))]
public sealed class Volcarona() : PokeModCard(2,CardType.Attack,
    CardRarity.Event, TargetType.AllEnemies)
{
    
    protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { PokeModCode.Tags.Evolve };
    //add evolve keyword
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

    ];    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(24m, ValueProp.Move),
        new DynamicVar("DexterityLoss", 2m)
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        await PowerCmd.Apply<DexterityPower>(choiceContext, base.Owner.Creature, -base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, this);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
            .WithHitCount(2)
            .Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(4m);
        base.DynamicVars["DexterityLoss"].UpgradeValueBy(-1m);
    }
    
}