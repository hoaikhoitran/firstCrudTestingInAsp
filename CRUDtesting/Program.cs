using CRUDtesting.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //Kích hoạt khả năng "quét" các endpoint API
                                            //để Swagger biết được API của bạn có những route nào.

builder.Services.AddSwaggerGen(options => //Thêm dịch vụ Swagger vào DI container
                                          //DI container là một thành phần chứa, quản lý,
                                          //và cấp phát các đối tượng (services) mà ứng dụng cần dùng.
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Authentication Demo",
        Version = "23/05"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme() // định nghĩa một security scheme mới
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter token",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat =  "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, // là phần đã định nghĩa trên
                    Id = "Bearer" // tên của scheme đã định nghĩa ở AddSecurityDefinition
                }
            },
            []
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//1. Cấu hình Entity Framework và Database Context
//DefaultConnection đu định nghĩa trong appsettings.json là chuỗi kết nối đến database
builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

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
