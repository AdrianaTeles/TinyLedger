using Application.DTO;
using Application.Services.Abstractions;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/ledger/{customerId}")]
    public class TransactionsController(ILedgerService ledgerService) : ControllerBase
    {
        [HttpPost("deposit")]
        public IActionResult Deposit(string customerId, [FromBody] AmountRequest request)
        {
            ledgerService.RecordTransaction(customerId, TransactionType.Deposit, request.Amount);
            return Ok();
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw(string customerId, [FromBody] AmountRequest request)
        {
            try
            {
                ledgerService.RecordTransaction(customerId, TransactionType.Withdrawal, request.Amount);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("balance")]
        public ActionResult<decimal> GetBalance(string customerId) => 
            ledgerService.GetBalance(customerId);

        [HttpGet("history")]
        public ActionResult<List<Transaction>> GetHistory(string customerId) => 
            ledgerService.GetTransactionHistory(customerId);
    }
}
