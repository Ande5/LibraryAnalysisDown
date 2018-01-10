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
        }

        InitializeAnalysisDown analysis = new InitializeAnalysisDown();
        private void AnalysisOnPrintMessage(string text)
        {
            MessageBox.Show(text);
        }
        private void AnalysisOnPrintCompileInfo(string text)
        {
            richTextBox1.Text += text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            analysis.Initialize(richTextBox1.Text, textBox1.Text);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            richTextBox2.SelectionStart = richTextBox2.Text.Length;
            richTextBox2.ScrollToCaret();
        }
    }
}
