using Microsoft.AspNetCore.Identity;
using SandVolleyballWebApp.Models;

namespace SandVolleyballWebApp.Data
{
    public class Seed
    {

        //public static void SeedData(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        //        context.Database.EnsureCreated();

        //        if (!context.Courts.Any())
        //        {
        //            context.Courts.AddRange(new List<Court>()
        //                    {
        //                        new Court()
        //                        {
        //                            Title = "Sand Point",
        //                            Description ="there are 3 courts here and it is right next to a beautiful lake",
        //                            Image = "https://media.ussportscamps.com/media/images/volleyball/nike/facilities/_800x350_crop_top-center_100_none/sand-point-beach-volleyball-courts.jpg",
        //                            Address = new Address()
        //                            {
        //                                Street = "14349 Crest Ave NE",
        //                                City = "Prior Lake",
        //                                State = "Minnesota"
        //                            }
        //                        },
        //                        new Court()
        //                        {
        //                            Title = "Theodore Wirth Regional Park Sand Volleyball court",
        //                            Description ="Great court",
        //                            Image = "https://www.clarksvilleonline.com/wp-content/uploads/2017/12/APSU-Beach-Volleyball-1.jpg",
        //                            Address = new Address()
        //                            {
        //                                Street = "3200 Glenwood Ave",
        //                                City = "Minneapolis",
        //                                State = "Minnesota",
        //                            }
        //                        },
        //                        new Court()
        //                        {
        //                            Title = "Bde Ska volleyBall court",
        //                            Description ="Great course in the heart of Minneapolis",
        //                            Image = "https://tse3.mm.bing.net/th?id=OIP.VRjYpaWHCXdGT_XRrbat3gHaFT&pid=Api&P=0",
        //                            Address = new Address()
        //                            {
        //                                Street = "3000 Bde Maka Ska Parkway",
        //                                City = "Minneapolis",
        //                                State = "Minnesota",
        //                            }
        //                        }
        //                        });
        //            context.SaveChanges();

        //        }

        //    }
        //}


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "alexcolemandeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "alexcolemandev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Password123456789!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user9@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app5-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}


    

