## Web Project using ReactJS and .NET - include Entity Framework Core
### Running Big Burguer in Dev (and Debug) - Running dev/debug
- Start the development server for the web project
    - cd Front-end/
    - run `npm install` to install all dependecies
    - run `npm start`
        - a new tab should open in your browser to [http://localhost:3000/](http://localhost:3000/)
    - Now, the browser will refresh automatically as you make changes in the web project.
    - Note, if you aren't seeing your changes, be sure to check the command line for any compile errors.  If needed, quit the dev server (CTRL+C) and restart is.
- Run the web api project in Visual Studio
    - Open the solution in Visual Studio
    - Use the command `update-database` on the Package Manager Console to create the database
    - Run the `InitialCreate` SQL query file on cd BigBurguer/
    - Make the BigBurguer.Api project the `Startup Project`
        - right click on the project and select `Set as Startup Project`
    - Press `F5`
        - This will open a browser tab to [http://localhost:5100/](http://localhost:5100/).
    - Now, this api will be used as you are making changes and navigating the Big Burguer web project.
        - Set debugger breakpoints in Visual Studio to debug the wep api.

### Debug endpoints with Swagger
    - After open on the browser the http://localhost:5100 route will live able to debug using Swagger.
        - To make yourself validate (after register and login), use the Authorize button and the Bearer token on the input.
	 	- Example: "Bearer 12345abcdef".
