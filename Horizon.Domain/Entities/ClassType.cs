namespace Horizon.Domain.Entities
{
    public class ClassType : BaseEntity
    {
        public string Name { get; set; }

        public List<Class> Classes { get; set; } = new List<Class>();
    }
}
