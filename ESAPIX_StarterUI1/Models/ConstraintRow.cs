using CommunityToolkit.Mvvm.ComponentModel;
using ESAPIX.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESAPIX_WPF_Example.Models
{
    public partial class ConstraintRow : ObservableObject
    {
        public ConstraintRow(IConstraint constraint)
        {
            Constraint = constraint;
        }

        [ObservableProperty]
        private IConstraint constraint;
        
        [ObservableProperty]
        private ConstraintResult result;
    }
}
