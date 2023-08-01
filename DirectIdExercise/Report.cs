namespace DirectIdExercise
{
    /// <summary>
    /// Statement object
    /// </summary>
    public class Report
    {
        public int InternalId { get; set; }
       public decimal CreditBalance { get; set; }
       public decimal DebitBalance { get; set; }
        public decimal InitialBalance { get; set; } 
       
       public decimal CurrentBalance { get; set; }
       public bool IsEndOfDayBalance { get; set; }
       public DateTime TransactionDate { get; set; }
    }
}
