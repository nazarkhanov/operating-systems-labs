using System.Collections.Generic;

namespace Application.Source.Components
{
    public class Memory
    {
        private readonly Dictionary<int, int> _cells = new Dictionary<int, int>();
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

        public Dictionary<int, int> cells => _cells;
    }
}