using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavmaxiaGame
{
    class DBHelper
    {
        private String connectionString = "Data Source=NavalBattleDB.db;Version=3;";

        public Boolean attemptLogin(String username,String password)    //Returns true if the username-password match was found in the in the user data table,otherwise returns false
        {
            String query = "Select * from UserData where username=@username and password=@password";
            using(SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public Boolean attemptRegister(String username,String password) //Returns true if a user with the same username doesn't exist in the user data table,otherwise it returns false
        {
            String query = "Select * from UserData where username=@username";
            using(SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return false;
                    }
                    else
                    {
                        reader.Close();
                        query = "Insert into UserData (username,password) values (@username,@password)";
                        command = new SQLiteCommand(query, conn);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
        }

        public List<String> playerResults(String username)  //Returns a list with all of a user's results if the username is found in the db and
        {                                                   //if there are recorded games of the player in the db ,else it returns an empty list
            String query = "Select * from ResultData where username=@username";
            List<String> resultsList = new List<String>();
            String tempResult;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tempResult = "";
                            tempResult += "Tries: " + reader[2].ToString();
                            tempResult += ", Time: " + reader[3].ToString();
                            tempResult += ", " + reader[4].ToString();
                            tempResult +="\n";
                            resultsList.Add(tempResult);
                        }
                        return resultsList;
                    }
                    return resultsList;
                }
            }
        }

        public void insertResult(String username,String tries,String time,String result)    //Insert a new result in the results table
        {
            String query = $"Insert into ResultData (username,tries,time,result) values (@username,'{tries}','{time}','{result}')";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.ExecuteNonQuery();
            }
        }
    }
}
