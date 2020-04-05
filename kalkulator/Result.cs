using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace kalkulator
{
    class Result
    {
        bool canWriteDot=true;
        TextBox result_tb;
        public TextBox Result_tb
        {
            get { return result_tb; }
            set { result_tb = value; }
        }

        public Result(Grid grid)
        {
            result_tb = new TextBox();
            Style();
            
            grid.Children.Add(result_tb);
        }

        void Style()
        {
            result_tb.Margin = new Thickness(5, 10, 5, 10);
            result_tb.SetValue(Grid.RowProperty, 0);
            result_tb.FontSize = 20;
            result_tb.IsReadOnly = true;
            result_tb.TextWrapping = System.Windows.TextWrapping.Wrap;
        }

        public void Write(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            result_tb.BorderBrush = new SolidColorBrush(Colors.SlateGray);
            char symbol = Convert.ToChar(button.Content.ToString());

            if (result_tb.Text == '∞'.ToString() || result_tb.Text == "NaN" || result_tb.Text == "Podano zbyt dużą liczbę")
                result_tb.Clear();

            int contentLenght = result_tb.Text.Length;
            char previousSymbol = ' ';
            if (contentLenght != 0)
                previousSymbol = result_tb.Text[contentLenght - 1];
            else if (result_tb.Text.Contains(","))
                canWriteDot = false;
            else
                canWriteDot = true;
            bool canWriteSymbol = contentLenght > 0 && previousSymbol >= '0' && previousSymbol <= '9';

            if (symbol >= '0' && symbol <= '9')
                result_tb.Text += symbol.ToString();
            else
                switch (symbol)
                {
                    case '-':
                        if (contentLenght == 0 || result_tb.Text[contentLenght - 1] != '-')
                        {
                            result_tb.Text += symbol.ToString();
                            canWriteDot = true;
                        }
                        break;
                    case '±':
                        if (canWriteSymbol)
                        {
                            string result = Calculator.GetResult(result_tb.Text);
                            if (result[0] == '-')
                                result_tb.Text = result.Remove(0, 1);
                            else
                                result_tb.Text = '-' + result;
                        }
                        else
                            result_tb.BorderBrush = new SolidColorBrush(Colors.Red);
                        break;
                    case '<':
                        if (contentLenght > 0)
                            result_tb.Text = result_tb.Text.Substring(0, contentLenght - 1);
                        break;
                    case 'C':
                        result_tb.Clear();
                        canWriteDot = true;
                        break;
                    case '√':
                        if (contentLenght == 0 || result_tb.Text[contentLenght - 1] != '√' && (previousSymbol < '0' || previousSymbol > '9'))
                            result_tb.Text += symbol.ToString();
                        break;
                    case '=':
                        if (canWriteSymbol)
                            result_tb.Text = Calculator.GetResult(result_tb.Text);
                        else
                            result_tb.BorderBrush = new SolidColorBrush(Colors.Red);
                        break;
                    case '.':
                        if (canWriteSymbol && canWriteDot)
                        {
                            result_tb.Text += symbol.ToString();
                            canWriteDot = false;
                        }
                        break;
                    default:
                        if (canWriteSymbol)
                        {
                            result_tb.Text += symbol.ToString();
                            canWriteDot = true;
                        }
                        break;
                }
            result_tb.ScrollToEnd();
        }

        public void Clear()
        {
            result_tb.Text = "";
        }
        
    }
}
