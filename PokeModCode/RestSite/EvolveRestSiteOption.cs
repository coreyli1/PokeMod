// sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MegaCrit.Sts2.Core.Entities.RestSite.EvolveRestSiteOption
using BaseLib.Abstracts;
using BaseLib.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using PokeMod.PokeModCode.Cards;




namespace PokeMod.PokeModCode.RestSite;


public class EvolveRestSiteOption : RestSiteOption
{
    private static readonly Dictionary<string, Func<CardModel>> EvolutionMap = new()
    {
        { "Wurmple", () => ModelDb.Card<Silcoon>() },
        { "Silcoon", () => ModelDb.Card<Beautifly>() },
        { "Spinarak", () => ModelDb.Card<Ariados>() }
    };
    
    private IEnumerable<CardModel>? _selection;

	public override string OptionId => "EVOLVE";
	
	public int EvolveCount { get; set; } = 1;

	public EvolveRestSiteOption(Player owner)
		: base(owner)
	{

	}
	
		public override LocString Description
    	{
    		get
    		{
    			LocString locString;
    			if (base.IsEnabled)
    			{
    				locString = new LocString("rest_site_ui", "OPTION_" + OptionId + ".description");
    				locString.Add("Count", EvolveCount);
    			}
    			else
    			{
    				locString = new LocString("rest_site_ui", "OPTION_" + OptionId + ".descriptionDisabled");
    			}
    			return locString;
    		}
    	}

public override async Task<bool> OnSelect()
{
    CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, EvolveCount)
    {
        Cancelable = true,
        RequireManualConfirmation = true
    };
    _selection = await CardSelectCmd.FromDeckGeneric(base.Owner, prefs, c => c.Keywords.Contains(Keywords.Evolve));

    if (!_selection.Any())
    {
        return false;
    }

    foreach (CardModel item in _selection)
    {
        if (EvolutionMap.TryGetValue(item.Title, out Func<CardModel>? getEvolution))
        {
            MainFile.Logger.Info(item.Title);
            CardModel evolved = (CardModel)base.Owner.RunState.CreateCard(getEvolution(), base.Owner);
            await CardCmd.Transform(item, evolved, CardPreviewStyle.HorizontalLayout);
        }
    }

    return true;
}

private CardModel Evolve(CardModel prevo, Func<CardModel> getEvolution, bool forPreview)
{
    CardModel cardModel = getEvolution();

    if (prevo.IsUpgraded && cardModel.IsUpgradable)
    {
        if (forPreview)
        {
            cardModel.UpgradeInternal();
        }
        else
        {
            CardCmd.Upgrade(cardModel);
        }
    }
    if (prevo.Enchantment != null)
    {
        EnchantmentModel enchantmentModel = (EnchantmentModel)prevo.Enchantment.MutableClone();
        if (enchantmentModel.CanEnchant(cardModel))
        {
            if (forPreview)
            {
                cardModel.EnchantInternal(enchantmentModel, enchantmentModel.Amount);
                enchantmentModel.ModifyCard();
            }
            else
            {
                CardCmd.Enchant(enchantmentModel, cardModel, enchantmentModel.Amount);
            }
        }
    }
    return cardModel;
}
	
/*
	public override Task DoLocalPostSelectVfx(CancellationToken ct = default(CancellationToken))
	{
		NDebugAudioManager.Instance.Play("sts_sfx_shovel_v1.mp3", 1f, PitchVariance.Small);
		return Task.CompletedTask;
	}

	public override Task DoRemotePostSelectVfx()
	{
		NDebugAudioManager.Instance?.Play("sts_sfx_shovel_v1.mp3", 0.5f, PitchVariance.Small);
		NRestSiteCharacter nRestSiteCharacter = NRestSiteRoom.Instance?.Characters.First((NRestSiteCharacter c) => c.Player == base.Owner);
		nRestSiteCharacter?.Shake();
		NRelicFlashVfx nRelicFlashVfx = NRelicFlashVfx.Create(ModelDb.Relic<Shovel>());
		if (nRelicFlashVfx == null)
		{
			return Task.CompletedTask;
		}
		nRestSiteCharacter?.AddChildSafely(nRelicFlashVfx);
		nRelicFlashVfx.Position = Vector2.Zero;
		return Task.CompletedTask;
	}
	*/
}
