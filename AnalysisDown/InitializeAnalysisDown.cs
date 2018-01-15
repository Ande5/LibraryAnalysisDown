using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisDown
{
    public class InitializeAnalysisDown
    {
        int[,] Tabel = {
                                     {1,28,28,28,28,28,28,28,28,28,28,28,28,28,2},
                                     {28,3,28,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,4,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {26,26,26,5,28,28,28,28,28,28,28,28,28,28,26},
                                     {28,28,28,28,6,6,28,6,6,6,6,6,6,6,28},
                                     {28,7,7,28,8,13,28,8,8,8,8,8,8,8,28},
                                     {28,28,28,28,9,10,28,10,10,10,10,10,10,10,28},
                                     {11,11,11,11,11,12,11,11,11,11,11,11,11,11,11},
                                     {28,28,28,28,28,28,14,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,17,15,28,18,19,19,19,19,19,19,28},
                                     {28,28,28,28,28,28,16,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,20,21,22,23,24,25,28},
                                     {29,28,28,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,29,28,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,29,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,28,29,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,29,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,29,28,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,29,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,29,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,29,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,28,29,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,28,28,29,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,28,28,28,29,28,28,28},
                                     {28,28,28,28,28,28,28,28,28,28,28,28,29,28,28},
                                     {28,28,28,28,28,28,28,28,28,28,28,28,28,29,28},
                                     {28,28,28,28,28,28,28,28,28,28,28,28,28,28,30}    
                                  };

        List<Grammatics> Rule = new List<Grammatics>() 
        { 
          new Grammatics(1, "else Y Z"),
          new Grammatics(2, "eps"),
          new Grammatics(3, "then Y I"),
          new Grammatics(4, "if K S"),
          new Grammatics(5, ":= id D"),
          new Grammatics(6,"A Y"),
          new Grammatics(7,"eps"),
          new Grammatics(8,"G"),
          new Grammatics(9,"id B"),
          new Grammatics(10,"A"),
          new Grammatics(11,"eps"),
          new Grammatics(12,"[ A C"),
          new Grammatics(13,"[ A C G"),
          new Grammatics(14,"]"),
          new Grammatics(15,"[ A F"),
          new Grammatics(16,"] G"),
          new Grammatics(17,"id"),
          new Grammatics(18,"const"),
          new Grammatics(19,"R A"),
          new Grammatics(20,"* A"),
          new Grammatics(21,"& A"),
          new Grammatics(22,"+ A"),
          new Grammatics(23,"- A"),
          new Grammatics(24,"< A"),
          new Grammatics(25,"| A"),
          new Grammatics(26,"eps"),
        };

        List<Grammatics> Terminals = new List<Grammatics>()
        {
            new Grammatics(1,"else"),
            new Grammatics(2,"then"),
            new Grammatics(3,"if"),
            new Grammatics(4,":="),
            new Grammatics(5,"id"),
            new Grammatics(6,"["),
            new Grammatics(7,"]"),
            new Grammatics(8,"const"),
            new Grammatics(9,"*"),
            new Grammatics(10,"&"),
            new Grammatics(11,"+"),
            new Grammatics(12,"-"),
            new Grammatics(13,"<"),
            new Grammatics(14,"|"),
            new Grammatics(15,"$"),
        };

        List<Grammatics> NTerminals = new List<Grammatics>()
        {
            new Grammatics(1,"S"),
            new Grammatics(2,"Z"),
            new Grammatics(3,"I"),
            new Grammatics(4,"Y"),
            new Grammatics(5,"K"),
            new Grammatics(6,"D"),
            new Grammatics(7,"G"),
            new Grammatics(8,"B"),
            new Grammatics(9,"C"),
            new Grammatics(10,"A"),
            new Grammatics(11,"F"),
            new Grammatics(12,"R"),
            new Grammatics(13,"else"),
            new Grammatics(14,"then"),
            new Grammatics(15,"if"),
            new Grammatics(16,":="),
            new Grammatics(17,"id"),
            new Grammatics(18,"["),
            new Grammatics(19,"]"),
            new Grammatics(20,"const"),
            new Grammatics(21,"*"),
            new Grammatics(22,"&"),
            new Grammatics(23,"+"),
            new Grammatics(24,"-"),
            new Grammatics(25,"<"),
            new Grammatics(26,"|"),
            new Grammatics(27,"$"),
        };
        private InitializeAnalysisDown() { }

        public InitializeAnalysisDown(string text)
        {
            m_analysis_down = new AnalysisDown(Rule, Tabel,Terminals, NTerminals);
            m_analysis_down.Run(text);
        }
        public InitializeAnalysisDown(string text, int [,] m_tabel, List<Grammatics> m_rule, List<Grammatics> m_terminals, List<Grammatics> m_nterminals)
        {
            m_analysis_down = new AnalysisDown(m_rule, m_tabel, m_terminals, m_nterminals);
            m_analysis_down.Run(text);
        }
        AnalysisDown m_analysis_down;
    }
}
