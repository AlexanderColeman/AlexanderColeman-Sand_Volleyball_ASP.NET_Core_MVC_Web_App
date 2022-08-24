# SandVolley ball ASP.NET Core MVC Web App

### A meet up app that allows users to see courts and other sand volleyball players that use the App. Ability click on profiles and details pages to find out more about either.
1. Users can Create, Read, Update and Delete their own accounts and courts.
2. Made with Entity Framework and utilized a code first migration to populate a Sql Server database.
3. Implemented a repository pattern to isolate the data access logic and business logic in order to allow for a more loosely coupled approach.
4. Authorization and Authentication done with Entity Identity to provide you with the login functionality and hidden tabs that only Admins can access. This allowed for restriction  updating and deleting courts that you did not create as a regular user.
5. The app takes advantage of dependency injection by using an IoC container to handle the newing up of objects.
6. All images are stored in a [Cloudinary API](https://cloudinary.com/ "Cloudinary Homepage") , and courts near you are achieved by calling an API called [ipinfo.io.](https://ipinfo.io./ "ipinfo Homepage"). the API takes your IP Address, to determine your location, and shows you sand volleyball courts near your city.
7. Ability to search for courts by state.

  
