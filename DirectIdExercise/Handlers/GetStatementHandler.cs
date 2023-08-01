using DirectIdExercise.Configuration;
using DirectIdExercise.Controllers;
using DirectIdExercise.Quaries;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Transactions;

namespace DirectIdExercise.Handlers
{
    /// <summary>
    /// Handler to generate staement
    /// </summary>
    public class GetStatementHandler : IRequestHandler<GetStatementQuery, List<Report>>
    {
        private readonly ILogger<GetStatementHandler> _logger;
        private readonly DirectIdConfiguration _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public GetStatementHandler(ILogger<GetStatementHandler> logger, IOptions<DirectIdConfiguration> options) 
        {
            _logger = logger;
            _options = options.Value;
        }

        /// <summary>
        /// Handler to handle request from controller
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<Report>> Handle(GetStatementQuery request, CancellationToken cancellationToken)
        {
            var reports = new List<Report>();

            try
            {
                using var jsonStream = File.OpenRead(_options.FileToRead!);
                var jsonText = JsonSerializer.Deserialize<Rootobject>(jsonStream);
                _logger.LogInformation("Json File read completed");
                reports = GenerateStatements(jsonText!);

                reports.GroupBy(report => report.TransactionDate).ToList().ForEach(group =>
                {
                    group.First().IsEndOfDayBalance = true;
                });
               
            }
            catch (Exception ex) {
                _logger.LogError("Error Orrurder while generating Statement: \n", ex);
            }
            return Task.FromResult(reports);
        }

        private List<Report> GenerateStatements(Rootobject jsonText)
        {
            var reports = new List<Report>();
            int internalId = 0;
            decimal currentBalance = jsonText!.accounts[0].balances.current.amount;

            var transactions = jsonText!.accounts[0].transactions.OrderByDescending(transaction => transaction.bookingDate);
            foreach (var transaction in transactions)
            {

                internalId++;
                var report = new Report();

                report.InternalId = internalId;
                report.CreditBalance = (transaction.creditDebitIndicator.ToLower() == CreditDebitIndicatorEnum.Credit.ToString().ToLower()) ? transaction.amount : 0;
                report.DebitBalance = (transaction.creditDebitIndicator.ToLower() == CreditDebitIndicatorEnum.Debit.ToString().ToLower()) ? transaction.amount : 0;
                report.InitialBalance = internalId == 1 ? currentBalance : reports.Last().CurrentBalance;
                report.CurrentBalance = report.InitialBalance + report.CreditBalance - report.DebitBalance;
                report.TransactionDate = transaction.bookingDate;

                report.TransactionDate = transaction.bookingDate;


                reports.Add(report);
            }
            return reports;
        }
    }
}
