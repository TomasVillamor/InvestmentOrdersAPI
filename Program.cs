using investmentOrders.DataAccess;
using InvestmentOrdersAPI.DataAccess.Repositories.AssetRepository;
using InvestmentOrdersAPI.DataAccess.Repositories.OrderRepository.OrderRepository;
using InvestmentOrdersAPI.DataAccess.Repositories.OrderStateRepository;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddDbContext<DataAccessContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderStateRepository, OrderStateRepository>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
