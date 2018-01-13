using System;
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
        public bool IsNumberDown(string str2, string dopstr)
        {
            int pr2 = 0;
            for (int j = 0; j < str2.Length; j++)
            {
                for (int i = 0; i < dopstr.Length; i++)
                {
                    if (str2[j] == dopstr[i]) pr2++;
                }
            }
            return (pr2 == 0) ? false : true;
        }
        //Метод для работы с числами для нисходящего разбора
        public NumberCheck IsThisNumberDown(string str1)
        {
            int ptp = 0;
            if (IsNumberDown(str1, "0123456789") == true)
            {
                for (int i = 0; i < str1.Length; i++)
                {
                    if (IsNumberDown(Convert.ToString(str1[i]), "0123456789.Ee-") == true)
                        ptp += 1;
                }
                if (ptp == str1.Length)
                {
                    m_element_str.Add(constNT);
                    return NumberCheck.True;
                }
                else
                {
                    AnalysisEvent.PrintMessage("Введите вещественное" + '\n' + " число с порядком!" + '\r' + "Ошибка --> " + str1);
                    return NumberCheck.Error;
                }
            }
            else
            {
                return NumberCheck.False;
            }
        }

        public bool CheckNumber(string str)
        {
            return "0123456789ABCDF".Contains(str);
        }

        public void Algoritm_Down()
        {
            string str_nterminals = m_nterminals[0].m_name + " " + eps.m_name;
            int index = 0, index_i = 0, index_j = 0;
            index_i = m_element_str[0].number;
            index_j = Search_Index_J(Convert.ToString(str_nterminals[0]));
            int number_rule = 0;
            string pr = "";
            while (m_tabel[index_j - 1, index_i - 1] != 30)
            {
                number_rule = m_tabel[index_j - 1, index_i - 1]; // определение номера правила
                if (number_rule == 29)
                {
                    str_nterminals = delFirstString(str_nterminals);
                    m_element_str = delFirstDown(m_element_str);
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
                            pr = pr + " " + Convert.ToString(rule.number);
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
        public List<Grammatics> delFirstDown(List<Grammatics> down)
        {
            List<Grammatics> newd = new List<Grammatics>();
            Grammatics elemDel = new Grammatics();
            for (int i = 1; i < down.Count; i++)
            {
                elemDel.number = down[i].number;
                elemDel.m_name = down[i].m_name;
                newd.Add(elemDel);
            }
            return newd;
        }
        public string delFirstString(string M)
        {
            string[] lastM = M.Split(' ');
            string newM = "";
            for (int i = 1; i < lastM.Length; i++)
            {
                newM += lastM[i] + " ";
            }
            return newM;
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
                if (rule.m_name == "  ")
                {
                    m_eps_rules.Add(rule);
                }
            }
        }

    }
}
