Plant Seeder is a web application based on MVC pattern. The application allows you to search for plants from the catalog based on species, type and subtype. 
You can see photos, description, list of online shops and gardener in your neighborhood who have seeds or seddlings of plants what you looking for. 
You can find gardener by localization (now searching is basing on drop down lists - in futer I want to use gps localization). If user want to exchange with other user seeds or seedlings can send messages to him.

Used packages:
- Entity framework
- AutoMapper
- FluentValidation
- DependencyInjection
  
Roles:
- Admin (can: add new plants to catalog, delete plants, edit plants, send messages to users, activate and deactivate plants)
- Private user (create a list of seeds and seedlings from the catalog that the user has, add new plant to catalog(with status not active - only admin can activate plant after weryfication information about plant), send messages to other users)
- Company (create a list of seeds and seedlings from the catalog that the user has, add new plant to catalog(with status not active - only admin can activate plant after weryfication information about plant))

Milestones:
- Better frontend (using for example blazor)
- Implementation of tests
- Implementation advanced filters adapted to the plant species, type and subtype
- Add catalog of other garden products with filtration
- Implementation of all functionality enabling shopping in stores
- Create a module for companies that allows them to add sales offers for gardening products from the catalog

Login data:

Role - Private user
email: kinga123@gmail.com
pass: Kinga1_123

Admin: 

email: admin@gmail.com
pass: Admin1_1

For the correct operation of drop-down lists it is necessary to add default values to table in databse. Execute queries on branch "Queries" in the following order:
- PlantTypesPL
- PlantGroupsTablePL
- PlantSectionsTablePL
- GrowthTypesPL
- FruitTypesPL
- FruitSizesPL
- GrowingSeazonsPL
- DestinationsPL
- ColorsTable

  







