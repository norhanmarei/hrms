namespace HRMS.Application.DTOs.V1.JobTitle
{
    public class JobTitleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description {get;set;}
        public bool IsActive{  get; set; }
    }
}