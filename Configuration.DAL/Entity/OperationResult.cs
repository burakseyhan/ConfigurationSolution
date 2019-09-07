namespace Configuration.DAL.Entity
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Operation { get; set; }
    }
}
