using System;
using System.Linq.Expressions;

namespace Membership.Core.Helper
{
    public static class ExceptionHelper
    {
        public static void ThrowIfNull(Expression<Func<object>> expression)
        {
            var res = expression.Compile();

            if (res.Invoke() != null) return;

            var lambda = (LambdaExpression)expression;

            var member = (MemberExpression)lambda.Body;

            throw new ArgumentNullException(member.Member.Name);
        }

        public static void ThrowIfNullOrEmpty(Expression<Func<string>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("Expected property or field expression.");

            var compiled = expression.Compile();

            var value = compiled();

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("String is null or empty.", body.Member.Name);
        }

        public static void ThrowIfInvalidOperation(Expression<Func<object>> expression)
        {
            var res = expression.Compile();

            if (res.Invoke() != null) return;

            var lambda = (LambdaExpression)expression;

            var member = (MemberExpression)lambda.Body;

            throw new InvalidOperationException(member.Member.Name + " should not be null.");
        }

        public static void ThrowIfInvalidArgument(Expression<Func<object>> expression)
        {
            throw new ArgumentException("Argument is invalid.");
        }

        public static string GetExceptionLogMessage(Exception exception)
        {
            return exception.Message + Environment.NewLine + exception.StackTrace;
        }
    }
}