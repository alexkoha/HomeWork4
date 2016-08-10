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
        private void CalcPrimes()
        {
            foreach (var number in Enumerable.Range(First, Last))
            {
                if (IsPrime(number))
                    _listOfNumbers.Add(number);
            }
        }

        private void UpDdatePrimeList()
        {
            listBox1.Items.Clear();
            foreach (var number in _listOfNumbers)
            {
                listBox1.Items.Add(number);
            }
        }

        private int Last { get; set; }
        private int First { get; set; }

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

        private void button1_Click(object sender, EventArgs e)
        {
            _listOfNumbers = new List<int>();

            int firstNum = 0;
            int lastNum = 0;
            var checkParse = int.TryParse(first.Text, out firstNum) 
                && int.TryParse(last.Text, out lastNum) && firstNum>0 &&lastNum>0 && lastNum> firstNum;
            if (!checkParse)
            {
                MessageBox.Show("Please enter positive numbers.\nand Last must be higher than First.");
                return;
            }

            listBox1.Items.Clear();
            listBox1.Items.Add("Calculating...");
            Thread worker = new Thread(() =>
            {
                CalcPrimes();
                Invoke(new Action(() =>
                {
                    UpDdatePrimeList();
                }));
            });
            worker.Start();
        }



    }
}
