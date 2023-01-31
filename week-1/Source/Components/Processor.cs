using Application.Source.Components.Details;

namespace Application.Source.Components
{
    public class Processor
    {
        private State _state = State.HALTING;
        private readonly Instructions _instructions = new Instructions();
        
        public readonly Register pc = new Register(); // program counter register
        public readonly Register hr = new Register(); // halt register 
        public readonly Register ir = new Register(); // instruction register
        public readonly Register mar = new Register(); // memory address register
        public readonly Register mbr = new Register(); // memory buffer register
        public readonly Register ac = new Register(); // accumulator register
 
        public readonly Bus bus;

        public Processor(Bus _bus)
        {
            bus = _bus;
        }
        
        private enum State
        {
            STARTING,
            FETCHING,
            EXECUTING,
            HALTING,
        }

        public void start()
        {
            if (bus == null) return;
            _state = State.STARTING;

            while (_state != State.HALTING)
            {
                if (pc.value == hr.value) {
                    _state = State.HALTING;
                    break;
                } 

                _state = State.FETCHING;
                fetch();
                print();

                _state = State.EXECUTING;
                execute();
                print();

                pc.value++;
            }
        }

        private void fetch()
        {
            mar.value = pc.value;
            mbr.value = bus.read();
            ir.value = mbr.value;
        }

        private void execute()
        {
            mar.value = ir.value & ~(ir.value >> 12 << 12);
            var i = _instructions.identify(ir.value >> 12);
            i?.execute(this);
        }

        public void halt()
        {
            _state = State.HALTING;
        }

        public void define(int id, Instruction instruction)
        {
            _instructions.define(id, instruction);
        }

        private static int step = 0;

        private void print() {
            Console.WriteLine($"\n{++step}. {_state} \n");
            Console.WriteLine("Memory:" + new string(' ', 28) + "CPU Registers");
            Console.WriteLine(new string('-', 50));

            var lines = bus.lines();
            var values = new Dictionary<int, string>
            {
                {0, $"PC: 0x{pc.value, 0:X3}"},
                {1, $"AC: 0x{ac.value, 0:X4}"},
                {2, $"IR: 0x{ir.value, 0:X4}"},
            };

            for (var i = 0; i < Math.Max(lines.Count, values.Count); i++)
            {
                var value1 = i <= lines.Count - 1 ? lines[i] : "";
                var value2 = i <= values.Count - 1 ? values[i] : "";
                Console.Write($"{value1}{new string(' ', 6)}{value2}\n");
            }
        }
    }
}