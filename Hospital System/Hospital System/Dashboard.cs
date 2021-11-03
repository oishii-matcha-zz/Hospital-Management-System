using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnNewPtRec_Click(object sender, EventArgs e)
        {
            //lblIndicators change colors based on tab chosen
            lblIndicator1.ForeColor = System.Drawing.Color.MediumPurple;
            lblIndicator2.ForeColor = System.Drawing.Color.Black;
            lblIndicator3.ForeColor = System.Drawing.Color.Black;
            lblIndicator4.ForeColor = System.Drawing.Color.Black;

            //Showcases the panel that we want seen 
            pnlAddNewPt.Visible = true;
            pnlAddDiagnosis.Visible = false;
            pnlPtsMedHistory.Visible = false;
            pnlHospInfo.Visible = false;
        }

        private void btnAddDiagnosisInfo_Click(object sender, EventArgs e)
        {
            //lblIndicators change colors based on tab chosen
            lblIndicator1.ForeColor = System.Drawing.Color.Black;  
            lblIndicator2.ForeColor = System.Drawing.Color.MediumPurple;
            lblIndicator3.ForeColor = System.Drawing.Color.Black;
            lblIndicator4.ForeColor = System.Drawing.Color.Black;

            //Showcases the panel that we want seen 
            pnlAddNewPt.Visible = false;
            pnlAddDiagnosis.Visible = true;
            pnlPtsMedHistory.Visible = false;
            pnlHospInfo.Visible = false;
        }

        private void btnPtsMedHistory_Click(object sender, EventArgs e)
        {
            //lblIndicators change colors based on tab chosen
            lblIndicator1.ForeColor = System.Drawing.Color.Black;
            lblIndicator2.ForeColor = System.Drawing.Color.Black;
            lblIndicator3.ForeColor = System.Drawing.Color.MediumPurple;
            lblIndicator4.ForeColor = System.Drawing.Color.Black;

            //Showcases the panel that we want seen 
            pnlAddNewPt.Visible = false;
            pnlAddDiagnosis.Visible = false;
            pnlPtsMedHistory.Visible = true;
            pnlHospInfo.Visible = false;

            //joining the AddPatient table and FurtherInfo table to output the information provided into the system
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = DESKTOP-F3IB5AU; database = hospital; integrated security = True";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from AddPatient inner join FurtherInfo on AddPatient.PatientID = FurtherInfo.Patient_ID";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            //Outputs innerjoined table information to windows forms
            dataGridView2.DataSource = DS.Tables[0];
        }

        private void btnHospitalInfo_Click(object sender, EventArgs e)
        {
            //lblIndicators change colors based on tab chosen
            lblIndicator1.ForeColor = System.Drawing.Color.Black;
            lblIndicator2.ForeColor = System.Drawing.Color.Black;
            lblIndicator3.ForeColor = System.Drawing.Color.Black;
            lblIndicator4.ForeColor = System.Drawing.Color.MediumPurple;

            //Showcases the panel that we want seen 
            pnlAddNewPt.Visible = false;
            pnlAddDiagnosis.Visible = false;
            pnlPtsMedHistory.Visible = false;
            pnlHospInfo.Visible = true;
        }

        //closes form when you click exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            //Showcases the panels that were created (default)
            pnlAddNewPt.Visible = false;
            pnlAddDiagnosis.Visible = false;
            pnlPtsMedHistory.Visible = false;
            pnlHospInfo.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //all information written in text box's for the "new pt tab" will be checked(example: letters in boxes that hold integers will throw and exception)
            try
            {
                string name = txtBoxName.Text;
                string address = txtBoxAddress.Text;
                Int64 contact = Convert.ToInt64(txtBoxContactNum.Text);
                int age = Convert.ToInt32(txtBoxAge.Text);
                string gender = comboBoxGender.Text;
                string blood = txtBoxBloodType.Text;
                string medHis = txtBoxMedHistory.Text;
                int ptID = Convert.ToInt32(txtBoxPtID.Text);

                //inputting new patient information into the database
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DESKTOP-F3IB5AU; database = hospital; integrated security = True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "insert into AddPatient values('" + name + "','" + address + "'," + contact + "," + age + ",'" + gender + "','" + blood + "','" + medHis + "'," + ptID + ")";

                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);
            }
            catch (Exception)
            {
                //this message box will show when there is exceptions (ex: missing or incorrect information)
                MessageBox.Show("Please check that the information is correct.");
            }

            //this message box will show when there is no exceptions
            MessageBox.Show("Your information has been saved!");

            //after youu click save: the txtbox's will be cleared
            txtBoxName.Clear();
            txtBoxAddress.Clear();
            txtBoxContactNum.Clear();
            txtBoxAge.Clear();
            comboBoxGender.ResetText();
            txtBoxBloodType.Clear();
            txtBoxMedHistory.Clear();
            txtBoxPtID.Clear();
        }

        private void txtBoxPtID2_TextChanged(object sender, EventArgs e)
        {
            //if the patient id textbox is not null: connect to database and input further information that is provided
            if (txtBoxPtID2.Text != "")
            {

                int ptID2 = Convert.ToInt32(txtBoxPtID2.Text);

                //Inputting information provided in the "patient's diagnosis tab" to SQL Server (assigns it to the patient that was retrieved by the patient ID)
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DESKTOP-F3IB5AU; database = hospital; integrated security = True";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from AddPatient where PatientID = " + ptID2 + "";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);
                
                //shows the patient retrieved in the gray box
                dataGridView1.DataSource = DS.Tables[0];
            }
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            //all information written in text box's for "patient diagnosis tab" will be checked(example: letters in boxes that hold integers will throw and exception)
            try
            {
                int ptID = Convert.ToInt32(txtBoxPtID2.Text);
                string sympt = txtBoxSymptoms.Text;
                string vitals = txtBoxVitals.Text;
                string meds = txtBoxMeds.Text;
                string iPtCare = comboBoxIptCare.Text;
                string clinic = comboBoxClinic.Text;

                //Connecting to SQL Server database to construct the patient's diagnosis and output information to patient records
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = DESKTOP-F3IB5AU; database = hospital; integrated security = True";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into FurtherInfo values(" + ptID + ", '" + sympt + "', '" + vitals + "', '" + meds + "', '" + iPtCare + "', '" + clinic + "')";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);
                
            }
            catch (Exception)
            {
                //this message box will show when there is exceptions (ex: missing or incorrect information)
                MessageBox.Show("Please check that the information is correct.");
            }
            //this message box will show when there is no exceptions
            MessageBox.Show("Your information has been saved!");

            //after youu click save: the txtbox's will be cleared
            txtBoxPtID2.Clear();
            txtBoxSymptoms.Clear();
            txtBoxVitals.Clear();
            txtBoxMeds.Clear();
            comboBoxIptCare.ResetText();
            comboBoxClinic.ResetText();
        }

    }
}
