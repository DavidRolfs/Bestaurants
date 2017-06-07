using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Bestaurants
{
  public class RestaurantsTest : IDisposable
  {
    public RestaurantsTest()
    { //  This tells the application where to find the test database.
      //  This overrides "DBConfiguration.ConnectionString" in Startup.cs.
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Bestaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmpty_True()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_NamesAreTheSame_True()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Frenchies", 1);
      Restaurant secondRestaurant = new Restaurant("Frenchies", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_SavesToDatabase_True()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Frenchies", 1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Frenchies", 1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
    }

  }
}
