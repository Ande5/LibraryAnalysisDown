using System.Collections.Generic;

namespace AnalysisDown
{
    public class InitializeAnalysisDown1
    {
        private int[,] m_tabel = {
                                     {28,28,1,1,28,1,1,1,1,1,1,1,2},
                                     {3,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,4,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,5,10,28,5,5,5,5,5,5,5,28},
                                     {28,28,7,6,28,6,6,6,6,6,6,6,28},
                                     {8,28,8,9,8,8,8,8,8,8,8,8,8},
                                     {28,28,28,28,11,28,28,28,28,28,28,28,28},
                                     {28,28,14,12,28,15,16,16,16,16,16,16,28},
                                     {28,28,28,28,13,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,17,18,19,20,21,22,28},
                                     {29,28,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,29,28,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,29,28,28,28,28,28,28,28,28,28,28},
                                     {28,28,28,29,28,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,29,28,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,29,28,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,29,28,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,29,28,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,29,28,28,28,28},
                                     {28,28,28,28,28,28,28,28,28,29,28,28,28},
                                     {28,28,28,28,28,28,28,28,28,28,29,28,28},
                                     {28,28,28,28,28,28,28,28,28,28,28,29,28},
                                     {28,28,28,28,28,28,28,28,28,28,28,28,30}
                                  };
        /*
S -> A E 
S -> '' 
E -> if W S 
W -> := id D 
D -> G 
G -> A 
G -> id B 
B -> '' 
B -> [ A C 
D -> [ A C G 
C -> ] 
A -> [ A F 
F -> ] G 
A -> id 
A -> const 
A -> R A 
R -> - A 
R -> ! A 
R -> = A 
R -> sqrt A 
R -> > A 
R -> < A
         */
        private List<Grammatics> Rule = new List<Grammatics>() 
        { 
          new Grammatics(1, "A E"),
          new Grammatics(2, " "),
          new Grammatics(3, "if W S"),
          new Grammatics(4, ":= id D"),
          new Grammatics(5, "G"),
          new Grammatics(6, "A"),
          new Grammatics(7, "id B"),
          new Grammatics(8, " "),
          new Grammatics(9, "[ A C"),
          new Grammatics(10,"[ A C G"),
          new Grammatics(11,"]"),
          new Grammatics(12,"[ A F"),
          new Grammatics(13,"] G"),
          new Grammatics(14,"id"),
          new Grammatics(15,"const"),
          new Grammatics(16,"R A"),
          new Grammatics(17,"- A"),
          new Grammatics(18,"! A"),
          new Grammatics(19,"= A"),
          new Grammatics(20,"sqrt A"),
          new Grammatics(21,"> A"),
          new Grammatics(22,"< A")
        };

        private List<Grammatics> Terminals = new List<Grammatics>()
        {
            //new Grammatics(1,"if"),
            //new Grammatics(2,":="),
            //new Grammatics(3,"id"),
            //new Grammatics(4,"["),
            //new Grammatics(5,"]"),
            //new Grammatics(6,"const"),
            //new Grammatics(7,"-"),
            //new Grammatics(8,"<"),
            //new Grammatics(9,">"),
            //new Grammatics(10,"!"),
            //new Grammatics(11,"="),
            //new Grammatics(12,"sqrt"),
            //new Grammatics(13,"$")
            new Grammatics(1,"if"),
            new Grammatics(2,":="),
            new Grammatics(3,"id"),
            new Grammatics(4,"["),
            new Grammatics(5,"]"),
            new Grammatics(6,"const"),
            new Grammatics(7,"-"),
            new Grammatics(8,"!"),
            new Grammatics(9,"="),
            new Grammatics(10,"sqrt"),
            new Grammatics(11,"<"),
            new Grammatics(12,">"),
            new Grammatics(13,"$")
        };

        private List<Grammatics> NTerminals = new List<Grammatics>()
        {
            new Grammatics(1,"S"),
            new Grammatics(2,"E"),
            new Grammatics(3,"W"),
            new Grammatics(4,"D"),
            new Grammatics(5,"G"),
            new Grammatics(6,"B"),
            new Grammatics(7,"C"),
            new Grammatics(8,"A"),
            new Grammatics(9,"F"),
            new Grammatics(10,"R"),
            new Grammatics(11,"if"),
            new Grammatics(12,":="),
            new Grammatics(13,"id"),
            new Grammatics(14,"["),
            new Grammatics(15,"]"),
            new Grammatics(16,"const"),
            new Grammatics(17,"-"),
            new Grammatics(18,"<"),
            new Grammatics(19,">"),
            new Grammatics(20,"!"),
            new Grammatics(21,"="),
            new Grammatics(22,"sqrt"),
            new Grammatics(23,"$")
        };

        private InitializeAnalysisDown1() { }

        public InitializeAnalysisDown1(string text)
        {
            m_analysis_down = new AnalysisDownNew2(Rule, m_tabel, Terminals, NTerminals);
            m_analysis_down.Run(text);
        }

        private AnalysisDownNew2 m_analysis_down;
    }
}
