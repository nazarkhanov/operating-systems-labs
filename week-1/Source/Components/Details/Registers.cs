namespace Application.Source.Components.Details
{
    public class Register
    {
        private int _value = 0;
        
        public int value
        {
            get => _value;
            set => _value = value;
        }
    }

    public class Registers
    {
        public readonly Register pc = new Register(); // program counter register
        public readonly Register hr = new Register(); // halt register 
        public readonly Register ir = new Register(); // instruction register
        public readonly Register mar = new Register(); // memory address register
        public readonly Register mbr = new Register(); // memory buffer register
        public readonly Register ac = new Register(); // accumulator register
    }
}