using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Data
{
	public class Seed
	{
		
		public static async Task SeedUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			if (await userManager.Users.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("../DataAccess/Data/UserSeedData.json");
			var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData, jsonOptions);

			var roles = new List<AppRole>
			{
				new AppRole { Name = "Audience"},
				new AppRole {Name = "Artist"},
				new AppRole {Name = "Manager"},
				new AppRole {Name = "Admin"}
			};

			foreach (var role in roles)
			{
				await roleManager.CreateAsync(role);
			}

			foreach (var user in users)
			{
				user.UserName = user.Name.ToLower();
				var result = await userManager.CreateAsync(user, "Pa$$w0rd");
				await userManager.AddToRoleAsync(user, "Audience");
			}

			var admin = new AppUser
			{
				UserName = "admin",
				Name = "admin",
				Description = "admin",
				Email = "admin@fas.com",
				Status = 1
			};

			var resultAdmin = await userManager.CreateAsync(admin, "Pa$$w0rd");
			await userManager.AddToRoleAsync(admin, "Admin");

			var manager = new AppUser
			{
				UserName = "manager",
				Email = "manager@fas.com",
				Status = 1
			};
			await userManager.CreateAsync(manager, "Pa$$w0rd");
			await userManager.AddToRoleAsync(manager, "Manager");

			var artist = new AppUser
			{
				UserName = "Davinci",
				Name = "Da Vinci",
				Email = "vinci@gmail.com",
				PhoneNumber = "0123456789",
				Description = "I am the most known world artist",
				Status = 1,
				UserImage = new UserImage
				{
					Url = "https://images.pexels.com/photos/11098559/pexels-photo-11098559.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
					isMain = true
				}
			};
			await userManager.CreateAsync(artist, "Pa$$w0rd");
			await userManager.AddToRoleAsync(artist, "Artist");
		}

		public static async Task SeedArtwork(DataContext context)
		{

			if (await context.Genres.AnyAsync())
			{
				return;
			}
			var genres = new List<Genre>
				{
					new Genre {Name = "Landscape"},
					new Genre {Name = "Portrait"},
					new Genre {Name = "Anime"},
					new Genre {Name = "Fiction"}
				};
			foreach (var genre in genres)
			{
				context.Genres.AddAsync(genre);
			}
			await context.SaveChangesAsync();

			if (await context.Artworks.AnyAsync())
			{
				return;
			}
			var artworks = await File.ReadAllTextAsync("../DataAccess/Data/ArtworkSeed.json");
			var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var arts = JsonSerializer.Deserialize<List<Artwork>>(artworks, jsonOptions);
			foreach (var art in arts)
			{
				await context.Artworks.AddAsync(art);
			}
			await context.SaveChangesAsync();
		}
		public static async Task SeedCommissionStatus(DataContext context)
		{
			if(await context.CommissionStatus.AnyAsync()) { return; }

			var listOfCommissionStatus = new List<CommissionStatus>
			{
				new CommissionStatus {Description = "Accepted"},
				new CommissionStatus {Description = "Pending"},
				new CommissionStatus {Description = "In Progress"},
				new CommissionStatus {Description = "Done"},
				new CommissionStatus {Description = "Not Accepted"}
			};
			foreach(var status in listOfCommissionStatus)
			{
				context.CommissionStatus.Add(status);
			}
			await context.SaveChangesAsync();
		}
	}
}
