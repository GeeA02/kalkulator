using System;
using System.Collections.Generic;


namespace kalkulator
{
    class Calculator
    {
        static int Piorytet(char z)
        {
            switch (z)
            {
                case '+':
                case '-': return 1;
                case '*':
                case '/': return 2;
                case '^': 
                case '√': return 3;
                default: return -1;
            }

        }

        static string ToOnp(string r)
        {
            char[] rownanie = r.ToCharArray();
            Stack<char> znaki = new Stack<char>();
            Stack<char> noweRownanie = new Stack<char>();
            for (int i=0;i<rownanie.Length;i++)
            {
                if (rownanie[i] >= '0' && rownanie[i] <= '9')
                    noweRownanie.Push(rownanie[i]);
                else if (rownanie[i] == '.')
                {
                    noweRownanie.Push(' ');
                    noweRownanie.Push(rownanie[i]);
                    noweRownanie.Push(' ');
                }
                else if (rownanie[i] == '-' & (i == 0 || (rownanie[i - 1] < '0' || rownanie[i - 1] > '9')))
                        noweRownanie.Push(rownanie[i]);
                else
                {
                    if (znaki.Count == 0)
                        znaki.Push(rownanie[i]);
                    else if (Piorytet(znaki.Peek()) > Piorytet(rownanie[i]))
                    {
                        while (znaki.Count != 0 && Piorytet(znaki.Peek()) > Piorytet(rownanie[i]))
                        {
                            noweRownanie.Push(' ');
                            noweRownanie.Push(znaki.Pop());
                        }
                        znaki.Push(rownanie[i]);
                    }
                    else
                    {
                        if (rownanie[i - 1] < '0' || rownanie[i - 1] > '9')
                            noweRownanie.Push(' ');
                        znaki.Push(rownanie[i]);
                    }

                    if (rownanie[i] != '√' && rownanie[i + 1] != '√')
                        noweRownanie.Push(' ');
                }
            }
            noweRownanie.Push(' ');
            while (znaki.Count != 0)
            {
                noweRownanie.Push(znaki.Pop());
                noweRownanie.Push(' ');
            }

            String o = "";
            foreach (var i in noweRownanie)
            {
                if (Piorytet(i) != 0)
                    o = String.Join("", i, o);
            }
            return o;
        }


        public static string GetResult(string rownanie)
        {
            string input = ToOnp(rownanie);
            Stack<decimal> st = new Stack<decimal>();
            string wynik = "Podano zbyt dużą liczbę";
            try { 
                int i = 0;
                while (i < input.Length)
                {
                    if ((input[i] >= '0' && input[i] <= '9') || ((i == 0 || (i != input.Length - 1 && input[i + 1] != ' ')) && input[i] == '-'))
                    {
                        bool minus = false;
                        if (input[i] == '-')
                        {
                            minus = true;
                            i++;
                        }
                        decimal a = 0;
                        while (input[i] >= '0' && input[i] <= '9')
                        {
                            a *= 10;
                            a += (input[i] - '0');
                            i++;
                        }
                        if (minus)
                            a *= -1;
                        st.Push(a);
                    }
                    else if (input[i] == '.')
                    {
                        i += 2;
                        decimal a = 0;
                        int j = 0;
                        while (input[i] >= '0' && input[i] <= '9')
                        {
                            a *= 10;
                            a += (input[i] - '0');
                            j++;
                            i++;
                        }
                        if (st.Peek() < 0)
                            a *= (-1);
                        a = a / (decimal)Math.Pow(10, j);
                        a += st.Pop();
                        st.Push(a);
                    }
                    else
                    {
                        try
                        {
                            decimal t1 = st.Pop(), t2;
                            switch (input[i])
                            {
                                case '+':
                                    t2 = st.Pop();
                                    st.Push(t2 + t1);
                                    break;
                                case '-':
                                    t2 = st.Pop();
                                    st.Push(t2 - t1);
                                    break;
                                case '*':
                                    t2 = st.Pop();
                                    st.Push(t2 * t1);
                                    break;
                                case '/':
                                    t2 = st.Pop();
                                    st.Push(t2 / t1);
                                    break;
                                case '√':
                                    st.Push((decimal)Math.Sqrt((double)t1));
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e} First exception caught.");
                        }
                        i++;
                    }
                    i++;
                }
            wynik = st.Pop().ToString();
            }
            catch(Exception e)
            {
                
            }
            return wynik;
        }
    }
}
