using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;
using PokeMod.PokeModCode.Character;
using PokeMod.PokeModCode.RestSite;


namespace PokeMod.PokeModCode.Relics;

[Pool(typeof(BugCatcherRelicPool))]
public class Pokedex() : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    
    public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
    {
        if (player != base.Owner)
        {
            return false;
        }

        options.Add(new EvolveRestSiteOption(player));
        return true;
    }
    
    public override bool ShouldDisableRemainingRestSiteOptions(Player player)
    {
        if (player != base.Owner)
        {
            return true;
        }
        Flash();
        return false;
    }


    
}