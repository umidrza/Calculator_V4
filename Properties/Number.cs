namespace Calculator_V4
{
    public class Number
    {
        public double Value { get; set; }
        public int Length { get; set; }
        public bool IsDecimal { get; set; }
        public bool IsNegative { get; set; }

        public Number(double value, int length, bool isdecimal, bool isNegative)
        {
            Value = value;
            Length = length;
            IsDecimal = isdecimal;
            IsNegative = isNegative;
        }
    }
}
