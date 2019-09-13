using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Keboho.Shared
{
    public class CashFlow
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public CashFlowFrequencyType FrequencyType { get; set; }
        public int FrequencyInterval { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int BudgetId { get; set; }
        public Budget Budget { get; set; }
    }

    public enum CashFlowFrequencyType
    {
        Once,
        Day,
        Week,
        Month,
        Yearly
    }

    public static class CashFlowExtensions
    {
        public static IEnumerable<DateTime> OccurencesBetween(this CashFlow cashFlow, DateTime startDate, DateTime endDate)
        {
            var occurences = new List<DateTime>();
            DateTime currentDate;
            var earliestEndDate = cashFlow.EndDate > cashFlow.StartDate ? new[] { cashFlow.EndDate.Date, endDate.Date }.Min() : endDate;

            switch (cashFlow.FrequencyType)
            {
                case CashFlowFrequencyType.Once:
                    if (startDate.Date <= cashFlow.StartDate.Date && cashFlow.StartDate.Date <= endDate)
                    {
                        occurences.Add(cashFlow.StartDate.Date);
                    }
                    break;
                case CashFlowFrequencyType.Day:
                    currentDate = cashFlow.StartDate.Date;
                    while (currentDate <= earliestEndDate)
                    {
                        if (currentDate >= startDate)
                        {
                            occurences.Add(currentDate);
                        }
                        currentDate = currentDate.AddDays(Math.Max(cashFlow.FrequencyInterval, 1));
                    }
                    break;
                case CashFlowFrequencyType.Week:
                    currentDate = cashFlow.StartDate.Date;
                    while (currentDate <= earliestEndDate)
                    {
                        if (currentDate >= startDate)
                        {
                            occurences.Add(currentDate);
                        }
                        currentDate = currentDate.AddDays(7 * Math.Max(cashFlow.FrequencyInterval, 1));
                    }
                    break;
                case CashFlowFrequencyType.Month:
                    currentDate = cashFlow.StartDate.Date;
                    while (currentDate <= earliestEndDate)
                    {
                        if (currentDate >= startDate)
                        {
                            occurences.Add(currentDate);
                        }
                        currentDate = currentDate.AddMonths(Math.Max(cashFlow.FrequencyInterval, 1));
                    }
                    break;
            }

            return occurences;
        }
    }
}
