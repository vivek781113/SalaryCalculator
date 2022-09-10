using Microsoft.Extensions.Logging;
using Models;

namespace TaxSlab
{
    public class TaxSlabService : ITaxSlabService
    {
        private readonly ILogger<TaxSlabService> _logger;
        readonly (double rate, double min, double max)[] rateSlabs;
        SlabBracket[] _slabBrackets;

        public TaxSlabService(ILogger<TaxSlabService> logger)
        {
            _logger = logger;
            rateSlabs = new (double rate, double min, double max)[5];
            rateSlabs[0] = (10.5, 0, 14000);
            rateSlabs[1] = (17.5, 14000, 48000);
            rateSlabs[2] = (30, 48000, 70000);
            rateSlabs[3] = (33, 70000, 180000);
            rateSlabs[4] = (39, 180000, -1);

            //this computation will happen only once
            SetupSlabBrackets();

        }

        private void SetupSlabBrackets()
        {
            int n = rateSlabs.Length - 1, j = 0;
            _slabBrackets = new SlabBracket[n];
            for (int i = 0; i < n; i++)
            {
                var (rate, min, max) = rateSlabs[i];
                _slabBrackets[j++] = new SlabBracket { Bracket = max - min, Rate = rate };
            }
        }

        public Slab GetTaxSlabs()
        {
            try
            {
                _logger.LogInformation($"TaxSlabService: GetTaxSlabs() In.");
                return new Slab(_slabBrackets)
                {
                    FinalRate = rateSlabs[^1].rate
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error -> TaxSlabService: GetTaxSlabs()\nErrorMessage: {ex.Message}\nInnerEx: {ex.InnerException?.Message}\nStackTrack:{ex.StackTrace}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"TaxSlabService: GetTaxSlabs() Exit.");

            }

        }
    }
}
