using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public enum TOKEN
    {
        IF_TOK, ELSE_TOK, IOW_TOK, SIOW_TOK, CHLO_TOK, CHAIN_TOK, IOWF_TOK, SIOWF_TOK, WORTHLESS_TOK,
        LOOP_ITERATE_WHEN_TOK, TURNBACK_TOK, STOP_TOK, LOLI_TOK, INCLUDE_TOK, ID_TOK, ADD_TOK, SUBTRACT_TOK, MULTIPLY_TOK,
        DIVISION_TOK, AND_TOK, OR_TOK, NOT_TOK, EQUALITY_TOK, SMALLER_TOK, GREATER_TOK, NOTEQUAL_TOK,
        SME_TOK, GRE_TOK, ASSIGNMENT_TOK, ACCESS_TOK, LBRACKET_TOK, RBRACKET_TOK, LBRACES_TOK, RBRACES_TOK,
        LPRAN_TOK, RPRAN_TOK, NUMBER_TOK, SINGLEQUOTES_TOK, DOUBLEQUOTES_TOK, SEMICOLON_TOK, ERROR_TOK, SINGLE_COMMENT_TOK,
        MULTIPLE_COMMENT_TOK, IGNORABLE_TOK
    }
    public class Scanner
    {
        int ptr = 0;
        public string ScannedText { get; set; }

        public int ErrorCounter = 0;
        public static int ASCIIMIN = 32;
        public static int ASCIIMAX = 127;
        private int NumberOfStates = 0;
        private int[,] transition;
        private List<int> FoundNewLineIndices = new List<int>(); 
        Dictionary<int, TOKEN> FinalStates = new Dictionary<int, TOKEN>();


        public Scanner() { FSATable(150); formTB(); }       

        private int getLineNum(int index)
        {
            int i = 1;
            index -= 2;
            if (FoundNewLineIndices.Count == 0)
                return 1;
            while(i<= FoundNewLineIndices.Count)
            {
                if (index < FoundNewLineIndices[i-1])
                    return i;
                else i++;
            }
            return i;


        }
        private string removeNewLine(string Str)
        {

            string Original = Str;
            List<char> charsToRemove = new List<char>() { '\n', '\r' };
            
            for (int i = 0; i < Str.Length; i++)
            {
                if (Str[i] == '\r')
                   FoundNewLineIndices.Add(i);
            }
            foreach (char c in charsToRemove)
            {
                Str = Str.Replace(c.ToString(), " ");
            }

            return Str;
        }
        private string subString(int StartIndex, int LengthOfToken)
        {

            string str = ScannedText.Substring(StartIndex, LengthOfToken);
            return str;

        }

        private void formTB()
        {

            addTrans(1, 'I', 2);
            addTrans(1, 'w', 38);
            addTrans(1, '\'', 23);
            addTrans(1, '\"', 24);
            addTrans(1, 'S', 25);
            addTrans(1, 'C', 55);
            addTrans(1, 'L', 33);
            addTrans(1, 'T', 47);
            addTrans(1, ';', 62);
            addTrans(1, '/', 63);
            addTrans(1, '$', 65);
            addTrans(1, '~', 69);
            addTrans(1, '|', 70);
            addTrans(1, '>', 74);
            addTrans(124, '=', 143);
            addTrans(1, '<', 75);
            addTrans(1, '=', 76);
            addTrans(1, '!', 77);
            addTrans(1, '*', 80);
            addTrans(1, '-', 81);
            addTrans(1, '+', 83);
            addTrans(1, '[', 84);
            addTrans(1, ']', 85);
            addTrans(1, '{', 86);
            addTrans(1, '}', 87);
            addTrans(1, '&', 72);
            addTrans(1, 'E', 92);
            addTrans(2, 'n', 3);
            addTrans(2, 'f', 9);
            addTrans(2, 't', 10);
            addTrans(2, 'o', 20);
            addTrans(3, 'c', 4);
            addTrans(4, 'l', 5);
            addTrans(5, 'u', 6);
            addTrans(6, 'd', 7);
            addTrans(7, 'e', 8);
            addTrans(8, ' ', 100);
            addTrans(9, ' ', 101);
            addTrans(10, 'e', 11);
            addTrans(11, 'r', 12);
            addTrans(12, 'a', 13);
            addTrans(13, 't', 14);
            addTrans(14, 'e', 15);
            addTrans(15, 'w', 16);
            addTrans(16, 'h', 17);
            addTrans(17, 'e', 18);
            addTrans(18, 'n', 19);
            addTrans(19, ' ', 102);
            addTrans(20, 'w', 21);
            addTrans(21, 'f', 22);
            addTrans(21, ' ', 103);
            addTrans(22, ' ', 104);
            addTrans(23, ' ', 105);
            addTrans(24, ' ', 106);
            addTrans(25, 'I', 26);
            addTrans(25, 't', 30);
            addTrans(26, 'o', 27);
            addTrans(27, 'w', 28);
            addTrans(28, 'f', 29);
            addTrans(28, ' ', 107);
            addTrans(29, ' ', 140);
            addTrans(30, 'o', 31);
            addTrans(31, 'p', 32);
            addTrans(32, ' ', 109);
            addTrans(33, 'o', 34);
            addTrans(34, 'l', 35);
            addTrans(34, 'o', 37);
            addTrans(35, 'i', 36);
            addTrans(36, ' ', 109);
            addTrans(37, 'p', 15);
            addTrans(38, 'o', 39);
            addTrans(39, 'r', 40);
            addTrans(40, 't', 41);
            addTrans(41, 'h', 42);
            addTrans(42, 'l', 43);
            addTrans(43, 'e', 44);
            addTrans(44, 's', 45);
            addTrans(45, 's', 46);
            addTrans(46, ' ', 111);
            addTrans(47, 'u', 48);
            addTrans(48, 'r', 49);
            addTrans(49, 'n', 50);
            addTrans(50, 'b', 51);
            addTrans(51, 'a', 52);
            addTrans(52, 'c', 53);
            addTrans(53, 'k', 54);
            addTrans(54, ' ', 112);
            addTrans(55, 'h', 56);
            addTrans(56, 'l', 57);
            addTrans(56, 'a', 59);
            addTrans(57, 'o', 58);
            addTrans(58, ' ', 113);
            addTrans(59, 'i', 60);
            addTrans(60, 'n', 61);
            addTrans(61, ' ', 114);
            addTrans(62, ' ', 115);
            addTrans(63, '$', 64);
            addTrans(63, ' ', 116);
            addTrans(64, ' ', 117);
            addTrans(65, '/', 68);
            addTrans(65, '$', 66);
            addTrans(66, '$', 67);
            addTrans(67, ' ', 118);
            addTrans(68, ' ', 119);
            addTrans(69, ' ', 120);
            addTrans(70, '|', 71);
            addTrans(71, ' ', 121);
            addTrans(72, '&', 73);
            addTrans(73, ' ', 122);
            addTrans(74, '=', 141);
            addTrans(141, ' ', 142);
            addTrans(74, ' ', 123);
            addTrans(75, '=', 143);
            addTrans(75, ' ', 124);
            addTrans(143, ' ', 144);
            addTrans(76, '=', 146);
            addTrans(146, ' ', 147);
            addTrans(76, ' ', 125);
            addTrans(77, '=', 78);
            addTrans(78, ' ', 126);
            addTrans(80, ' ', 127);
            addTrans(81, '>', 82);
            addTrans(81, ' ', 128);
            addTrans(82, ' ', 129);
            addTrans(83, ' ', 130);
            addTrans(84, ' ', 131);
            addTrans(85, ' ', 132);
            addTrans(86, ' ', 133);
            addTrans(87, ' ', 134);
            addTrans(90, ' ', 136);
            addTrans(91, ' ', 137);
            addTrans(92, 'l', 93);
            addTrans(93, 's', 94);
            addTrans(94, 'e', 95);
            addTrans(95, ' ', 135);


            addFinal(100, TOKEN.INCLUDE_TOK);
            addFinal(101, TOKEN.IF_TOK);
            addFinal(102, TOKEN.LOOP_ITERATE_WHEN_TOK);
            addFinal(103, TOKEN.IOW_TOK);
            addFinal(104, TOKEN.IOWF_TOK);
            addFinal(105, TOKEN.SINGLEQUOTES_TOK);
            addFinal(106, TOKEN.DOUBLEQUOTES_TOK);
            addFinal(107, TOKEN.SIOW_TOK);
            addFinal(140, TOKEN.SIOWF_TOK);
            addFinal(108, TOKEN.STOP_TOK);
            addFinal(109, TOKEN.LOLI_TOK);
            addFinal(110, TOKEN.LOOP_ITERATE_WHEN_TOK);
            addFinal(111, TOKEN.WORTHLESS_TOK);
            addFinal(112, TOKEN.TURNBACK_TOK);
            addFinal(113, TOKEN.CHLO_TOK);
            addFinal(114, TOKEN.CHAIN_TOK);
            addFinal(115, TOKEN.SEMICOLON_TOK);
            addFinal(116, TOKEN.DIVISION_TOK);
            addFinal(117, TOKEN.MULTIPLE_COMMENT_TOK);
            addFinal(118, TOKEN.SINGLE_COMMENT_TOK);
            addFinal(119, TOKEN.MULTIPLE_COMMENT_TOK);
            addFinal(120, TOKEN.NOT_TOK);
            addFinal(121, TOKEN.OR_TOK);
            addFinal(122, TOKEN.AND_TOK);
            addFinal(123, TOKEN.GREATER_TOK);
            addFinal(124, TOKEN.SMALLER_TOK);
            addFinal(125, TOKEN.ASSIGNMENT_TOK);
            addFinal(126, TOKEN.NOTEQUAL_TOK);
            addFinal(127, TOKEN.MULTIPLY_TOK);
            addFinal(128, TOKEN.SUBTRACT_TOK);
            addFinal(129, TOKEN.ACCESS_TOK);
            addFinal(130, TOKEN.ADD_TOK);
            addFinal(131, TOKEN.LBRACKET_TOK);
            addFinal(132, TOKEN.RBRACKET_TOK);
            addFinal(133, TOKEN.LBRACES_TOK);
            addFinal(134, TOKEN.RBRACES_TOK);
            addFinal(135, TOKEN.ELSE_TOK);
            addFinal(142, TOKEN.GRE_TOK);
            addFinal(144, TOKEN.SME_TOK);
            addFinal(147, TOKEN.EQUALITY_TOK);
        }

        private static int stringLength(string str)
        {
            int counter = 0;
            try
            {
                for (int i = 0; ; i++)
                {
                    char temp = str[i];
                    counter++;
                }
            }
            catch (IndexOutOfRangeException Exception) { }

            return counter;
        }

        public int retrieveErrorCounter()
        {
            return ErrorCounter;

        }

        private static Boolean isConstant(String s)
        {
            Boolean flag = true;
            for (int i = 0; i < stringLength(s); i++)
            {
                if ((s[i] >= 48 && s[i] <= 57) || (s[i] == ' ' && i == stringLength(s) - 1))
                    continue;
                else flag = false;
            }
            return flag;
        }
        private static Boolean isAlpha(char c)
        {
            if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || c == 95)
                return true;
            else
                return false;
        }
        private static Boolean isIdentifier(string s)
        {
            Boolean flag = true;



            if (isAlpha(s[0])) { }
            else return false;



            for (int i = 1; i < stringLength(s); i++)
            {
                if (isAlpha(s[i]) || (s[i] >= 48 && s[i] <= 57) || s[i] == ' ')
                    continue;
                else flag = false;
            }
            return flag;
        }
        public void FSATable(int NumberOfRows)
        {
            NumberOfStates = NumberOfRows;

            transition = new int[NumberOfStates, (ASCIIMAX + 1)];

            for (int s = 0; s < NumberOfStates; s++)
                for (int j = 0; j < ASCIIMAX; j++)
                    transition[s, j] = -1;
        }
        public void addTrans(int currentState, char input, int nextState)
        {
            if (currentState >= 0 && currentState < NumberOfStates &&
            nextState >= 0 && nextState < NumberOfStates &&
            input >= ASCIIMIN && input <= ASCIIMAX)
            {
                transition[currentState, input] = nextState;
            }



        }
        public void addFinal(int state, TOKEN t)
        {
            FinalStates.Add(state, t);
        }
        private bool isFinal(int state)
        {
            if (FinalStates.ContainsKey(state) == true)
                return true;
            else
                return false;
        }
        public TOKEN scan()
        {

            int state = 1;
            Char current = '\0';
            string temp = "";
            ScannedText = removeNewLine(ScannedText);
            while (ptr < stringLength(ScannedText))
            {
                while (current != ' ' && ptr < stringLength(ScannedText))
                {



                    current = ScannedText[ptr];
                    temp += current;
                    ptr++;
                    if (state != -1)
                        state = transition[state, current];

                }

                if (isFinal(state))
                    return FinalStates[state];
                else
                {
                    if (temp != " ")
                    {
                        if (isConstant(temp))
                            return TOKEN.NUMBER_TOK;
                        else if (isIdentifier(temp))
                            return TOKEN.ID_TOK;
                        else
                        {
                            
                            return TOKEN.ERROR_TOK;
                        }

                    }
                    else
                        return TOKEN.IGNORABLE_TOK;

                }

            }
            return TOKEN.IGNORABLE_TOK;
        }

        public String displayTokens()
        {
            TOKEN t;
            String ReturnedTokenType = "";
            String AllReturnedTokens = "";
            
            if (ScannedText == null || stringLength(ScannedText) == 0) return AllReturnedTokens;
            while (ptr < stringLength(ScannedText))
            {
                int StartIndex = ptr;  

                t = scan();

                int LengthOfToken = ptr - StartIndex;

                switch (t)
                {

                    case TOKEN.IF_TOK: ReturnedTokenType = "If Condition"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.ELSE_TOK: ReturnedTokenType = "Else Of If Condition"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.IOW_TOK: ReturnedTokenType = "Integer"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SIOW_TOK: ReturnedTokenType = "SInteger"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.CHLO_TOK: ReturnedTokenType = "Character"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.CHAIN_TOK: ReturnedTokenType = "String"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.IOWF_TOK: ReturnedTokenType = "Float"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SIOWF_TOK: ReturnedTokenType = "SFloat"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.WORTHLESS_TOK: ReturnedTokenType = "Void"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.LOOP_ITERATE_WHEN_TOK: ReturnedTokenType = "Loop"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.TURNBACK_TOK: ReturnedTokenType = "Return"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.STOP_TOK: ReturnedTokenType = "Break"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.LOLI_TOK: ReturnedTokenType = "Struct"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.INCLUDE_TOK: ReturnedTokenType = "Inclusion"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.ID_TOK: ReturnedTokenType = "Identifier"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.ADD_TOK: ReturnedTokenType = "Addition Arithmetic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SUBTRACT_TOK: ReturnedTokenType = "Subtraction Arithmetic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break; 
                    case TOKEN.MULTIPLY_TOK: ReturnedTokenType = "Multiply Arthimitic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.DIVISION_TOK: ReturnedTokenType = "Division Arthimitic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.AND_TOK: ReturnedTokenType = "And Logic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.OR_TOK: ReturnedTokenType = "OR Logic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.NOT_TOK: ReturnedTokenType = "Not Logic Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.EQUALITY_TOK: ReturnedTokenType = "Equality Relational Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SMALLER_TOK: ReturnedTokenType = "Smaller Than Relational Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.GREATER_TOK: ReturnedTokenType = "Greater Than Relational Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.NOTEQUAL_TOK: ReturnedTokenType = "Not Equal Relational Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SME_TOK: ReturnedTokenType = "Smaller Than Or Equal Relational Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.GRE_TOK: ReturnedTokenType = "Greater Than Or Equal Relational Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.ASSIGNMENT_TOK: ReturnedTokenType = "Assignment Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.ACCESS_TOK: ReturnedTokenType = "Access Operator"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.LBRACKET_TOK: ReturnedTokenType = "Left Bracket"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.RBRACKET_TOK: ReturnedTokenType = "Right Bracket"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.LBRACES_TOK: ReturnedTokenType = "Left Braces"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.RBRACES_TOK: ReturnedTokenType = "Right Braces"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.LPRAN_TOK: ReturnedTokenType = "Left Parentheses"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.RPRAN_TOK: ReturnedTokenType = "Right Parentheses"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.NUMBER_TOK: ReturnedTokenType = "Constant"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SINGLEQUOTES_TOK: ReturnedTokenType = "Single Quote"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.DOUBLEQUOTES_TOK: ReturnedTokenType = "Double Quote"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SEMICOLON_TOK: ReturnedTokenType = "Semicolon"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.SINGLE_COMMENT_TOK: ReturnedTokenType = "Single Line Comment"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.MULTIPLE_COMMENT_TOK: ReturnedTokenType = "Multiple Line Comment"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Token Text: " + subString(StartIndex, LengthOfToken) + " Token Type: " + ReturnedTokenType + '\n'; break;
                    case TOKEN.IGNORABLE_TOK: ReturnedTokenType = ""; AllReturnedTokens += ReturnedTokenType; break;
                    case TOKEN.ERROR_TOK: ReturnedTokenType = "ERROR"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Error in Token Text: " + subString(StartIndex, LengthOfToken)+ '\n'; ErrorCounter++; break;
                    default: ReturnedTokenType = "ERROR"; AllReturnedTokens += "Line : " + getLineNum(ptr) + " Error in Token Text: " + subString(StartIndex, LengthOfToken) + '\n'; ErrorCounter++; break;



                }
            }

            return AllReturnedTokens;



        }
    }
}





