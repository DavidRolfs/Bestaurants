using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Bestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
//  root -> index
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

//  cusines/add -> add-cuisine
      Get["/cuisines/add"] = _ => {
        return View["add-cuisine.cshtml"];
      };

      Get["/cuisines/all"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["all-cuisine.cshtml", allCuisines];
      };

//  cusines/add -> all-cuisine
      Post["/cuisines/all"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["all-cuisine.cshtml", allCuisines];
      };

//    restaurants/add -> add-restaurants
      Get["/restaurants/add"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["add-restaurants.cshtml", allCuisines];
      };

//    restaurants/add -> all-restaurants
      Post["/restaurants/all"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["index.cshtml"];
      };
      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        List<Restaurant> CuisineRestaurants = SelectedCuisine.GetRestaurant();
        model.Add("Cuisine", SelectedCuisine);
        model.Add("restaurants", CuisineRestaurants);
        return View["cuisine-restaurants.cshtml", model];
      };
      Post["/cuisines/delete"] = _ => {
        Cuisine.DeleteAll();
        return View["cuisine-delete.cshtml"];
      };
    }
  }
}
