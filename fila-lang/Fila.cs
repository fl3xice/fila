using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;

namespace fila_lang
{
    public class Fila
    {
        public Fila()
        {
            var fileSource = File.ReadAllText("./source.fila", Encoding.UTF8);

            var chainExecution = new ChainExecution(new Collection<Variable>());

            foreach (var code in fileSource.Split(SyntaxTree.Delimiter))
            {
                chainExecution.Observe(code);
            }
            
            foreach (var t in chainExecution.Variables)
            {
                Console.WriteLine(t.Name + " = " + t.Value);
            }
        }
        
        
    }
}