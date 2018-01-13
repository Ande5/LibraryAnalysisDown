using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisDown
{
    class AnalysisDownExp
    {
        public string str = ""; // не нужна
        public int[,] arrZDown;
        List<Grammatics> arrNT, arrNTT, arrPr;
        List<Grammatics> arrStr = new List<Grammatics>();
        public string[] arrM = new string[1000]; // нужно сделать локальной
      //  public Grammatics elemUStr = new Grammatics(); // не нужно
       // public int ind = 0; не нужно
        public Grammatics eps, id, constNT;
        public List<Grammatics> probels = new List<Grammatics>();
        public enum NumberCheck { True, False, Errors};
        //Метод нисходящего разбора
        //public void Down(string richTextBox1, string text)
        //{
        //    // ind = 0;
        //    str += text + " $";
        //    int l = 0;
        //    bool check_number = true;
        //    bool flag = false;
        //    arrM = str.Split(' ');

        //    for (int i = 0; i < arrM.Length; i++)
        //    {
        //        for (int j = 0; j < arrNT.Count; j++)
        //        {
        //            if (arrM[i] == arrNT[j].m_name)
        //            {
        //                ElementUpStr(arrNT[j]); // Поиск терминалов в строке
        //                flag = true;
        //            }
        //        }
        //        if (flag == false)
        //        {
        //            check_number = CheckNumber(arrM[i]);
        //            if (check_number == true)
        //            {
        //                if (arrM[i].Length <= 8 && arrM[i].Length > 0)
        //                {
        //                    ElementUpStr(id); // поиск id
        //                    flag = true;
        //                }
        //                else
        //                {
        //                    if (arrM[i].Length > 8)
        //                    {
        //                        check_number = false;
        //                        AnalysisEvent.PrintMessage("Длина идентификатора должна быть меньше 8 символов!" + '\n' + "Ошибка --> " + arrM[i]);
        //                    }
        //                    if (arrM[i].Length == 0)
        //                    {
        //                        check_number = false;
        //                        AnalysisEvent.PrintMessage("Длина идентификатора должна быть больше 0 символов!" + '\n' + "Слово № " + (i + 1).ToString() + " является пробелом.");
        //                        str = null;
        //                    }
        //                }
        //            }
        //        }
        //        flag = false;
        //    }
        //    if (check_number != false) algoritmUp(richTextBox1);
        //}
        public void Down(string richTextBox1, string text)
        {
            //ind = 0;
            str += text + " $";
            NumberCheck l = NumberCheck.False;
            bool flag = false;
            arrM = str.Split(' ');

            for (int i = 0; i < arrM.Length; i++)
            {
                for (int j = 0; j < arrNT.Count; j++)
                {
                    if (arrM[i] == arrNT[j].m_name && l != NumberCheck.Errors)
                    {
                        ElementUpStr(arrNT[j]);
                        flag = true;
                    }
                }
              
                if (flag == false && l != NumberCheck.Errors)
                {
                    l = IsThisNumberDown(arrM[i]);
                    if (l == NumberCheck.False)
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
                                l = NumberCheck.Errors;
                                AnalysisEvent.PrintMessage("Длина идентификатора должна быть меньше 8 символов!" + '\n' + "Ошибка --> " + arrM[i]);
                            }
                            if (arrM[i].Length == 0)
                            {
                                l = NumberCheck.Errors;
                                AnalysisEvent.PrintMessage("Длина идентификатора должна быть больше 0 символов!" + '\n' + "Слово № " + (i + 1).ToString() + " является пробелом.");
                                str = null;
                            }
                        }
                    }
                }
                flag = false;
            }
            if (l != NumberCheck.Errors) algoritmUp(richTextBox1);
        }
        public void ElementUpStr(Grammatics element)
        {
            arrStr.Add(element);
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
           // NumberCheck s = NumberCheck.False;
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
                        ElementUpStr(constNT);
                        //return 1;
                        //s = NumberCheck.True;
                        return NumberCheck.True;
                    }
                    else
                    {
                        AnalysisEvent.PrintMessage("Введите вещественное" + '\n' + " число с порядком!" + '\r' + "Ошибка --> " + str1);
                        str = "";
                        //return -1;
                       // s = NumberCheck.Errors;
                        return NumberCheck.Errors;
                    }
                }
                else
                {
                    //return 0;
                  //  s = NumberCheck.False;
                    return NumberCheck.False;
                }
        }
        public bool CheckNumber(string str1)
        {
            return "0123456789".Contains(str1);
        }
        public void algoritmUp(string richTextBox1)////
        {
            string M = "S $", 
                   pr = "";
            int IarrStr = 0,
                jTr = 0, 
                f = 0;

            printDown(arrStr, M, pr, richTextBox1);
            int iTr = arrStr[IarrStr].number;
            // Проверят наличие правила S зачем нужно, не понятно))
            for (int i = 0; i < arrNTT.Count; i++)
            {
                if (Convert.ToString(M[0]) == arrNTT[i].m_name)
                    jTr = arrNTT[i].number;
            }
            while (arrZDown[jTr - 1, iTr - 1] != 30)
            {
                f = arrZDown[jTr - 1, iTr - 1]; // определение номера правила
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
                    //Array.Clear(arrStr, 0, arrStr.Length);
                    arrStr.Clear();
                    Array.Clear(arrM, 0, arrM.Length);
                    str = "";
                    break;
                }
            }
        }
        public List<Grammatics> delFirstDown(List<Grammatics> down)
        {
            List<Grammatics> newd = new List<Grammatics>();
            Grammatics elemDel = new Grammatics();
            for (int i = 1; i < down.Count; i++)
            {
                elemDel.number = down[i].number;
                elemDel.m_name = down[i].m_name;
                //newd[i - 1] = elemDel;
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
        public void printDown(List<Grammatics> arrR, string M, string pr, string richTextBox1)
        {
            string s1 = "";
            for (int i = 0; i < arrR.Count; i++)
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
                if (arrPr[k].m_name == "  ")
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
                    case "$":
                        {
                            eps = gr;
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
        public void LoadingAnalysis(int[,] tabel, List<Grammatics> Grammatics, List<Grammatics> Terminals, List<Grammatics> NTerminals)
        {
            arrStr.Clear();
            Array.Clear(arrM, 0, arrM.Length);
            probels.Clear();
            arrZDown = tabel; arrPr = Grammatics;
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

