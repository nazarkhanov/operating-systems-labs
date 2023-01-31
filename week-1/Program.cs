using System;
using Application.Source.Components.Details;

namespace Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sys = new Source.System();
            
            // register instructions
            sys.processor.specification.define(0x3, new LoadInstruction());
            sys.processor.specification.define(0x5, new AddInstruction());
            sys.processor.specification.define(0x7, new StoreInstruction());
            
            // initialize memory
            sys.memory.write(0x300, 0x3005);
            sys.memory.write(0x301, 0x5940);
            sys.memory.write(0x302, 0x7006);
            // ...
            sys.memory.write(0x005, 0x0003);
            sys.memory.write(0x940, 0x0002);
            sys.memory.write(0x006, 0x0000);

            // initialize registers
            sys.processor.registers.pc = 0x300; // start point
            sys.processor.registers.hr = 0x303; // halt point

            // start system
            sys.processor.start();

            // show result of computing - hex number
            var value = sys.memory.read(0x006);
            Console.WriteLine($"\n Result: 0x{value, 0:X4}");
        }
    }
}
