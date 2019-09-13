using Keboho.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keboho.Server.Controllers
{
    [ApiController]
    [Route("api/budgets")]
    public class BudgetController : ControllerBase
    {
        private BudgetContext _dbContext { get; set; }

        public BudgetController(BudgetContext budgets)
        {
            _dbContext = budgets;
        } 

        [HttpGet]
        public async Task<IEnumerable<Budget>> ListBudgetsAsync()
        {
            return await _dbContext.Budgets.ToListAsync();
        }

        [HttpPost]
        public async Task<int> AddBudgetAsync(Budget budget)
        {
            await _dbContext.Budgets.AddAsync(budget);
            await _dbContext.SaveChangesAsync();

            return budget.Id;
        }

        [HttpPut("{id}")]
        public async Task<int> UpdateBudgetAsync(int id, Budget budget)
        {
            _dbContext.Budgets.Update(budget);
            await _dbContext.SaveChangesAsync();

            return budget.Id;
        }

        [HttpGet("{id}")]
        public async Task<Budget> GetBudgetAsync(int id)
        {
            return await _dbContext.Budgets
                .Include(x => x.CashFlows)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBudgetAsync(int id)
        {
            var budget = await _dbContext.Budgets
                .SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Budgets.Remove(budget);
            await _dbContext.SaveChangesAsync();
        }

        [HttpPost("{budgetId}/cashflows")]
        public async Task<int> AddCashFlowAsync(int budgetId, CashFlow cashFlow)
        {
            cashFlow.BudgetId = budgetId;
            await _dbContext.CashFlows.AddAsync(cashFlow);
            await _dbContext.SaveChangesAsync();

            return cashFlow.Id;
        }

        [HttpDelete("{budgetId}/cashflows/{id}")]
        public async Task<int> DeleteCashFlowAsync(int budgetId, int id)
        {
            var budget = await _dbContext.Budgets
                .Include(x => x.CashFlows)
                .SingleOrDefaultAsync(x => x.Id == budgetId);

            var cashFlow = budget
                .CashFlows
                .SingleOrDefault(x => x.Id == id);

            _dbContext.CashFlows.Remove(cashFlow);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
