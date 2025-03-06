using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesAbsances
{
    public partial class FormAbsences : Form
    {
        private int eleveID;

        private Label labelID;
        private TextBox textBoxID;
        private Label labelSemaine;
        private TextBox textBoxSemaine;
        private Label labelNbrAbs;
        private TextBox textBoxNbrAbs;

        private Button buttonEnregistrer;
        private Button buttonSupprimer;
        private Button buttonAfficherTotal;
        private Button buttonAfficherSemaine;

        private TextBox textBoxResult;  // to show total or specific week result
        private DataGridView dataGridViewAbs;


        public FormAbsences(int idEleve)
        {
            eleveID = idEleve;
            InitializeCoomponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Gestion d'absences";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            labelID = new Label { Text = "ID", Location = new Point(20, 20), Size = new Size(80, 20) };
            textBoxID = new TextBox { Location = new Point(100, 20), Size = new Size(120, 20), ReadOnly = true };
            textBoxID.Text = eleveID.ToString();

            labelSemaine = new Label { Text = "Semaine", Location = new Point(20, 50), Size = new Size(80, 20) };
            textBoxSemaine = new TextBox { Location = new Point(100, 50), Size = new Size(120, 20) };

            labelNbrAbs = new Label { Text = "Nbr Absences", Location = new Point(20, 80), Size = new Size(80, 20) };
            textBoxNbrAbs = new TextBox { Location = new Point(100, 80), Size = new Size(120, 20) };

            buttonEnregistrer = new Button { Text = "Enregistrer", Location = new Point(20, 120), Size = new Size(90, 30) };
            buttonEnregistrer.Click += ButtonEnregistrer_Click;

            buttonSupprimer = new Button { Text = "Supprimer", Location = new Point(120, 120), Size = new Size(90, 30) };
            buttonSupprimer.Click += ButtonSupprimer_Click;

            buttonAfficherTotal = new Button { Text = "Total absences", Location = new Point(220, 120), Size = new Size(90, 30) };
            buttonAfficherTotal.Click += ButtonAfficherTotal_Click;

            buttonAfficherSemaine = new Button { Text = "Abs. semaine", Location = new Point(320, 120), Size = new Size(90, 30) };
            buttonAfficherSemaine.Click += ButtonAfficherSemaine_Click;

            textBoxResult = new TextBox { Location = new Point(20, 160), Size = new Size(250, 20), ReadOnly = true };

            dataGridViewAbs = new DataGridView
            {
                Location = new Point(20, 200),
                Size = new Size(440, 150),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dataGridViewAbs.DoubleClick += DataGridViewAbs_DoubleClick;

            this.Controls.Add(labelID);
            this.Controls.Add(textBoxID);
            this.Controls.Add(labelSemaine);
            this.Controls.Add(textBoxSemaine);
            this.Controls.Add(labelNbrAbs);
            this.Controls.Add(textBoxNbrAbs);
            this.Controls.Add(buttonEnregistrer);
            this.Controls.Add(buttonSupprimer);
            this.Controls.Add(buttonAfficherTotal);
            this.Controls.Add(buttonAfficherSemaine);
            this.Controls.Add(textBoxResult);
            this.Controls.Add(dataGridViewAbs);

            this.Load += FormAbsences_Load;
        }

        private void FormAbsences_Load(object sender, EventArgs e)
        {
            ChargerAbsences();
        }

        private void ChargerAbsences()
        {
            DataTable dt = GestionAbsences.GetAbsencesForEleve(eleveID);
            dataGridViewAbs.DataSource = dt;
        }

        private void ButtonEnregistrer_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxSemaine.Text, out int semaine) &&
                int.TryParse(textBoxNbrAbs.Text, out int nbrAbs))
            {
                GestionAbsences.AjouterOuModifierAbsence(eleveID, semaine, nbrAbs);
                ChargerAbsences();
            }
        }

        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridViewAbs.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewAbs.SelectedRows[0];
                int semaine = Convert.ToInt32(row.Cells["Num_semaine"].Value);
                GestionAbsences.SupprimerAbsence(eleveID, semaine);
                ChargerAbsences();
            }
        }

        // Get total absences for this student
        private void ButtonAfficherTotal_Click(object sender, EventArgs e)
        {
            int total = GestionAbsences.GetTotalAbsences(eleveID);
            textBoxResult.Text = $"Total absences: {total}";
        }

        // Get absences for a specific week
        private void ButtonAfficherSemaine_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxSemaine.Text, out int semaine))
            {
                int absWeek = GestionAbsences.GetAbsencesForWeek(eleveID, semaine);
                textBoxResult.Text = $"Semaine {semaine}: {absWeek} absence(s)";
            }
        }

        // Double-click on a row to load Num_semaine & Nbr_absences
        private void DataGridViewAbs_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewAbs.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewAbs.SelectedRows[0];
                textBoxSemaine.Text = row.Cells["Num_semaine"].Value.ToString();
                textBoxNbrAbs.Text = row.Cells["Nbr_absences"].Value.ToString();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormAbsences
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "FormAbsences";
            this.Load += new System.EventHandler(this.FormAbsences_Load_1);
            this.ResumeLayout(false);

        }

        private void FormAbsences_Load_1(object sender, EventArgs e)
        {

        }
    }
}
