using System.Collections.Generic;

namespace Application.Source.Components.Details
{
    using Specification = Dictionary<int, Instruction>;

    public interface Instruction
    {
        void execute(Processor processor);
    } 
    
    public class Instructions
    {
        private Specification spec = new Specification();

        public void define(int id, Instruction instruction)
        {
            spec[id] = instruction;
        }

        public Instruction identify(int id)
        {
            return spec[id];
        }
    }
    
    public class LoadInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.mbr.value = processor.bus.read();
            processor.ac.value = processor.mbr.value;
        } 
    }
    
    public class AddInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.mbr.value = processor.bus.read();
            processor.ac.value += processor.mbr.value;
        } 
    }

    public class StoreInstruction : Instruction {
        public void execute(Processor processor)
        {
            processor.mbr.value = processor.ac.value;
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