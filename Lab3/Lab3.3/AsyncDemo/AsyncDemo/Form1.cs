using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDemo
{
    public partial class Form1 : Form
    {
        //bad name
        private delegate IEnumerable<int> ListOfPrimes(int one, int two);

        private IEnumerable<int> CalcPrimes(int first, int last)
        {
            var listOfNumbers = new List<int>();
            foreach (var number in Enumerable.Range(first,last))
            {
                if(IsPrime(number))
                    listOfNumbers.Add(number);
            }
            return listOfNumbers;
        }

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
            // ????
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ????
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // ????
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // ????
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListOfPrimes hendler = CalcPrimes;
            var first = 0;
            var last = 0;
            //Extract to new method
            if (first > last || first < 0 || last < 0 || !int.TryParse(textBox1.Text, out first) || !int.TryParse(textBox2.Text, out last))
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Error ! \n");
                listBox1.Items.Add("Number must be positives.");
                listBox1.Items.Add("First muber be smaller then second."); 
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Working ....");

                IAsyncResult asyncResult = hendler.BeginInvoke(first, last, (isync) =>
                {

                    // Invoke - synchronic , BeginInvoke - Asynchronic
                    // Your UI is blocked, you should use BeginInvoke
                    
                    this.Invoke(new Action(() =>
                    {
                        listBox1.Items.Clear();
                        // end.invoke should be at 102 line, pass the result to new BeginInvoke (line 103) 
                        IEnumerable<int> list = hendler.EndInvoke(isync);
                        foreach (var item in list)
                        {
                            listBox1.Items.Add(item);
                        }
                    }));
                }, null);
            }
        }
    }
}
