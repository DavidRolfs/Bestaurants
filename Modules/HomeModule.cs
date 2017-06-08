using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Bestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
// root -> index.cshtml
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

// cusines/add -> add-cuisine.cshtml
      Get["/cuisines/add"] = _ => {
        return View["add-cuisine.cshtml"];
      };

      Get["/cuisines/all"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["all-cuisine.cshtml", allCuisines];
      };

// cusines/add -> all-cuisine.cshtml
      Post["/cuisines/all"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["all-cuisine.cshtml", allCuisines];
      };

// restaurants/add -> add-restaurants.cshtml
      Get["/restaurants/add"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["add-restaurants.cshtml", allCuisines];
      };

// restaurants/add -> all-restaurants.cshtml
      Post["/restaurants/all"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["index.cshtml"];
      };

// cuisint/{id} -> cuisine-restaurants.cshtml
      Get["/cuisines/{id}"] = parameter => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine SelectedCuisine = Cuisine.Find(parameter.id);
        List<Restaurant> CuisineRestaurants = SelectedCuisine.GetRestaurant();
        model.Add("Cuisine", SelectedCuisine);
        model.Add("restaurants", CuisineRestaurants);
        return View["cuisine-restaurants.cshtml", model];
      };

// cuisine/delete -> cuisine-delete.cshtml
      Post["/cuisines/delete"] = _ => {
        Cuisine.DeleteAll();
        return View["deleteAll-confirm.cshtml"];
      };

// cusine/edit/{id} -> edit-cuisine.cshtml
      Get["cuisine/edit/{id}"] = parameter => {
        Cuisine SelectedCuisine = Cuisine.Find(parameter.id);
        return View["edit-cuisine.cshtml", SelectedCuisine];
      };

// cusine/edit/{id} -> index.cshtml
      Patch["cuisine/edit/{id}"] = parameter => {
        Cuisine SelectedCuisine = Cuisine.Find(parameter.id);
        SelectedCuisine.Update(Request.Form["edit-cuisine"]);
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["all-cuisine.cshtml", allCuisines];
      };

// cuisine/delete/{id} -> cuisine-delete.cshtml
      Get["cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine-delete.cshtml", SelectedCuisine];
      };

// cuisine/delete/{id} -> index.cshtml
      Delete["cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Delete();
        return View["index.cshtml"];
      };

    }
  }
}
