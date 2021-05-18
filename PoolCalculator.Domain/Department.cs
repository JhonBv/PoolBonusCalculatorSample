using System.Collections.Generic;

namespace PoolCalculator.Domain
{
    /// <summary>
    /// Changing the Setter to private set so that values cannot be changed outside of the Entity but only by the Entity.
    /// </summary>
    public class Department : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Employee> Employees { get; private set; }
       

        public Department(
            int id,
            string title,
            string description) : base(id)
        {
            Title = title;
            Description = description;
        }
    }
}
