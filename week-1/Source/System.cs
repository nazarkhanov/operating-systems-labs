using Application.Source.Components;

namespace Application.Source
{
    public class System
    {
        public readonly Processor processor;
        public readonly Memory memory;
        private readonly Bus _bus = new Bus();

        public System()
        {
            processor = new Processor(_bus);
            memory = new Memory(_bus);
            _bus.attach(processor, memory);
        }
    }
}