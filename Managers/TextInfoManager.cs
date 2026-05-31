using System.Windows.Forms;

namespace lab1.Managers
{
    public class TextInfoManager
    {
        public void ShowTaskStatement()
        {
            MessageBox.Show("Разработать синтаксический анализатор (парсер) в соответствии с индивидуальным вариантом курсовой работы, интегрировать его в приложение из лабораторной работы №1 и обеспечить наглядный вывод результатов анализа.",
                "Постановка задачи", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowGrammar()
        {
            MessageBox.Show("Грамматика создания функции языка C/C++ G[<FTYPE>] в нотации Хомского с продукциями P:\n\n1 <FTYPE> → TYPE <FSPACE>\n2 <FSPACE> → '_' <FID>\n3 <FID> → IDENTIFIER <OPENQ>\n4 <OPENQ> → '(' <PTYPE>\n5 <PTYPE> → TYPE <PSPACE>\n6 <PSPACE> → '_' <PID>\n7 <PID> → IDENTIFIER <COMMA> | IDENTIFIER <CLOSEQ>\n8 <COMMA> → ',' <PTYPE>\n9 <CLOSEQ> → ')' <OPENF>\n10 <OPENF> → '{' <RETURN>\n12 <RETURN> → 'return' <BSPACE>\n13 <BSPACE> → '_' <BSPACE>\n14 <BSPACE> → <E> <COLON>\n15 <COLON> → ';' <CLOSEF>\n16 <CLOSEF> → '}' <COLONEND>\n14 <E> → <T> <A>\n15 <A> → '+' <T> <A> | '-' <T> <A> | ε\n16 <T> → <O> <B>\n17 <B> → '*' <O> <B> | '/' <O> <B> | ε\n18 <O> → 'id' '('<E>')'\n19 <COLONEND> → ';'\n20 IDENTIFIER → letter <INDENTIFIER_REM>\n21 <IDENTIFIER_REM> → letter | digit | ε\nletter -> ‘a’ | ‘b’ | … | ‘z’ | ‘A’ | ‘B’ | … | ‘Z’\ndigit -> ‘0’ | ‘1’ | … | ‘9’\nTYPE -> ‘int’ | ‘float’ | 'double' | 'long'\n\nСледуя введенному формальному определению грамматики, представим G[<FTYPE>] ее составляющими:\n- Z = <FTYPE>\n\n- VT = {a, b, ..., z, A, B, ..., Z, 0, 1, ..., 9, +, -, /, *, {, }, (, ), ;, }\n\n- VN = {<FTYPE>, <FSPACE>, <FID>, <OPENQ>, <PTYPE>, <PSPACE>, <PID>, <COMMA>, <CLOSEQ>, <OPENF>, <RETURN>, <BSPACE>, <COLON>, <CLOSEF>, <E>, <T>, <B>}",
                "Разработанная грамматика", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowGrammarClassification()
        {
            MessageBox.Show("Согласно классификации Хомского, полученная порождающая грамматика G[<FTYPE>] соответствует типу контекстно-свободных, так как правая часть каждой редукции начинается либо с терминального символа, либо с нетерминального, принадлежащего объединённому словарю.\nA → a, A ∈ V_N , a ∈ V^* .\nГрамматика G[<FTYPE>] не является автоматной, так как не все её редукции начинаются с терминального символа. По этой же причине данная грамматика не является S - грамматикой.",
                "Классификация грамматики", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowAnalysisMethod()
        {
            MessageBox.Show("Так как грамматика G[<FTYPE>] принадлежит классу контекстно-свободных, анализ реализован методом рекурсивного спуска.\nИдея метода заключается в том, что каждому нетерминалу ставится в соответствие программная функция, которая распознает цепочку, порожденную этим нетерминалом.\nЭти функции вызываются в соответствии с правилами грамматики и иногда вызывают сами себя, поэтому для реализации необходимо выбрать язык, обладающий рекурсивными возможностями, в нашем случае это язык C#.",
                "Метод анализа", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowTestExample()
        {
            MessageBox.Show("int sum(int x, int y, int z) {\nreturn x + (y * z);\n};",
                "Тестовый пример", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowReferences()
        {
            MessageBox.Show("1.  Шорников Ю.В. Теория и практика языковых процессоров : учеб. пособие / Ю.В. Шорников. – Новосибирск: Изд-во НГТУ, 2004.\n2.  Gries D. Designing Compilers for Digital Computers. New York, Jhon Wiley, 1971. 493 p.\n3.  Теория формальных языков и компиляторов [Электронный ресурс] / Электрон. дан. URL: https://dispace.edu.nstu.ru/didesk/course/show/8594, свободный. Яз.рус. (дата обращения 01.04.2021).\n4.  Хантер Р. Проектирование и конструирование компиляторов / Р. Хантер. – Москва : Мир, 1984. – 232 с.",
                "Список литературы", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowSourceCode()
        {
            MessageBox.Show("https://github.com/Dekaditch/courseWorking",
                "Ссылка на GitHub с исходным кодом", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}