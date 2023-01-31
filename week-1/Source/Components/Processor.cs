using Application.Source.Components.Details;

namespace Application.Source.Components
{
    public class Processor
    {
        private State _state = State.HALTING;
        public readonly Instructions instructions = new Instructions();
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
                if (registers.pc.value == registers.hr.value) {
                    _state = State.HALTING;
                    break;
                } 

                _state = State.FETCHING;
                fetch();
                print();

                _state = State.EXECUTING;
                execute();
                print();

                registers.pc.value++;
            }
        }

        private void fetch()
        {
            registers.mar.value = registers.pc.value;
            registers.mbr.value = bus.read();
            registers.ir.value = registers.mbr.value;
        }

        private void execute()
        {
            registers.mar.value = registers.ir.value & ~(registers.ir.value >> 12 << 12);
            var i = instructions.identify(registers.ir.value >> 12);
            i?.execute(this);
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
                {0, $"PC: 0x{registers.pc.value, 0:X3}"},
                {1, $"AC: 0x{registers.ac.value, 0:X4}"},
                {2, $"IR: 0x{registers.ir.value, 0:X4}"},
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