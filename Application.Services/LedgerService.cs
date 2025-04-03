using Application.Services.Abstractions;
using Data.Repository.Abstractions;
using Domain.Model;

namespace Application.Services
{
    public class LedgerService : ILedgerService
    {
        private readonly ITransactionRepository _repository;

        public LedgerService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public void RecordTransaction(TransactionType type, decimal amount)
        {
            if (type == TransactionType.Withdrawal && GetBalance() < amount)
                throw new InvalidOperationException("Insufficient balance.");

            _repository.Add(new Transaction
            {
                Type = type,
                Amount = amount
            });
        }

        public decimal GetBalance()
        {
            var transactions = _repository.GetAll();
            return transactions.Sum(t => t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);
        }

        public List<Transaction> GetTransactionHistory()
        {
            return _repository.GetAll();
        }
    }
}
