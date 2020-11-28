using System;

namespace Covid.Shared.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string No { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}