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

        public ObservableCollection<AbsenteeismModel> GetACodes()
        { 
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {

                ObservableCollection<AbsenteeismModel> TheCollection = new ObservableCollection<AbsenteeismModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetACodes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    TheCollection.Add(new AbsenteeismModel(
                                            Convert.ToString(reader["AbsenteeismRef"]),
                                            Convert.ToString(reader["AbsenteeismDescription"]),
                                            Convert.ToString(reader["AbsenteeismAbbreviation"])
                                            ));
                }

                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetACodes\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String CreateACode(String p_Reference, String p_Description, String p_Abbreviation)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            String strError = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_CreateACode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reference", p_Reference);
                cmd.Parameters.AddWithValue("@Description", p_Description);
                cmd.Parameters.AddWithValue("@Abbreviation", p_Abbreviation);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    strError = Convert.ToString(reader["strError"]);
                }
                return strError;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".CreateACode\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteACode(String p_Reference)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_DeleteACode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reference", p_Reference);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".DeleteACode\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateACode(String p_Reference, String p_Description, String p_Abbreviation)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_UpdateACode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reference", p_Reference);
                cmd.Parameters.AddWithValue("@Description", p_Description);
                cmd.Parameters.AddWithValue("@Abbreviation", p_Abbreviation);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".UpdateACode\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
