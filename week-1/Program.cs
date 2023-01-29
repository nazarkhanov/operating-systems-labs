using System;
using Application.Source.Components.Details;

namespace Application
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var sys = new Source.System();
            
            // init instructions
            sys.processor.define(0x3, new LoadInstruction());
            sys.processor.define(0x5, new AddInstruction());
            sys.processor.define(0x7, new StoreInstruction());
            
            // init memory
            sys.memory.write(0x300, 0x3005);
            sys.memory.write(0x301, 0x5940);
            sys.memory.write(0x302, 0x7006);
            // ...
            sys.memory.write(0x005, 0x0003);
            sys.memory.write(0x940, 0x0002);
            sys.memory.write(0x006, 0x0000);

            // init start and end points
            sys.processor.pc.value = 0x300; // start here
            sys.processor.hr.value = 0x303; // halt here

            // start system
            sys.processor.start();

            // show result of computing - hex number
            var value = sys.memory.read(0x006);
            Console.WriteLine($"Result: 0x{value, 0:X4}");
        }
    }
}
