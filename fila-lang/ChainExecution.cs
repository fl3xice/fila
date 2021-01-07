using System;
using System.Collections.ObjectModel;

namespace fila_lang
{
    public class ChainExecution
    {
        public Collection<Variable> Variables { get; set; }

        public ChainExecution(Collection<Variable> variables)
        {
            Variables = variables;
        }

        private bool ReassignVariable(Variable variable)
        {
            var reassigned = false;
            foreach (var t in Variables)
            {
                if (t.Name == variable.Name)
                {
                    t.Value = variable.Value;
                    reassigned = true;
                }
            }

            return reassigned;
        }
        
        public void Observe(string code)
        {
            if (SyntaxDetector.IsVariable(code))
            {
                var variable = SyntaxDetector.GetVariable(code);
                if (!ReassignVariable(variable))
                {
                    Variables.Add(variable);
                }
            }
        }
    }
}