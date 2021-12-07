namespace EauxWebApi.ViewModels
{
    public class WorkItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Done { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectViewModel Project { get; set; }
    }
}
