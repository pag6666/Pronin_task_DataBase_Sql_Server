using System.Data.SqlClient;

namespace DatabasePRO
{
    //DB
    //create table Direct_table(id int IDENTITY(1,1) PRIMARY KEY,Direct nchar(50));
    //1
    //create table pref_table(id int IDENTITY(1,1) PRIMARY KEY,f_name nchar(25), i_name nchar(25),o_name nchar(25), number_name nchar(25),pot_name nchar(25))
    //2
    //create table rest_table(id int IDENTITY(1,1) PRIMARY KEY, type_box nchar(25), pref_box nchar(25), groupe_box nchar(25), date_name nchar(25),time_text nchar(25), audio_box nchar(25), direct_box nchar(25))
    
    class DataBase
    {
        SqlConnection sqlConnection;
        public DataBase(string name_server, string derict) 
        {
            string Quer = $"Server = {name_server}; Initial catalog = {derict}; Integrated Security = True";
            sqlConnection = new SqlConnection(Quer);    
        }
        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }


        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

    }
}
