using Keboho.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Keboho.Server
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {}

        public DbSet<Budget> Budgets { get; set; }
        public DbSet<CashFlow> CashFlows { get; set; }
    }
}
