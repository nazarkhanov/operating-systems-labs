namespace Application.Source.Components
{
    public class Bus
    {
        private Processor _processor;
        private Memory _memory;

        public void attach(Processor processor, Memory memory)
        {
            _processor = processor;
            _memory = memory;
        }

        public int read()
        {
            return _memory.read();
        }

        public void write()
        {
            _memory.write();
        }
        
        public int address()
        {
            return _processor.registers.mar;
        }
        
        public int value()
        {
            return _processor.registers.mbr;
        }

        public List<string> lines()
        {
            var lines = new List<string>();

            foreach (var cell in _memory.cells)
                lines.Add($"Address: 0x{cell.Key, 0:X3}, Value: 0x{cell.Value, 0:X4}");

            return lines;
        }
    }
}