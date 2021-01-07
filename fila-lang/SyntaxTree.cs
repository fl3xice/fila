using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace fila_lang
{
    public static class SyntaxTree
    {
        public const char Appropriation = '=';
        public const char Addition = '+';
        public const char More = '>';
        public const char Less = '<';
        public const char Delimiter = ';';
        public const string Equality = "==";
        public const string EqualityMore = "=>";
        public const string EqualityLess = "<=";
        public const string CreateFunction = "()";
        public const string CreateIf = "if";
        public const string CreateCycle = "cycle";
        public const string CreateSwitch = "switch";
    }

    // For next uses
    public enum Type
    {
        Number,
        String,
        Char,
        Array,
        Null,
        Void
    }

    public enum MagicMeanings
    {
        Variable
    }
    
    public static class SyntaxDetector
    {
        private static readonly Regex VariableDetect = new Regex(@"(string|null|array|number|char)\s+(.[A-z0-9]*)");
        private static readonly Regex AppropriationValue = new Regex(SyntaxTree.Appropriation + @"\s*(.*)");

        public static bool IsVariable(string code)
        {
            return VariableDetect.IsMatch(code);
        }

        public static Variable GetVariable(string code)
        {
            var match = VariableDetect.Match(code);
            if (match.Groups[1].Value.Equals("null"))
            {
                return new Variable()
                {
                    Type = Type.Null,
                    Name = match.Groups[2].Value,
                    Value = null
                };
            }

            var matchValue = AppropriationValue.Match(code).Groups[1].Value;
            return new Variable()
            {
                Type = GetTypeFromString(match.Groups[1].Value),
                Name = match.Groups[2].Value,
                Value = matchValue
            };
        }

        private static Type GetTypeFromString(string name)
        {
            return name.Trim().ToLower() switch
            {
                "null" => Type.Null,
                "string" => Type.String,
                "char" => Type.Char,
                "array" => Type.Array,
                "number" => Type.Number,
                _ => Type.Void
            };
        }
    }
    
    public class Variable
    {
        public Type Type = Type.Null;
        public string Name = nameof(Variable);
        public string Value = nameof(Variable);
    }
}        