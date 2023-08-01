namespace DirectIdExercise.MiddleWare.GlobarException
{
    /// <summary>
    /// Global Error object
    /// </summary>
    /// <param name="StatusCode"></param>
    /// <param name="Message"></param>
    public record GlobalErrorDetails(int StatusCode, string Message);
    
}
