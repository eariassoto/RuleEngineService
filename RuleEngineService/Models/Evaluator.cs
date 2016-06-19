using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngineService.Models
{
    public class Evaluator
    {
        Expression BuildExpression<T>(Rule r, ParameterExpression parameter)
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

        Func<T, bool> CompileRule<T>(Rule r)
        {
            var paramUser = Expression.Parameter(typeof(T));
            Expression expr = BuildExpression<T>(r, paramUser);

            return Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile();
        }

        List<Func<T, bool>> CompileRules<T>(List<Rule> rules)
        {
            var compiledRules = new List<Func<T, bool>>();

            rules.ForEach(rule => {
                var paramUser = Expression.Parameter(typeof(T));
                Expression expr = BuildExpression<T>(rule, paramUser);
                compiledRules.Add(Expression.Lambda<Func<T, bool>>(expr, paramUser).Compile());

            });

            return compiledRules;
        }

        public List<Person> EvaluatePolicy(Policy policy)
        {
            var db = new RuleEngineContext();
            var rules = policy.Rules;
            var people = db.People.ToList();
            List<Person> res = new List<Person>();

            var compiledRules = CompileRules<Person>(rules.ToList());

            people.ForEach(person => {
                if (compiledRules.TakeWhile(r => r(person)).Count() == compiledRules.Count())
                {
                    res.Add(person);
                }
            });

            return res;
        }

    }
}
