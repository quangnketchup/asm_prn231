
using WebAPI.Extension;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions());

// add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddService();
builder.Services.AddSwagger();


var app = builder.Build();

app.RegisterFeatures();
app.Run();