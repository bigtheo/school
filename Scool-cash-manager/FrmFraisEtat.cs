using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class FrmFraisEtat : Form
    {
        public FrmFraisEtat()
        {
            InitializeComponent();
            Operations.ChargerClassesDansComboBox(cbx_classe);
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            AfficherToutPaiemenFraisEtat();
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new FrmNouveauPaiementFraisEtat().ShowDialog();
        }

        private void C(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void FrmFraisEtat_Load(object sender, EventArgs e)
        {
            Views.AfficherTout("v_paimentFraisEtatJounalier", dgvliste);
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
        #region Affichages

        private void AfficherToutPaiemenFraisEtat()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id, p.date_paie as 'Date et Heure',concat_ws(' ',e.nom,e.postnom) as Noms,f.Designation,c.nom as 'Classe',cf.montant from paiement_etat as p " +
                    "inner join eleve as e on e.id = p.eleve_id " +
                    "inner join frais_etat as f on f.id = p.frais_etat_id " +
                    "inner join classe as c on c.id = e.classe_id " +
                    "inner join classe_frais_etat as cf on c.id = cf.classe_id and cf.frais_etat_id = f.id";

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
        private void AfficherToutPaiementParClasse()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select p.id, p.date_paie as 'Date et Heure',concat_ws(' ',e.nom,e.postnom) as Noms,f.Designation,c.nom as 'Classe',cf.montant from paiement_etat as p " +
                    "inner join eleve as e on e.id = p.eleve_id " +
                    "inner join frais_etat as f on f.id = p.frais_etat_id " +
                    "inner join classe as c on c.id = e.classe_id " +
                    "inner join classe_frais_etat as cf on c.id = cf.classe_id and cf.frais_etat_id = f.id where c.nom='"+cbx_classe.Text+"'";

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

        #endregion Affichages

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
            string t_title_principal = "Liste des paiements du frais de l'état";
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

        private void BtnImprimer_Click(object sender, EventArgs e)
        {
            Imprimer();
        }

        private void Cbx_classe_SelectedIndexChanged(object sender, EventArgs e)
        {
            AfficherToutPaiementParClasse();
        }
    }
}