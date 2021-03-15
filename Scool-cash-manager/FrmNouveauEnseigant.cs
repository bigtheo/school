using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class FrmNouveauEnseigant : Form
    {
        public FrmNouveauEnseigant()
        {
            InitializeComponent();
            
        }
        #region barre de titre
        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr intPtr, int v1, int v2, int v3);

        private void PanelBarreDeTitre_MouseEnter(object sender, EventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
      

        private void PanelBarreDeTitre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region enregistrement...
        private void BtnEnregistrer_Click(object sender, EventArgs e)
        {
            Enrgistrer();
        }

        private void Enrgistrer()
        {
            using (MySqlCommand cmd=new MySqlCommand ())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "INSERT INTO Enseignant(noms,Genre,Fonction) values(@noms,@Genre,@Fonction)";
                // les parametres MySql...
                MySqlParameter p_noms = new MySqlParameter("@noms", MySqlDbType.VarChar, 50);
                MySqlParameter p_genre = new MySqlParameter("@Genre", MySqlDbType.Enum);
                MySqlParameter p_fonction = new MySqlParameter("@Fonction", MySqlDbType.Set);

                //les valeurs de parametres....

                p_noms.Value = txtNoms.Text;
                p_genre.Value = cbxGenre.Text;
                p_fonction.Value = cbxFonction.Text;

                //assigantion à la commande
                cmd.Parameters.Add(p_noms);
                cmd.Parameters.Add(p_genre);
                cmd.Parameters.Add(p_fonction);

                try
                {
                    DialogResult result = MessageBox.Show("Voulez-vous vraiment enregistrer ?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int RowsAffected = cmd.ExecuteNonQuery();
                       
                        MessageBox.Show($"Enrgistrement effectué avec succès \n{RowsAffected} ligne(s) affectée(s) !!!");
                    }
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            txtNoms.Clear();
            cbxFonction.Text = "";
            cbxGenre.Text="";
            txtNoms.Focus();
        }

        private void FrmNouveauEnseigant_Load(object sender, EventArgs e)
        {

        }
    }
}
