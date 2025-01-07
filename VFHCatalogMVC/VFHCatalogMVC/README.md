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
<li>Implementation of tests</li>
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
<p></p>
<dl>
  <dt>Admin:</dt>
    <dd>email: admin@gmail.com</dd>
    <dd>pass: Admin1_1</dd>
</dl>

<h3>Configuration</h3>
For the correct operation of drop-down lists it is necessary to add default values to table in databse. Execute queries on branch "Queries" in the following order:
<ul>
  <li>PlantTypesPL</li>
  <li>PlantGroupsPL</li>
  <li>PlantSectionsPL</li>
  <li>GrowthTypesPL</li>
  <li>FruitTypesPL</li>
  <li>FruitSizesPL</li>
  <li>GrowingSeazonsPL</li>
  <li>DestinationsPL</li>
  <li>ColorsTable</li>
</ul>

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
<h5>User messages</h5>
<img src="/VFHCatalogMVC.Web/Screens/UserMessages.png">
<br>
<h5>Registration</h5>
<img src="/VFHCatalogMVC.Web/Screens/Register.png">

  







