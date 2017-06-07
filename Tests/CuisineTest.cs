using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Bestaurants
{
  public class CuisineTest
  // : IDisposable
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



    // public void Dispose()
    // {
    //   Cuisine.DeleteAll();
    // }

  }
}
