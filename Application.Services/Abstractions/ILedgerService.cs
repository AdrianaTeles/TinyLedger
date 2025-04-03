using Domain.Model;

namespace Application.Services.Abstractions
{
    public interface ILedgerService
    {
        void RecordTransaction(TransactionType type, decimal amount);
        decimal GetBalance();
        List<Transaction> GetTransactionHistory();
    }
}
