using Application.Services.Abstractions;
using Data.Repository.Abstractions;
using Domain.Model;

namespace Application.Services
{
    public class LedgerService(ITransactionRepository repository) : ILedgerService
    {
        public void RecordTransaction(string customerId, TransactionType type, decimal amount)
        {
            if (type == TransactionType.Withdrawal && GetBalance(customerId) < amount)
                throw new InvalidOperationException("Insufficient balance.");

            repository.Add(new Transaction
            {
                Type = type,
                Amount = amount,
                CustomerId = customerId
            });
        }

        public decimal GetBalance(string customerId)
        {
            var transactions = repository.GetAll(customerId);
            return transactions.Sum(t => t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);
        }

        public List<Transaction> GetTransactionHistory(string customerId)
        {
            return repository.GetAll(customerId);
        }
    }
}
