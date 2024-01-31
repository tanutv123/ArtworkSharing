using BusinessObject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Data
{
	public class Seed
	{
		public static async Task SeedImage(DataContext context)
		{
			if(await context.Images.AnyAsync())
			{
				return;
			}
			var photoData = await File.ReadAllTextAsync("D:\\du lieu o kia\\WPF\\ArtworkSharing\\DataAccess\\Data\\PhotoSeedData.json");
			var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var photos = JsonSerializer.Deserialize<List<Image>>(photoData, jsonOptions);
			foreach (var photo in photos)
			{
				await context.Images.AddAsync(photo);
			}
			await context.SaveChangesAsync();
		}
		public static async Task SeedUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			if (await userManager.Users.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("D:\\du lieu o kia\\WPF\\ArtworkSharing\\DataAccess\\Data\\UserSeedData.json");
			var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData, jsonOptions);

			var roles = new List<AppRole>
			{
				new AppRole { Name = "Audience"},
				new AppRole {Name = "Artist"},
				new AppRole {Name = "Manager"},
				new AppRole {Name = "Admin"}
			};

			foreach(var role in roles)
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
				Email = "admin@fas.com",
				ImageId = 1,
				Status = 1
			};

			var resultAdmin = await userManager.CreateAsync(admin, "Pa$$w0rd");
			await userManager.AddToRoleAsync(admin, "Admin");

			var manager = new AppUser
			{
				UserName = "manager",
				Email = "manager@fas.com",
				ImageId = 1,
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
				ImageId = 11,
				Status = 1
			};
			await userManager.CreateAsync(artist, "Pa$$w0rd");
			await userManager.AddToRoleAsync(artist, "Artist");
		}
	}
}
