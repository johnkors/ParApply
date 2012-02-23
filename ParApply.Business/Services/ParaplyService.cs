using System;
using System.Collections.Generic;
using System.Linq;

namespace ParApply.Business
{
    public class ParaplyService
    {
        public UseParaplyResult ShouldUseParaply(Result<IEnumerable<YrData>> yrResult)
        {
            var useParaplyResult = new UseParaplyResult();
            if(!yrResult.HasError())
            {
                var yrData = yrResult.Value.First();
                useParaplyResult.YrData = yrData;
                useParaplyResult.Result = yrData.SymbolName.Contains("regn", StringComparison.InvariantCultureIgnoreCase) ? UseParaply.Yes : UseParaply.No;
                return useParaplyResult;
            }
            return useParaplyResult;
        }
    }
}