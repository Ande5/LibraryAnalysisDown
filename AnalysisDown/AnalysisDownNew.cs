using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisDown
{
    class AnalysisDownNew
    {
        private List<Grammatics> m_rule, m_terminals, m_nterminals;
        private Grammatics eps, id, constNT;

        public AnalysisDownNew (List<Grammatics> rule, List<Grammatics> terminals, List<Grammatics> nterminals)
        {
            m_rule = rule;
            m_terminals = terminals;
            m_nterminals = nterminals;
        }



        public void Run(string grammar_str)
        {
            grammar_str += " " + eps.m_name;
            string[] str = grammar_str.Split(' ');

            for (int k = 0; k < str.Length; k++)
            {
                Search_Terminals(str, k);
            }
    
   

        }
        public void AddElement(Grammatics element)
        {
           
        }
        public void Search_Terminals(string [] str, int index)
        {
            foreach (var terminal in m_terminals)
            {
                if (terminal.m_name == str[index])
                {
                    AddElement(terminal);
                }
            } 
        }

        public void ScanningKeyNTerminals()
        {
            foreach (var gr in m_terminals)
            {
                switch (gr.m_name)
                {
                    case "eps":
                        {
                            eps.m_name = gr.m_name;
                            eps.number = gr.number;
                            break;
                        }
                    case "id":
                        {
                            id.m_name = gr.m_name;
                            id.number = gr.number;
                            break;
                        }
                    case "const":
                        {
                            constNT.m_name = gr.m_name;
                            constNT.number = gr.number;
                            break;
                        }
                }
            }
        }
    }
}
