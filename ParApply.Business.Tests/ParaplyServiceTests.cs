using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParApply.Business;

namespace ParApply.Business.Tests
{
    [TestClass]
    public class ParaplyServiceTests
    {
        [TestMethod]
        public void ShouldUse_WhenRegn_ReturnsYes()
        {
            AssertUseParaplyWhenYrSymbolIs(UseParaply.Yes, "Regn");
            AssertUseParaplyWhenYrSymbolIs(UseParaply.Yes, "regn");
        }

        [TestMethod]
        public void ShouldUse_WhenNotRegn_ReturnsNo()
        {
            AssertUseParaplyWhenYrSymbolIs(UseParaply.No, "Something other");
        }

        private static void AssertUseParaplyWhenYrSymbolIs(UseParaply useParaply, string symbolName)
        {
            // arrange
            var paraplyService = new ParaplyService();
            var yrResult = GetYrResult(symbolName);

            // act
            var useParaplyResult = paraplyService.ShouldUseParaply(yrResult);

            // assert
            Assert.AreEqual(useParaply, useParaplyResult.Result, "One should use a paraply when yr symbol is {0}", symbolName);
        }

        private static Result<IEnumerable<YrData>> GetYrResult(string symbolName)
        {
            List<YrData> yrData = new List<YrData>();
            yrData.Add(new YrData()
                           {
                               SymbolName = symbolName
                           });
            var yrResult = new Result<IEnumerable<YrData>>(yrData);
            return yrResult;
        }
    }
}
