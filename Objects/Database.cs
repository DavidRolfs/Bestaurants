
using System.Data;
using System.Data.SqlClient;

namespace Bestaurants
{
  public class DB
  {
    //  This creates a new SqlConnection object named "conn" that uses the
    //  connection styring located in Startup.cs (line 42)
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.ConnectionString);
      return conn;
    } //  When communication with the database is needed in the application,
      //  all that is needed is to call "DB.Connection()".
  }
}
