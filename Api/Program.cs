using Api.Extensions;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPostgreSqlDbContext(builder.Configuration);
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IBooksService, BooksService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors(opt =>
    opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*"));
app.MapControllers();

app.Run();
