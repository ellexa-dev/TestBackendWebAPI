using ExerciceWebAPI.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;

namespace ExerciceWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dbName = "FollowersDB.db";
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            using (var dbContext = new FollowerDbContext())
            {
                //Ensure database is created
                dbContext.Database.EnsureCreated();
                if (!dbContext.Followers.Any())
                {
                    dbContext.Followers.AddRange(new Models.Follower[]
                        {
                             new Models.Follower{ ID=new Guid(), FirstName="Steve", LastName="Jobs" },
                             new Models.Follower{ ID=new Guid(), FirstName="Bill", LastName="Gates" },
                             new Models.Follower{ ID=new Guid(), FirstName="Benjamin", LastName="Franklin" }
                        });
                    dbContext.SaveChanges();
                }
                foreach (var follower in dbContext.Followers)
                {
                    Console.WriteLine($"FollowerId={follower.ID}\tFirst Name={follower.FirstName}\tLast Name={follower.LastName}");
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
