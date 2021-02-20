using System.Threading.Tasks;

namespace PizzaDelicious.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}