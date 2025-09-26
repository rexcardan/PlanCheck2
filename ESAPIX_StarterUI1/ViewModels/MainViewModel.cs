using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ESAPIX.Common;
using ESAPIX.Constraints.DVH;
using ESAPIX_WPF_Example.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ESAPX_StarterUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            CreateConstraints();
        }

        private void CreateConstraints()
        {
            Constraints.AddRange(new ConstraintRow[]
            {
                new ConstraintRow(ConstraintBuilder.Build("PTV45", "Max[%] <= 110")),
                new ConstraintRow(ConstraintBuilder.Build("Rectum", "V75Gy[%] <= 15")),
                new ConstraintRow(ConstraintBuilder.Build("Rectum", "V65Gy[%] <= 35")),
                new ConstraintRow(ConstraintBuilder.Build("Bladder", "V80Gy[%] <= 15")),
             //   new PlanConstraint(new CTDateConstraint())
            });
        }

        [RelayCommand]
        public async Task Evaluate()
        {
            foreach (var pc in Constraints)
            {
                var result = await VMS.GetValueAsync(sc =>
                {

                    //Check if we can constrain first
                    var canConstrain = pc.Constraint.CanConstrain(sc.PlanSetup);
                    //If not..report why
                    if (!canConstrain.IsSuccess) { return canConstrain; }
                    else
                    {
                        //Can constrain - so do it
                        return pc.Constraint.Constrain(sc.PlanSetup);
                    }
                });
                //Update UI
                pc.Result = result;
            }
        }

        public ObservableCollection<ConstraintRow> Constraints { get; private set; } = new ObservableCollection<ConstraintRow>();
    }
}