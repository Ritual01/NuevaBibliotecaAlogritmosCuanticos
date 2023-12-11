using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NuevaBibliotecaAlogritmosCuanticos
{

    public partial class FormularioShor : Form
    {
        private Button factorizeButton;
        private TextBox inputTextBox;
        private Button CerrarButton;
        Panel panel = new Panel();
        private List<int> factorsFound = new List<int>();
        private Button btnYoutube;
        private Label lblTitulo;
        private Label lblTexto;
        private PictureBox pictureShor;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.LightSkyBlue, Color.DarkMagenta, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        public FormularioShor()
        {
            InitializeComponent();
            this.Text = "Algoritmo de Shor";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            // Crear TextBox
            inputTextBox = new TextBoxConBordesRedondeados();
            inputTextBox.Location = new Point(170, 455);
            Controls.Add(inputTextBox);
            inputTextBox.KeyDown += new KeyEventHandler(inputTextBox_KeyDown);

            //Crear botón Youtube
            btnYoutube = new Button();
            btnYoutube.Text = "Youtube";
            btnYoutube.Location = new Point(620, 110);
            btnYoutube.TextAlign = ContentAlignment.MiddleCenter;
            btnYoutube.Click += new EventHandler(btnYoutube_Click);
            Controls.Add(btnYoutube);

            //Crear Picture Box
            pictureShor = new PictureBox();
            pictureShor.Location = new Point(170, 160);
            pictureShor.Width = 465;
            pictureShor.Height = 200;
            pictureShor.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureShor.Image = Image.FromFile("Shor.png");
            Controls.Add(pictureShor);

            // Crear Button
            factorizeButton = new Button();
            factorizeButton.Text = "Factorizar";
            factorizeButton.Location = new Point(185, 480);
            factorizeButton.Click += new EventHandler(FactorizeButton_Click);
            Controls.Add(factorizeButton);

            //Botón cerrar
            CerrarButton = new BotonCierre();
            CerrarButton.Size = new Size(30, 30);
            CerrarButton.BackColor = Color.Transparent;
            CerrarButton.Location = new Point(this.Width - CerrarButton.Width, 0);
            Controls.Add(CerrarButton);

            panel = new Panel();
            panel.Size = new Size(350, 200);
            panel.Location = new Point(400, 380);
            Controls.Add(panel);
            panel.Paint += new PaintEventHandler(panel_Paint);

            //Crear titulo y texto
            lblTexto = new Label();
            lblTexto.Text = "El algoritmo de Shor es un algoritmo cuántico que se utiliza para descomponer un número en factores de una manera eficiente. Fue desarrollado por Peter Shor y es especialmente útil en el campo de la criptografía, ya que puede romper sistemas de clave pública como RSA en tiempo polinómico";
            lblTexto.Size = new Size(400, 100);
            lblTexto.Location = new Point(200, 50);
            lblTexto.BackColor = Color.Transparent;
            Controls.Add(lblTexto);
            lblTexto.TextAlign = ContentAlignment.MiddleCenter;

            lblTitulo = new Label();
            lblTitulo.Size = new Size(200, 30);
            lblTitulo.Text = "Algoritmo de Shor";
            lblTitulo.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTitulo.Location = new Point(300, 20);
            lblTitulo.BackColor = Color.Transparent;
            Controls.Add(lblTitulo);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FactorizeButton_Click(sender, e);
            }
        }
        private void FactorizeButton_Click(object sender, EventArgs e)
        {
            int numberToFactor = Convert.ToInt32(inputTextBox.Text);

            // Call Shor's algorithm function to factorize the number
            FactorizeNumber(numberToFactor);
        }

        private void btnYoutube_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/watch?v=6SlX_FR4wOM";

            // Abres el enlace en el navegador predeterminado del sistema
            Process.Start(url);
        }
        // Function to factorize the number using Shor's algorithm
        private void FactorizeNumber(int number)
        {
            factorsFound.Clear();
            var numbersToFactor = new Queue<int>();
            numbersToFactor.Enqueue(number);

            while (numbersToFactor.Count > 0)
            {
                int numberToFactor = numbersToFactor.Dequeue();
                bool factorFound = false;

                for (int i = 2; i < numberToFactor; i++) // Loop through numbers to find factors
                {
                    int gcd = GCD(i, numberToFactor);

                    if (gcd != 1 && gcd != numberToFactor)
                    {
                        numbersToFactor.Enqueue(gcd);
                        numbersToFactor.Enqueue(numberToFactor / gcd);
                        factorFound = true;
                        break;
                    }
                }

                if (!factorFound)
                {
                    factorsFound.Add(numberToFactor);
                }
            }

            factorsFound.Sort(); // Sort factors

            // Redraw the panel with the factors
            panel.Invalidate();
        }

        // Método para manejar el evento Paint
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            // Obtener el objeto Graphics del evento
            Graphics g = e.Graphics;

            // Dibujar cada factor en el Panel
            for (int i = 0; i < factorsFound.Count; i++)
            {
                g.DrawString(factorsFound[i].ToString(), new Font("Arial", 16), Brushes.Black, new PointF(10, 10 + i * 20));
            }
        }

        // Function to calculate the Greatest Common Divisor (GCD)
        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }

}