using DirectIdExercise.MiddleWare.GlobarException;

namespace DirectIdExercise.MiddleWare
{
    /// <summary>
    /// Extension method for IApplicationBuilder
    /// </summary>
    public static class MiddleWareExtensiton
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleWare>();
        }
    }
}
