using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class frmExetat : Form
    {
        public frmExetat()
        {
            InitializeComponent();
            Operations.ChargerClassesDansComboBox(cbx_classe);

        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region au chargement du formulaire

        private void ListerPaiementJournalier()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    Connexion.connecter();
                    cmd.Connection = Connexion.con;
                    cmd.CommandText = "SELECT pe.id AS id,pe.date_paie AS 'Date et heure'," +
                        "concat_ws(' ', e.nom, e.postnom, e.prenom) AS Noms, fe.designation AS 'Désignation', c.nom as Classe,cfe.Montant AS Montant from eleve as e " +
                        " INNER JOIN classe as c on c.id = e.classe_id " +
                        " INNER JOIN paiement_exetat as pe on pe.eleve_id = e.id " +
                        " INNER JOIN frais_exetat as fe on fe.id = pe.frais_exetat_id " +
                        " INNER JOIN classe_frais_exetat as cfe ON fe.id=cfe.frais_exetat_id and  c.id=cfe.classe_id " +
                        " WHERE date(pe.date_paie) =date(now())";
                 
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
                        lblMessage.Hide();
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListerPaiement()
        {
            try
            {
                using (MySqlCommand cmd=new MySqlCommand ())
                {
                    Connexion.connecter();
                    cmd.Connection = Connexion.con;
                    cmd.CommandText = "SELECT pe.id AS id,pe.date_paie AS 'Date et heure'," +
                        "concat_ws(' ', e.nom, e.postnom, e.prenom) AS Noms, fe.designation AS 'Désignation', c.nom as Classe,cfe.Montant AS Montant from eleve as e " +
                        " INNER JOIN classe as c on c.id = e.classe_id " +
                        " INNER JOIN paiement_exetat as pe on pe.eleve_id = e.id " +
                        " INNER JOIN frais_exetat as fe on fe.id = pe.frais_exetat_id " +
                        " INNER JOIN classe_frais_exetat as cfe ON fe.id=cfe.frais_exetat_id and  c.id=cfe.classe_id " +
                        " WHERE c.nom = @classe";
                    MySqlParameter p_classe = new MySqlParameter("@classe", MySqlDbType.VarChar) 
                    { 
                        Value=cbx_classe.Text
                    };
                    cmd.Parameters.Add(p_classe);

                    using (MySqlDataAdapter adapter=new MySqlDataAdapter (cmd))
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
                        lblMessage.Hide();
                    }
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmExetat_Load(object sender, EventArgs e)
        {
            ListerPaiementJournalier();
        }

        #endregion au chargement du formulaire

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new frmNouveauPaiementExetat().ShowDialog();
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

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            AnnulerPaiementExetat();
            ListerPaiement();
        }

        private void cbx_classe_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListerPaiement();

        }

        #region impression de des données de la grille

        //la fonction qui permet d'imprimer la liste des frais exetat
        private void Imprimer()
        {
            #region Création du document
            this.Cursor = Cursors.WaitCursor;
            Document doc = new Document();


            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ficher_rapport.pdf");
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fs);

                PDFBackgroundHelper myEvent = new PDFBackgroundHelper();
                pdfWriter.PageEvent = myEvent;


            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);

            }
            #endregion

            doc.Open(); //on ouvre le document pour y écrire...


            #region Police utilisé
            Font police_titre = FontFactory.GetFont("Arial", 14, 1);
            Font police_titre_principal = FontFactory.GetFont("Times new roman", 12);
            Font police_entete_tableau = FontFactory.GetFont("Century Gothic", 12, 1);
            Font police_Cellule = FontFactory.GetFont("arial", 8);
            Paragraph PasserLigne = new Paragraph(Environment.NewLine);
            #endregion

            #region en-tête du document
            string t_nom_ecole = Operations.ObtenirNomEtablissement() + "\n";
            string t_classe = "Classe : Toutes\n";
            string t_date_du_jour = "Imprimé le  : " + DateTime.Now.ToString() + "\n";
            if (!cbx_classe.Text.Equals(String.Empty))
            {
                t_classe = "Classe : " + cbx_classe.Text + "\n";
            }
            string t_title_principal = "Liste des paiements EXETAT ou TENAFEP.";
            doc.Add(PasserLigne);

            Phrase phrase_nom_ecole = new Phrase(t_nom_ecole, police_titre);
            Phrase phrase_classe = new Phrase(t_classe, police_titre);
            Phrase phrase_date_jour = new Phrase(t_date_du_jour, police_Cellule);
            Chunk chunkUnderline = new Chunk(t_title_principal, police_titre_principal);
            chunkUnderline.SetUnderline(0.4f, -2f);
            Paragraph p_title_principal = new Paragraph(chunkUnderline)
            {
                Alignment = 1 //on centre le titre principal

            };


            //on ajoute les paragraphes au document
            doc.Add(phrase_nom_ecole);
            doc.Add(phrase_classe);
            doc.Add(phrase_date_jour);
            doc.Add(p_title_principal);
            doc.Add(PasserLigne);

            #endregion

            #region le tableau
            PdfPTable tableau = new PdfPTable(6)
            {
                WidthPercentage = 100
            }; //un tableau de 5 colonnes N°, nom, postnom et prenom
            tableau.SetWidths(new float[] { 6, 12, 22, 15, 6, 8 });

            Phrase p_numero = new Phrase("N°", police_entete_tableau);
            Phrase p_Id = new Phrase("Date et Heure", police_entete_tableau);
            Phrase p_nom = new Phrase("Noms", police_entete_tableau);
            Phrase p_postnom = new Phrase("Désignation", police_entete_tableau);
            Phrase p_prenom = new Phrase("Classe", police_entete_tableau);
            Phrase p_adresse = new Phrase("Montant", police_entete_tableau);



            tableau.AddCell(p_numero);
            tableau.AddCell(p_Id);
            tableau.AddCell(p_nom);
            tableau.AddCell(p_postnom);
            tableau.AddCell(p_prenom);
            tableau.AddCell(p_adresse);
            int nombre_ligne = dgvliste.Rows.Count;

            for (int i = 0; i < nombre_ligne; i++)
            {


                tableau.AddCell(new Phrase(dgvliste.Rows[i].Cells[0].Value.ToString(), police_Cellule));
                tableau.AddCell(new Phrase(dgvliste.Rows[i].Cells[1].Value.ToString(), police_Cellule));
                tableau.AddCell(new Phrase(dgvliste.Rows[i].Cells[2].Value.ToString(), police_Cellule));
                tableau.AddCell(new Phrase(dgvliste.Rows[i].Cells[3].Value.ToString(), police_Cellule));
                tableau.AddCell(new Phrase(dgvliste.Rows[i].Cells[4].Value.ToString(), police_Cellule));
                tableau.AddCell(new Phrase(dgvliste.Rows[i].Cells[5].Value.ToString(), police_Cellule));

            }

            doc.Add(tableau);
            #endregion


            doc.Close();
            this.Cursor = Cursors.Default;
            new FrmApercuAvantImpression().Show();
        }
        private void btnImprimer_Click(object sender, EventArgs e)
        {
            Imprimer();
        } 
        #endregion
    }
}