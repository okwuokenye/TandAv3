using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TandA.Models;
using TandA.ViewModels;

namespace TandA.DALs
{
    class DALEmployee
    {
        private String ConnectionString;

        public DALEmployee()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DevConnectionString"].ConnectionString;
        }

        public ObservableCollection<EmployeeModel> GetEmployees()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {

                ObservableCollection<EmployeeModel> TheCollection = new ObservableCollection<EmployeeModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    

                    TheCollection.Add(new EmployeeModel(
                                            
                                            Convert.ToString(reader["EmployeeNo"]),
                                            Convert.ToString(reader["Firstname"]),
                                            Convert.ToString(reader["Lastname"]),
                                            Convert.ToString(reader["EmailAddress"])
                                            ));
                }

                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetEmployees\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public String CreateEmployee(String p_EmployeeNumber, String p_Password, String p_Firstname, String p_Lastname, String p_EmailAddress)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            String strError = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_CreateEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNumber);
                cmd.Parameters.AddWithValue("@PasswordEncrypt", p_Password);
                cmd.Parameters.AddWithValue("@Firstname", p_Firstname);
                cmd.Parameters.AddWithValue("@Lastname", p_Lastname);
                cmd.Parameters.AddWithValue("@EmailAddress", p_EmailAddress);
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
                throw new Exception(this.ToString() + ".CreateEmployee\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AddEmployeeToGroup(String p_EmployeeNumber, String p_GroupRef)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_AddEmployeeToGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNumber);
                cmd.Parameters.AddWithValue("@GroupRef", p_GroupRef);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".AddEmployeeToGroup\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public ObservableCollection<EmployeeGroupsModel> GetEmployeeGroups(String p_EmployeeNo)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {

                ObservableCollection<EmployeeGroupsModel> TheCollection = new ObservableCollection<EmployeeGroupsModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetEmployeeGroups", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNo);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    TheCollection.Add(new EmployeeGroupsModel(
                                            Convert.ToString(reader["GroupRef"]),
                                            Convert.ToBoolean(reader["IsThere"])
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

        public ObservableCollection<EmployeeModel> GetEmployeesInGroup(String p_GroupRef)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {

                ObservableCollection<EmployeeModel> TheCollection = new ObservableCollection<EmployeeModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetEmployeesInGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Reference", p_GroupRef);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {


                    TheCollection.Add(new EmployeeModel(

                                            Convert.ToString(reader["EmployeeNo"]),
                                            Convert.ToString(reader["Firstname"]),
                                            Convert.ToString(reader["Lastname"]),
                                            Convert.ToString(reader["EmailAddress"]),
                                            Convert.ToString(reader["MemberStatus"])
                                            ));
                }

                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetEmployeesInGroup\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void AddEmployeeToGroupAsSupervisor(String p_EmployeeNumber, String p_GroupRef)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_AddEmployeeToGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNumber);
                cmd.Parameters.AddWithValue("@GroupRef", p_GroupRef);
                cmd.Parameters.AddWithValue("@Type", "Supervisor");
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".AddEmployeeToGroupAsSupervisor\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public void UpdateEmployee(String p_EmployeeNumber, String p_Password, String p_Firstname, String p_Lastname, String p_EmailAddress)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_UpdateEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNumber);
                cmd.Parameters.AddWithValue("@PasswordEncrypt", p_Password);
                cmd.Parameters.AddWithValue("@Firstname", p_Firstname);
                cmd.Parameters.AddWithValue("@Lastname", p_Lastname);
                cmd.Parameters.AddWithValue("@EmailAddress", p_EmailAddress);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".UpdateEmployee\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteEmployee(String p_EmployeeNumber)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_DeleteEmployees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNumber);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".DeleteEmployee\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void RemoveEmployeeFromGroup(String p_EmployeeNumber, String p_GroupRef)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_RemoveEmployeeFromGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeNumber);
                cmd.Parameters.AddWithValue("@GroupRef", p_GroupRef);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".RemoveEmployeeFromGroup\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public EmployeeModel Login(String p_EmployeepRef, String p_Password)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {

                EmployeeModel TheCollection = null;
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_Login", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeepRef", p_EmployeepRef);
                cmd.Parameters.AddWithValue("@Password", p_Password);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {


                    TheCollection = new EmployeeModel(

                                            Convert.ToString(reader["EmployeeNo"]),
                                            Convert.ToString(reader["Firstname"]),
                                            Convert.ToString(reader["Lastname"]),
                                            Convert.ToString(reader["EmailAddress"]),
                                            Convert.ToString(reader["MemberStatus"])
                                            );
                }

                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".Login\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
