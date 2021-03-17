﻿using System;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
            CustomizerDesign();
        }

        #region slide menu et sous menu

        private void CustomizerDesign()
        {
            panelSousMenuPaiement.Visible = false;
            panelSousMenuConfigurationFrais.Visible = false;
            panelSousMenuJournal.Visible = false;
            panelSousMenuSolvabilite.Visible = false;
            //...
        }

        private void CacherSousMenu()
        {
            if (panelSousMenuConfigurationFrais.Visible == true)
                panelSousMenuConfigurationFrais.Hide();
            if (panelSousMenuPaiement.Visible == true)
                panelSousMenuPaiement.Hide();
            if (panelSousMenuSolvabilite.Visible == true)
                panelSousMenuSolvabilite.Hide();
            if (panelSousMenuJournal.Visible == true)
                panelSousMenuJournal.Hide();
        }

        private void AfficherSousMenu(Panel panelSousMenu)
        {
            if (panelSousMenu.Visible == false)
            {
                CacherSousMenu();
                panelSousMenu.Visible = true;
            }
            else
                panelSousMenu.Hide();
        }


        private Form activeForm = null;

        private void OuvrirFormulaire(Form formulaireEnfant)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = formulaireEnfant;
            formulaireEnfant.TopLevel = false;
            formulaireEnfant.FormBorderStyle = FormBorderStyle.None;
            formulaireEnfant.Dock = DockStyle.Fill;
            panelConteneur.Controls.Add(formulaireEnfant);
            panelConteneur.Tag = formulaireEnfant;
            formulaireEnfant.BringToFront();
            formulaireEnfant.Show();

        }

       

        private void BtnMenuPaiement_Click(object sender, EventArgs e)
        {
            AfficherSousMenu(panelSousMenuPaiement);
        }

        private void BtnConfiguirationFrais_Click(object sender, EventArgs e)
        {
            AfficherSousMenu(panelSousMenuConfigurationFrais);
        }

        private void BtnSolvabilite_Click(object sender, EventArgs e)
        {
            AfficherSousMenu(panelSousMenuSolvabilite);
        }

        private void BtnJournal_Click(object sender, EventArgs e)
        {
            AfficherSousMenu(panelSousMenuJournal);
        }

        #endregion slide menu et sous menu

        private void BtnPaiemlentInscription_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmInscription());
            CacherSousMenu();
        }

        private void BtnFraisPaiementMensuel_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmFraisMensuel());
            CacherSousMenu();
        }

        private void BtnPaiementExamens_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmFraisExamen());
            CacherSousMenu();
        }

        private void BtnPaiementExetat_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmExetat());
            CacherSousMenu();
        }

        private void BtnPaiementFraisEtat_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmFraisEtat());
            CacherSousMenu();
        }

        private void BtnAchatManuel_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmManuel());
            CacherSousMenu();
        }

        private void BtnConfigMensuel_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmConfigFraisMensuel());
            CacherSousMenu();
        }

        private void BtnConfigExamen_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmConfigExamen());
            CacherSousMenu();
        }

        private void BtnManuels_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmConfigManuels());
            CacherSousMenu();
        }

        private void BtnConfigFraisEtat_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmConfigFraisEtat());
            CacherSousMenu();
        }

        private void BtnInsolvables_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmInsolvables());
            CacherSousMenu();
        }

        private void BtnSolvables_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmSolvables());
            CacherSousMenu();
        }

        private void BtnJournalCentralise_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmJournalCentralise());
            CacherSousMenu();
        }

        private void BtnJournalFrais_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmJournalFrais());
            CacherSousMenu();
        }

        #region configuration
        private void BtnParametre_Click(object sender, EventArgs e)
        {
            ContextMenuStrip MenuContextuel = new ContextMenuStrip();
            MenuContextuel.Items.Add("Configuration",null, Click_BtnConfiguration);
            MenuContextuel.Items.Add("Sauvegarde", null, Click_Sauvegarde);
            MenuContextuel.Show(btnParametre,50,50);


        }

        private void Click_Sauvegarde(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Operations.Backup();
            CacherSousMenu();
            this.Cursor = Cursors.Default;
        }

        private void Click_BtnConfiguration(object sender, EventArgs e)
        {
            new FrmConfigurationApplication().ShowDialog();
            CacherSousMenu();
        }
        #endregion

        private void BtnConfigClasse_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmConfigClasses());
            CacherSousMenu();
        }

        private void BtnConfigExetat_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new frmConfigExetat());
            CacherSousMenu();
        }

        private void BtnVuDensemble_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmVuDensemble());
            CacherSousMenu();
        }

        private void BntEcheance_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmEcheance());
            CacherSousMenu();
        }

        private void BtnAccompte_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmAccompte());
            CacherSousMenu();
        }

        private void BtnEnseignat_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmEnseignant());
            CacherSousMenu();

        }

        private void BtnAnnulations_Click(object sender, EventArgs e)
        {
            OuvrirFormulaire(new FrmAnnulations());
            CacherSousMenu();
        }
    }
}