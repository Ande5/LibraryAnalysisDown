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
        private int[,] rule_table;
        List<Grammatics> m_Rules = new List<Grammatics>();
        List<string> m_nterminals = new List<string>();
        List<string> m_terminals = new List<string>();
        List<Grammatics> m_Terminals = new List<Grammatics>();
        List<Grammatics> m_NTerminals = new List<Grammatics>();
        public LLParserLoader(string pathToTabel, string pathToRule)
        {
            Read_Regulation(pathToRule);
            CheckRule_terminals();
            Read_ControlTable(pathToTabel);
        }
        /// <summary>
        /// Загрузка управляющей таблицы
        /// </summary>
        public void Read_Regulation(string file_name)
        {
            Grammatics rule_down = new Grammatics();
            using (StreamReader read_regulation = new StreamReader(file_name))
            {
                int k = 0;
                while (!read_regulation.EndOfStream)
                {
                    string[] str = read_regulation.ReadLine().Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                    if  ((Search_nterminals(str[0])))
                    {
                       m_nterminals.Add(str[0]);
                    }
                    rule_down.m_name = str[1];
                    rule_down.number = k + 1;
                    m_Rules.Add(rule_down);
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
        /// <summary>
        /// Парсим правило на поиск терминалов
        /// </summary>
        public void CheckRule_terminals()
        {
            foreach(var rule in m_Rules)
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
            AddTerminals();
            AddNTerminals();
        }
        /// <summary>
        /// Формирование Териминалов
        /// </summary>
        public void AddTerminals()
        {
            for(int k=0; k<m_terminals.Count; k++)
            {
                m_Terminals.Add(new Grammatics(k+1, m_terminals[k]));
            }
        }
        /// <summary>
        /// Формирование грамматики с нетерминалами и терминалами
        /// </summary>
        public void AddNTerminals()
        {
            int index = 1;
            for (int k=0; k< m_nterminals.Count; k++)
            {
                m_NTerminals.Add(new Grammatics(index, m_nterminals[k]));
                index++;
            }
            for (int k = 0; k < m_terminals.Count; k++)
            {
                m_NTerminals.Add(new Grammatics(index, m_terminals[k]));
                index++;
            }
        }
        /// <summary>
        /// Поиск терминалов в списке
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Загрузка управялющей таблицы
        /// </summary>
        /// <returns></returns>
        public void Read_ControlTable(string file_name)
        {
            int widht = System.IO.File.ReadAllLines(file_name).Length;
            int length = Length_line_parser(file_name, ',');
            rule_table = new int[widht + 1, length];
            int str_number = 0;

            using (StreamReader read_table = new StreamReader(file_name))
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
        public List<Grammatics> Terminals
        {
            get { return m_Terminals; }
        }
        public List<Grammatics> NTerminals
        {
            get { return m_NTerminals; }
        }
        public List<Grammatics> Rules
        {
            get { return m_Rules; }
        }
        public int [,] Tabel
        {
            get { return rule_table; }
        }
    }
}
