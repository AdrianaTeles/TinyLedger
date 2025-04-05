using Domain.Model;

namespace Application.Services.Abstractions
{
    public interface ILedgerService
    {
        void RecordTransaction(string customerId, TransactionType type, decimal amount);
        decimal GetBalance(string customerId);
        List<Transaction> GetTransactionHistory(string customerId);
    }
}
