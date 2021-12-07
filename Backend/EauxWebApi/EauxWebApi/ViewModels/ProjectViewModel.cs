namespace EauxWebApi.ViewModels
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<WorkItemViewModel>? WorkItems { get; set; }
    }
}
