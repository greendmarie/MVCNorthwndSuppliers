using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DataLayer.Models;
using System.Data.SqlClient;

namespace DataLayer
{
    public class UserDAO
    {
        private string connectionString;
        private string filePath;


        public UserDAO(string dataConnection, string path)
        {
            connectionString = dataConnection;
            filePath = path;
        }

        public UserDO ReadUserByUsername(string username)
        {
            UserDO user = new UserDO();
            try
            {
                
                //Opening SQL connection.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                using (SqlCommand enterCommand = new SqlCommand("DISPLAY_USERS_BY_USERNAME", northWndConn))
                {    //Creating a new SqlCommand to use a stored procedure.

                    enterCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    enterCommand.Parameters.AddWithValue("@Username", username);
                    northWndConn.Open();

                    //Using SqlDataAdapter to get SQL table.
                    using (SqlDataReader userReader = enterCommand.ExecuteReader())
                    {
                        userReader.Read();
                        user.UserID = userReader.GetInt64(0);
                        user.Username = userReader.GetString(1);
                        user.Password = userReader.GetString(2);
                        user.Firstname = userReader.GetString(3);
                        user.Lastname = userReader.GetString(4);
                        user.EmailAddress = userReader["EmailAddress"] == DBNull.Value ? String.Empty: (string)userReader["EmailAddress"];
                        user.RoleID = userReader.GetInt32(6);
                    }
                }
                
            }
            catch(Exception ex)
            {

                throw ex;
            }
            return user;
        }

        public void CreateUser(UserDO user)
        {
            try
            {
                //Creates a new connections.
                using (SqlConnection northWndConn = new SqlConnection(connectionString))
                {
                    //Creating a new SqlCommand to use a stored procedure.
                    SqlCommand enterCommand = new SqlCommand("REGISTER_USER", northWndConn);
                    enterCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    //Parameters that are being passed to the stored procedures.
                    enterCommand.Parameters.AddWithValue("@Username", user.Username);
                    enterCommand.Parameters.AddWithValue("@Password", user.Password);
                    enterCommand.Parameters.AddWithValue("@Firstname", user.Firstname);
                    enterCommand.Parameters.AddWithValue("@Lastname", user.Lastname);
                    enterCommand.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                    enterCommand.Parameters.AddWithValue("@RoleID", user.RoleID);

                    //Opening connection.
                    northWndConn.Open();
                    //Execute Non Query command.
                    enterCommand.ExecuteNonQuery();
                    northWndConn.Close();
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }
    }
}
