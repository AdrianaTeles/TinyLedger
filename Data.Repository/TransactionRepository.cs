using Data.Repository.Abstractions;
using Domain.Model;
using System.Text.Json;

namespace Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private const string FilePath = "ledger.json";
        private readonly List<Transaction> transactions;

        public TransactionRepository()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                transactions = JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
            }
            else
            {
                transactions = new List<Transaction>();
                SaveChanges();
            }
        }

        public void Add(Transaction transaction)
        {
            transactions.Add(transaction);
            SaveChanges();
        }

        public List<Transaction> GetAll(string customerId) => 
            transactions.Where(t => t.CustomerId.Equals(customerId)).ToList();

        private void SaveChanges()
        {
            var json = JsonSerializer.Serialize(transactions, 
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
