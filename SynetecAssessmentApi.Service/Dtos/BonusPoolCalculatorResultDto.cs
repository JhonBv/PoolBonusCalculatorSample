namespace SynetecAssessmentApi.Service.Dtos
{
    public class BonusPoolCalculatorResultDto
    {
        //JB. Decimal is a better type to use in this cases.
        public decimal BonusAmount { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
