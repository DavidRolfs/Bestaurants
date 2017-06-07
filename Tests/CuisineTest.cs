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
      //Arrange, Act
      Cuisine testCuisine = new Cuisine("French");
      testCuisine.Save();
      Console.WriteLine("Id: {0} Name: {1}", testCuisine.GetId(), testCuisine.GetName());
      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};
      Console.WriteLine("Id: {0} Name: {1}", result[0].GetId(), result[0].GetName());

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


    public void Dispose()
    {
      Cuisine.DeleteAll();
    }

  }
}
