using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class FrmEnseignant : Form
    {
        public FrmEnseignant()
        {
            InitializeComponent();
            AfficherAffiliation();
            AfficherNombreEnseignantAyantDesEnfant();
            AfficherNombreEnfantAffilie();
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new FrmNouveauEnseigant().ShowDialog();
        }

        private void BtnNouvelleAssociation_Click(object sender, EventArgs e)
        {
            new FrmLienDeParente().ShowDialog();
        }

        #region Selection
        private void AfficherAffiliation()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select a.id,en.id as Matricule ,en.noms as Enseignant,concat_ws(' ',e.nom,e.postnom) as Enfants" +
                    " from associer as a " +
                    "inner join enseignant as en on en.id = a.enseignant_id " +
                    "inner join eleve as e on e.id = a.eleve_id ";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    if (dataGridView1.Rows.Count == 0)
                    {
                        dataGridView1.Hide();
                        lblMessage.Show();
                    }
                    else
                    {
                        dataGridView1.Show();
                        lblMessage.Hide();
                    }
                }
            }
        }

        private void AfficherNombreEnseignantAyantDesEnfant()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select count(*) from enseignant";
                try
                {
                    lbl_nombre_enseigant.Text = Convert.ToString(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AfficherNombreEnfantAffilie()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select count(*) from associer";
                try
                {
                    lbl_nombre_enfant.Text = Convert.ToString(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
    }
}