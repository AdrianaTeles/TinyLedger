using Domain.Model;

namespace Data.Repository.Abstractions
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        List<Transaction> GetAll();
    }
}
