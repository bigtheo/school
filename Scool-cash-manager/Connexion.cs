using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public static class Connexion
    {
        //attributs de la classe de connexion

        public static MySqlConnection con;
        public static string uid;
        public static string pwd;
        public static string db;

        #region la méthode de connexion

        // la méthode qui permet l'ouverture de la connection
        public static bool connecter()
        {
            string serveur = "127.0.0.1";
            pwd = "1993";
            uid = "root";
            db = frmLogin.DatabaseName;
            string constring = "persistsecurityinfo=True; server=" + serveur + "; database="+ db + ";uid=" + uid + ";password=" + pwd + "";
            con = new MySqlConnection(constring);
            con.Open();
            try
            {
                return true;
            }
            catch (MySqlException ex)
            {
                int number = ex.Number;
                if (number == 1042)
                    System.Windows.Forms.MessageBox.Show("Erreur de connexion au serveur des données !\nVeuillez demarrer le serveur avant de lancer la connexion\nLe code d'erreur : " + number, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (number == 0)
                    System.Windows.Forms.MessageBox.Show("Erreur de connexion au serveur des données !\nLe login ou le nom utilisateur est incorect\nLe code d'erreur : " + number, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    System.Windows.Forms.MessageBox.Show(ex.Message + "\nLe code d'erreur : " + number, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //détermine si l'utilisateur est admin
        public static bool IsAdmin(string username,string password)
        {
            try {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    connecter();
                }
                string sql = "select id from users where nom=@name and password=SHA1(@password) and role=1";
                using (MySqlCommand cmd=new MySqlCommand (sql,con))
                {
                    MySqlParameter p_username = new MySqlParameter("@name", MySqlDbType.VarChar)
                    {
                        Value = username
                    };
                    MySqlParameter p_password = new MySqlParameter("@password", MySqlDbType.VarChar)
                    {
                        Value = password
                    };
                    cmd.Parameters.Add(p_username);
                    cmd.Parameters.Add(p_password);

                    int user_id = Convert.ToInt32(cmd.ExecuteScalar());
                    if (user_id > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return default;
            }
        } 
   
        //detrmine si l'utilisateur est autre que admin
        public static bool IsOtherUser(string username,string password)
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    connecter();
                }

                string sql = "select id from users where nom=@name and password=SHA1(@password) and role=2";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    MySqlParameter p_username = new MySqlParameter("@name", MySqlDbType.VarChar)
                    {
                        Value = username
                    };
                    MySqlParameter p_password = new MySqlParameter("@password", MySqlDbType.VarChar)
                    {
                        Value = password
                    };
                    cmd.Parameters.Add(p_username);
                    cmd.Parameters.Add(p_password);

                    int user_id = Convert.ToInt32(cmd.ExecuteScalar());
                    if (user_id > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return default;
            }
        }

        
        #endregion la méthode de connexion
    }
}