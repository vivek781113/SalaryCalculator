using Models;

namespace ComputeService
{
    public class ComputeService : IComputeService
    {
        //TODO -- MAKE SLAB IN CONSTRUCTOR
        static ComputedSalary ComputeSingle(Slab slab ,double salary)
        {
            double sal = salary, netTax = 0, lastRate = slab.FinalRate;
            int i = 0;
            var slabs = slab.Brackets;
            int n = slabs.Length;
            
            while (sal > 0 && i < n)
            {
                var slabBracket = slabs[i++];
                var rate = slabBracket.Rate;
                var bracket = slabBracket.Bracket;
                var tax = rate * (bracket >= sal ? sal : bracket) / 100;
                netTax += tax;
                sal -= bracket;
            }
            if (sal > 0)
            {
                var tax = (lastRate * sal / 100);
                netTax += tax;
            }

            var netSalary = salary - netTax;
            var monthlySalary = netSalary / 12;

            return new ComputedSalary
            {
                NetAnnual = Math.Round(netSalary, 2),
                NetMonthly = Math.Round(monthlySalary, 2),
                NetTax = Math.Round(netTax, 2),
            };
        }

        public Dictionary<double, ComputedSalary> ComputeSalary(Slab slab, double[] salaries)
        {
            Dictionary<double, ComputedSalary> computedSalaries = new();
            foreach (double sal in salaries)
                if (!computedSalaries.ContainsKey(sal)) 
                    computedSalaries[sal] = ComputeSingle(slab, sal);

            return computedSalaries;
        }
    }
}
