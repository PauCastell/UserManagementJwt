using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        public readonly RequestDelegate _next; // Permite que la petición continúe en el pipeline de middlewares.

        public ErrorHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) //HttpContext información sobre la solicitud HTTP actual.
        {
            try
            {
                await _next(context); // Llama al siguiente middleware en la cadena de middlewares.fgtjhkio`´l+p
                                    
            }catch (Exception ex)
            {
                // Aquí se puede manejar la excepción, por ejemplo, registrándola o devolviendo una respuesta de error personalizada.
                context.Response.ContentType = "application/json";
                var statusCode = (ex as dynamic)?.StatusCode ?? 500; // Si la excepción tiene una propiedad StatusCode, úsala; de lo contrario, usa 500.
                context.Response.StatusCode = statusCode;
                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = ex.Message
                };

                var json = System.Text.Json.JsonSerializer.Serialize(errorResponse); //Convertir a JSON.
                await context.Response.WriteAsync(json); //Escribir la respuesta JSON en el cuerpo de la respuesta HTTP.
            }
        }
    }
}
