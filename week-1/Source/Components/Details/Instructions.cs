using System.Collections.Generic;

namespace Application.Source.Components.Details
{
    public interface Instruction
    {
        void execute(Processor processor);
    } 
    
    public class Instructions
    {
        private Dictionary<int, Instruction> map = new Dictionary<int, Instruction>();

        public void define(int id, Instruction instruction)
        {
            map[id] = instruction;
        }

        public Instruction identify(int id)
        {
            return map[id];
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