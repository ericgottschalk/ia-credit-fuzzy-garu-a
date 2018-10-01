using ENG.Credit.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENG.Credit.Test
{
    [TestClass]
    public class CreditTest
    {
        [TestMethod]
        public void Test()
        {
            var validation = new RiskValidation();

            var result = validation.Validate(3000, 12, 19, 1, 10000);

            Assert.IsNotNull(result);
        }
    }
}
