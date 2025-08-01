Plant Seeder is a web application based on MVC pattern. The application allows you to search for plants from the catalog based on species, type and subtype. 
You can see photos, description, list of online shops and gardener in your neighborhood who have seeds or seddlings of plants what you looking for. 
You can find gardener by localization (now searching is basing on drop down lists - in futer I want to use gps localization). If user want to exchange with other user seeds or seedlings can send messages to him.

<h3>Technology stack</h3>
<p><strong>Back-end</strong> </p>
<ul>
  <li>ASP.NET Core MVC</li>
  <li>SQL</li>
  <li> Entity framework</li>
  <li>AutoMapper</li>
  <li>FluentValidation</li>
  <li>DependencyInjection</li>
  <li>JavaScript - cascading DropDownList and modal pop-up functionality</li>
  <li>Google OAuth2.0</li>
  <li>Clean Architecture</li>
 </ul>
 
<p><strong>Front-end</strong> </p>
<ul>
  <li>Bootstrap</li>
 </ul>
  
<h3>Roles</h3>
<ul>
  <li>Admin (can: add new plants to catalog, delete plants, edit plants, send messages to users, activate and deactivate plants)</li>
  <li> Private user (create a list of seeds and seedlings from the catalog that the user has, add new plant to catalog(with status not active - only admin can activate plant after veryfication information about plant), send messages to other users, add opinions about plants, users)</li>
  <li>Company (create a list of seeds and seedlings from the catalog that the user has, add new plant to catalog(with status not active - only admin can activate plant after weryfication information about plant))  </li>
</ul>

<h3>Development</h3>
<ul>
<li>Better frontend (using for example blazor)</li>
<li>Implementation async</li>
<li>Implementation advanced filters adapted to the plant species, type and subtype</li>
<li>Add catalog of other garden products with filtration</li>
<li>Implementation of all functionality enabling shopping in stores</li>
<li>Create a module for companies that allows them to add sales offers for gardening products from the catalog</li>
<li>Safety</li>
</ul>

<h3>Login data</h3>
<p></p>
<dl>
  <dt>Private user:</dt>
    <dd>email: kinga123@gmail.com</dd>
    <dd>pass: Kinga1_123</dd>
</dl>
<dl>
  <dt>Private user:</dt>
    <dd>email: sara2013@gmail.com</dd>
    <dd>pass: Sara_123</dd>
</dl>
<p></p>
<dl>
  <dt>Admin:</dt>
    <dd>email: admin@gmail.com</dd>
    <dd>pass: Admin1_1</dd>
</dl>

<h3>Screenshots</h3>
<br>
<h5>Home Page</h5>
<img src="/VFHCatalogMVC.Web/Screens/Index.png" alt="Home Page" title="Home Page">
<br>
<h5>Plant details</h5>
<img src="/VFHCatalogMVC.Web/Screens/PlantDetails.png">
<br>
<h5>Home page for PrivateUser</h5>
<img src="/VFHCatalogMVC.Web/Screens/IndexPrivateUser.png">
<br>
<h5>Add seeds/seedlings for choosen plant (PrivateUser)</h5>
<img src="/VFHCatalogMVC.Web/Screens/PrivateUserAddSeeds.png">
<br>
<h5>User seeds or seedlings list</h5>
<img src="/VFHCatalogMVC.Web/Screens/IndexUserSeedsList.png">
<br>
<h5>Add opinion about plant</h5>
<img src="/VFHCatalogMVC.Web/Screens/AddOpinion.png">
<br>
<h5>User messages</h5>
<img src="/VFHCatalogMVC.Web/Screens/UserMessages.png">
<br>
<h5>User messages for choosen plant</h5>
<img src="/VFHCatalogMVC.Web/Screens/UserIndexPlantMessages.png">
<br>
<h5>Send message to other user for choosen plant</h5>
<img src="/VFHCatalogMVC.Web/Screens/SendMessagePUByPU.png">
<br>
<h5>Home page for admin</h5>
<img src="/VFHCatalogMVC.Web/Screens/IndexAdmin.png">  
<br>
<h5>Add new plant</h5>
<img src="/VFHCatalogMVC.Web/Screens/AdminAddNewPlant.png">  
<br>
<h5>Add new plant validation</h5>
<img src="/VFHCatalogMVC.Web/Screens/AdminAddNewPlantValidation.png">  
<br>
<h5>Edit Plant</h5>
<img src="/VFHCatalogMVC.Web/Screens/AdminEditPlant.png">  
<br>
<h5>List of new plant added by users</h5>
<img src="/VFHCatalogMVC.Web/Screens/NewAddedPlantsByUsersIndexAdmin.png">  
<br>
<h5>Registration</h5>
<img src="/VFHCatalogMVC.Web/Screens/Register.png">

## Prerequisites

* Docker Desktop (with WSL 2 enabled for Windows, if applicable)
* (Optional: .NET SDK, if someone wants to work without Docker)

## Running the Project with Docker Compose

This project uses Docker Compose to easily set up the development environment, including an SQL Server database and an ASP.NET Core application.

### 1. Environment Variables Configuration

This project requires certain environment variables, such as the database password and OAuth authentication keys (e.g., Google Client ID/Secret).
*Download project 
* Copy the `env.example` file to a new file named `.env` in the same directory as `docker-compose.yml`:
    ```bash
    cp env.example .env  # Linux/macOS
    copy env.example .env # Windows
    ```
* Open the newly created `.env` file and fill in your actual values for `SA_PASSWORD`, `Authentication__Google__ClientId`, and `Authentication__Google__ClientSecret`.

    **Example `.env` file (with your data):**
    ```
    SA_PASSWORD=YourSecurePassword!
    Authentication__Google__ClientId=your_actual_google_client_id
    Authentication__Google__ClientSecret=your_actual_google_client_secret
    ```
    * **How to get Google Client ID/Secret:**
        1.  Go to [Google Cloud Console](https://console.cloud.google.com/).
        2.  Create a new project (or select an existing one).
        3.  Navigate to **"APIs & Services" > "Credentials"**.
        4.  Click **"Create Credentials"** and select **"OAuth client ID"**.
        5.  Choose application type **"Web application"**.
        6.  In the **"Authorized redirect URIs"** section, add:
            ```
            http://localhost:8080
            ```
            (If your application uses other redirect URIs, add them as well).
        7.  After creation, copy your **Client ID** and **Client Secret** and paste them into your `.env` file.

### 2. Running the Docker Containers

In the directory containing `docker-compose.yml` (and `.env`), run:
```bash
docker-compose up --build
```
This command will build the images, create and start the containers, and initialize the database.

### 3. Accessing the Application
Once all services are up and running (this might take a few minutes on the first run), the application will be available at:

http://localhost:8080






