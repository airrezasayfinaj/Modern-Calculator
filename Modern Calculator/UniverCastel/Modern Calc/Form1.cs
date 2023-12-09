using CtlButton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modern_Calc
{
    public partial class Form1 : Form
    {
        Double result = 0;
        string oprtion = string.Empty;
        string fstnum,secnum;
        bool entervalue = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpration_Click(object sender, EventArgs e)
        {
            if (result != 0) btnEquals.PerformClick();
            else result = double.Parse(TxtDisplay1.Text);
            CtlBut ctlBut = (CtlBut)sender;
            oprtion = ctlBut.Text;
            entervalue = true;
            if (TxtDisplay1.Text != "0")
            {
                TxtDisplay1.Text = fstnum = $"{result}{oprtion}";
                TxtDisplay1.Text = string.Empty;
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            secnum = TxtDisplay1.Text;
            TxtDispaly2.Text = $"{TxtDispaly2.Text}{TxtDisplay1.Text}";
            if (TxtDisplay1.Text == "0") TxtDispaly2.Text = string.Empty;
            switch (oprtion)
            {
                case "+":TxtDisplay1.Text = (result + double.Parse(TxtDisplay1.Text)).ToString();
                    RtBoxDisplayHistory.AppendText($"{fstnum}{secnum}={TxtDisplay1.Text}\n");
                    break;
                case "-":
                    TxtDisplay1.Text = (result - double.Parse(TxtDisplay1.Text)).ToString();
                    RtBoxDisplayHistory.AppendText($"{fstnum}{secnum}={TxtDisplay1.Text}\n");
                    break;
                case "×":
                    TxtDisplay1.Text = (result * double.Parse(TxtDisplay1.Text)).ToString();
                    RtBoxDisplayHistory.AppendText($"{fstnum}{secnum}={TxtDisplay1.Text}\n");
                    break;
                case "÷":
                    TxtDisplay1.Text = (result / double.Parse(TxtDisplay1.Text)).ToString();
                    RtBoxDisplayHistory.AppendText($"{fstnum}{secnum}={TxtDisplay1.Text}\n");
                    break;
                default:TxtDispaly2.Text = $"{TxtDispaly2.Text}=";
                    break;
            }
            result = double.Parse(TxtDisplay1.Text);
            oprtion = string.Empty;
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            PanelHistory.Height = (PanelHistory.Height == 5) ? PanelHistory.Height = 368 : 5;
            this.PanelHistory.BringToFront();
        }

        private void BtnClearHis_Click(object sender, EventArgs e)
        {
            RtBoxDisplayHistory.Clear();
            if (RtBoxDisplayHistory.Text == string.Empty)
                RtBoxDisplayHistory.Text = "theres no history yet";
        }

        private void btnbackspace_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text.Length > 0)
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length-1,1);
            if (TxtDisplay1.Text == string.Empty) TxtDisplay1.Text = "0";
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
            TxtDispaly2.Text = string.Empty;
            result = 0;
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            TxtDisplay1.Text = "0";
        }

        private void btnmathop_click(object sender, EventArgs e)
        {
            CtlBut ctlBut = (CtlBut)sender;
            oprtion = ctlBut.Text;
            switch (oprtion)
            {
                case "√x":
                    TxtDispaly2.Text = $"√({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Math.Sqrt(double.Parse(TxtDisplay1.Text)));
                    break;
                case "^2":
                    TxtDispaly2.Text = $"({TxtDisplay1.Text})^2";
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text)* Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "1/":
                    TxtDispaly2.Text = $"1/({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(1.0/Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "%":
                    TxtDispaly2.Text = $"%({TxtDisplay1.Text})";
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text)/Convert.ToDouble(100));
                    break;
                case "±":
                    TxtDisplay1.Text = Convert.ToString(-1*Convert.ToDouble(TxtDisplay1.Text));
                    break;

            }
            RtBoxDisplayHistory.AppendText($"{TxtDispaly2.Text}={TxtDisplay1.Text}\n");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            if (TxtDisplay1.Text == "0" || entervalue) TxtDisplay1.Text = string.Empty;
            entervalue = false;
            CtlBut ctlBut =(CtlBut)sender;
            if (ctlBut.Text == ".")
            {
                if (!TxtDisplay1.Text.Contains("."))
                    TxtDisplay1.Text = TxtDisplay1.Text + ctlBut.Text;
            }
            else TxtDisplay1.Text = TxtDisplay1.Text + ctlBut.Text;
        }
    }
}
