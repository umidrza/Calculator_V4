using System.Collections.Generic;

namespace Calculator_V4
{
    public class Calculator
    {
        public List<Number> Numbers { get; private set; }
        public List<Operator> Operators { get; private set; } 
        public List<double> Results { get; private set; }
        public double Result { get; set; }

        public Calculator()
        {
            Numbers = new List<Number>() { new Number (0, 1, false, false) };
            Operators = new List<Operator> { new Operator('+') };
            Results = new List<double>() { 0 } ;
        }

        #region Number Functions
        public Number NewNumber(string num)
        {
            if (double.TryParse(num, out double number))
                return new Number(number, num.Length, num.Contains("."), num.Contains("-"));
            else
                return null;
        }

        public void UpdateNumber(string num, int i)
        {
            Number newNumber = NewNumber(num);
            if (newNumber == null) return;

            if (IsNumChanged(i))
            {
                EditNumber(newNumber, i);
                EditResult(i);
            }
            else
            {
                Numbers.Add(newNumber);
            }

            Calculate(i);
        }

        private void EditNumber(Number number, int i)
        {
            Numbers[i] = number;
        }

        public void RemoveLastNum()
        {
            Numbers.RemoveAt(Numbers.Count - 1);
        }

        public void EditResult(int i)
        {
            Results.Remove(Result);
            Result = i == 0 ? 0 : Results[i - 1];
        }

        public void RemoveLastResult()
        {
            Results.RemoveAt(Results.Count - 1);
            Result = Results[Results.Count - 1];
        }

        private bool IsNumChanged(int i)
        {
            return Numbers.Count - 1 == i;
        }

        #endregion

        #region Operator Functions

        public void AddOper(char nextOper)
        {
            if (CanAddOper())
            {
                Operators.Add(new Operator(nextOper));
            }
            else
            {
                RemoveLastOper();
                AddOper(nextOper);
            }
        }

        public void RemoveLastOper()
        {
            Operators.RemoveAt(Operators.Count - 1);
        }

        public bool CanAddOper()
        {
            return Operators.Count == Numbers.Count;
        }

        #endregion

        public void Calculate(int i)
        {
            switch (Operators[i].OperType)
            {
                case '+':
                    Result += Numbers[i].Value;
                    break;
                case '-': 
                    Result -= Numbers[i].Value;
                    break;
                case '×':
                    if (Operators[i - 1].OperType == '-')
                        Result = Results[i - 2] - (Numbers[i - 1].Value * Numbers[i].Value);
                    else if (Operators[i - 1].OperType == '+' && i != 1)
                        Result = Results[i - 2] + (Numbers[i - 1].Value * Numbers[i].Value);
                    else 
                        Result *= Numbers[i].Value;

                    break;
                case '÷':
                    if (Operators[i - 1].OperType == '-')
                        Result = Results[i - 2] - (Numbers[i - 1].Value / Numbers[i].Value);
                    else if (Operators[i - 1].OperType == '+' && i != 1)
                        Result = Results[i - 2] + (Numbers[i - 1].Value / Numbers[i].Value);
                    else
                        Result /= Numbers[i].Value;
                    break;
                default:
                    break;

            }

            Results.Add(Result);
        }
    }
}
