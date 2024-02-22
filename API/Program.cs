using API.Validators;
using BLL.Service;
using DAL.Data.Repository;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGenericRepository<Exercise>, DataExercise>();
builder.Services.AddScoped<IGenericRepository<Segment>, DataSegment>();
builder.Services.AddScoped<IGenericRepository<Models.Type>, DataType>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<ISegmentService, SegmentService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ExerciseValidator>();
builder.Services.AddScoped<SegmentValidator>();
builder.Services.AddScoped<TypeValidator>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
