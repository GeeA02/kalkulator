using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace kalkulator
{
    class Buttons
    {
        Button[] buttons = new Button[20];
        Result result_tb;

        public Buttons(Grid grid, Result result)
        {
            result_tb = result;
            for(int i = 0; i < 20; i++)
            {
                buttons[i] = new Button();
                buttons[i].SetValue(Grid.ColumnProperty, i % 4);
                buttons[i].SetValue(Grid.RowProperty, i / 4);
                buttons[i].SetValue(Control.FontSizeProperty, 15.0);
                buttons[i].Click += result_tb.Write;

                grid.Children.Add(buttons[i]);
            }
            MakeSymbols();
        }

        void MakeSymbols()
        {
            char[] symbols = new char[] { '√', '*', '/', 'C', '-', '+', '<', '±', '0', '.', '=' };
            int s = -1;
            int number = 1;
            for(int i=0; i <20; i++)
            {
                if (i / 4 > 0 && i % 4 != 3 && i < 16)
                {
                    buttons[i].Content = number++;
                    buttons[i].SetValue(Button.BackgroundProperty, Brushes.WhiteSmoke);
                }
                else
                {
                    buttons[i].Content = symbols[++s];
                    if (s + 1 != 9)
                        buttons[i].SetValue(Button.BackgroundProperty, Brushes.Bisque);
                    else
                        buttons[i].SetValue(Button.BackgroundProperty, Brushes.WhiteSmoke);
                }
                buttons[i].SetValue(Button.BorderBrushProperty, Brushes.AntiqueWhite);
            }
            for(int i = 4; i < 8; i++)
            {
                var tmp = buttons[i].Content;
                buttons[i].Content= buttons[i + 8].Content;
                buttons[i+8].Content=tmp;
            }

            for(int i = 0; i < 20; i++)
            {
                SetTooltip(buttons[i]);
            }
        }


        void SetTooltip(Button button)
        {
            var symbol = button.Content;
            String tooltip;
            switch (symbol)
            {
                case '+':
                    tooltip = "Operator dodawania liczb";
                    break;
                case '-':
                    tooltip = "Operator odejmowania liczb";
                    break;
                case '√':
                    tooltip = "Operator pierwiastkowania liczb \n Pierwiastkuje liczbę wpisaną po znaku ";
                    break;
                case '*':
                    tooltip = "Operator mnożenia liczb";
                    break;
                case '/':
                    tooltip = "Operator dzielenia liczb";
                    break;
                case '±':
                    tooltip = "Operator zmiany znaku wpisanej liczby";
                    break;
                case '<':
                    tooltip = "Usunięcie ostatniego znaku";
                    break;
                case 'C':
                    tooltip = "Usunięcie wpisanych znaków";
                    break;
                case '=':
                    tooltip = "Wynik";
                    break;
                case '.':
                    tooltip = "Przecinek";
                    break;
                default:
                    tooltip = $"Liczba {symbol}";
                    break;
            }

            button.ToolTip = tooltip;
        }

       

    }
}