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
            textBox1.Text = "else := k [ 10 ] const then := id [ id ] const if > id id else := id [ id ] const then := id [ id ] const if id := id [ id ] const";
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
            LLParserLoader ll_parser = new LLParserLoader("MyTabel.txt", "MyGramm.txt");
            ll_parser.Read_Regulation();
            ll_parser.CheckRule_terminals();
            ll_parser.Read_ControlTable();
            analysis = new InitializeAnalysisDown(textBox1.Text,ll_parser.Tabel,ll_parser.Rules,ll_parser.Terminals, ll_parser.NTerminals);
            //analysis = new InitializeAnalysisDown1(textBox1.Text);
        }

        private void butCheckRule_Click(object sender, EventArgs e)
        {
           
        }
    }
}
