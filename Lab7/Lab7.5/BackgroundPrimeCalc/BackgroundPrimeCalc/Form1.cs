using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundPrimeCalc
{
    public partial class Form1 : Form
    {
        public int _first;
        public int _last;

        private List<int> _listOfNumbers;

        private bool IsPrime(int num)
        {
            if ((num & 1) == 0)
            {
                if (num == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            for (int i = 3; (i * i) <= num; i += 2)
            {
                if ((num % i) == 0)
                {
                    return false;
                }
            }
            return num != 1;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void first_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var number in Enumerable.Range(_first, _last))
            {
                if (IsPrime(number))
                    _listOfNumbers.Add(number);

                backgroundWorker1.ReportProgress((int)(100L * (number - _first + 1) / (_last - _first + 1)));
            }
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            _listOfNumbers = new List<int>();

            var checkParse = int.TryParse(firstTextBox.Text, out _first)
                && int.TryParse(lastTextBox.Text, out _last) && _first > 0 && _last > 0 && _last > _first;
            if (!checkParse)
            {
                MessageBox.Show("Please enter positive numbers.\nand Last must be higher than First.");
                return;
            }

            listBox1.Items.Clear();
            listBox1.Items.Add("Calculating...");
            backgroundWorker1.RunWorkerAsync();

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            listBox1.Items.Clear();
            listBox1.Items.Add("Cancaled!");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var number in _listOfNumbers)
            {
                listBox1.Items.Add(number);
            }
        }
    }
}
