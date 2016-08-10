using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimesCalculator
{
    public partial class Form1 : Form
    {
        private EventWaitHandle _cancel;

        private void UpDatePrimeList(IEnumerable<int> _listOfNumbers)
        {
            listBox1.Items.Clear();
            foreach (var number in _listOfNumbers)
            {
                listBox1.Items.Add(number);
            }
        }
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int firstNum = 0;
            int lastNum = 0;
            var checkParse = int.TryParse(first.Text, out firstNum)
                && int.TryParse(last.Text, out lastNum) && firstNum > 0 && lastNum > 0 && lastNum > firstNum;
            if (!checkParse)
            {
                MessageBox.Show("Please enter positive numbers.\nand Last must be higher than First.");
                return;
            }

            listBox1.Items.Clear();
            listBox1.Items.Add("Calculating...");

            _cancel = new EventWaitHandle(false, EventResetMode.AutoReset, "_CancelPrimeCalc");

            var data = new CalcPrimes(firstNum, lastNum , this, UpDatePrimeList, CancelCalc);

            Thread th = new Thread(data.Calculat);
            th.Start();
        }

        private void CancelCalc()
        {
            _cancel.Close();
            listBox1.Items.Clear();
            listBox1.Items.Add("Operation cancelled.");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            _cancel.Set();
        }
    }
}
