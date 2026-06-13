using BaseLib.Utils;
using System.Linq;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using PokeMod.PokeModCode.Character;

namespace PokeMod.PokeModCode.Cards;


[Pool(typeof(BugCatcherCardPool))]
public sealed class CompoundEyes() : PokeModCard(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(4)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
        foreach (var enemy in base.CombatState.HittableEnemies)
        {
            var debuffs = enemy.Powers
                .Where(p => p.TypeForCurrentAmount == PowerType.Debuff)
                .ToList();
            foreach (var debuff in debuffs)
            {
                await PowerCmd.Remove(debuff);
            }
        }
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}