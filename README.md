# PetalMD Technical Exercise - Backend Developer
## Author: Cristhian Buitrago D.

## Repository objective: 
   Present the required test given by PetalMD. The test consists of developing a Restful API to perform CRUD operations over a given csv file.

## Applied Architecture: Domain-Driven-Design (DDD)

![image](https://user-images.githubusercontent.com/42946312/140680884-aeff050c-37c9-49a7-b397-346ca9d29fb4.png)

DDD is an architecture that focuses on a domain aproach rather than a UI/Database orientation.

### Here are some brief key notes about DDD:

* (Development) becomes domain oriented not UI/Database oriented 
* (Domain layer) captures all of the business logic, making your service layer very thin i.e. just a gateway in to your domain via DTO's
* (Domain oriented) development allows you to implement true service-oriented architecture i.e. making your services reusable as they are not UI/Presentation layer specific 
* (Unit tests) are easy to write as code scales horizontally and not vertically, making your methods thin and easily testable
* (DDD) is a set of Patterns and Principles, this gives developers a framework to work with, allowing everyone in the development team to head in the same direction

### STEPS

1. Security

    In order to use the exposed methods you have to consume the authentication method:

    * URL: [server_url:port]/api/Auth/login
    * Method: POST
    * JSON Body Request:
                          {
                              "UserId": "Valid_User",
                              "Password": "Password"
                          }
    * Response: The method returns a json with the token and the expiration date. This token is necessary to consume the CRUD methods.

    POSTMAN Authentication Exhibit
    ![image](https://user-images.githubusercontent.com/42946312/140682021-8ba6d7d6-e11d-415d-b266-2306b777cb73.png)
    
    POSTMAN Token Exhibit: In the Auth option, the "Type" must be "Bearer Token". Paste the token you retrieved in step 1.
    ![image](https://user-images.githubusercontent.com/42946312/140684242-5bfb8de3-5713-4403-8949-e88f1625f5c8.png)


 2. API Methods
 
    #### Retrieve Pokemon List
      
      * URL: [server_url:port]/api/Pokemon/pokemon
      * Method: GET
      * JSON Body Request:
                          {
                              "columnOrderBy": "id", (The column you wish to use as reference to sort the list)
                              "searchText": "", (Search by name through a "contains" method)
                              "desc": false, (Sort order)
                              "take": 0, (Choose how many items you wish to see per page)
                              "skip": 0 (Pagination)
                          }
      * Response: The method returns a json with the list of pokemons. The structure indicates the total items and reference attributes of the pagination.

      POSTMAN GET Exhibit A
      ![image](https://user-images.githubusercontent.com/42946312/140683048-4b8a0cfa-8599-4866-9b7e-de89060d6c4f.png)
    
      POSTMAN GET Exhibit B
      ![image](https://user-images.githubusercontent.com/42946312/140683108-e2fcd1ed-fdb5-4810-be86-13dd93fb7636.png)
    
    #### Insert a Pokemon

      * URL: [server_url:port]/api/Pokemon/pokemon
      * Method: POST
      * JSON Body Request:
                          {
                              "Name": "New Cool Pokemon",
                              "Type1": "Telekinesis",
                              "Type2": "Fire",
                              "Total": 600,
                              "Hp": 100,
                              "Attack": 80,
                              "Defense": 200,
                              "Sp_Attack": 250,
                              "Sp_Defense": 250,
                              "Speed": 80,
                              "Generation": 4,
                              "Legendary": true
                          }
      * Response: The method returns a json with the list of pokemons including the new Pokemon at the top.

      POSTMAN POST Exhibit
      ![image](https://user-images.githubusercontent.com/42946312/140683326-5f925179-aedc-4560-8d2f-5c0775306d6b.png)
    
    #### Update a Pokemon

      * URL: [server_url:port]/api/Pokemon/pokemon
      * Method: PATCH
      * JSON Body Request:
                          {
                              "Id": 4,
                              "Type1": "Telekinesis",
                              "Type2": "Fire",
                              "Total": 600,
                              "Hp": 100,
                              "Attack": 80,
                              "Defense": 200,
                              "Sp_Attack": 250,
                              "Sp_Defense": 250,
                              "Speed": 80,
                              "Generation": 4,
                              "Legendary": true
                          }
       * Note: The "Id" attribute is required. You can update one field or all. 
       * Example of JSON Body Request:
                          {
                              "Id": 4,
                              "Type1": "Other Type 1",
                          }
                          
       * Response: The method returns a json with the new list of Pokemons.
        
       POSTMAN PATCH Exhibit
       ![image](https://user-images.githubusercontent.com/42946312/140683872-4a498f03-f002-4cb1-bf98-011c3106735a.png)

     #### Delete a Pokemon
     
       * URL: [server_url:port]/api/Pokemon/pokemon
       * Method: DELETE
       * JSON Body Request:
                          {
                              "Id": 4
                          }
                          
       * Response: The method returns a json with the new list of Pokemons.
       
       POSTMAN DELETE Exhibit
       ![image](https://user-images.githubusercontent.com/42946312/140684117-53c2cce6-2399-442b-b739-d40b6439a20f.png)
       
 ## Technologies
 
   The project was developed in Visual Studio 2019.
 
   * .NET Core 3.1
   * C#
   * DDD Architecture
   * JWT (JSON Web Token)

## Structure (DDD)

  ![image](https://user-images.githubusercontent.com/42946312/140685039-93774221-6632-4adb-aa47-b486383d43b7.png)
