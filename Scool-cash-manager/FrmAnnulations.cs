﻿using MySql.Data.MySqlClient;
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
    public partial class FrmAnnulations : Form
    {
        public FrmAnnulations()
        {
            InitializeComponent();

        }
        private void FrmAnnulations_Load(object sender, EventArgs e)
        {
            ListerAnnulationsMensuel();
        }
        #region Lister les annualations

        private void ListerAnnulationsMensuel()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie',p.date_paie_suppression as 'Date Suppresion',concat_ws(' ',e.nom,e.postnom,e.prenom) as Noms,f.Montant from paiement_mensuel_archive as p " +
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
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie',p.date_annulation as 'Date Suppresion',concat_ws(' ',e.nom,e.postnom,e.prenom) as Noms,f.Montant from paiement_etat_archive as p " +
                    " inner join eleve as e on e.id = p.eleve_id " +
                    " inner join classe_frais_etat as f on f.id=p.frais_etat_id " +
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
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie',p.date_paie_suppression as 'Date Suppresion',concat_ws(' ',e.nom,e.postnom,e.prenom) as Noms,f.Montant from paiement_mensuel_archive as p " +
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
        private void ListerAnnulationsExamen()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie',p.date_paie_suppression as 'Date Suppresion',concat_ws(' ',e.nom,e.postnom,e.prenom) as Noms,f.Montant from paiement_mensuel_archive as p " +
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
        private void ListerAnnulationsAccompte()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id as 'N° reçu',p.date_paie as 'Date de la paie',p.date_paie_suppression as 'Date Suppresion',concat_ws(' ',e.nom,e.postnom,e.prenom) as Noms,f.Montant from paiement_mensuel_archive as p " +
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

        private void ListerPaiement()
        {
            switch (cbx_frais.SelectedIndex)
            {
                case 0:
                    ListerAnnulationsMensuel();
                    break;
                case 1:
                    ListerAnnulationsExamen();
                    break;
                case 2:
                    ListerAnnulationsExetat();
                    break;
                case 3:
                    ListerAnnulationsEtat();
                    break;
                case 4:
                    ListerAnnulationsAccompte();
                    break;
                default:
                    MessageBox.Show("Veuillez selectionner le frais","Information",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    break;
            }
        }
        #endregion

        #region Restauration
        private void SupprimerRecu()
        {
            if (long.TryParse(dgvliste.CurrentRow.Cells[0].Value.ToString(), out long numero_recu))
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = Connexion.con;
                        cmd.CommandText = "delete from paiement_mensuel_archive where id=@id";
                        DialogResult result = MessageBox.Show($"Voulez-vous vraiment restaurer le reçu N° {numero_recu}", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        MySqlParameter p_id = new MySqlParameter("@id", MySqlDbType.Int64);
                        p_id.Value = numero_recu;
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
        private void btnRestaurer_Click(object sender, EventArgs e)
        {
            SupprimerRecu();
            ListerAnnulationsMensuel();
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
        #endregion

        private void cbx_frais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListerPaiement();
        }
    }
}
