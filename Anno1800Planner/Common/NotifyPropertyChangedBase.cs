using Anno1800Planner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Anno1800Planner.Common
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            DB.MarkDirty();
            return true;
        }

        // 💡 Set for model-backed property (Plan.Name, etc.)
        protected bool Set<T>(Expression<Func<T>> getExpr, T newValue)
        {
            var memberExpr = getExpr.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentException("Expression must be a member access", nameof(getExpr));

            var propInfo = memberExpr.Member as System.Reflection.PropertyInfo;
            if (propInfo == null || !propInfo.CanWrite)
                throw new ArgumentException("Expression must point to a writable property", nameof(getExpr));

            // Get the target object dynamically
            var objectExpr = Expression.Lambda(memberExpr.Expression!).Compile();
            var target = objectExpr.DynamicInvoke();

            // Get current value
            var getter = getExpr.Compile();
            var oldValue = getter();

            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
                return false;

            propInfo.SetValue(target, newValue);

            OnPropertyChanged(propInfo.Name);
            DB.MarkDirty();
            return true;
        }

        // 💡 Optional: Accept assignment action (for more control)
        protected bool Set<T>(Action assign, T oldValue, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
                return false;

            assign();
            OnPropertyChanged(propertyName);
            DB.MarkDirty();
            return true;
        }
    }
}
