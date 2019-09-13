using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Keboho.Shared
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CashFlow> CashFlows { get; set; } = new List<CashFlow>();
    }

    public static class BudgetExtensions
    {
        public static IEnumerable<IEnumerable<CashFlow>> DailyCashFlowsBetween(this Budget budget, DateTime startDate, DateTime endDate)
        {
            var dayCount = Math.Max(1, (endDate.Date - startDate.Date).Days + 1);
            var dailyCashFlows = new List<List<CashFlow>>(dayCount);
            var cashFlowOccurences = budget.CashFlows.ToDictionary(x => x, x => x.OccurencesBetween(startDate.Date, endDate.Date));

            for (int i = 0; i < dayCount; i++)
            {
                var currentDayCashFlows = new List<CashFlow>();
                dailyCashFlows.Add(currentDayCashFlows);
                var currentDate = startDate.Date.AddDays(i);

                foreach (var cashFlow in cashFlowOccurences)
                {
                    if (cashFlow.Value.Any(x => x.Date == currentDate.Date))
                    {
                        currentDayCashFlows.Add(cashFlow.Key);
                    }
                }
            }

            return dailyCashFlows;
        }

        public static decimal CashFlowAmountBetween(this Budget budget, DateTime startDate, DateTime endDate)
        {
            return budget.DailyCashFlowsBetween(startDate, endDate)
                .SelectMany(x => x)
                .Sum(x => x.Amount);
        }

        public static (DateTime startDate, IEnumerable<IEnumerable<CashFlow>> cashFlows) DailyCashflowsForMonth(this Budget budget, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            startDate = startDate.AddDays(-(((int)startDate.DayOfWeek + 6) % 7));

            var endDate = startDate.AddDays(7 * 6 - 1);

            return (startDate, budget.DailyCashFlowsBetween(startDate, endDate));
        }

        public static (DateTime startDate, IEnumerable<IEnumerable<CashFlow>> cashFlows) DailyCashflowsForMonth(this Budget budget)
        {
            return budget.DailyCashflowsForMonth(DateTime.Now.Year, DateTime.Now.Month);
        }

        public static decimal CashFlowAmountForMonth(this Budget budget, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return budget.DailyCashFlowsBetween(startDate, endDate)
                .SelectMany(x => x)
                .Sum(x => x.Amount);
        }

        public static decimal CashFlowAmountForMonth(this Budget budget)
        {
            return budget.CashFlowAmountForMonth(DateTime.Now.Year, DateTime.Now.Month);
        }
    }
}
