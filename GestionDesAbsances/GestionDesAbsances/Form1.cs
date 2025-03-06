using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesAbsances
{
    public partial class Form1 : Form
    {
        private Label labelID;
        private TextBox textBoxID;
        private Label labelNom;
        private TextBox textBoxNom;
        private Label labelPrenom;
        private TextBox textBoxPrenom;
        private Label labelGroupe;
        private TextBox textBoxGroupe;

        private Button buttonAjouter;
        private Button buttonModifier;
        private Button buttonSupprimer;
        private Button buttonRechercher;
        private Button buttonGestionAbsences;

        private DataGridView dataGridViewEleves;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Form properties
            this.Text = "Gestion des élèves";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // ID
            labelID = new Label { Text = "ID", Location = new Point(20, 20), Size = new Size(80, 20) };
            textBoxID = new TextBox { Location = new Point(100, 20), Size = new Size(120, 20) };

            // Nom
            labelNom = new Label { Text = "Nom", Location = new Point(20, 50), Size = new Size(80, 20) };
            textBoxNom = new TextBox { Location = new Point(100, 50), Size = new Size(120, 20) };

            // Prenom
            labelPrenom = new Label { Text = "Prénom", Location = new Point(20, 80), Size = new Size(80, 20) };
            textBoxPrenom = new TextBox { Location = new Point(100, 80), Size = new Size(120, 20) };

            // Groupe
            labelGroupe = new Label { Text = "Groupe", Location = new Point(20, 110), Size = new Size(80, 20) };
            textBoxGroupe = new TextBox { Location = new Point(100, 110), Size = new Size(120, 20) };

            // Buttons
            buttonAjouter = new Button { Text = "Ajouter", Location = new Point(20, 150), Size = new Size(80, 30) };
            buttonAjouter.Click += ButtonAjouter_Click;

            buttonModifier = new Button { Text = "Modifier", Location = new Point(110, 150), Size = new Size(80, 30) };
            buttonModifier.Click += ButtonModifier_Click;

            buttonSupprimer = new Button { Text = "Supprimer", Location = new Point(200, 150), Size = new Size(80, 30) };
            buttonSupprimer.Click += ButtonSupprimer_Click;

            buttonRechercher = new Button { Text = "Rechercher", Location = new Point(290, 150), Size = new Size(80, 30) };
            buttonRechercher.Click += ButtonRechercher_Click;

            buttonGestionAbsences = new Button { Text = "Gestion d'absences", Location = new Point(380, 150), Size = new Size(120, 30) };
            buttonGestionAbsences.Click += ButtonGestionAbsences_Click;

            // DataGridView
            dataGridViewEleves = new DataGridView
            {
                Location = new Point(20, 200),
                Size = new Size(640, 200),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dataGridViewEleves.DoubleClick += DataGridViewEleves_DoubleClick;

            // Add controls
            this.Controls.Add(labelID);
            this.Controls.Add(textBoxID);
            this.Controls.Add(labelNom);
            this.Controls.Add(textBoxNom);
            this.Controls.Add(labelPrenom);
            this.Controls.Add(textBoxPrenom);
            this.Controls.Add(labelGroupe);
            this.Controls.Add(textBoxGroupe);

            this.Controls.Add(buttonAjouter);
            this.Controls.Add(buttonModifier);
            this.Controls.Add(buttonSupprimer);
            this.Controls.Add(buttonRechercher);
            this.Controls.Add(buttonGestionAbsences);

            this.Controls.Add(dataGridViewEleves);

            // Load event
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChargerEleves();
        }

        private void ChargerEleves()
        {
            DataTable dt = GestionEleve.ListerEleves();
            dataGridViewEleves.DataSource = dt;
        }

        // Fill text boxes when row is double-clicked
        private void DataGridViewEleves_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewEleves.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewEleves.SelectedRows[0];
                textBoxID.Text = row.Cells["ID"].Value.ToString();
                textBoxNom.Text = row.Cells["Nom"].Value.ToString();
                textBoxPrenom.Text = row.Cells["Prenom"].Value.ToString();
                textBoxGroupe.Text = row.Cells["Groupe"].Value.ToString();
            }
        }

        private void ButtonAjouter_Click(object sender, EventArgs e)
        {
            GestionEleve.AjouterEleve(textBoxNom.Text, textBoxPrenom.Text, textBoxGroupe.Text);
            ChargerEleves();
        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxID.Text)) return;

            int id = Convert.ToInt32(textBoxID.Text);
            GestionEleve.ModifierEleve(id, textBoxNom.Text, textBoxPrenom.Text, textBoxGroupe.Text);
            ChargerEleves();
        }

        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridViewEleves.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridViewEleves.SelectedRows[0].Cells["ID"].Value);
                GestionEleve.SupprimerEleve(id);
                ChargerEleves();
            }
        }

        private void ButtonRechercher_Click(object sender, EventArgs e)
        {
            DataTable dt = GestionEleve.RechercherEleves(textBoxNom.Text, textBoxPrenom.Text, textBoxGroupe.Text);
            dataGridViewEleves.DataSource = dt;
        }

        // Open the Absences form for the selected student
        private void ButtonGestionAbsences_Click(object sender, EventArgs e)
        {
            if (dataGridViewEleves.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridViewEleves.SelectedRows[0].Cells["ID"].Value);
                FormAbsences formAbs = new FormAbsences(id);
                formAbs.ShowDialog();
            }
        }
    }
}
