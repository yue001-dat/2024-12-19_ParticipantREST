using _2024_12_19_ParticipantsLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "AllowAll",
							  policy =>
							  {
								  policy.AllowAnyOrigin()
								  .AllowAnyMethod()
								  .AllowAnyHeader();
							  });
});

builder.Services.AddControllers();

builder.Services.AddSingleton<ParticipantsRepository>(new ParticipantsRepository());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
