﻿using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Scool_cash_manager
{
    public partial class frmFraisExamen : Form
    {
        public frmFraisExamen()
        {
            InitializeComponent();
            Operations.ChargerClassesDansComboBox(cbx_classe);
        }

        private void FrmFraisExamen_Load(object sender, EventArgs e)
        {
            ListerJournal();
        }
        private void ListerJournal()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    Connexion.connecter();
                    cmd.Connection = Connexion.con;
                    cmd.CommandText = "SELECT pe.id AS id,pe.date_paie AS 'Date et heure'," +
                        "concat_ws(' ', e.nom, e.postnom, e.prenom) AS Noms, fe.designation AS 'Désignation', c.nom AS Classe from eleve as e " +
                        " INNER JOIN classe as c on c.id = e.classe_id " +
                        " INNER JOIN paiement_examen as pe on pe.eleve_id = e.id " +
                        " INNER JOIN frais_examen as fe on fe.id = pe.frais_examen_id " +
                        " WHERE c.nom = @classe";
                    MySqlParameter p_classe = new MySqlParameter("@classe", MySqlDbType.VarChar)
                    {
                        Value = cbx_classe.Text
                    };
                    cmd.Parameters.Add(p_classe);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dgvliste.DataSource = table;
                    }
                    if (dgvliste.Rows.Count == 0)
                    {
                        dgvliste.Hide();
                        lblMessage.Show();
                    }
                    else
                    {
                        dgvliste.Show();
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
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
                DialogResult result = MessageBox.Show($"Voulez-vous vraiment annuler le reçu numéro {p_id.Value} ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ;
                if (result == DialogResult.Yes)
                {
                    int RowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Opération effectuée avec succès, \n {RowsAffected} ligne(s) affectée(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            AnnulerUnPaiement();
            ListerJournal();
        }

        private void Dgvliste_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void cbx_classe_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListerJournal();
        }
    }
}