using Application.Services.Abstractions;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/ledger")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILedgerService ledgerService;

        public TransactionsController(ILedgerService ledgerService)
        {
            this.ledgerService = ledgerService;
        }

        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] AmountRequest request)
        {
            ledgerService.RecordTransaction(TransactionType.Deposit, request.Amount);
            return Ok();
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] AmountRequest request)
        {
            try
            {
                ledgerService.RecordTransaction(TransactionType.Withdrawal, request.Amount);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("balance")]
        public ActionResult<decimal> GetBalance() => ledgerService.GetBalance();

        [HttpGet("history")]
        public ActionResult<List<Transaction>> GetHistory() => ledgerService.GetTransactionHistory();
    }

    public class AmountRequest
    {
        public decimal Amount { get; set; }
    }

}
