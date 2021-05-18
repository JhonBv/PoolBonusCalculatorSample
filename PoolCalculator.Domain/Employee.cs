using System;

namespace PoolCalculator.Domain
{
    public class Employee : Entity
    {
        public string Fullname { get; private set; }
        public string JobTitle { get; private set; }
        //JB. Decimal is a more acurate type for this case.
        public decimal Salary { get; private set; }
        public int DepartmentId { get; private set; }
        public Department Department { get; private set; }

        public Employee(
            int id,
            string fullname,
            string jobTitle,
            decimal salary,
            int departmentId) :base(id)
        {
            Id = id;
            Fullname = fullname;
            JobTitle = jobTitle;
            Salary = salary;
            DepartmentId = departmentId;
        }
    }
}
