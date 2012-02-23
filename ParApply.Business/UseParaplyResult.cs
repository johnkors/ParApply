namespace ParApply.Business
{
    public class UseParaplyResult
    {
        public bool HasError()
        {
            return YrData == null;
        }
        public YrData YrData { get; set; }
        public UseParaply Result { get; set; }
    }
}