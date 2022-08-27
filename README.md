# SandVolley ball ASP.NET Core MVC Web App

### A meet up app that allows users to view sand volleyball courts and other sand volleyball players. Ability click on profiles and details pages to find out more about either.
1. Users can Create, Read, Update and Delete their own accounts and courts.
2. Made with Entity Framework and utilized a code first migration to populate a Sql Server database.
3. Implemented a repository pattern to isolate the data access logic and business logic in order to allow for a more loosely coupled approach.
4. Authorization and Authentication was accomplished by introducing .NET Core Identity to provide you with the login functionality and hidden tabs that only Admins can access. This allowed only admins to update and delete courts and accounts that were not theirs. 
5. The app takes advantage of dependency injection by using an IoC container to handle the newing up of objects.
6. All images are stored in a [Cloudinary API](https://cloudinary.com/ "Cloudinary Homepage") , and courts near you are achieved by calling an API called [ipinfo.io.](https://ipinfo.io./ "ipinfo Homepage"). the API takes your IP Address, to determine your location, and shows you sand volleyball courts near your city.
7. Ability to search for courts by state.
8. Validate Anti Forgery Token Tags were added to Put requests to prevent cross-site request forgery attacks. 
9. .NET Core Identity also allowed me to give admins access to create, read, update and delete user roles that have different access levels. 
10. Link to my website hosted on Azure [MVCAzure](https://mvc20220824121339.azurewebsites.net/ "MVCAzure Homepage")

  
