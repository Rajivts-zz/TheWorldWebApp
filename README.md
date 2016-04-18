# TheWorldWebApp
A sample web application built using ASP.NET 5, AngularJS 1.5.3 and Bootstrap 3. 
Can be used to understand how ASP .NET core is different from the previous .NET versions, and how easily it can be used with existing Web technologies out there.
The application is a trip creation site, where you can login and create different trips (eg. India trip, World Trip) by adding stops in them, which are in turn are reflected
on a map.Developed as part of a course on WebApp development on PluralSight

#Dependencies
<ul>
  <li> The Application will require a local database to be setup, which can be done by using the snapshot of the latest migration in the migrations folder,
  or by seeding the data with a fresh identical database using the WorldContextSeedData.cs in Models directory. </li>
  <li> The Application also leverages GoogleMaps and Bing API, both of which require an API key. The API key is read from the user environment variables and
  hence the same should be populated with your own keys.
    <ul>
        <li> "AppSettings:BingKey" should be the name of the environment variable for the Bing API key</li>
        <li> "AppSettings:GoogleKey" should be the name of the environment variable for the Google Maps API key</li>
    </ul>
  </li>
</ul>

#Technologies Used
<ul>
  <li>ASP .NET 5 (ASP .NET Core)</li>
  <li>MVC 6</li>
  <li>Angular JS 1.5.3</li>
  <li>Bootstrap 3</li>
  <li>Entity Framework 7</li>
</ul>



