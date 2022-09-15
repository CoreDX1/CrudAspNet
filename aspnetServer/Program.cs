using aspnetServer.Data;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var Myallow = "_Myconfig";

builder.Services.AddCors(options =>
{
    options.AddPolicy(Myallow, 
        builder =>
        {
            builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader().AllowAnyMethod();
        }
        );
   });

builder.Services.AddAuthentication(
    CertificateAuthenticationDefaults.AuthenticationScheme
    ).AddCertificate();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Asp.Net React", Version = "1v" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUiOptions =>
{
    swaggerUiOptions.DocumentTitle = "Asp.Net React";
    swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API serving a very simple Post model.");
    swaggerUiOptions.RoutePrefix = string.Empty;
}
);
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseCors(Myallow);

app.MapGet("/get-all-post", async () => await PostRepository.GetPostsAsync())
    .WithTags("Post Endpoints");

app.MapGet("/get-post-by-id/{postId}", async (int postId) =>
{
    Post postToReturn = await PostRepository.GetPostByIdAsync(postId);
    if (postToReturn != null)
    {
        return Results.Ok(postToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post Endpoints");

app.MapPost("/create-post", async (Post postToCreate) =>
{
    bool createSuccessful = await PostRepository.CreatePostAsync(postToCreate);
    if (createSuccessful)
    {
        return Results.Ok("Create succesful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post Endpoints");

app.MapPut("/update-post", async (Post postToUpdate) =>
{
    bool updateSuccessful = await PostRepository.UpdatePostAsync(postToUpdate);
    if (updateSuccessful)
    {
        return Results.Ok("Update succesful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post Endpoints");

app.MapDelete("/delete-post-by-id/{postId}", async (int postId) =>
{
    bool deleteSuccessful = await PostRepository.DeletePostAsync(postId);
    if (deleteSuccessful)
    {
        return Results.Ok("Delete succesful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Post Endpoints");
app.Run();
