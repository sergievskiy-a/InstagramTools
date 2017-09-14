namespace InstagramTools.Common.Helpers
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public OperationResult(bool success)
        {
            this.Success = success;
        }

        public OperationResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }

    public class OperationResult<TModel> : OperationResult
    {
        public TModel Model { get; set; }

        public OperationResult(bool success, string message)
            : base(success, message)
        {
        }

        public OperationResult(TModel model) : base(true)
        {
            this.Model = model;
        }
    }
}
