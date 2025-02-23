var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Orleans Silo & �N���X�^�̃Z�b�g�A�b�v
builder.Host.UseOrleans(silo =>
{
    silo.UseLocalhostClustering() // ���[�J�����ł̃N���X�^
        .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
