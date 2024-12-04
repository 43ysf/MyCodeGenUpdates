using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace CodeGenAccess
{
    public static class CRUD
    {


        private static SqlConnection _Connection = new SqlConnection(clsSettings.connectionString);

        public async static Task<int> AddNew(SqlCommand command)
        {

            command.Connection = _Connection;

            int ID = -1;
            try
            {
                _Connection.Open();
                object Reuslt =  command.ExecuteScalar();
                if (Reuslt != null && int.TryParse(Reuslt.ToString(), out int R))
                {
                    ID = R;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _Connection.Close();
            }
            return ID;
        }

        public  static DataTable GetAll(SqlCommand command)
        {
            DataTable dt = new DataTable();

             SqlConnection _Connection = new SqlConnection(clsSettings.connectionString);
            command.Connection = _Connection;
            try
            {
                _Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _Connection.Close();

            }
            return dt;
        }

        public static bool UpdateOrDelete(SqlCommand command)
        {
            command.Connection = _Connection;
            int RowsAffected = 0;
            try
            {
                _Connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _Connection.Close();
            }
            return RowsAffected > 0;
        }

        public static bool IsExist(SqlCommand command)
        {
            command.Connection = _Connection;
            bool isFound = false;
            try
            {
                _Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _Connection.Close();
            }
            return isFound;

        }


    }
}
