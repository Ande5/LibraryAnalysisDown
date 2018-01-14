﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisDown
{
    class AnalysisDownNew2
    {
        private List<Grammatics> m_rule, m_terminals, m_nterminals;
        private Grammatics eps, id, constNT;
        private int[,] m_tabel;
        private List<Grammatics> m_element_str = new List<Grammatics>();
        private List<Grammatics> m_eps_rules = new List<Grammatics>();
        public enum NumberCheck { True, False, Error };
        public AnalysisDownNew2() { }
        public AnalysisDownNew2(List<Grammatics> rule, int[,] tabel, List<Grammatics> terminals, List<Grammatics> nterminals)
        {
            m_rule = rule;
            m_tabel = tabel;
            m_terminals = terminals;
            m_nterminals = nterminals;
        }
        public void Run(string grammar_str)
        {
            m_element_str.Clear();
            m_eps_rules.Clear();
            Search_Terminals();
            Search_Rules_Eps();
            grammar_str += " " + eps.m_name;
            string[] str = grammar_str.Split(' ');
            bool flag = false;
            NumberCheck check_number = NumberCheck.False;
            for (int k = 0; k < str.Length; k++)
            {
                Search_Terminals(str, k, ref flag, check_number);
                if (flag == false && check_number != NumberCheck.Error)
                {
                    Search_ID(str[k], ref flag, ref check_number);
                }
                flag = false;
            }
            if (check_number != NumberCheck.Error)
            {
                Algoritm_Down();
            }
            Array.Clear(str, 0, str.Length);
        }

        public void Search_ID(string number, ref bool flag, ref NumberCheck check_number)
        {
            if (IsThisNumberDown(number) == NumberCheck.False)
            {
                if (number.Length <= 8 && number.Length > 0)
                {
                    m_element_str.Add(id);
                    flag = true;
                }
                else
                {
                    Message_Errors(number, check_number);
                }
            }
        }

        public void Message_Errors(string number, NumberCheck check_number)
        {
            if (number.Length > 8)
            {
                check_number = NumberCheck.Error;
                AnalysisEvent.PrintMessage("Длина идентификатора должна быть меньше 8 символов!\nОшибка --> " + number);
            }
            if (number.Length == 0)
            {
                check_number = NumberCheck.Error;
                AnalysisEvent.PrintMessage("Длина идентификатора должна быть больше 0 символов!\n");
            }
        }
        //Метод для работы с числами для нисходящего разбора
        public NumberCheck IsThisNumberDown1(string str1)
        {
            bool error = true;
            if (CheckNumber(str1, "0123456789ABCDF.Ee-"))
            {
                //if (CheckNumber(str1, ".Ee-"))
                //    {
                for (int k = 0; k < str1.Length; k++ )
                {
                    if (Convert.ToString(str1[k]) == "E")
                    {
                        m_element_str.Add(constNT);
                        return NumberCheck.True;
                        error = false;
                    }
                   
                }
                 if (error)
                    {
                        AnalysisEvent.PrintMessage("Введите вещественное" + '\n' + " число с порядком!" + '\r' + "Ошибка --> " + str1);
                        return NumberCheck.Error;
                    } 
            }
            else
            {
                return NumberCheck.False;
            }
            return NumberCheck.Error;
        }
        public NumberCheck IsThisNumberDown(string str1)
        {
            if (CheckNumber(str1, "0123456789ABCDF.Ee-"))
            {
                m_element_str.Add(constNT);
                return NumberCheck.True;   
            }
            else
            {
                return NumberCheck.False;
            }
        }

        public bool CheckNumber(string str, string symbol)
        {
            bool number = false;
            for (int k = 0; k < str.Length; k++ )
            {
               number = symbol.Contains(Convert.ToString(str[k]));
               if (!number) { return false; }
            }
            return number;
        }
        public void Algoritm_Down()
        {
            string str_nterminals = m_nterminals[0].m_name + " " + eps.m_name;
            int index_i = 0, index_j = 0;
            index_i = m_element_str[0].number;
            index_j = Search_Index_J(Convert.ToString(str_nterminals[0]));
            int number_rule = 0;
            string pr = "";
            while (m_tabel[index_j - 1, index_i - 1] != 30)
            {
                number_rule = m_tabel[index_j - 1, index_i - 1]; // определение номера правила
                if (number_rule == 29)
                {
                    int rule = str_nterminals.IndexOf(" ");
                    str_nterminals = str_nterminals.Remove(0, rule + 1);
                    m_element_str.RemoveAt(0);
                    printDown(m_element_str, str_nterminals, pr);
                }
                else
                {
                    foreach (var rule in m_rule)
                    {
                        if (rule.number == number_rule)
                        {
                            string[] str_nterminals_array = str_nterminals.Split(' ');
                            str_nterminals_array[0] = rule.m_name;
                            str_nterminals = "";
                            ScaningEPSRule(str_nterminals_array, ref str_nterminals, rule.number);
                            //str_nterminals.Insert(0, rule.m_name);
                            pr += " " + Convert.ToString(rule.number);
                            printDown(m_element_str, str_nterminals, pr);
                        }
                    }
                }
                index_i = m_element_str[0].number;
                string[] laM = str_nterminals.Split(' ');
                index_j = Search_Index_J(laM[0]);
                if (m_tabel[index_j - 1, index_i - 1] == 28)
                {
                    AnalysisEvent.PrintCompileInfo("Ошибка при выполнении нисходящего разбора!");
                    m_element_str.Clear();
                    break;
                }
            }

        }
        public int Search_Index_J(string symbol)
        {
            foreach (var nterminal in m_nterminals)
            {
                if (symbol == nterminal.m_name)
                {
                     return nterminal.number;
                }
            }
            return 0;
        }
        public void printDown(List<Grammatics> arrR, string M, string pr)
        {
            string s1 = "";
            for (int i = 0; i < arrR.Count; i++)
            {
                s1 += arrR[i].m_name + " ";
            }
            AnalysisEvent.PrintCompileInfo("\nСтрока:" + s1);
            AnalysisEvent.PrintCompileInfo("Магазин:" + M);
            AnalysisEvent.PrintCompileInfo("Правила:" + pr);
        }
        public void Search_Terminals(string[] str, int index, ref bool flag, NumberCheck check_number)
        {
            foreach (var terminal in m_terminals)
            {
                if (terminal.m_name == str[index] && check_number != NumberCheck.Error)
                {
                    m_element_str.Add(terminal);
                    flag = true;
                }
            }
        }
        public void ScaningEPSRule(string[] laaM, ref string M, int p)
        {
            bool flag = false;
            foreach (var eps_rule in m_eps_rules)
            {
                if (p == eps_rule.number)
                {
                    AddProbelM(laaM, ref M);
                    flag = true;
                }
            }
            if (flag == false)
            {
                AddProbelM(laaM, ref M);
                M = laaM[0] + " " + M;
            }
        }
        public void AddProbelM(string[] laaM, ref string M)
        {
            //List<string> dinosaurs = new List<string>(laaM);
            //M = dinosaurs.GetRange(1, dinosaurs.Count-1).ToArray() + " ";
            for (int k = 1; k < laaM.Length; k++)
            {
                M += laaM[k] + " ";
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
                            eps = terminal;
                            break;
                        }
                    case "id":
                        {
                            id = terminal;
                            break;
                        }
                    case "const":
                        {
                            constNT = terminal;
                            break;
                        }
                }
            }
        }
        public void Search_Rules_Eps()
        {
            // Для eps правил можно задать свой символ
            foreach (var rule in m_rule)
            {
                if (rule.m_name == "eps")
                {
                    m_eps_rules.Add(rule);
                }
            }
        }

    }
}
