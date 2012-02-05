using System.Collections.Generic;
using System.Linq;

namespace ParApply
{
    internal class ParaplyService
    {
        public UseParaply ShouldUseParaply(Result<IEnumerable<YrData>> yrResult)
        {
            if(!yrResult.HasError())
            {
                var yr = yrResult.Value.First();
                return yr.SymbolName.Contains("regn") ? UseParaply.Yes : UseParaply.No;
            }
            return UseParaply.Unknown;
        }
    }
}