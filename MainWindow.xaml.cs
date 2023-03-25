using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string number1 = "";
        string number2 = "";
        string operation = "";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void HandleNumberInput(string number)
        {
            if (operation == "")
            {
                number1 += number;
                display.Text = number1;
            }
            else
            {
                number2 += number;
                display.Text = number2;
            }
        }
        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            string strKey = new KeyConverter().ConvertToString(e.Key);
            // Converted to string can be named NumPadN or N where N is a digit
            char lastChar = strKey[strKey.Length - 1];
            if (char.IsDigit(lastChar))
            {
                HandleNumberInput(lastChar.ToString());
            }
            if (strKey == "OemPeriod" || strKey == "OemComma" || strKey == "Decimal")
            {
                AddDecimal();
            }

        }
        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            string buttonValue = ((Button)sender).Content.ToString();
            HandleNumberInput(buttonValue);
        }

        private void OperationButtonClick(object sender, RoutedEventArgs e)
        {
            operation = ((Button)sender).Content.ToString();
            display.Text = "";
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (number1 != "" && number2 != "")
            {
                double result = new double();
                double num1 = new double();
                double num2 = new double();

                bool num1IsValid = double.TryParse(number1, NumberStyles.Any, CultureInfo.InvariantCulture, out num1);
                bool num2IsValid = double.TryParse(number2, NumberStyles.Any, CultureInfo.InvariantCulture, out num2);

                if (num1IsValid && num2IsValid)
                {
                    switch (operation)
                    {
                        case "+":
                            result = num1 + num2;
                            break;
                        case "-":
                            result = num1 - num2;
                            break;
                        case "x":
                            result = num1 * num2;
                            break;
                        case "/":
                            if (num2 != 0)
                            {
                                result = num1 / num2;
                            }
                            else
                            {

                            }
                            break;
                        default:
                            break;
                    }
                    number1 = result.ToString(CultureInfo.InvariantCulture);
                    number2 = "";
                    operation = "";
                    display.Text = number1;
                }
                else if (!num1IsValid)
                {
                    MessageBox.Show($"Invalid number: {number1}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!num2IsValid)
                {
                    MessageBox.Show($"Invalid number: {number2}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 = "";
            }
            else
            {
                number2 = "";
            }
            display.Text = "0";
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            number1 = "";
            number2 = "";
            operation = "";
            display.Text = "0";
        }
        private string DeleteLastChar(string s)
        {
            return s.Length > 0 ? s.Remove(s.Length - 1, 1) : s;
        }
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 = DeleteLastChar(number1);
                display.Text = number1;
            }
            else
            {
                number2 = DeleteLastChar(number2);
                display.Text = number2;
            }
        }
        private string ToggleNegation(string s)
        {
            // Write 1 if the current string is empty
            if (s == "")
            {
                s = "1";
            }
            return s.StartsWith("-") ? s.Remove(0, 1) : "-" + s;
        }
        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                number1 = ToggleNegation(number1);
                display.Text = number1;
            }
            else
            {
                number2 = ToggleNegation(number2);
                display.Text = number2;
            }
        }
        private void AddDecimal()
        {
            string decimalSeparator = ".";
            if (operation == "")
            {
                if (!number1.Contains(decimalSeparator))
                {
                    number1 += decimalSeparator;
                }
                display.Text = number1;
            }
            else
            {
                if (!number2.Contains(decimalSeparator))
                {
                    number2 += decimalSeparator;
                }
                display.Text = number2;
            }
        }
        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            AddDecimal();
        }
    }
}
