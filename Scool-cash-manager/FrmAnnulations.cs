using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class FrmAnnulations : Form
    {
        public FrmAnnulations()
        {
            InitializeComponent();
        }

        private void FrmAnnulations_Load(object sender, EventArgs e)
        {
            ListerPaiement();
            MasquerBoutonRestaurer();
        }

        #region Lister les annualations

        private void ListerAnnulationsMensuel()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie',p.date_paie_suppression as 'Date Suppresion',concat_ws(' ',e.nom,e.postnom,e.prenom) as Noms,f.Montant " +
                    ",f.designation from paiement_mensuel_archive as p " +
                    " inner join eleve as e on e.id = p.eleve_id " +
                    " inner join frais_mensuel as f on f.id=p.frais_mensuel_id " +
                    " where date(date_paie_suppression)=@dateAnnulation";
                MySqlParameter p_date = new MySqlParameter("@dateAnnulation", MySqlDbType.Date)
                {
                    Value = dtp_date.Value
                };
                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvliste.DataSource = table;
                }
            }
        }

        private void ListerAnnulationsEtat()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie'," +
                    " p.date_annulation as 'Date Suppresion',concat_ws(' ', e.nom, e.postnom, e.prenom) as Noms," +
                    " f.Montant,fe.designation from paiement_etat_archive as p " +
                    " inner join eleve as e on e.id = p.eleve_id " +
                    " inner join classe_frais_etat as f on f.id = p.frais_etat_id " +
                    " inner join frais_etat as fe on fe.id = f.frais_etat_id" +
                    " where date(p.date_annulation)=@dateAnnulation";
                MySqlParameter p_date = new MySqlParameter("@dateAnnulation", MySqlDbType.Date)
                {
                    Value = dtp_date.Value
                };
                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvliste.DataSource = table;
                }
            }
        }

        private void ListerAnnulationsExetat()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie'," +
                    " p.date_Annulation as 'Date Suppresion' " +
                    " ,concat_ws(' ', e.nom, e.postnom, e.prenom) as Noms,cfe.montant," +
                    " f.designation from paiement_exetat_archive as p " +
                    " inner join eleve as e on e.id = p.eleve_id " +
                    " inner join frais_exetat as f on f.id = p.frais_exetat_id " +
                    " inner join classe_frais_exetat as cfe on f.id = cfe.frais_exetat_id " +
                    " where date(p.date_Annulation)=@dateAnnulation";
                MySqlParameter p_date = new MySqlParameter("@dateAnnulation", MySqlDbType.Date)
                {
                    Value = dtp_date.Value
                };
                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvliste.DataSource = table;
                }
            }
        }

        private void ListerAnnulationsExamen()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie'," +
                   " p.date_Annulation as 'Date Suppresion',concat_ws(' ', e.nom, e.postnom, e.prenom) as Noms," +
                   " cfe.Montant,f.designation " +
                   " from paiement_examen_archive as p " +
                   " inner join eleve as e on e.id = p.eleve_id " +
                   " inner join frais_examen as f on f.id = p.frais_examen_id " +
                   " INNER JOIN classe_frais_exetat as cfe on cfe.id = p.frais_examen_id " +
                   " where date(date_Annulation)=@dateAnnulation";
                MySqlParameter p_date = new MySqlParameter("@dateAnnulation", MySqlDbType.Date)
                {
                    Value = dtp_date.Value
                };
                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvliste.DataSource = table;
                }
            }
        }

        private void ListerAnnulationsAccompte()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select a.id as 'N° reçu',a.date_paie as 'Date de la paie'," +
                    "a.date_annulation as 'Date Suppresion',concat_ws(' ', e.nom, e.postnom, e.prenom) as Noms,a.montant" +
                    " from accompte_archive a " +
                    "INNER JOIN eleve e on e.id = a.eleve_id " +
                    "INNER join frais_mensuel f on f.id = a.frais_mensuel_id " +
                    "where date(a.date_annulation)=@dateAnnulation";


                MySqlParameter p_date = new MySqlParameter("@dateAnnulation", MySqlDbType.Date)
                {
                    Value = dtp_date.Value
                };
                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvliste.DataSource = table;
                }
            }
        }

        private void ListerPaiement()
        {
            switch (cbx_frais.SelectedIndex)
            {
                case 0:
                    ListerAnnulationsMensuel();
                    lblMessage.Text = "Les annulations-Frais mensuel";
                    break;

                case 1:
                    ListerAnnulationsExamen();
                    lblMessage.Text = "Les annulations-Frais examen";
                    break;

                case 2:
                    ListerAnnulationsExetat();
                    lblMessage.Text = "Les annulations-Frais exetat";
                    break;

                case 3:
                    ListerAnnulationsEtat();
                    lblMessage.Text = "Les annulations-Frais de l'état";
                    break;

                case 4:
                    ListerAnnulationsAccompte();
                    lblMessage.Text = "Les annulations-Accomptes";
                    break;

                default:
                    MessageBox.Show("Veuillez selectionner le frais", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        #endregion Lister les annualations

        #region Restauration

        private void RestaurerPaiement(string table)
        {
            if (long.TryParse(dgvliste.CurrentRow.Cells[0].Value.ToString(), out long numero_recu))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = Connexion.con;
                        cmd.CommandText = $"delete from {table} where id=@id";
                        DialogResult result = MessageBox.Show($"Voulez-vous vraiment restaurer le reçu N° {numero_recu}", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        MySqlParameter p_id = new MySqlParameter("@id", MySqlDbType.Int64)
                        {
                            Value = numero_recu
                        };
                        cmd.Parameters.Add(p_id);
                        if (result == DialogResult.Yes)
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show($"opération effectuée avec succès, {rowsAffected} ligne(s) affectées", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("conversion impossible", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRestaurer_Click(object sender, EventArgs e)
        {
            Restaurer();
            ListerPaiement();
        }
        private void Restaurer()
        {
            switch (cbx_frais.SelectedIndex)
            {
                case 0:
                    RestaurerPaiement("paiement_mensuel_archive");
                    break;
                case 1:
                    ListerAnnulationsExamen();
                    RestaurerPaiement("paiement_examen_archive");
                    break;
                case 2:
                    RestaurerPaiement("paiement_exetat_archive");
                    break;
                case 3:
                    RestaurerPaiement("paiement_etat_archive");
                    break;
                case 4:
                    RestaurerPaiement("accompte_archive");
                    break;

                default:
                    MessageBox.Show("Veuillez selectionner le frais", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
        private void Dgvliste_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvliste.Rows.Count >= 0)
            {
                btnRestaurer.Enabled = true;
            }
            else
            {
                btnRestaurer.Enabled = false;
            }
        }
        private void MasquerBoutonRestaurer()
        {
            if (dgvliste.Rows.Count > 0)
            {
                btnRestaurer.Enabled = true;
            }
            else
            {
                btnRestaurer.Enabled = false;
            }
        }
        #endregion Restauration

        private void Cbx_frais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListerPaiement();
            MasquerBoutonRestaurer();
        }

        private void Dtp_date_ValueChanged(object sender, EventArgs e)
        {
            ListerPaiement();
            MasquerBoutonRestaurer();
        }
    }
}