using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESAPX_StarterUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Constraints;

namespace ESAPX_StarterUI.Models.Tests
{
    [TestClass()]
    public class CTDateConstraintTests
    {
        [TestMethod()]
        public void ConstraintPassesWhenLessThan60DaysOld()
        {
            //Arrange
            var ctDate = DateTime.Now.AddDays(-59);

            //Act
            var constraint = new CTDateConstraint();
            var result = constraint.ConstrainDate(ctDate);

            //Assert
            var expected = ResultType.PASSED;
            var actual = result.ResultType;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConstraintFailsWhenMoreThan60DaysOld()
        {
            //Arrange
            var ctDate = DateTime.Now.AddDays(-61);

            //Act
            var constraint = new CTDateConstraint();
            var result = constraint.ConstrainDate(ctDate);

            //Assert
            var expected = ResultType.ACTION_LEVEL_3;
            var actual = result.ResultType;
            Assert.AreEqual(expected, actual);
        }


    }
}