namespace Domain.Model
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public required string CustomerId { get; set; }
        public Status Status { get; set; }
    }
}
