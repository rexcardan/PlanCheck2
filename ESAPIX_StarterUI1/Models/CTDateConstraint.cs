using ESAPIX.Constraints;
using ESAPIX.Extensions;
using VMS.TPS.Common.Model.API;

namespace ESAPX_StarterUI.Models
{
    public class CTDateConstraint : IConstraint
    {
        public string Name => "";
        public string FullName => "CT is < 60 days old";

        public ConstraintResult CanConstrain(PlanningItem pi)
        {
            var pqa = new PQAsserter(pi);
            return pqa.HasImage()
                  .CumulativeResult;
        }

        public ConstraintResult Constrain(PlanningItem pi)
        {
            var image = pi.StructureSet.Image;
            var ctDate = image.Series.Study.CreationDateTime.Value;
            return ConstrainDate(ctDate);
        }

        public ConstraintResult ConstrainDate(System.DateTime ctDate)
        {
            var daysOld = (System.DateTime.Now - ctDate).TotalDays;

            if (daysOld <= 60)
            {
                return new ConstraintResult(this, ResultType.PASSED, "All good");
            }
            else
            {
                return new ConstraintResult(this, ResultType.ACTION_LEVEL_3, $"CT is {daysOld:N0} days old");
            }
        }
    }
}