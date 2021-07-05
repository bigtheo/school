using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class frmLogin : Form
    {
       private static MySqlConnection connection;
       
        public frmLogin()
        {
            InitializeComponent();
        }

        #region barre de titre

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr intPtr, int v1, int v2, int v3);

        private void panelBarreDeTitre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion barre de titre

        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            username = txt_username.Text;
            password = txt_password.Text;
            databaseName = cbx_database.Text;
            if (Connexion.connecter())
            {
                if (Connexion.IsAdmin(username, password)
                || Connexion.IsOtherUser(username, password))
                {
                    this.Hide();
                    new FrmMenuPrincipal().Show();
                }
                else
                {
                    MessageBox.Show("password/username incorrect","Information",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            this.Cursor = Cursors.Default;
            
        }

        #region proprietés

        public static string Serveur { get; internal set; }
        public static string DatabaseName { get => databaseName; set => databaseName = value; }

        private static string password;

        public static string GetPassword()
        {
            return password;
        }

        internal static void SetPassword(string value)
        {
            password = value;
        }

        private static string username;

        public static string GetUsername()
        {
            return username;
        }

        internal static void SetUsername(string value)
        {
            username = value;
        }

        private static string databaseName;

        #endregion proprietés

        private void BtnFemer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (Connecter())
            {
                cbx_database.DataSource =DatabaseList();
            }
            
        }
        //connexion à information_schema
        public static bool Connecter()
        {
            string serveur = "127.0.0.1";
            string pwd = "1993";
            string uid = "root";
            string constring = "persistsecurityinfo=True; server=" + serveur + "; database=information_schema;uid=" + uid + ";password=" + pwd + "";
            connection = new MySqlConnection(constring);
            connection.Open();
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
        public static List<string> DatabaseList()
        {
            Connecter();
            string sql = "select SCHEMA_NAME from SCHEMATA where schema_name like 'school_cash%'";
            List<string> databases = new List<string>();
            try
            {
                using (var cmd = new MySqlCommand(sql, connection))
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        databases.Add(dataReader.GetString(0));
                    }

                }
                return databases;
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return default;
            }

        }
    }
}