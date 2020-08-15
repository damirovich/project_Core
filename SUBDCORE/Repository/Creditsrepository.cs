using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;

namespace SUBDCORE.Repository
{
    public class Creditsrepository : ICrudable<Credits>
    {
        SQLSpAdapter sqladapter;
        List<Credits> credits = new List<Credits>();
        public IEnumerable<Credits> GetList()
        {
            sqladapter = new SQLSpAdapter("GetCredits");
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage)
            {
                credits.Add(new Credits()
                {
                    IdCredits = Convert.ToInt16(row[0]),
                    Name = row[1].ToString(),
                    CreaditAmount = Convert.ToDecimal(row[2]),
                    InterestRate = Convert.ToByte(row[3]),
                    CreditTerm = Convert.ToInt16(row[4]),
                    DateOfIssue = Convert.ToDateTime(row[5]),
                    MonthlyPayment = Convert.ToDecimal(row[6]),
                    Fine = Convert.ToDecimal(row[7])
                });

            }
            return credits;
        }
        public void Create(Credits credits) {
            sqladapter = new SQLSpAdapter("AddCredits");
            sqladapter.AddSqlParameter("@name", credits.Name);
            sqladapter.AddSqlParameter("@creditAmount", credits.CreaditAmount);
            sqladapter.AddSqlParameter("@interestRate", credits.InterestRate);
            sqladapter.AddSqlParameter("@creditTerm", credits.CreditTerm);
            sqladapter.AddSqlParameter("@dateOfIssue", credits.DateOfIssue);
            sqladapter.ExecNonQuery();
        }
        public void Update(Credits credits) {
            sqladapter = new SQLSpAdapter("UpCredits");
            sqladapter.AddSqlParameter("@id", credits.IdCredits);
            sqladapter.AddSqlParameter("@name", credits.Name);
            sqladapter.AddSqlParameter("@creditAmount", credits.CreaditAmount);
            sqladapter.AddSqlParameter("@interestRate", credits.InterestRate);
            sqladapter.AddSqlParameter("@creditTerm", credits.CreditTerm);
            sqladapter.AddSqlParameter("@dateOfIssue", credits.DateOfIssue);
            sqladapter.ExecNonQuery();
        }
        public void Delete(int id ) {
            sqladapter = new SQLSpAdapter("DelCredits");
            sqladapter.AddSqlParameter("@id", id);
            sqladapter.ExecNonQuery();
        }
    }
}
