using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class frmExetat : Form
    {
        public frmExetat()
        {
            InitializeComponent();
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region au chargement du formulaire
        private void ListerPaiement()
        {
            Views.AfficherTout("v_paimentExetatJournalier", dgvliste);
            if (dgvliste.Rows.Count == 0)
            {
                dgvliste.Hide();
                lblMessage.Show();
            }
        }
        private void FrmExetat_Load(object sender, EventArgs e)
        {
            ListerPaiement();   
        }
        #endregion
        private void BtnNouveau_Click(object sender, EventArgs e)
        {
          new  frmNouveauPaiementExetat().ShowDialog();
        }
        private bool DGVPossedeUnEnregistrement()
        {
            return dgvliste.Rows.Count > 0;
        }
        private void AnnulerPaiementExetat()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Connexion.con;
                cmd.CommandText = "Delete from paiement_exetat where id=@id";
                MySqlParameter p_id = new MySqlParameter("@id", MySqlDbType.Int64)
                {
                    Value = dgvliste.CurrentRow.Cells[0].Value
                };
                cmd.Parameters.Add(p_id);
                DialogResult result = MessageBox.Show($"Voulez-vous vraiment annuler le reçu numéro {p_id.Value} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int RowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Opération effectuée avec succès, \n {RowsAffected} ligne(s) affectée(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            AnnulerPaiementExetat();
            ListerPaiement();
        }
    }
}
