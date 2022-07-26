using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Parser
    {



        public String input = "";
        private int indexOfInput = -1;

        Stack<String> stack = new Stack<String>();

        String[][] table =
        {
        

        };
        String[] nonTers = { };
        String[] terminals = { };


        public Parser(String text)
        {
            this.input = text;
        }

        private void pushRule(String rule)
        {
            for (int i = rule.Length - 1; i >= 0; i--)
            {
                char ch = rule[i];
                push(rule);
            }
        }

        public void algorithm()
        {


            push(this.input[0] + "");
            push("G");

            String token = read();
            String top = null;

            do
            {
                top = this.pop();
                if (isNonTerminal(top))
                {
                    String rule = this.getRule(top, token);
                    this.pushRule(rule);
                }
                else if (isTerminal(top))
                {
                    if (!top.Equals(token))
                    {
                        error("this token is not corrent , By Grammer rule . Token : (" + token + ")");
                    }
                    else
                    {

                        token = read();

                    }
                }
                else
                {
                    error("Never Happens , Because top : ( " + top + " )");
                }
                if (token.Equals("$"))
                {
                    break;
                }


            } while (true);

            if (token.Equals("$"))
            {
                System.Console.WriteLine("Input is Accepted by LL1");
            }
            else
            {
                System.Console.WriteLine("Input is not Accepted by LL1");
            }
        }

        private bool isTerminal(String s)
        {
            for (int i = 0; i < this.terminals.Length; i++)
            {
                if (s.Equals(this.terminals[i]))
                {
                    return true;
                }

            }
            return false;
        }

        private bool isNonTerminal(String s)
        {
            for (int i = 0; i < this.nonTers.Length; i++)
            {
                if (s.Equals(this.nonTers[i]))
                {
                    return true;
                }

            }
            return false;
        }

        private String read()
        {
            indexOfInput++;
            char ch = this.input[indexOfInput];
            String str = "";

            return str;
        }

        private void push(String s)
        {
            stack.Push(s);
        }
        private String pop()
        {
           return stack.Pop();
        }

        private void error(String message)
        {
            System.Console.WriteLine(message);

        }
        public String getRule(String non, String term)
        {

            int row = getnonTermIndex(non);
            int column = getTermIndex(term);
            String rule = this.table[row][column];
            if (rule == null)
            {
                error("There is no Rule by this , Non-Terminal(" + non + ") ,Terminal(" + term + ") ");
            }
            return rule;
        }

        private int getnonTermIndex(String non)
        {
            for (int i = 0; i < this.nonTers.Length; i++)
            {
                if (non.Equals(this.nonTers[i]))
                {
                    return i;
                }
            }
            error(non + " is not NonTerminal");
            return -1;
        }

        private int getTermIndex(String term)
        {
            for (int i = 0; i < this.terminals.Length; i++)
            {
                if (term.Equals(this.terminals[i]))
                {
                    return i;
                }
            }
            error(term + " is not Terminal");
            return -1;
        }


        public static void main(String[] args)
        {
            

            Parser parser = new Parser("a+(a+a*(a)+a)$");
            parser.algorithm();

        }

    }
}
