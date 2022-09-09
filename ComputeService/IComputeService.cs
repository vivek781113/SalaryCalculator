using Models;

namespace ComputeService
{
    public interface IComputeService
    {
        Dictionary<double, ComputedSalary> ComputeSalary(Slab slab, double[] salaries);
    }
}
