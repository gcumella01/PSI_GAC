using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//CS 311 Programming Project 2
//PSI GUI written in C#
// Author; Gage Cumella
namespace PSI_GAC_Project2

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalcPsi_Click(object sender, RoutedEventArgs e)
        {
            int psi = 0;
            bool riskClassI = true;

            psi += Int32.Parse(txtBoxAge.Text);

            if (cboBoxSex.Text == "Female") //if the patient is female, then 10 points will be reducted 
                psi = psi - 10;

            if (chkBoxYesNHR.IsChecked == true) //if the yes checkbox is selected, then 10 points will be added
            {
                psi += 10;
                riskClassI = false;
            }
            if (chkBoxYesND.IsChecked == true) // if the patient does indeed have Neoplastic Disease, then 30 points will be added
            {
                psi += 30;
                riskClassI = false;
            }
            if (chkBoxYesLD.IsChecked == true) //if the patient has Liver Disease, then 30 points will be added
            {
                psi += 20;
                riskClassI = false;
            }
            if (chkBoxYesCHF.IsChecked == true) //if the patient is in Congestive Heart Failure, then 10 points will be added
            {
                psi += 10;
                riskClassI = false;
            }
            if (chkBoxYesCD.IsChecked == true)//if the patient has Cerebrovascular Disease, then 30 points will be added
            {
                psi += 10;
                riskClassI = false;
            }
            if (chkBoxYesRD.IsChecked == true) //if the patient has Renal Disease, then 10 points will be added
            {
                psi += 10;
                riskClassI = false;
            }
            if (chkBoxYesAMS.IsChecked == true) //if the patient is in an Altered Mental Status, then 20 points will be added
            {
                psi += 20;
                riskClassI = false;
            }
            if (chkBoxYesPE.IsChecked == true) // if the patient is experiencing Pleural Effusion on an x-ray, then 10 points will be added
            {
                psi += 10;
                riskClassI = false;
            }
            if (Int32.Parse(txtboxRR.Text) >= 30)
            {
                psi += 20;
                riskClassI = false;
            }
            if (Int32.Parse(txtboxSBP.Text) < 90)
            {
                psi += 20;
                riskClassI = false;
            }
            double tempCelsius;
            if (rdoBtnFahren.IsChecked == true)
            {
                tempCelsius = (Double.Parse(txtboxTemp.Text) - 32.0) * (5.0 / 9.0);
                if (tempCelsius < 35.0 || tempCelsius > 39.9)
                {
                    psi += 15;
                    riskClassI = false;
                }
            }
            else if (rdoBtnCelsius.IsChecked == true &&
                (Double.Parse(txtboxTemp.Text) < 35.0 || Double.Parse(txtboxTemp.Text) > 39.0))
            {
                psi += 15;
                riskClassI = false;
            }

            if (Int32.Parse(txtboxPulse.Text) >= 125)
            {
                psi += 10;
                riskClassI = false;
            }
            if (Double.Parse(txtboxpH.Text) < 7.35)
            {
                psi += 30;
                riskClassI = false;
            }
            double BUNmg;
            if (rdoBtnmmolL.IsChecked == true)
            {
                BUNmg = mmolTomg(Double.Parse(txtboxBUN.Text));
                if (BUNmg >= 30.0)
                {
                    psi += 20;
                    riskClassI = false;
                }
            }
            else if (rdoBtnmgdl.IsChecked == true && Double.Parse(txtboxBUN.Text) >= 30.0)
            {
                psi += 20;
                riskClassI = false;
            }

            if (Double.Parse(txtboxSodium.Text) < 130)
            {
                psi += 20;
                riskClassI = false;
            }
            double Glumg;
            if (rdoBtnGlummolL.IsChecked == true)
            {
                Glumg = mmolTomg(Double.Parse(txtboxGlucose.Text));
                if (Glumg >= 250.0)
                {
                    psi += 10;
                    riskClassI = false;
                }
            }
            else if (rdoBtnGlumgdl.IsChecked == true && Double.Parse(txtboxGlucose.Text) >= 250.0)
            {
                psi += 10;
                riskClassI = false;
            }
            if (Double.Parse(txtboxHem.Text) < 30.0)
            {
                psi += 10;
                riskClassI = false;
            }
            double pressuremmHg;
            if (rdoBtnPPkPa.IsChecked == true)
            {
                pressuremmHg = Double.Parse(txtboxPPOO.Text) * 7.501;
                if (pressuremmHg < 60.0)
                {
                    psi += 10;
                    riskClassI = false;
                }
            }
            else if (rdoBtnPPmmHg.IsChecked == true && Double.Parse(txtboxPPOO.Text) < 60.0)
            {
                psi += 10;
                riskClassI = false;
            }
            MessageBox.Show(psi.ToString());//Risk Classes
            if (riskClassI == true)
            {
                txtblkRiskClassResult.Text = "Risk Class 01 (Risk Low)";
                txtblkAdmissionResult.Text = "Outpatient Care";
            }

            else if (psi <= 70)
            {
                txtblkRiskClassResult.Text = "Risk Class 02 (Risk Low)";
                txtblkAdmissionResult.Text = "Outpatient Care";
            }
            else if (psi > 70 && psi < 91)
            {
                txtblkRiskClassResult.Text = "Risk Class 03 (Risk Low)";
                txtblkAdmissionResult.Text = "Outpatient / Observation Admission";
            }
            else if (psi > 90 && psi < 131)
            {
                txtblkRiskClassResult.Text = "Risk Class 04 (Risk Moderate)";
                txtblkAdmissionResult.Text = "Inpatient Admission";
            }
            else if (psi > 130)
            {
                txtblkRiskClassResult.Text = "Risk Class 05 (Risk High)";
                txtblkAdmissionResult.Text = "Inpatient Admission, Need to check for Sepsis)";
            }

        }
        public double mmolTomg(double mmol)
        {
            double mgDl;
            mgDl = mmol / .0555;
            return mgDl;
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            String statusNHR = "0";
            String statusND =  "0";
            String statusLD =  "0";
            String statusCHF = "0";
            String statusCD =  "0";
            String statusRD =  "0";
            String statusAMS = "0";
            String statusPE =  "0";
            if (chkBoxYesNHR.IsChecked == true)
            {
                statusNHR = "1";
            }
            if (chkBoxYesND.IsChecked == true)
            {
                statusND = "1";
            }
            if (chkBoxYesLD.IsChecked == true)
            {
                statusLD = "1";
            }
            if (chkBoxYesCHF.IsChecked == true)
            {
                statusCHF = "1";
            }
            if (chkBoxYesCD.IsChecked == true)
            {
                statusCD = "1";
            }
            if (chkBoxYesRD.IsChecked == true)
            {
                statusRD = "1";
            }
            if (chkBoxYesAMS.IsChecked == true)
            {
                statusAMS = "1";
            }
            if (chkBoxYesPE.IsChecked == true)
            {
                statusPE = "1";
            }
            double statusCelsius;
            if (rdoBtnFahren.IsChecked == true)
            {
                statusCelsius = (Double.Parse(txtboxTemp.Text) - 32.0) * (5.0 / 9.0);
            }
            else
                statusCelsius = Double.Parse(txtboxTemp.Text);
            double statusBUN;
            if (rdoBtnmmolL.IsChecked == true)
            {
                statusBUN = mmolTomg(Double.Parse(txtboxBUN.Text));
            }
            else
                statusBUN = Double.Parse(txtboxBUN.Text);
            double statusGlucose;
            if (rdoBtnGlummolL.IsChecked == true)
                statusGlucose = mmolTomg(Double.Parse(txtboxGlucose.Text));
            else
                statusGlucose = Double.Parse(txtboxGlucose.Text);
            double statusPPOO;
            if (rdoBtnPPkPa.IsChecked == true)
                statusPPOO = Double.Parse(txtboxPPOO.Text) * 7.501;
            else
                statusPPOO = Double.Parse(txtboxPPOO.Text);
            if (File.Exists("data.csv"))
            {
                int lineCount = File.ReadAllLines("data.csv").Length;
                using (StreamWriter sw = File.AppendText("data.csv"))
                {
                    sw.WriteLine(lineCount +
                        " Age:" + txtBoxAge.Text +
                        " Sex:" + cboBoxSex.Text +
                        " Nursing Home Resident:" + statusNHR +
                        " Neoplastic Disease:" + statusND +
                        " Liver Disease:" + statusLD +
                        " Congestive Heart Failure:" + statusCHF +
                        " Cerobrascular Disease:" + statusCD +
                        " Renal Disease:" + statusRD +
                        " Altered Mental Status:" + statusAMS +
                        " Pleural Effusion on x-ray:" + statusPE +
                        " Respriatory Rate:" + txtboxRR.Text +
                        " Systolic Blood Pressure:" + txtboxSBP +
                        " Temperature(C):" + statusCelsius +
                        " Pulse:" + txtboxRR.Text +
                        " Ph Level:" + txtboxpH.Text +
                        " BUN(mg/dL):" + statusBUN +
                        " Sodium:" + txtboxSodium +
                        " Glucose(mg/dl):" + statusGlucose +
                        " Hemtocrit:" + txtboxHem.Text + "%" +
                        " Partial pressure of oxygen:" + statusPPOO
                        );
                    MessageBox.Show("Data has been saved");
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText("data.csv"))
                {
                    int lineCount = File.ReadAllLines("data.csv").Length;
                    sw.WriteLine(lineCount +
                        " Age:" + txtBoxAge.Text +
                        " Sex:" + cboBoxSex.Text +
                        " Nursing Home Resident:" + statusNHR +
                        " Neoplastic Disease:" + statusND +
                        " Liver Disease:" + statusLD +
                        " Congestive Heart Failure:" + statusCHF +
                        " Cerobrascular Disease:" + statusCD +
                        " Renal Disease:" + statusRD +
                        " Altered Mental Status:" + statusAMS +
                        " Pleural Effusion on x-ray:" + statusPE +
                        " Respriatory Rate:" + txtboxRR.Text +
                        " Systolic Blood Pressure:" + txtboxSBP +
                        " Temperature(C):" + statusCelsius +
                        " Pulse:" + txtboxRR.Text +
                        " Ph Level:" + txtboxpH.Text +
                        " BUN(mg/dL):" + statusBUN +
                        " Sodium:" + txtboxSodium +
                        " Glucose(mg/dl):" + statusGlucose +
                        " Hemtocrit:" + txtboxHem.Text + "%" +
                        " Partial pressure of oxygen:" + statusPPOO
                        );
                    MessageBox.Show("Data has been saved");
                }
            }

        }
    }
}
