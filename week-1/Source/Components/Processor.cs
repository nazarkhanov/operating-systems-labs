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
            RUNNING,
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
                _state = State.RUNNING;
                execute();

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

        public void define(int address, Instruction instruction)
        {
            _instructions.define(address, instruction);
        }
    }
}