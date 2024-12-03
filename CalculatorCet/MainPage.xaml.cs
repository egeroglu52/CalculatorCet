using System.Globalization;

namespace CalculatorCet
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private double FirstNumber;

        private bool isFirstNumberAfterOperator = true;

        private Operator PreviousOperator = Operator.None;
        
        private readonly Stack<double> storedValue = new Stack<double>();

        private void SubtractButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Subtract;
        }

        private void MultiplyButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Multiply;
        }

        private void DivideButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Divide;
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.Add;
        }

        void DoCalculation()
        {
            switch (PreviousOperator)
            {
                case Operator.None:
                    FirstNumber = GetNumber();
                    break;
                case Operator.Add:
                    FirstNumber = FirstNumber + GetNumber();
                    break;
                case Operator.Subtract:
                    FirstNumber = FirstNumber - GetNumber();

                    break;
                case Operator.Multiply:
                    FirstNumber = FirstNumber * GetNumber();

                    break;
                case Operator.Divide:
                    FirstNumber = FirstNumber / GetNumber();

                    break;
            }

            Display.Text = string.Empty;

            isFirstNumberAfterOperator = true;
            ResultValue.Text = "Result is " + FirstNumber;        
        }

        private double GetNumber()
        {
            string text = Display.Text;


            if (text.Contains("."))
            {
                double.TryParse(text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result);

                return result;
            }

            return Double.Parse(text);
        }

        private void Digit_Clicked(object sender, EventArgs e)
        {
            Button digitButton = sender as Button;
            if (isFirstNumberAfterOperator)
            {
                Display.Text = digitButton.Text;
                isFirstNumberAfterOperator = false;
            }
            else
            {
                Display.Text += digitButton.Text;
            }
        }

        private void EqualButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.None;
        }

        private void CEButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            isFirstNumberAfterOperator = true;
        }

        private void CButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            FirstNumber = 0;
            PreviousOperator = Operator.None;
            isFirstNumberAfterOperator = true;
        }

        private void MRButton_Clicked(object? sender, EventArgs e)
        {
            double value = storedValue.Pop();
            Display.Text = value.ToString();
            FirstNumber = value;
        }

        private void MSButton_Clicked(object sender, EventArgs e)
        {
            storedValue.Push(GetNumber());
        }

        private void DotButton_Clicked(object sender, EventArgs e)
        {
           Display.Text += ".";
        }

        private void MCButton_Clicked(object sender, EventArgs e)
        {
            storedValue.Clear();
            Display.Text = string.Empty;
            FirstNumber = 0f;
        }
    }
}