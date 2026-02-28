namespace HRMS.Application.Common
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public ApiMeta Meta { get; set; }
        public ApiResponse(T data, ApiMeta meta)
        {
            Data = data;
            Meta = meta;
        }
    }
}