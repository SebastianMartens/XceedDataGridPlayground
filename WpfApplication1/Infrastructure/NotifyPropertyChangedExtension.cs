using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace WpfApplication1.Infrastructure
{
    /// <summary>
    /// Class that hold extension methods for the INotifyPropertyChanged interface.    
    /// </summary>
    public static class NotifyPropertyChangedExtension
    {

        /// <summary>
        /// The method extracts the property name of a given lambda expression.
        /// It can be used in base classes that provide their own RaisePropertyChanged implementations
        /// (unfortunately the complete RaisePropertyChanged code cannot be coded in here as it need a "event" field).
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string ExtractPropertyName<TProperty>(this INotifyPropertyChanged instance, Expression<Func<TProperty>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                    if (memberExpression == null) throw new NotImplementedException();
                }
                else
                    throw new NotImplementedException();
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", "expression");
            }

            //if (!property.DeclaringType.IsAssignableFrom(GetType()))
            //{
            //    throw new ArgumentException("The referenced property belongs to a different type.", "propertyExpression");
            //}

            var getMethod = property.GetGetMethod(true);
            if (getMethod == null)
            {
                // this shouldn't happen - the expression would reject the property before reaching this far
                throw new ArgumentException("The referenced property does not have a get method.", "expression");
            }

            if (getMethod.IsStatic)
                throw new ArgumentException("The referenced property is a static property.", "expression");

            return property.Name;
        }

    }
}
