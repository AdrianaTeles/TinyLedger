using Application.DTO;
using Application.Services.Abstractions;
using Data.Repository.Abstractions;
using Domain.Model;

namespace Application.Services
{
    public class LedgerService(ITransactionRepository repository) : ILedgerService
    {
        public void RecordTransaction(string customerIdSender, TransactionType type, AmountRequest request)
        {
            
            if ((type == TransactionType.Withdrawal || type == TransactionType.Transfer ) && GetBalance(customerIdSender) < request.Amount)
                throw new InvalidOperationException("Insufficient balance.");

            if (type == TransactionType.Transfer)
                Transfer(request.CustomerIdReceiver, customerIdSender, request.Amount);

            repository.Add(new Transaction
            {
                Type = type,
                Amount = request.Amount,
                CustomerId = customerIdSender
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

        private void Transfer(string customerIdReceiver, string customerIdSender, decimal amount)
        {
            try
            {
                repository.Add(new Transaction
                {
                    Type = TransactionType.Transfer,
                    Amount = amount,
                    CustomerId = customerIdReceiver
                });

                repository.Add(new Transaction
                {
                    Type = TransactionType.Transfer,
                    Amount = -amount,
                    CustomerId = customerIdSender
                });
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                
            }
          
        }
    }
}
