using Application.Source.Components.Details;

namespace Application.Source.Components
{
    public class Processor
    {
        private State _state = State.HALTING;
        public readonly Specification specification = new Specification();
        public readonly Registers registers = new Registers();
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
                if (registers.pc == registers.hr) {
                    halt();
                    break;
                } 

                _state = State.FETCHING;
                fetch();
                print();

                _state = State.EXECUTING;
                execute();
                print();

                registers.pc++;
            }
        }

        private void fetch()
        {
            registers.mar = registers.pc;
            registers.mbr = bus.read();
            registers.ir = registers.mbr;
        }

        private void execute()
        {
            registers.mar = registers.ir & ~(registers.ir >> 12 << 12);
            var instruction = specification.identify(registers.ir >> 12);
            instruction?.execute(this);
        }

        public void halt()
        {
            _state = State.HALTING;
        }

        private static int step = 0;

        private void print() {
            Console.WriteLine($"\n{++step}. {_state} \n");
            Console.WriteLine("Memory:" + new string(' ', 28) + "CPU Registers");
            Console.WriteLine(new string('-', 50));

            var lines = bus.lines();
            var values = new Dictionary<int, string>
            {
                {0, $"PC: 0x{registers.pc, 0:X3}"},
                {1, $"AC: 0x{registers.ac, 0:X4}"},
                {2, $"IR: 0x{registers.ir, 0:X4}"},
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