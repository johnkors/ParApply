using System.Collections.Generic;
using System.Linq;

namespace ParApply
{
    internal class ParaplyService
    {
        public UseParaplyResult ShouldUseParaply(Result<IEnumerable<YrData>> yrResult)
        {
            var useParaplyResult = new UseParaplyResult();
            if(!yrResult.HasError())
            {
                var yrData = yrResult.Value.First();
                useParaplyResult.YrData = yrData;
                useParaplyResult.Result = yrData.SymbolName.Contains("regn") ? UseParaply.Yes : UseParaply.No;
                return useParaplyResult;
            }
            return useParaplyResult;
        }
    }

    internal class UseParaplyResult
    {
        public bool HasError()
        {
            return YrData == null;
        }
        public YrData YrData { get; set; }
        public UseParaply Result { get; set; }
    }
}