using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AnalysisDown;


namespace AnalysisDown
{
    class LLParserLoader
    {
        private string m_pathToTabel;
        private string m_pathToRule;
        Grammatics rule_down = new Grammatics();
        int[,] rule_table;
        List<Grammatics> Rules = new List<Grammatics>();
        List<string> m_nterminals = new List<string>();
        List<string> m_terminals = new List<string>();
        List<Grammatics> Terminals = new List<Grammatics>();
        List<Grammatics> NTerminals = new List<Grammatics>();
        public LLParserLoader(string pathToTabel, string pathToRule)
        {
            m_pathToRule = pathToRule;
            m_pathToTabel = pathToTabel;
        }
        /// <summary>
        /// Загрузка управляющей таблицы
        /// </summary>
        public void Read_Regulation()
        {
            using (StreamReader read_regulation = new StreamReader(m_pathToRule))
            {
                int k = 0;
                while (!read_regulation.EndOfStream)
                {
                    string[] str = read_regulation.ReadLine().Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                    //m_nterminals.Add(str[0]);
                    if  ((Search_nterminals(str[0])))
                    {
                       m_nterminals.Add(str[0]);
                    }
                    rule_down.m_name = str[1];
                    rule_down.number = k + 1;
                    Rules.Add(rule_down);
                    k++;
                }
            }
        }
        /// <summary>
        /// Поиск нетерминалов в списке
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Search_nterminals(string str)
        {
            bool flag = true;
            foreach (var nterminal in m_nterminals)
            {
                if (nterminal == str)
                {
                    flag = false;
                }
            }
            return flag;
        }
        public void CheckRule_terminals()
        {
            foreach(var rule in Rules)
            {
                string[] rule_pars = rule.m_name.Split(' ');
                for(int k=0; k< rule_pars.Length; k++)
                {
                    if (Search_terminals(rule_pars[k]) && rule_pars[k] != "eps")
                    {
                        if (Search_nterminals(rule_pars[k]))
                        {
                            m_terminals.Add(rule_pars[k]);
                        }
                    }
                }
            }
            m_terminals.Add("$");
            

         
        }
        public void AddTerminals()
        {
            for(int k=0; k<m_terminals.Count; k++)
            {
                Terminals.Add(new Grammatics(k, m_terminals[k]));
            }
        }
        public void AddNTerminals()
        {
            int index = 0;
            for (int k=0; k< m_nterminals.Count; k++)
            {
                NTerminals.Add(new Grammatics(index, m_nterminals[k]));
                index++;
            }
            for (int k = 0; k < m_terminals.Count; k++)
            {
                NTerminals.Add(new Grammatics(index, m_terminals[k]));
                index++;
            }
        }
        public bool Search_terminals(string str)
        {
            bool flag = true;
            foreach (var terminal in m_terminals)
            {
                if (terminal == str)
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool Ser()
        {
            bool flag = true;
            foreach (var nterminal in m_nterminals)
            {
                foreach (var terminal in m_terminals)
                if (terminal == nterminal)
                {
                    flag = false;
                }
            }
            return flag;
        }
        /// <summary>
        /// Загрузка управялющей таблицы
        /// </summary>
        /// <returns></returns>
        public int[,] Read_ControlTable()
        {
            int widht = System.IO.File.ReadAllLines(m_pathToTabel).Length;
            int length = Length_line_parser(m_pathToTabel, ',');
            rule_table = new int[widht + 1, length];
            int str_number = 0;

            using (StreamReader read_table = new StreamReader(m_pathToTabel))
            {
                while (!read_table.EndOfStream)
                {
                    string[] str = read_table.ReadLine().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < length; k++)
                    {
                        rule_table[str_number, k] = int.Parse(str[k]);
                    }
                    str_number++;
                }
            }
            return rule_table;
        }
        /// <summary>
        /// Парсим {,} для определения правильной длины строки
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public int Length_line_parser(string file_name, char symbol)
        {
            using (StreamReader read_table = new StreamReader(file_name))
            {
                string[] str = read_table.ReadLine().Split(new[] { symbol }, StringSplitOptions.RemoveEmptyEntries);
                return str.Length;
            }
        }



    }
}
