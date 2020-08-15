using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class Budgetrepository 
    {
        SQLSpAdapter sqladapter;
        List<Budget> budgets = new List<Budget>();
        public IEnumerable<Budget> GetList()
        {
            Budget budget;
            sqladapter = new SQLSpAdapter("GetBudget");
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage)
            {
                budget = new Budget();
                budget.IdBudget = Convert.ToInt32(row[0]);
                budget.BudgetSum = Convert.ToDouble(row[1]);
                budget.Prosent = Convert.ToByte(row[2]);
                budget.ProsentSalary = Convert.ToByte(row[3]);
                budgets.Add(budget);
            }
            return budgets;
        }
        public void Update(Budget budget)
        {
            sqladapter = new SQLSpAdapter("UpBudget");
            sqladapter.AddSqlParameter("@idBudget", budget.IdBudget);
            sqladapter.AddSqlParameter("@budgetsum", budget.BudgetSum);
            sqladapter.AddSqlParameter("@Prosent", budget.Prosent);
            sqladapter.AddSqlParameter("@ProsentSalary", budget.ProsentSalary);
            sqladapter.ExecNonQuery();
        }
    }
}
