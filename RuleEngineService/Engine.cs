using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngineService
{
    class Engine
    {
        Expression BuildExpression<T>(Models.Rule r, ParameterExpression parameter)
        {
            // Get the property from the member passed by parameter
            var left = MemberExpression.Property(parameter, r.MemberName);

            // Get the property type from the member passed by parameter
            var tProp = typeof(T).GetProperty(r.MemberName).PropertyType;


            ExpressionType tBinary;
            // look for a .NET operator
            if (ExpressionType.TryParse(r.Operator, out tBinary))
            {
                // Set the right side of the tree as the target value of the rule
                var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tProp));
                return Expression.MakeBinary(tBinary, left, right);
            }
            // evaluate with a method call
            else
            {
                var method = tProp.GetMethod(r.Operator);
                var tParam = method.GetParameters()[0].ParameterType;
                var right = Expression.Constant(Convert.ChangeType(r.TargetValue, tParam));
                return Expression.Call(left, method, right);
            }
        }

        Func<T, bool> CompileRule<T>(Models.Rule r)
        {
            var paramUser = Expression.Parameter(typeof(T));
            Expression expr = BuildExpression<T>(r, paramUser);

            return Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile();
        }

        List<Func<T, bool>> CompileRules<T>(List<Models.Rule> rules)
        {
            var compiledRules = new List<Func<T, bool>>();

            rules.ForEach(rule => {
                var paramUser = Expression.Parameter(typeof(T));
                Expression expr = BuildExpression<T>(rule, paramUser);
                compiledRules.Add(Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile());

            });

            return compiledRules;
        }

        public Engine()
        {
            using (var db = new RuleEngineContext())
            {

                // Display all Blogs from the database 
                var query = db.People;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.ID);
                }

                Console.WriteLine("finish");
            }
            /*List<Person> persons = new List<Person>();

            var person1 = new Person
            {
                Name = "John",
                Age = 13,
                Temperature = 37,
                HasCough = true,
                HasBlisters = false
            };

            var person2 = new Person
            {
                Name = "Mary",
                Age = 3,
                Temperature = 38,
                HasCough = true,
                HasBlisters = true
            };


            persons.Add(person1);
            persons.Add(person2);

            List<Rule> chickenpoxRules = new List<Rule>{
                new Rule("Age", "LessThan", "20"),
                new Rule("Temperature", "GreaterThan", "36"),
                new Rule("HasBlisters", "Equal", "true")
            };

            var compiledRules = CompileRules<Person>(chickenpoxRules);

            persons.ForEach(person => {
                if (compiledRules.TakeWhile(r => r(person)).Count() == compiledRules.Count())
                {
                    Console.WriteLine(string.Concat(person.Name, " has the Chickenpox!"));
                }
                else
                {
                    Console.WriteLine(string.Concat(person.Name, " does not have the Chickenpox!"));
                }
            });

    */
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            new Engine();
        }
    }
}
