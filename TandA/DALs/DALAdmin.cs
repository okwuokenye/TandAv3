using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TandA.Models;
using TandA.ViewModels;

namespace TandA.DALs
{
    class DALAdmin
    {
        private String ConnectionString;

        public DALAdmin()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString;
        }

        public ObservableCollection<GroupModel> GetGroups()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {

                ObservableCollection<GroupModel> TheCollection = new ObservableCollection<GroupModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetGroups", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    TheCollection.Add(new GroupModel(
                                            Convert.ToInt32(reader["Id"]),
                                            Convert.ToString(reader["GroupRef"]),
                                            Convert.ToString(reader["GroupDescription"]),
                                            Convert.ToString(reader["SupervisorNo"])
                                            ));
                }

                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetGroups\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String CreateGroup(String p_GroupRef, String p_GroupDescription)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            String strError = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_CreateGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupRef", p_GroupRef);
                cmd.Parameters.AddWithValue("@GroupDescription", p_GroupDescription);
                //cmd.ExecuteNonQuery();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    strError = Convert.ToString(reader["strError"]);
                }
                return strError;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".CreateGroup\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
