using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SUBDCORE.Models;
using SUBDCORE.Repository;

namespace SUBDCORE.Repository
{
    public class LoanRepaymentRepository : ICrudable<LoanRepayment>
    {
        SQLSpAdapter sqladapter;
        List<LoanRepayment> loanRepayments = new List<LoanRepayment>();

        public void Create(LoanRepayment _object) {
            sqladapter = new SQLSpAdapter("AddLoanRepayment");
            sqladapter.AddSqlParameter("@creditId", _object.CreaditId);
            sqladapter.AddSqlParameter("@monthForPay", _object.MonthForPay);
            sqladapter.AddSqlParameter("@yearForPay",_object.YearForPay);
            sqladapter.AddSqlParameter("@dateofPay",_object.DateOfPay);
            sqladapter.ExecNonQuery();
        }
        public void Delete(int id) {
            sqladapter = new SQLSpAdapter("DelLoanRepayment");
            sqladapter.AddSqlParameter("@id", id);
            sqladapter.ExecNonQuery();
        }
        public void Update(LoanRepayment loanRepayment) {
            sqladapter = new SQLSpAdapter("UpLoanRepayment");
            sqladapter.AddSqlParameter("@id",loanRepayment.IdLoanRepayment);
            sqladapter.AddSqlParameter("@creditId", loanRepayment.CreaditId);
            sqladapter.AddSqlParameter("@monthForPay", loanRepayment.MonthForPay);
            sqladapter.AddSqlParameter("@yearForPay", loanRepayment.YearForPay);
            sqladapter.AddSqlParameter("@dateofPay", loanRepayment.DateOfPay);
            sqladapter.ExecNonQuery();
        }
        public IEnumerable<LoanRepayment> GetList()
        {
            LoanRepayment lp;
            sqladapter = new SQLSpAdapter("GetLoanRepayment");
            sqladapter.ExecReader();
            foreach (var row in sqladapter.baggage)
            {
                lp = new LoanRepayment();
                lp.Creadit = new Credits();
                lp.MonthForPayNavigation = new Month();
                lp.IdLoanRepayment = Convert.ToInt32(row[0]);
                lp.CreaditId = Convert.ToInt16(row[1]);
                lp.MonthForPay = Convert.ToByte(row[2]);
                lp.YearForPay = Convert.ToInt16(row[3]);
                lp.DateOfPay = Convert.ToDateTime(row[4]);
                lp.MonthlyPayment = Convert.ToDecimal(row[5]);
                lp.ExpiredDays = Convert.ToByte(row[6]);
                lp.Fine = Convert.ToDecimal(row[7]);
                lp.TotalPayment = Convert.ToDecimal(row[8]);
                //lp.Creadit.IdCredits = Convert.ToInt16(row[9]);
                //lp.Creadit.Name = row[10].ToString();
                //lp.MonthForPayNavigation.IdMonth = Convert.ToByte(row[11]);
                //lp.MonthForPayNavigation.MonthName= row[12].ToString();
                loanRepayments.Add(lp);
            }
            return loanRepayments;
        }

    }
}
