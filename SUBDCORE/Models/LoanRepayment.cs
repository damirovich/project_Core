using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace SUBDCORE.Models
{
    public partial class LoanRepayment
    {
        public int IdLoanRepayment { get; set; }
        public short? CreaditId { get; set; }
        public byte? MonthForPay { get; set; }
        public short? YearForPay { get; set; }
        public DateTime? DateOfPay { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public byte? ExpiredDays { get; set; }
        public decimal? Fine { get; set; }
        public decimal? TotalPayment { get; set; }

        public virtual Credits Creadit { get; set; }
        public virtual Month MonthForPayNavigation { get; set; }
        public byte GetPermission()
        {
            byte res = 0;
            string connetionString = "Data Source=DESKTOP-L449UJT;Initial Catalog = Proziv; User ID = sa; Password = 123";
            SqlConnection connection = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand("SP_Loan_repayment", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@creditId", CreaditId);
            cmd.Parameters.AddWithValue("@mounth_for_pay", MonthForPay);
            cmd.Parameters.AddWithValue("@year_for_pay", YearForPay);
            cmd.Parameters.AddWithValue("@date_of_pay", DateOfPay);
            SqlParameter result = new SqlParameter
            {
                ParameterName = "@result",
                SqlDbType = System.Data.SqlDbType.TinyInt
            };
            connection.Open();
            result.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(result);
            cmd.ExecuteNonQuery();
            connection.Close();
            if (Convert.ToInt32(cmd.Parameters["@result"].Value) == 0) res = 0;
            else if (Convert.ToInt32(cmd.Parameters["@result"].Value) == 1) res = 1;
            else if (Convert.ToInt32(cmd.Parameters["@result"].Value) == 2) res = 2;
            else if (Convert.ToInt32(cmd.Parameters["@result"].Value) == 3) res = 3;
            return res;
        }
    }
   
}
