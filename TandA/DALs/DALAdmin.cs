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

        public void CreateEmployeePunch(String p_EmployeeReference, DateTime p_PunchDate, String p_PunchTIme, Int32 p_PeriodId, String p_PunchType)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
           
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_CreateEmployeePunch", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeNo", p_EmployeeReference);
               // cmd.Parameters.AddWithValue("@PeriodId", p_PeriodId);
                cmd.Parameters.AddWithValue("@PunchDate", DateTime.Parse(p_PunchDate.Month + "/" + p_PunchDate.Day + "/" + p_PunchDate.Year + " " + p_PunchTIme, System.Globalization.CultureInfo.CurrentCulture));
                //cmd.Parameters.AddWithValue("@PunchTIme", p_PunchTIme);
                cmd.Parameters.AddWithValue("@PunchType", p_PunchType);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".CreateEmployeePunch\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<PunchesModel> GetEmployeePunches(String p_EmployeeReference, Int32 p_PeriodId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                ObservableCollection<PunchesModel> TheCollection = new ObservableCollection<PunchesModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetPunches", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeRef", p_EmployeeReference);
                cmd.Parameters.AddWithValue("@PeriodId", p_PeriodId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TheCollection.Add(new PunchesModel(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToInt32(reader["PeriodId"]),
                                    Convert.ToDateTime(reader["PunchDate"]),
                                    Convert.ToString(reader["PunchTime"]),
                                    Convert.ToString(reader["PunchType"]),
                                    Convert.ToString(reader["EmployeeRef"])
                                ));
                }
                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetEmployeePunches\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public PeriodModel GetCurrentPeriodDetail()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            Int32 l_Id = 0;
            DateTime l_StartDate = DateTime.Now;
            DateTime l_EndDate = l_StartDate.AddDays(4);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetCurrentPeriod", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    l_Id = Convert.ToInt32(reader["Id"]);
                    l_StartDate = Convert.ToDateTime(reader["dtmDate"]);
                    l_EndDate = l_StartDate.AddDays(4);
                }
                return new PeriodModel(l_Id, l_StartDate, l_EndDate, 1);
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetCurrentPeriodDetail\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<EmployeeAbsenteeismModel> GetEmployeeAbsenteeism()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                ObservableCollection<EmployeeAbsenteeismModel> TheCollection = new ObservableCollection<EmployeeAbsenteeismModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetEmployeeAbsenteeism", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TheCollection.Add(new EmployeeAbsenteeismModel(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToString(reader["EmployeeNo"]),
                                    Convert.ToString(reader["EmployeeName"]),
                                    Convert.ToDateTime(reader["DateAbsent"]),
                                    Convert.ToDateTime(reader["DateAbsent"]),
                                    Convert.ToDateTime(reader["DateReturned"]),
                                    Convert.ToString(reader["AbsenteeismRef"]),
                                    Convert.ToBoolean(reader["IsPaid"]),
                                    Convert.ToString(reader["Note"])
                                ));
                }
                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetEmployeeAbsenteeism\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void CreateEmployeeAbsenteeism(String p_EmployeeReference, DateTime p_DateAbsent, String p_TimeAbsent, DateTime p_DateReturned, String p_TimeReturned, String p_AbsenteeismRef, Boolean p_IsPaid, String p_Note)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_CreateEmployeeAbsenteeism", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeRef", p_EmployeeReference);
                cmd.Parameters.AddWithValue("@DateAbsent", DateTime.Parse(p_DateAbsent.Month + "/" + p_DateAbsent.Day + "/" + p_DateAbsent.Year + " " + p_TimeAbsent, System.Globalization.CultureInfo.CurrentCulture));
                cmd.Parameters.AddWithValue("@DateReturned", DateTime.Parse(p_DateReturned.Month + "/" + p_DateReturned.Day + "/" + p_DateReturned.Year + " " + p_TimeReturned, System.Globalization.CultureInfo.CurrentCulture));
                cmd.Parameters.AddWithValue("@AbsenteeismRef", p_AbsenteeismRef);
                cmd.Parameters.AddWithValue("@fIsPaid", p_IsPaid);
                cmd.Parameters.AddWithValue("@Note", p_Note);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".CreateEmployeeAbsenteeism\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateGroup(String p_GroupRef, String p_GroupDescription)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_UpdateGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupRef", p_GroupRef);
                cmd.Parameters.AddWithValue("@GroupDescription", p_GroupDescription);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".UpdateGroup\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteGroup(String p_GroupRef)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_DeleteGroup", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GroupRef", p_GroupRef);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".DeleteGroup\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateEmployeeAbsenteeism(Int32 p_Id, String p_EmployeeReference, DateTime p_DateAbsent, String p_TimeAbsent, DateTime p_DateReturned, String p_TimeReturned, String p_AbsenteeismRef, Boolean p_IsPaid, String p_Note)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_UpdateEmployeeAbsenteeism", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", p_Id);
                cmd.Parameters.AddWithValue("@EmployeeRef", p_EmployeeReference);
                cmd.Parameters.AddWithValue("@DateAbsent", DateTime.Parse(p_DateAbsent.Month + "/" + p_DateAbsent.Day + "/" + p_DateAbsent.Year + " " + p_TimeAbsent, System.Globalization.CultureInfo.CurrentCulture));
                cmd.Parameters.AddWithValue("@DateReturned", DateTime.Parse(p_DateReturned.Month + "/" + p_DateReturned.Day + "/" + p_DateReturned.Year + " " + p_TimeReturned, System.Globalization.CultureInfo.CurrentCulture));
                cmd.Parameters.AddWithValue("@AbsenteeismRef", p_AbsenteeismRef);
                cmd.Parameters.AddWithValue("@fIsPaid", p_IsPaid);
                cmd.Parameters.AddWithValue("@Note", p_Note);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".UpdateEmployeeAbsenteeism\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteEmployeeAbsenteeism(Int32 p_Id)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_DeleteEmployeeAbsenteeism", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", p_Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".DeleteEmployeeAbsenteeism\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<PeriodModel> GetPeriods()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            ObservableCollection<PeriodModel> TheCollection = new ObservableCollection<PeriodModel>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetPeriods", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TheCollection.Add(new PeriodModel(
                                                        Convert.ToInt32(reader["Id"]),
                                                        Convert.ToDateTime(reader["dtmDate"]))
                                    );
                }
                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetPeriods\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<PunchesModel> GetEmployeeHRRPunches(String p_EmployeeReference, Int32 p_PeriodId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                ObservableCollection<PunchesModel> TheCollection = new ObservableCollection<PunchesModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetHRRPunches", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeRef", p_EmployeeReference);
                cmd.Parameters.AddWithValue("@PeriodId", p_PeriodId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TheCollection.Add(new PunchesModel(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToInt32(reader["PeriodId"]),
                                    Convert.ToDateTime(reader["PunchDate"]),
                                    Convert.ToString(reader["PunchTime"]),
                                    Convert.ToString(reader["PunchType"]),
                                    Convert.ToString(reader["EmployeeRef"])
                                ));
                }
                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetEmployeePunches\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<EmployeeAbsenteeismModel> GetEmployeeHRRAbsenteeism(String p_EmployeeReference, Int32 p_PeriodId)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                ObservableCollection<EmployeeAbsenteeismModel> TheCollection = new ObservableCollection<EmployeeAbsenteeismModel>();
                conn.Open();
                SqlCommand cmd = new SqlCommand("spTandA_GetEmployeeHRRAbsenteeism", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeRef", p_EmployeeReference);
                cmd.Parameters.AddWithValue("@PeriodId", p_PeriodId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TheCollection.Add(new EmployeeAbsenteeismModel(
                                    Convert.ToInt32(reader["Id"]),
                                    Convert.ToString(reader["EmployeeNo"]),
                                    Convert.ToString(reader["EmployeeName"]),
                                    Convert.ToDateTime(reader["DateAbsent"]),
                                    Convert.ToDateTime(reader["DateAbsent"]),
                                    Convert.ToDateTime(reader["DateReturned"]),
                                    Convert.ToString(reader["AbsenteeismRef"]),
                                    Convert.ToBoolean(reader["IsPaid"]),
                                    Convert.ToString(reader["Note"])
                                ));
                }
                return TheCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(this.ToString() + ".GetEmployeeHRRAbsenteeism\n" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
