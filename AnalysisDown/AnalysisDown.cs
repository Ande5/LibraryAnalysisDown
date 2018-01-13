using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisDown
{
    class AnalysisDown
    {
        public string str = "";
        public int[,] arrZDown;
        List<Grammatics> arrNT ,arrNTT , arrPr;
        public Grammatics[] arrStr = new Grammatics[1000];
        public string[] arrM = new string[1000];
        public Grammatics elemUStr = new Grammatics();
        public int ind = 0;
        public Grammatics eps, id, constNT;
        public List<Grammatics> probels = new List<Grammatics>();
        //Метод нисходящего разбора
        public void Down(string richTextBox1, string text)
        {
            ind = 0;
            str += text + " $";
            int l = 0;
            bool flag = false;
            arrM = str.Split(' ');

            for (int i = 0; i < arrM.Length; i++)
            {
                for (int j = 0; j < arrNT.Count; j++)
                {
                    if (arrM[i] == arrNT[j].m_name && l != -1)
                    {
                        ElementUpStr(arrNT[j]);
                        flag = true;
                    }
                }
                if (arrM[i] == "$" && l != -1)
                {
                    ElementUpStr(eps);
                    flag = true;
                }
                if (flag == false && l != -1)
                {
                    l = IsThisNumberDown(arrM[i]);
                    if (l == 0)
                    {
                        if (arrM[i].Length <= 8 && arrM[i].Length > 0)
                        {
                            ElementUpStr(id);
                            flag = true;
                        }
                        else
                        {
                            if (arrM[i].Length > 8)
                            {
                                l = -1;
                                AnalysisEvent.PrintMessage("Длина идентификатора должна быть меньше 8 символов!" + '\n' + "Ошибка --> " + arrM[i]);
                            }
                            if (arrM[i].Length == 0)
                            {
                                l = -1;
                                AnalysisEvent.PrintMessage("Длина идентификатора должна быть больше 0 символов!" + '\n' + "Слово № " + (i + 1).ToString() + " является пробелом.");
                                str = null;
                            }
                        }
                    }
                }
                flag = false;
            }
            if (l != -1) algoritmUp(richTextBox1);
        }
        public void ElementUpStr(Grammatics element)
        {
            elemUStr.m_name = element.m_name;
            elemUStr.number = element.number;
            arrStr[ind] = elemUStr;
            ind = (element.m_name == eps.m_name) ? ind : ind += 1;
        }
        //Метод для работы с числами для нисходящего разбора
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
        public int IsThisNumberDown(string str1)
        {
            int ptp = 0;
            //if (str1 == "true" || str1 == "false")
            //{
            //    ElementUpStr(constNT);
            //    return 1;
            //}
            //else
                if (IsNumberDown(str1, "0123456789") == true)
                {
                    for (int i = 0; i < str1.Length; i++)
                    {
                        if (IsNumberDown(Convert.ToString(str1[i]), "0123456789.Ee-") == true)
                            ptp += 1;
                    }
                    if (ptp == str1.Length)
                    {
                        ElementUpStr(constNT);
                        return 1;
                    }
                    else
                    {
                        AnalysisEvent.PrintMessage("Введите вещественное" + '\n' + " число с порядком!" + '\r' + "Ошибка --> " + str1);
                        str = "";
                        return -1;
                    }
                }
                else
                {
                    return 0;
                }
        }
        public void algoritmUp(string richTextBox1)
        {
            string M = "S $", pr = "";
            int IarrStr = 0, jTr = 0, f = 0;
            printDown(arrStr, M, pr, richTextBox1);
            int iTr = arrStr[IarrStr].number;

            for (int i = 0; i < arrNTT.Count; i++)
            {
                if (Convert.ToString(M[0]) == arrNTT[i].m_name)
                    jTr = arrNTT[i].number;
            }
            while (arrZDown[jTr - 1, iTr - 1] != 30)
            {
                f = arrZDown[jTr - 1, iTr - 1];
                if (f == 29)
                {
                    M = delFirstString(M);
                    arrStr = delFirstDown(arrStr);
                    printDown(arrStr, M, pr, richTextBox1);
                }
                else
                {
                    for (int i = 0; i < arrPr.Count; i++)
                    {
                        if (arrPr[i].number == f)
                        {
                            string ps = arrPr[i].m_name;
                            int p = arrPr[i].number;
                            string[] laaM = M.Split(' ');
                            laaM[0] = ps; M = "";
                            ScaningEPSRule(laaM, ref M, p);
                            pr = pr + " " + Convert.ToString(p);
                            printDown(arrStr, M, pr, richTextBox1);
                        }
                    }
                }
                iTr = arrStr[IarrStr].number;
                string[] laM = M.Split(' ');
                for (int i = 0; i < arrNTT.Count; i++)
                {
                    if (Convert.ToString(laM[0]) == arrNTT[i].m_name)
                        jTr = arrNTT[i].number;
                }
                if (arrZDown[jTr - 1, iTr - 1] == 28)
                {
                    richTextBox1 += "Ошибка при выполнении нисходящего разбора!";
                    AnalysisEvent.PrintCompileInfo(richTextBox1);
                    Array.Clear(arrStr, 0, arrStr.Length);
                    Array.Clear(arrM, 0, arrM.Length);
                    str = "";
                    break;
                }
            }
        }
        public Grammatics[] delFirstDown(Grammatics[] down)
        {
            Grammatics[] newd = new Grammatics[down.Length - 1];
            Grammatics elemDel = new Grammatics();
            for (int i = 1; i < down.Length; i++)
            {
                elemDel.number = down[i].number;
                elemDel.m_name = down[i].m_name;
                newd[i - 1] = elemDel;
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
        public void printDown(Grammatics[] arrR, string M, string pr, string richTextBox1)
        {
            string s1 = "";
            for (int i = 0; i < arrR.Length; i++)
            {
                s1 += arrR[i].m_name + " ";
            }
            richTextBox1 += "Строка:" + s1 + '\n';
            richTextBox1 += "Магазин:" + M + '\n';
            richTextBox1 += "Правила:" + pr + '\n';
            richTextBox1 += "      " + '\n';
            AnalysisEvent.PrintCompileInfo(richTextBox1);
        }
        public int[,] LoadingTabel
        {
            set { arrZDown = value; }
        }
        public List<Grammatics> LoadingGrammatics
        {
            set { arrPr = value; }
        }
        public List<Grammatics> LoadingTerminals
        {
            set { arrNT = value; }
        }
        public List<Grammatics> LoadingNTerminals
        {
            set { arrNTT = value; }
        }
        public void ScanningProbels()
        {
            Grammatics probel = new Grammatics();
            // Для eps правил можно задать свой символ
            for (int k = 0; k < arrPr.Count; k++)
            {
                if (arrPr[k].m_name == " ")
                {
                    probel.m_name = arrPr[k].m_name;
                    probel.number = arrPr[k].number;
                    probels.Add(probel);
                }
            }
        }
        public void ScaningEPSRule(string[] laaM, ref string M, int p)
        {
            bool flag = false;
            foreach (var probel in probels)
            {
                if (p == probel.number)
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
        public void ScanningKeyNTerminals()
        {
            foreach (var gr in arrNT)
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
        public void LoadingAnalysis(int [,] tabel, List<Grammatics> Grammatics, List<Grammatics> Terminals, List<Grammatics> NTerminals )
        {
            Array.Clear(arrStr, 0, arrStr.Length);          
            Array.Clear(arrM, 0, arrM.Length);
            probels.Clear();
            arrZDown  =  tabel; arrPr = Grammatics;
            arrNT = Terminals; arrNTT = NTerminals;
        }
        public void AnalysisStart(string richTextBox1, string text)
        {
            ScanningKeyNTerminals();
            ScanningProbels();
            str = "";
            Down(richTextBox1, text);
        }
    }
}

