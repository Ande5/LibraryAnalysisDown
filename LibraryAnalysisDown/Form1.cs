using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisDown;

namespace LibraryAnalysisDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnalysisEvent.PrintCompileInfo = new AnalysisEvent.PrintResult(AnalysisOnPrintCompileInfo);
            AnalysisEvent.PrintMessage = new AnalysisEvent.PrintResult(AnalysisOnPrintMessage);
            textBox1.Text = "else := id [ id ] const then := id [ id ] const if < id id else := id [ id ] const then := id [ id ] const if id := id [ id ] const";
            textBox1.Text = "else := k [ 10 ] const then := id [ id ] const if < id id else := id [ id ] const then := id [ id ] const if id := id [ id ] const";
        }
        // dev
        InitializeAnalysisDown analysis;
        private void AnalysisOnPrintMessage(string text)
        {
            MessageBox.Show(text);
        }
        private void AnalysisOnPrintCompileInfo(string text)
        {
            richTextBox1.AppendText(text + "\r");
            richTextBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            analysis = new InitializeAnalysisDown(textBox1.Text);
           // analysis.Initialize(richTextBox1.Text, textBox1.Text);
        }
    }
}
