using demo_app_API.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace demo_app_API.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        public IConfiguration Configuration;
        public string ConnectionStrings;

        public object UserModels { get; private set; }

        public UserDataRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionStrings = Configuration["ConnectionStrings:Connection"];
        }
        public UserModel AddUser(UserModel user)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertUser]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@C_date", user.C_date);
                    cmd.Parameters.AddWithValue("@A_date", user.A_date);
                    cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Payment", user.Payment);
                    cmd.Parameters.AddWithValue("@R_payment", user.R_payment);
                    cmd.Parameters.AddWithValue("@workdone", user.Workdone);

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    LogEvent("Exception in GetStatusOKOrNotOk() method: " + ex.Message.ToString());
                }
                return user;
            }
        }

        public void DeletUser(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteUser]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    LogEvent("Exception in GetStatusOKOrNotOk() method: " + ex.Message.ToString());
                }

            }

        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            List<UserModel> UserModels = new List<UserModel>();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    var query = "select * from UserData";

                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        UserModels.Add(new UserModel
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            C_date = Convert.ToDateTime(dr["C_date"]),
                            A_date = Convert.ToDateTime(dr["A_date"]),
                            Mobile = dr["Mobile"].ToString(),
                            Payment = dr["Payment"].ToString(),
                            R_payment = dr["R_Payment"].ToString(),
                            Workdone = dr["Workdone"].ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    LogEvent("Exception in GetStatusOKOrNotOk() method: " + ex.Message.ToString());
                    UserModels = null;
                }
            }
            return UserModels;
        }

        public UserModel GetUserById(int id)
        {
            List<UserModel> usermodel = new List<UserModel>();
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    var query = "select * from UserData where Id = " + id;
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        usermodel.Add(new UserModel
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            C_date = Convert.ToDateTime(dr["C_date"]),
                            A_date = Convert.ToDateTime(dr["A_date"]),
                            Mobile = dr["Mobile"].ToString(),
                            Payment = dr["Payment"].ToString(),
                            R_payment = dr["R_Payment"].ToString(),
                            Workdone = dr["Workdone"].ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    LogEvent("Exception in GetStatusOKOrNotOk() method: " + ex.Message.ToString());
                    usermodel = null;
                }
            }
            return usermodel.FirstOrDefault<UserModel>(x => x.Id.Equals(id));
        }

        public void UpdateUser(UserModel user)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStrings))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateUser]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@C_date", user.C_date);
                    cmd.Parameters.AddWithValue("@A_date", user.A_date);
                    cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Payment", user.Payment);
                    cmd.Parameters.AddWithValue("@R_payment", user.R_payment);
                    cmd.Parameters.AddWithValue("@workdone", user.Workdone);
                    cmd.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    LogEvent("Exception in GetStatusOKOrNotOk() method: " + ex.Message.ToString());
                }

            }


        }
        public static void LogEvent(string logevent)
        {
            string message = DateTime.Now.ToString() + " \t  " + logevent;
            // string path = Application.StartupPath+ @"\EventLog\ErrorLog.txt";
            string date = DateTime.Now.ToString("ddMMMyyyy");
            System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"DebugLog\");
            string path = AppDomain.CurrentDomain.BaseDirectory + @"DebugLog\" + date + ".txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
