using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Framework {
    public class ApplicationViewModelBase : ViewModelBase {
        public override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            var memberExpr = propertyExpression.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentException("Property Expression should represent access to a member");
            string memberName = memberExpr.Member.Name;
            RaisePropertyChanged(memberName);
        }
    }
}
