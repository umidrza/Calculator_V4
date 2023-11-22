using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator_V4
{
    public partial class MainWindow : Window
    {
        Calculator calculator;
        int numCount, textCount;

        public MainWindow()
        {
            InitializeComponent();
            Reset();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad0:
                    Number0_Button(sender, e);
                    break;
                case Key.NumPad1:
                    Number1_Button(sender, e);
                    break;
                case Key.NumPad2:
                    Number2_Button(sender, e);
                    break;
                case Key.NumPad3:
                    Number3_Button(sender, e);
                    break;
                case Key.NumPad4:
                    Number4_Button(sender, e);
                    break;
                case Key.NumPad5:
                    Number5_Button(sender, e);
                    break;
                case Key.NumPad6:
                    Number6_Button(sender, e);
                    break;
                case Key.NumPad7:
                    Number7_Button(sender, e);
                    break;
                case Key.NumPad8:
                    Number8_Button(sender, e);
                    break;
                case Key.NumPad9:
                    Number9_Button(sender, e);
                    break;
                case Key.Decimal:
                    Decimal_Button(sender, e);
                    break;
                case Key.Add:
                    Plus_Button(sender, e);
                    break;
                case Key.Subtract:
                    Minus_Button(sender, e);
                    break;
                case Key.Multiply:
                    Multiply_Button(sender, e);
                    break;
                case Key.Divide:
                    Divide_Button(sender, e);
                    break;
                case Key.Return:
                    Equal_Button(sender, e);
                    break;
                case Key.Back:
                    BackSpace_Button(sender, e);
                    break;
                case Key.Delete:
                    Reset_Button(sender, e);
                    break;
            }
        }

        private void AddNum(string num)
        {
            AddNumToLabel(num);
            calculator.UpdateNumber(lblMain.Text.Substring(textCount), numCount);
            SetEqualLabel();
        }

        private void AddNumToLabel(string num)
        {
            switch (lblMain.Text)
            {
                case "0":
                    lblMain.Text = num;
                    break;
                case "-0":
                    lblMain.Text = "-" + num;
                    break;
                default:
                    lblMain.Text += num;
                    break;
            }
        }

        #region Number Buttons
        private void Number0_Button(object sender, RoutedEventArgs e)
        {
            AddNum("0");
        }
        private void Number1_Button(object sender, RoutedEventArgs e)
        {
            AddNum("1");
        }
        private void Number2_Button(object sender, RoutedEventArgs e)
        {
            AddNum("2");
        }
        private void Number3_Button(object sender, RoutedEventArgs e)
        {
            AddNum("3");
        }
        private void Number4_Button(object sender, RoutedEventArgs e)
        {
            AddNum("4");
        }
        private void Number5_Button(object sender, RoutedEventArgs e)
        {
            AddNum("5");
        }
        private void Number6_Button(object sender, RoutedEventArgs e)
        {
            AddNum("6");
        }
        private void Number7_Button(object sender, RoutedEventArgs e)
        {
            AddNum("7");
        }
        private void Number8_Button(object sender, RoutedEventArgs e)
        {
            AddNum("8");
        }
        private void Number9_Button(object sender, RoutedEventArgs e)
        {
            AddNum("9");
        }

        #endregion

        private void AddOperator(char oper)
        {
            AddOperToLabel(oper);
            calculator.AddOper(oper);
        }

        private void AddOperToLabel(char oper)
        {
            if (calculator.CanAddOper())
            {
                lblMain.Text += oper;
                textCount = lblMain.Text.Length;
                numCount++;
            }
            else
            {
                UpdateOper(oper);
            }
        }

        private void UpdateOper(char oper)
        {
            lblMain.Text = lblMain.Text.Remove(lblMain.Text.Length - 1);
            lblMain.Text += oper;
        }

        private void Plus_Button(object sender, RoutedEventArgs e)
        {
            AddOperator('+');
        }
        private void Minus_Button(object sender, RoutedEventArgs e)
        {
            AddOperator('-');
        }
        private void Divide_Button(object sender, RoutedEventArgs e)
        {
            AddOperator('÷');
        }
        private void Multiply_Button(object sender, RoutedEventArgs e)
        {
            AddOperator('×');
        }
        private void Equal_Button(object sender, RoutedEventArgs e)
        {
            lblEqual.Text = lblMain.Text + "=";
            lblMain.Text = calculator.Result.ToString();

            Reset();
            calculator.UpdateNumber(lblMain.Text, 0);
        }

        private void Decimal_Button(object sender, RoutedEventArgs e)
        {
            if (numCount < calculator.Numbers.Count)
            {
                if (!calculator.Numbers[numCount].IsDecimal)
                {
                    lblMain.Text += ".";
                    calculator.Numbers[numCount].IsDecimal = true;
                }
            }
        }

        private void Percent_Button(object sender, RoutedEventArgs e)
        {
            if (numCount < calculator.Numbers.Count)
            {
                double num = calculator.Numbers[numCount].Value /= 100;
                int numLength = calculator.Numbers[numCount].Length;
                lblMain.Text = lblMain.Text.Remove(textCount, numLength);
                lblMain.Text += num.ToString();
                calculator.UpdateNumber(num.ToString(), numCount);
                SetEqualLabel();
            }
        }
        private void BackSpace_Button(object sender, RoutedEventArgs e)
        {
            RemoveLabelText();
        }

        private void RemoveLabelText()
        {
            if (lblMain.Text.Length <= 1)
            {
                Reset();
                ResetLabels();
            }
            else
            {
                string removedChar = lblMain.Text.Remove(0, lblMain.Text.Length - 1);
                lblMain.Text = lblMain.Text.Remove(lblMain.Text.Length - 1);

                if (IsNum(removedChar) || removedChar == ".")
                {
                    string nextNum = lblMain.Text.Substring(textCount);

                    if (!IsNum(nextNum))
                    {
                        calculator.RemoveLastNum();
                        calculator.RemoveLastResult();
                    }
                    else
                    {
                        calculator.UpdateNumber(nextNum, numCount);
                    }

                    SetEqualLabel();
                }
                else
                {
                    calculator.RemoveLastOper();
                    numCount--;
                    SetTextCount();
                }
            }
        }

        private void SetTextCount()
        {
            textCount = 0;
            for (int i = 0; i < calculator.Numbers.Count - 1; i++)
            {
                textCount += calculator.Numbers[i].Length + 1;
            }
        }

        private bool IsNum(string c)
        {
            return double.TryParse(c, out double num);
        }

        private void Reset_Button(object sender, RoutedEventArgs e)
        {
            Reset();
            ResetLabels();
        }

        private void Reset()
        {
            calculator = new Calculator();
            numCount = 0;
            textCount = 0;
        }

        private void ResetLabels()
        {
            lblEqual.Text = "";
            lblMain.Text = "0";
        }

        private void lblMain_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdjustLabel();
        }

        private void SetEqualLabel()
        {
            lblEqual.Text = "= " + calculator.Result.ToString();
        }

        void AdjustLabel()
        {
            if (lblMain.Text.Length == 1)
            {
                lblMain.FontSize = 50;
            }
            if (lblMain.Text.Length == 9)
            {
                lblMain.FontSize = 32;
            }
            else if (lblMain.Text.Length == 16)
            {
                lblMain.FontSize = 20;
            }
        }
    }
}
