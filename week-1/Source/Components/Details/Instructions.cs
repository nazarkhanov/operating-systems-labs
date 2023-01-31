namespace Application.Source.Components.Details
{
    public class Specification : Dictionary<int, Instruction>
    {
        public void define(int id, Instruction instruction)
        {
            this[id] = instruction;
        }

        public Instruction identify(int id)
        {
            return this[id];
        }
    };

    public interface Instruction
    {
        void execute(Processor processor);
    }

    public class LoadInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.registers.mbr = processor.bus.read();
            processor.registers.ac = processor.registers.mbr;
        } 
    }
    
    public class AddInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.registers.mbr = processor.bus.read();
            processor.registers.ac += processor.registers.mbr;
        } 
    }

    public class StoreInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.registers.mbr = processor.registers.ac;
            processor.bus.write();
        } 
    }

    public class HaltInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.halt();
        } 
    }
}