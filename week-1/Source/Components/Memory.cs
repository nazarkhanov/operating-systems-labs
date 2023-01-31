using System.Collections.Generic;

namespace Application.Source.Components
{
    using Cells = Dictionary<int, int>;
    
    public class Memory
    {
        private readonly Cells _cells = new Cells();
        private readonly Bus _bus;

        public Memory(Bus bus)
        {
            _bus = bus;
        }

        public int read(int address)
        {
            return _cells[address];
        }

        public void write(int address, int value)
        {
            _cells[address] = value & ~(value >> 16 << 16);
        }
        
        public int read()
        {
            var address = _bus.address();
            return _cells[address];
        }

        public void write()
        {
            var address = _bus.address();
            var value = _bus.value();
            _cells[address] = value & ~(value >> 16 << 16);
        }

        public Cells cells => _cells;
    }
}