using System.Text.Json.Serialization;

namespace DirectIdExercise
{
    /// <summary>
    /// RootObject for Json file
    /// </summary>

    public class Rootobject
    {
        public string providerName { get; set; }
        public string countryCode { get; set; }
        public List<Account> accounts { get; set; }
    }

    /// <summary>
    /// Account object
    /// </summary>
    public class Account
    {
       
        public Balances balances { get; set; }
        public List<Transaction> transactions { get; set; }
    }
      
    /// <summary>
    /// Balanace object
    /// </summary>
    public class Balances
    {
        public Current current { get; set; }
      
    }

    /// <summary>
    /// Current balance
    /// </summary>
    public class Current
    {
        public decimal amount { get; set; }
        public string? creditDebitIndicator { get; set; } 
       
    }

    /// <summary>
    /// Available balance
    /// </summary>
    public class Available
    {
        public float amount { get; set; }
        public string creditDebitIndicator { get; set; }
        
    }

    /// <summary>
    /// Party object
    /// </summary>
    public class Party
    {
        public string PartyId { get; set; }
        public string FullName { get; set; }
        public object[] Addresses { get; set; }
        public object PartyType { get; set; }
        public object IsIndividual { get; set; }
        public object IsAuthorizingParty { get; set; }
        public object EmailAddress { get; set; }
        public object[] PhoneNumbers { get; set; }
    }

    /// <summary>
    /// Transaction object
    /// </summary>
    public class Transaction
    {
      
        public decimal amount { get; set; }
        public string creditDebitIndicator { get; set; }
         public DateTime bookingDate { get; set; }
        
    }

 }
