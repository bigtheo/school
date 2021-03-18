using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Scool_cash_manager
{
    public partial class frmFraisExamen : Form
    {
        public frmFraisExamen()
        {
            InitializeComponent();
        }

        private void FrmFraisExamen_Load(object sender, EventArgs e)
        {
            ListerJournal();
        }
        private void ListerJournal()
        {
            Views.AfficherTout("V_PaiementExemanJournalier", dgvliste);
            if (dgvliste.Rows.Count == 0)
            {
                dgvliste.Hide();
                lblMessage.Show();
            }
        }
        private bool DGVPossedeUnEnregistrement()
        {
            return dgvliste.Rows.Count > 0;
        }
        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new frmNouveauPaiementExamen().ShowDialog();
        }
        private void AnnulerUnPaiement()
        {
            using (MySqlCommand cmd=new MySqlCommand())
            {
                cmd.Connection = Connexion.con;
                cmd.CommandText = "Delete from paiement_examen where id=@id";
                MySqlParameter p_id = new MySqlParameter("@id", MySqlDbType.Int64)
                {
                    Value = dgvliste.CurrentRow.Cells[0].Value
                };
                cmd.Parameters.Add(p_id);
                DialogResult result = MessageBox.Show($"Voulez-vous vraiment annuler le reçu numéro {p_id.Value}", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ;
                if (result == DialogResult.Yes)
                {
                    int RowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Voulez-vous vraiment annuler le reçu numéro {p_id.Value}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            AnnulerUnPaiement();
            ListerJournal();
        }

        private void dgvliste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVPossedeUnEnregistrement())
            {
                btnAnnuler.Enabled = true;
            }
            else
            {
                btnAnnuler.Enabled = false;
            }
        }
    }
}