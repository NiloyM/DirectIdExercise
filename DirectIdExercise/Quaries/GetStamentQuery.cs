using MediatR;

namespace DirectIdExercise.Quaries
{
    /// <summary>
    /// Query object for making the report
    /// </summary>
    public class GetStatementQuery:IRequest<List<Report>>
    {
    }
}
