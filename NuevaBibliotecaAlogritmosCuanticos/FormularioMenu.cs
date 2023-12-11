using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace NuevaBibliotecaAlogritmosCuanticos
{
    
    
    public partial class FormularioMenu : Form
    {
        Panel menuPanel = new Panel();
        Button menuButton = new Button();
        bool menuExpanded = false;
        private BotonCierre CerrarBtn;
        Timer timer = new Timer();
        Label label = new Label();
        private Button btnCarpeta;


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.LightSkyBlue, Color.DarkMagenta, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            // Actualiza el texto del Label con la hora actual cada vez que se dispara el evento Tick
            label.Text = DateTime.Now.ToString("HH:mm:ss");
        }
            public FormularioMenu()
        {
            InitializeComponent();
            // Configura el Timer
            timer.Interval = 1000; // establece el intervalo en 1000 milisegundos (1 segundo)
            timer.Tick += new EventHandler(this.timer_Tick); // agrega un manejador de eventos para el evento Tick
            timer.Start(); // inicia el Timer

            // Configura el Label
            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            label.Location = new System.Drawing.Point(700,550);
            label.Size = new Size(200, 50);
            label.BackColor = Color.Transparent;

            //Crear botón Youtube
            btnCarpeta = new Button();
            btnCarpeta.Text = "Carpeta del grupo";
            btnCarpeta.Size = new Size(150, 25);
            btnCarpeta.Location = new Point(700, 500);
            btnCarpeta.TextAlign = ContentAlignment.MiddleCenter;
            btnCarpeta.Click += new EventHandler(btnCarpeta_Click);
            Controls.Add(btnCarpeta);

            // Añade el Label al Formulario
            this.Controls.Add(label);

            this.Text = "Base";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Crea el botón del menú
            menuButton.Text = "≡"; // Icono de tres rayas
            menuButton.Location = new Point(0, 0);
            menuButton.Click += MenuButton_Click;
            menuButton.BackColor = Color.Transparent;
            this.Controls.Add(menuButton);

            // Crea el panel del menú
            menuPanel.Size = new Size(0, this.Height);
            menuPanel.Location = new Point(0, 0);
            menuPanel.BackColor = Color.MediumPurple; // Cambia al color que prefieras
            this.Controls.Add(menuPanel);
            menuPanel.BringToFront();

            // Crea los elementos del menú
            Button elemento1 = new Button();
            elemento1.Text = "Algoritmo de Shor";
            elemento1.Location = new Point(10, 30);
            elemento1.Width = 150;
            elemento1.Height = 20;
            elemento1.Click += elemento1_CLick;
            menuPanel.Controls.Add(elemento1);

            Button elemento2 = new Button();
            elemento2.Text = "Algoritmo de Grover";
            elemento2.Width = 150;
            elemento2.Height = 20;
            elemento2.Location = new Point(10, elemento1.Height + 30);
            elemento2.Click += elemento2_CLick;
            menuPanel.Controls.Add(elemento2);

            //boton cerrar
            CerrarBtn = new BotonCierre();
            CerrarBtn.Size = new Size(30, 20);
            CerrarBtn.Location = new Point(this.Width - CerrarBtn.Width, 0);
            Controls.Add(CerrarBtn);
        }


        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            string url = "https://www.youtube.com/watch?v=6SlX_FR4wOM";

            // Abres el enlace en el navegador predeterminado del sistema
            Process.Start(url);
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (menuExpanded)
            {
                // Contrae el menú
                menuPanel.Width = 0;
                menuExpanded = false;
            }
            else
            {
                // Expande el menú
                menuPanel.Width = 300;
                menuExpanded = true;
                menuButton.BringToFront();
            }
        }
        private void elemento1_CLick(object sender, EventArgs e)
        {
            FormularioShor form = new FormularioShor();
            form.Show();
            this.Hide();
        }
        private void elemento2_CLick(object sender, EventArgs e)
        {
            FormularioGrover form = new FormularioGrover();
            form.Show();
            this.Hide();
        }
    }
}
