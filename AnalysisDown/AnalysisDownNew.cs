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
        private List<Grammatics> m_element_str = new List<Grammatics>();
        private List<Grammatics> m_eps_rules = new List<Grammatics>();
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
                Search_ID(str[k]);
            }
    
   

        }

        public void Search_ID(string number)
        {
            if (CheckNumber(number))
            {
                if (number.Length <= 8 && number.Length > 0)
                {
                    m_element_str.Add(id);
                }
                else
                {
                    Message_Errors(number);
                }
            }
        }

        public void Message_Errors(string number)
        {
            switch (number.Length)
            {
                case 8:
                    {
                        AnalysisEvent.PrintMessage("Длина идентификатора должна быть меньше 8 символов!" + '\n' + "Ошибка --> " + number);
                        break;
                    }
                case 0:
                    {
                        AnalysisEvent.PrintMessage("Длина идентификатора должна быть больше 0 символов!" + '\n');
                        number = null;
                        break;
                    }
            }
        }

        public bool CheckNumber(string str)
        {
            return "0123456789ABCDF".Contains(str);
        }
        public void Search_Terminals(string [] str, int index)
        {
            foreach (var terminal in m_terminals)
            {
                if (terminal.m_name == str[index])
                {
                    m_element_str.Add(terminal);
                }
            } 
        }
        public void Search_Rules_Eps()
        {
          // Для eps правил можно задать свой символ
            foreach (var rule in m_rule)
            {
                if (rule.m_name == "  ")
                {
                    m_eps_rules.Add(rule);
                }
            }
        }

        public void Search_Terminals()
        {
            foreach (var terminal in m_terminals)
            {
                switch (terminal.m_name)
                {
                    case "$":
                        {
                            eps.m_name = terminal.m_name;
                            eps.number = terminal.number;
                            break;
                        }
                    case "id":
                        {
                            id.m_name = terminal.m_name;
                            id.number = terminal.number;
                            break;
                        }
                    case "const":
                        {
                            constNT.m_name = terminal.m_name;
                            constNT.number = terminal.number;
                            break;
                        }
                }
            }
        }



    }
}
