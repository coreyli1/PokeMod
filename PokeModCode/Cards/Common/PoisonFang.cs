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
public sealed class PoisonFang() : PokeModCard(2,CardType.Skill,
    CardRarity.Common, TargetType.AnyEnemy)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [

        CardKeyword.Retain
    ];    
    
    public override bool GainsBlock => true;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        //HoverTipFactory.FromPower<PlatingPower>()
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PoisonPower>(8m)
    ];
    
    
    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        VfxCmd.PlayOnCreatureCenter(play.Target, "vfx/vfx_bite");
        await PowerCmd.Apply<PoisonPower>(choiceContext, play.Target, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Poison.UpgradeValueBy(3m);
    }
}