// DTO for displaying food items

// ViewModel for search filters + results
using FoodieMVC.DTO;

public class Search
{
    // Dropdown lists
    public List<string> Cities { get; set; }
    public List<string> Restaurants { get; set; }
    public List<string> PostalCodes { get; set; }
    public List<string> Locations { get; set; }

    // Selected filter values
    public string SelectedCity { get; set; }
    public string SelectedRestaurant { get; set; }
    public string SelectedPostalCode { get; set; }
    public string SelectedLocation { get; set; }

    // Search results
    public List<FoodItemDTO> result { get; set; }
}
