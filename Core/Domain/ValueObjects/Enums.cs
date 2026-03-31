namespace Domain.ValueObjects
{
    

 public enum LoanStatus
    {
        Active = 1,
        FullyPaid = 2,
        Defaulted = 3, // Per your docs: records are "Frozen" here
        Settled = 4,
        WrittenOff = 5
    }
    }