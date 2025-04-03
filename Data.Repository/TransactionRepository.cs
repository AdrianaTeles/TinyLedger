using Data.Repository.Abstractions;
using Domain.Model;

namespace Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly List<Transaction> _transactions = new();
        public void Add(Transaction transaction) => _transactions.Add(transaction);

        public List<Transaction> GetAll() => _transactions;
    }
}
