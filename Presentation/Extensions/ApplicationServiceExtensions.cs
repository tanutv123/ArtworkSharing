using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Presentation.Helpers;
using Presentation.SignalR;

namespace Presentation.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			//Add DBContext
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			});
			return services;
		}

	}
}
