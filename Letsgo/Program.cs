using Letsgo.Data;
using Letsgo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();




builder.Services.AddHttpClient<ServizioVoli>();
builder.Services.AddHttpClient<ServizioMeteo>();
builder.Services.AddHttpClient<ServizioGemini>();


builder.Services.AddScoped<ServizioDestinazioni>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // 1. Crea il ruolo "Admin" se non c'è già
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    // 2. Inserisci qui tutte le email che vuoi far diventare Admin separate da virgola
    string[] adminEmails = { "tartari.thomas.studente@itispaleocap.it", "gabriele.arcuri@itispaleocapa.it"};

    // 3. Controlla ogni email nella lista e assegna il ruolo
    foreach (var email in adminEmails)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
            await userManager.AddToRoleAsync(user, "Admin");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    // ... resto del tuo codice (app.UseMigrationsEndPoint() ecc.)
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();


app.Run();
