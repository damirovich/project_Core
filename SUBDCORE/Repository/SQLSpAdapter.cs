using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUBDCORE.Repository
{
    public class SQLSpAdapter
    {
        string ConnectionString = "Data Source=DESKTOP-L449UJT;Database=Proziv;Trusted_Connection=True;";
        SqlConnection connection;
        SqlCommand command;
        List<SqlParameter> OutParameters;
        public List<List<object>> baggage; // записвыается данные из выборки ХП
        public SQLSpAdapter(string sp_name)
        {
            baggage = new List<List<object>>();
            OutParameters = new List<SqlParameter>();
            connection = new SqlConnection(ConnectionString);
            command = new SqlCommand(sp_name, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
        }

        public object getParameter(string name)
        {
            return command.Parameters[name].Value;
        }

        public void ExecReader()
        {
            connection.Open();
            foreach (var param in OutParameters)
            {
                command.Parameters.Add(param);
            }
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                int i = 0;
                while (dataReader.Read())
                {
                    baggage.Add(new List<object>());
                    while (i != dataReader.FieldCount)
                    {
                        baggage.Last().Add(dataReader[i]);
                        i++;
                    }
                    i = 0;
                }
            }
            connection.Close();
        }

        public void ExecNonQuery()
        {
            connection.Open();
            if (OutParameters.Count != 0)
            {
                foreach (var param in OutParameters)
                {
                    command.Parameters.Add(param);
                }
            }
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddSqlParameter<T>(string name, T value)
        {
            command.Parameters.AddWithValue(name, value);
        }
        public void AddSqlOutParameter(string name, byte type)
        {
            if (type == 1)
            {
                OutParameters.Add(new SqlParameter()
                {
                    ParameterName = name,
                    SqlDbType = System.Data.SqlDbType.Int
                });
            }
            else if (type == 0)
            {
                OutParameters.Add(new SqlParameter()
                {
                    ParameterName = name,
                    SqlDbType = System.Data.SqlDbType.Bit
                });
            }
            else if (type == 2)
            {
                OutParameters.Add(new SqlParameter()
                {
                    ParameterName = name,
                    SqlDbType = System.Data.SqlDbType.VarChar
                });
            }
            else if (type == 3)
            {
                OutParameters.Add(new SqlParameter()
                {
                    ParameterName = name,
                    SqlDbType = System.Data.SqlDbType.Date
                });
            }
            else if (type == 4)
            {
                OutParameters.Add(new SqlParameter()
                {
                    ParameterName = name,
                    SqlDbType = System.Data.SqlDbType.Char
                });
            }
            OutParameters.Last().Direction = System.Data.ParameterDirection.Output;
        }

    }
}

