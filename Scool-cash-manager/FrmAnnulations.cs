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
    public partial class FrmAnnulations : Form
    {
        public FrmAnnulations()
        {
            InitializeComponent();

        }
        private void FrmAnnulations_Load(object sender, EventArgs e)
        {
            ListerAnnulations();
        }
        private void ListerAnnulations()
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
    }
}
