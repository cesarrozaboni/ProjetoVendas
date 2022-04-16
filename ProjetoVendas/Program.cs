using Infra.Departament;
using Infra.SalesRecord;
using Infra.Seller;
using Microsoft.AspNetCore.Localization;
using ProjetoVendas.Data;
using Services.Departament;
using Services.SalesRecord;
using Services.Seller;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<SellerDal>();
builder.Services.AddScoped<SellerService>();

builder.Services.AddScoped<DepartamentDal>();
builder.Services.AddScoped<DepartamentService>();

builder.Services.AddScoped<SalesRecordDal>();
builder.Services.AddScoped<SalesRecordService>();

builder.Services.AddScoped<SeedingData>();

var app = builder.Build();

var enUS = new CultureInfo("pt-BR");
var localizationOption = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS }
};

app.UseRequestLocalization(localizationOption);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    new SeedingData(new DepartamentService(new DepartamentDal()), new SellerService(new SellerDal()), new SalesRecordService(new SalesRecordDal())).Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
