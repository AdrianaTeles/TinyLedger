using Application.DTO;
using Domain.Model;

namespace Application.Services.Abstractions
{
    public interface ILedgerService
    {
        void RecordTransaction(string customerIdSender, TransactionType type, AmountRequest request);
        decimal GetBalance(string customerId);
        List<Transaction> GetTransactionHistory(string customerId);
    }
}
