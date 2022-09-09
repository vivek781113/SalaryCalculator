using Models;

namespace TaxSlab
{
    public class TaxSlabService : ITaxSlabService
    {
        public Slab GetTaxSlabs()
        {
            return new Slab(GetBracketRates())
            {
                FinalRate = 39
            };
        }

        static SlabBracket[] GetBracketRates()
        {
            return new SlabBracket[]
            {
                new SlabBracket { Bracket = 14000, Rate = 10.5},
                new SlabBracket { Bracket = 34000, Rate = 17.5},
                new SlabBracket { Bracket = 22000, Rate = 30},
                new SlabBracket { Bracket = 110000, Rate = 33},
            };
        }
    }
}
