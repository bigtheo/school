using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class FrmDuplicata : Form
    {
        public FrmDuplicata()
        {
            InitializeComponent();
        }

        #region Barre de titre

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr handle, int v1, int v2, int v3);

        private void PanelBarreDeTitre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Barre de titre

        private void GetInformationRecu()
        {
            using (MySqlCommand cmd=new MySqlCommand ())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select e.id,concat_ws(' ',e.nom,e.postnom,e.prenom) as noms,c.nom as classe,f.designation,f.montant,p.date_paie as 'Date heure' from " +
                    "paiement_mensuel as p " +
                    "inner join frais_mensuel as f on f.id = p.frais_mensuel_id " +
                    "inner join eleve as e on e.id = p.eleve_id " +
                    "inner join classe as c on c.id = e.classe_id " +
                    "where p.id = @id ";

                MySqlParameter p_id = new MySqlParameter("@id", MySqlDbType.Int64)
                {
                    Value = nup_id.Value
                };
                cmd.Parameters.Add(p_id);

                txt_nom.Clear();
                txt_classe.Clear();
                txt_frais.Clear();
                txt_montant.Clear();
                txt_date.Clear();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //obtenir les valeurs 
                    txt_nom.Text = reader.GetValue(0).ToString() +" "+ reader.GetValue(1).ToString();
                    txt_classe.Text = reader.GetValue(2).ToString();
                    txt_frais.Text = reader.GetValue(3).ToString();
                    txt_montant.Text = reader.GetValue(4).ToString();
                    txt_date.Text = reader.GetValue(5).ToString();

                }

            }
        }

        private void Nup_id_ValueChanged(object sender, EventArgs e)
        {
            GetInformationRecu();
        }

        #region Reçu du paiement mensuel

        /// <summary>
        /// cette méthode permet de créer les document qui contient les infos du réçu
        /// </summary>
        private void CreerRecu()
        {
            #region Création du document

            this.Cursor = Cursors.WaitCursor;

            Rectangle taille = new iTextSharp.text.Rectangle(new Rectangle(288, 720)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille);
            doc.SetMargins(30, 30, 7, 30);
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ficher_rapport.pdf");
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                PdfWriter.GetInstance(doc, fs);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            doc.Open(); //ouverture du document pour y écrire

            #endregion Création du document

            #region les polices utilisées et les couleurs

            BaseColor couleur_cellule = new BaseColor(0, 0, 0);
            BaseColor couleur_noms = new BaseColor(1, 101, 201);

            Font police_cellule = FontFactory.GetFont("TIMES NEW ROMAN", 9, couleur_cellule);
            Font police_noms_eleve = FontFactory.GetFont("TIMES NEW ROMAN", 11, couleur_noms);
            Font police_nom_ecole = FontFactory.GetFont("TIMES NEW ROMAN", 14, 1, new BaseColor(255, 140, 0));

            #endregion les polices utilisées et les couleurs

            #region les en-têtes

            #region en-tête date et annee scolaire

            //On récupére le jour actuel Et l'annéé scolaire

            string date_du_jour = txt_date.Text;
            string anne_scolaire = "2020-2021";

            Chunk chunk_date_du_jour = new Chunk("Date du jour ", police_cellule);
            Chunk chunk_anne_scolaire = new Chunk("Année Scolaire ", police_cellule);

            //les paragraphes
            Paragraph p_date_du_jour = new Paragraph(chunk_date_du_jour);
            Paragraph p_date_annee_scolaire = new Paragraph(chunk_anne_scolaire);

            //on crée le petit table qui va prendre les en-tête (date du jour et année scolaire)
            PdfPTable tableau_entete = new PdfPTable(2); //deux colonns

            tableau_entete.SetWidths(new float[] { 40, 60 });
            //on ajoute les en-entetes au tableaux
            tableau_entete.AddCell(p_date_annee_scolaire);
            tableau_entete.AddCell(p_date_du_jour);

            //les paragraphes des mes chunks
            Paragraph p_chunk_annee_scolaire = new Paragraph(new Chunk(anne_scolaire, police_cellule));
            Paragraph p_chunk_date_du_jour = new Paragraph(new Chunk(date_du_jour, police_cellule));

            //on ajoute les valeurs aux tableau
            tableau_entete.AddCell(p_chunk_annee_scolaire);
            tableau_entete.AddCell(p_chunk_date_du_jour);

            #endregion en-tête date et annee scolaire

            #region les en-têtes nom école, id et noms élèves

            string nom_ecole ="   "+ Operations.ObtenirNomEtablissement() + "\n";
            string noms_eleve = txt_nom.Text + " " + txt_classe.Text;
            string numero_recu = "Duplicata\nréçu N° " + nup_id.Value;
            string adresse_ecole = ObtenirAdresse();

            Chunk chunk_nom_ecole = new Chunk(nom_ecole, police_nom_ecole);
            Chunk chunk_noms_eleve = new Chunk(noms_eleve, police_noms_eleve);
            Chunk chunk_numero_recu = new Chunk(numero_recu, police_cellule);
            Chunk chunk_adresse_ecole = new Chunk(adresse_ecole, police_cellule);

            //les paragraphes

            Paragraph p_chunk_nom_ecole = new Paragraph(chunk_nom_ecole);
            Paragraph p_chunk_noms_eleve = new Paragraph(chunk_noms_eleve);
            Paragraph p_chunk_numero_recu = new Paragraph(chunk_numero_recu);
            Paragraph p_chunk_adresse_ecole = new Paragraph(chunk_adresse_ecole);

            //alignement

            p_chunk_nom_ecole.Alignment = 0;
            p_chunk_adresse_ecole.Alignment = 0;
            p_chunk_noms_eleve.Alignment = 1;
            p_chunk_numero_recu.Alignment = 1;

            #endregion les en-têtes nom école, id et noms élèves

            #endregion les en-têtes

            #region le tableau principal

            PdfPTable tableau_principal = new PdfPTable(2); //déclarer le tableau de deux colonnes
            Chunk chunk_frais = new Chunk("Frais payés", police_cellule);
            Chunk chunk_frais_valeur = new Chunk(txt_frais.Text, police_cellule);

            //les paragraphes
            PdfPCell colspan = new PdfPCell(new Phrase("Information sur le paiement", police_cellule))
            {
                Colspan = 2,
                HorizontalAlignment = 1,
                PaddingBottom = 5,
                PaddingTop = 5
            };
            Paragraph p_chunck_frais = new Paragraph(chunk_frais);
            Paragraph p_chunck_montant = new Paragraph(chunk_frais_valeur);

            tableau_principal.AddCell(colspan);
            tableau_principal.AddCell(p_chunck_frais);
            tableau_principal.AddCell(p_chunck_montant);
            //on ajoute les en-têtes

            //on ajoute les valeurs
            Chunk chunk_montant = new Chunk("Montant payé", police_cellule);
            Chunk chunk_montant_valeur = new Chunk(txt_montant.Text  + " CDF", police_cellule);
            //les paragraphes des mes chunks
            Paragraph p_chunk_montant = new Paragraph(chunk_montant);
            Paragraph p_chunk_montant_valeur = new Paragraph(chunk_montant_valeur);

            tableau_principal.AddCell(p_chunk_montant);
            tableau_principal.AddCell(p_chunk_montant_valeur);

            #endregion le tableau principal

            #region ajout des en-têtes et tableau aux document

            //on ajoutes les élèments de l'en-entête
            Paragraph passerligne = new Paragraph("\n", police_cellule);
            doc.Add(p_chunk_nom_ecole);
            doc.Add(p_chunk_adresse_ecole);
            doc.Add(p_chunk_noms_eleve);
            doc.Add(p_chunk_numero_recu);
            doc.Add(passerligne); //on passe à la ligne

            //on ajoutes le tableau en-tête
            doc.Add(tableau_entete);

            doc.Add(passerligne); //on passe à la ligne
                                  //on ajoute le tableau principal
            doc.Add(tableau_principal);

            #endregion ajout des en-têtes et tableau aux document

            //on ferme le document après écriture
            doc.Close();
            new FrmApercuAvantImpression().ShowDialog();

            this.Cursor = Cursors.Default;
        }

        private string ObtenirAdresse()
        {
            using (MySqlCommand cmd = new MySqlCommand())
            {
                Connexion.connecter();
                cmd.Connection = Connexion.con;
                cmd.CommandText = "select adresse from configuration limit 1";
                return cmd.ExecuteScalar().ToString();
            }
        }

        #endregion Reçu du paiement mensuel

        private void BtnImprimer_Click(object sender, EventArgs e)
        {
            CreerRecu();
        }
    }
}