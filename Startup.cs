using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using user_stuff_share_app.Mappings;
using user_stuff_share_app.Repository.Collect_Repository;
using user_stuff_share_app.Repository.Cool_Repository;
using user_stuff_share_app.Repository.Tag_Repository;
using user_stuff_share_app.Repository.User_Repository;
using user_stuff_share_app.Repository_Interfaces.ICollect_Repository;
using user_stuff_share_app.Repository_Interfaces.ICool_Repository;
using user_stuff_share_app.Repository_Interfaces.ITag_Repository;
using user_stuff_share_app.Repository_Interfaces.IUser_Repository;
using user_stuff_share_app.Status_Messages;

namespace user_stuff_share_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICollectRepository, CollectRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICoolCollectRepository, CoolCollectRepository>();
            services.AddScoped<ICoolItemRepository, CoolItemRepository>();
            services.AddScoped<ITagCollectRepository, TagCollectRepository>();
            services.AddScoped<ITagItemRepository, TagItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<UserInfo>();
            services.AddSingleton<StatusMessages>();
            services.AddAutoMapper(typeof(SSAMappings));
            services.AddControllers();




            var appSettingsSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Tok);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = (context) =>
                            {
                                if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                                {
                                    context.Token = context.Request.Cookies["X-Access-Token"];
                                }
                                return Task.CompletedTask;
                            }
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            
            app.UseCors(options => options.WithOrigins("http://localhost:3000", "https://localhost:3000", "http://localhost:54878")
                .AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials()
             );
            
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
