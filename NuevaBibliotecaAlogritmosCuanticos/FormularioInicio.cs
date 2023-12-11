using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NuevaBibliotecaAlogritmosCuanticos
{
    public class BotonCierre : Button
    {
        public BotonCierre()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Text = "X";
            this.ForeColor = Color.White;
            this.BackColor = Color.FromArgb(128, 0, 128);
            this.Click += new EventHandler(BotonCierre_Click);
        }

        private void BotonCierre_Click(object sender, EventArgs e)
        {
            Form formulario = this.FindForm();
            formulario.Close();
        }
    }
    public class TextBoxConBordesRedondeados : TextBox
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
    }
    public partial class FormularioInicio : Form
    {
        private Button InicioBtn;
        private TextBox UsuarioTxt;
        private BotonCierre CerrarBtn;
        private Label InformacionPantallaLbl;
        private PictureBox LogoPbx;
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.FromArgb(128, 0, 128), Color.FromArgb(0, 0, 255), 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        bool usuarioTxtEditado = false;
        public FormularioInicio()
        {
            InitializeComponent();
            this.Text = "Inicio de sesión";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            InicioBtn = new Button();
            InicioBtn.Size = new System.Drawing.Size(70, 20);
            InicioBtn.Location = new System.Drawing.Point(315, 215);
            InicioBtn.Text = "Inicio";
            InicioBtn.Click += new EventHandler(InicioBtn_Click);
            InicioBtn.BackColor = Color.White;
            Controls.Add(InicioBtn);

            UsuarioTxt = new TextBoxConBordesRedondeados();
            UsuarioTxt.Size = new System.Drawing.Size(100, 30);
            UsuarioTxt.Location = new System.Drawing.Point(300, 180);
            UsuarioTxt.Text = "Usuario";
            UsuarioTxt.ForeColor = Color.Gray; 
            UsuarioTxt.GotFocus += (source, e) =>
            {
                if (!usuarioTxtEditado)
                {
                    UsuarioTxt.Text = "";
                    UsuarioTxt.ForeColor = Color.Black;
                }
            };
            UsuarioTxt.TextChanged += (source, e) =>
            {
                usuarioTxtEditado = UsuarioTxt.Text.Length > 0 && UsuarioTxt.Text != "Usuario";
            };
            UsuarioTxt.LostFocus += (source, e) =>
            {
                if (UsuarioTxt.Text == "")
                {
                    usuarioTxtEditado = false;
                    UsuarioTxt.Text = "Usuario";
                    UsuarioTxt.ForeColor = Color.Gray;
                }
            };
            UsuarioTxt.KeyDown += new KeyEventHandler(UsuarioTxt_KeyDown);
            Controls.Add(UsuarioTxt);

            CerrarBtn = new BotonCierre();
            CerrarBtn.Size = new Size(30, 20);
            CerrarBtn.Location = new Point(this.Width - CerrarBtn.Width, 0);
            Controls.Add(CerrarBtn);

            InformacionPantallaLbl = new Label();
            InformacionPantallaLbl.Size = new System.Drawing.Size(500, 20);
            InformacionPantallaLbl.Location = new System.Drawing.Point(2,0) ;
            InformacionPantallaLbl.Text = "Inicio de Sesión";
            InformacionPantallaLbl.BackColor = Color.FromArgb(128, 0, 128);
            InformacionPantallaLbl.ForeColor = Color.White;
            Controls.Add(InformacionPantallaLbl);

            LogoPbx = new PictureBox();
            LogoPbx.Size = new Size(150, 150);
            LogoPbx.Location = new Point(275, 20);
            LogoPbx.BackColor = Color.Transparent;
            LogoPbx.Image = Image.FromFile("condor.png");
            LogoPbx.SizeMode = PictureBoxSizeMode.StretchImage;
            Controls.Add(LogoPbx);

            Panel fondoPnl = new Panel();
            fondoPnl.Size = new Size(200,300);
            fondoPnl.BackgroundImage = Image.FromFile("Algoritmo.png");
            fondoPnl.BackgroundImageLayout = ImageLayout.None;
            this.Controls.Add(fondoPnl);
            fondoPnl.SendToBack();
        }

        private void InicioBtn_Click(object sender, EventArgs e)
        {
            string nombre = UsuarioTxt.Text;
            if (usuarioTxtEditado == true)
            {
                var result = MessageBox.Show("Bienvenido" + " de nuevo " + nombre, "Inicio Autorizado", MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    FormularioMenu form = new FormularioMenu();
                    form.Show();
                    this.Hide();
                }
               
            }
            else
            {
                MessageBox.Show("Por favor, ingresa tu nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UsuarioTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InicioBtn_Click(sender, e);
            }
        }
    }
}
