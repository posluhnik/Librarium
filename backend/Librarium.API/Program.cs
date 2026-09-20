using Librarium.DataAccess;
using Microsoft.EntityFrameworkCore;
using Librarium.Application.Services;
using Librarium.DataAccess.Repositories;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CampaignDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CampaignDbContext)));
    });

builder.Services.AddScoped<ICampaignsService, CampaignsService>();
builder.Services.AddScoped<ICampaignsRepository, CampaignsRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();