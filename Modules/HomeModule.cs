using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Bestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/cuisines/add"] = _ => {
        return View["add-cuisine.cshtml"];
      };
      Post["/cuisines/all"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["all-cuisine.cshtml", allCuisines];
      };
    }
  }
}
