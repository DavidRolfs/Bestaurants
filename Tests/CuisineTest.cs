using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Bestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    { //  This tells the application where to find the test database.
      //  This overrides "DBConfiguration.ConnectionString" in Startup.cs.
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Bestaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmpty()
    {
      //ARRANGE, application
      int result = Cuisine.GetAll().Count;

      //ASSERT
        Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("French");
      Cuisine secondCuisine = new Cuisine("French");

      //ASSERT
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("French");
      testCuisine.Save();
      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      Cuisine testCuisine = new Cuisine("French");
      testCuisine.Save();

      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_GetRestaurants_RetrievesAllRestaurantsWithinCuisine()
    {
      Cuisine testCuisine = new Cuisine("French");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Frenchies", testCuisine.GetId());
      Restaurant secondRestaurant = new Restaurant("Frenchies", testCuisine.GetId());

      firstRestaurant.Save();
      secondRestaurant.Save();

      List<Restaurant> testRestaurantList = new List<Restaurant>{firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurant();
    }

    [Fact]
    public void Test_Update_UpdatesCategoryInDatabase()
    {
      string name = "French";
      Cuisine testCuisine = new Cuisine(name);
      testCuisine.Save();
      string newName = "Frenchies";

      testCuisine.Update(newName);

      string result = testCuisine.GetName();

      Assert.Equal(newName, result);
    }





    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }

  }
}
