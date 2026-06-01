using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace PokeMod.PokeModCode;

public class Keywords
{
    [CustomEnum("Evolve")]
    [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Evolve;
}