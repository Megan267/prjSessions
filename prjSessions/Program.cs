namespace prjSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //1. Add session services to the dependency injection container
            builder.Services.AddSession(options => 
            {
                //You can set options here, like the session timeout
                options.IdleTimeout = TimeSpan.FromMinutes(20); //The session will expire after 20 minutes of inactivity
                options.Cookie.HttpOnly = true; //Makes the cookie inaccessible to client-side scripts script
                options.Cookie.IsEssential = true; //Required for the GDPR compliance
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //2. Important: Add the session middleware BEFORE useAuthorization and MapControllerRoute
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
