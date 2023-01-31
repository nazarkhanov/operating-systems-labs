namespace Application.Source.Components.Details
{
    public class Registers
    {
        public int pc = 0x0000; // program counter register
        public int hr = 0x0000; // halt register 
        public int ir = 0x0000; // instruction register
        public int mar = 0x0000; // memory address register
        public int mbr = 0x0000; // memory buffer register
        public int ac = 0x0000; // accumulator register
    }
}