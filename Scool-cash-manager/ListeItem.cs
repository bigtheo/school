using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Scool_cash_manager
{
    public partial class ListeItem : UserControl
    {
        public ListeItem()
        {
            InitializeComponent();
        }

        #region Proprités

        private string _noms;
        private string _section;
        private string _classe;
        private Color _iconBackground;

        [Category("custom Props")]
        public string Noms
        {
            get { return _noms; }
            set { _noms = value; lbl_noms.Text = value; }
        }

        [Category("custom Props")]
        public string Section
        {
            get { return _section; }
            set { _section = value; lbl_section.Text = value; }
        }
        [Category("Custom Props")]
       

        public Color IconBackground
        {
            get { return _iconBackground; }
            set { _iconBackground = value;panel_color.BackColor = value; }
        }


        [Category("custom Props")]
        public string Classe
        {
            get { return _classe; }
            set { _classe = value; lbl_classe.Text = value; }
        }

        #endregion Proprités
    }
}