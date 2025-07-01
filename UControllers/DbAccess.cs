using FoodieApp.DTO;
using FoodieApp.Models;
using Microsoft.EntityFrameworkCore;



namespace FoodieApp.DBAccess
{
    public class DbAccess
    {
        private readonly FoodieDbContext context;

        public DbAccess(FoodieDbContext dbContext)
        {
            context = dbContext;
        }
        //-------------User-----------
        public bool AddUser(UserDTO data)
        {
            var user = new User
            {
                FirstName = data.firstName,
                LastName = data.lastName,
                Email = data.email,
                PhoneNumber = data.phoneNumber,
                UserRole = data.userRole,
                Password = data.password
            };

            context.Users.Add(user);
            context.SaveChanges();

            return true;
        }

        // Get all users as a list of UserDTO
        public List<UserDTO> GetAllUsers()
        {
            return context.Users
                .Select(u => new UserDTO
                {
                    userId = u.UserId,
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    email = u.Email,
                    phoneNumber = u.PhoneNumber,
                    password = u.Password,
                    userRole = u.UserRole
                })
                .ToList();

        }

        // Get a single user by id, return UserDTO or null if not found
        public UserDTO GetUserById(int id)
        {
            var user = context.Users.Find(id);
            if (user == null) return null;

            return new UserDTO
            {
                userId = user.UserId,
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                phoneNumber = user.PhoneNumber,
                password = user.Password,
                userRole = user.UserRole
            };
        }

        // Update user by id, return true if success, false if not found
        public bool UpdateUser(int id, UserDTO data)
        {
            var user = context.Users.Find(id);
            if (user == null) return false;

            user.FirstName = data.firstName;
            user.LastName = data.lastName;
            user.Email = data.email;
            user.PhoneNumber = data.phoneNumber;
            user.Password = user.Password;

            user.UserRole = data.userRole;

            context.SaveChanges();
            return true;
        }

        // Delete user by id, return true if success, false if not found
        public bool DeleteUser(int id)
        {
            var user = context.Users.Find(id);
            if (user == null) return false;

            context.Users.Remove(user);
            context.SaveChanges();
            return true;
        }
        //---------------------Restaurant------------------------------------


        public bool AddRestaurant(RestaurantDTO data, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!context.Locations.Any(l => l.LocationId == data.RestLocId))
            {
                errorMessage = "Location not found";
                return false;
            }

            var rest = new Restaurant
            {
                RestaurantName = data.RestaurantName,
                RestaurantAddress = data.RestaurantAddress,
                Likes = data.Likes,
                Rating = data.Rating,
                OpeningTime = data.OpeningTime,
                CloseTime = data.CloseTime,
                RestLocId = data.RestLocId
            };

            context.Restaurants.Add(rest);
            context.SaveChanges();

            return true;
        }

        public List<RestaurantDTO> GetAllRestaurants()
        {
            return context.Restaurants.Select(r => new RestaurantDTO
            {
                RestaurantId = r.RestaurantId,
                RestaurantName = r.RestaurantName,
                RestaurantAddress = r.RestaurantAddress,
                Likes = r.Likes,
                Rating = r.Rating,
                OpeningTime = r.OpeningTime,
                CloseTime = r.CloseTime,
                RestLocId = r.RestLocId
            }).ToList();
        }

        public RestaurantDTO GetRestaurantById(int id)
        {
            var r = context.Restaurants.Find(id);
            if (r == null) return null;

            return new RestaurantDTO
            {
                RestaurantId = r.RestaurantId,
                RestaurantName = r.RestaurantName,
                RestaurantAddress = r.RestaurantAddress,
                Likes = r.Likes,
                Rating = r.Rating,
                OpeningTime = r.OpeningTime,
                CloseTime = r.CloseTime,
                RestLocId = r.RestLocId
            };
        }

        public bool UpdateRestaurant(int id, RestaurantDTO data)
        {
            var r = context.Restaurants.Find(id);
            if (r == null) return false;

            r.RestaurantName = data.RestaurantName;
            r.RestaurantAddress = data.RestaurantAddress;
            r.Likes = data.Likes;
            r.Rating = data.Rating;
            r.OpeningTime = data.OpeningTime;
            r.CloseTime = data.CloseTime;
            r.RestLocId = data.RestLocId;

            context.SaveChanges();
            return true;
        }

        public bool DeleteRestaurant(int id)
        {
            var r = context.Restaurants.Find(id);
            if (r == null) return false;

            context.Restaurants.Remove(r);
            context.SaveChanges();
            return true;
        }

        //-----------------------------Locations----------------------------

        public bool AddLocation(LocationDTO data)
        {
            var existingLocation = context.Locations
            .FirstOrDefault(l => l.City == data.City && l.Area == data.Area);

            if (existingLocation != null)
            {
                
                return false;
            }

            var location = new Location
            {
                City = data.City,
                Area = data.Area,
                PostalCode = data.PostalCode
            };

            context.Locations.Add(location);
            context.SaveChanges();
            return true;
        }

        // Get all Locations
        public List<LocationDTO> GetAllLocations()
        {
            return context.Locations.Select(l => new LocationDTO
            {
                LocationId = l.LocationId,
                City = l.City,
                Area = l.Area,
                PostalCode = l.PostalCode
            }).ToList();
        }

        // Get Location by ID
        public LocationDTO GetLocationById(int id)
        {
            var loc = context.Locations.Find(id);
            if (loc == null) return null;

            return new LocationDTO
            {
                LocationId = loc.LocationId,
                City = loc.City,
                Area = loc.Area,
                PostalCode = loc.PostalCode
            };
        }

        // Update Location
        public bool UpdateLocation(int id, LocationDTO data)
        {
            var loc = context.Locations.Find(id);
            if (loc == null) return false;

            loc.City = data.City;
            loc.Area = data.Area;
            loc.PostalCode = data.PostalCode;

            context.SaveChanges();
            return true;
        }

        // Delete Location
        public bool DeleteLocation(int id)
        {
            var loc = context.Locations.Find(id);
            if (loc == null) return false;

            context.Locations.Remove(loc);
            context.SaveChanges();
            return true;
        }
        ///------------------------------------Address--------------------------
        public bool AddAddress(AddressDTO data)
        {
            var address = new Address
            {
                Street = data.Street,
                City = data.City,
                PostalCode = data.PostalCode,
                UserId = data.UserId
            };
            context.Addressesss.Add(address);
            context.SaveChanges();
            return true;
        }

        // Update Address by UserId (update first matching address)
        public bool UpdateAddressByUserId(int userId, AddressDTO data)
        {
            var address = context.Addressesss.FirstOrDefault(a => a.UserId == userId);
            if (address == null)
                return false;

            address.Street = data.Street;
            address.City = data.City;
            address.PostalCode = data.PostalCode;

            context.SaveChanges();
            return true;
        }

        // Get all addresses
        public List<AddressDTO> GetAllAddresses()
        {
            return context.Addressesss.Select(a => new AddressDTO
            {
                AddressId = a.AddressId,
                Street = a.Street,
                City = a.City,
                PostalCode = a.PostalCode,
                UserId = a.UserId
            }).ToList();
        }

        // Get address by UserId
        public AddressDTO GetAddressByUserId(int userId)
        {
            var address = context.Addressesss.FirstOrDefault(a => a.UserId == userId);
            if (address == null) return null;

            return new AddressDTO
            {
                AddressId = address.AddressId,
                Street = address.Street,
                City = address.City,
                PostalCode = address.PostalCode,
                UserId = address.UserId
            };
        }

        // Delete Address by AddressId
        public bool DeleteAddress(int addressId)
        {
            var address = context.Addressesss.Find(addressId);
            if (address == null)
                return false;

            context.Addressesss.Remove(address);
            context.SaveChanges();
            return true;
        }

        //------------------------FoodItems-------------------------------
        public FoodItem AddFoodItem(FoodItem foodItem)
        {
            context.FoodItems.Add(foodItem);
            context.SaveChanges();
            return foodItem;
        }
        public List<FoodItem> GetFoodItemsByRestaurant(int restaurantId)
        {
            return context.FoodItems
                .Where(f => f.RestaurantId == restaurantId)
                .ToList();
        }

        public FoodItem GetFoodItem(int restaurantId, int foodItemId)
        {
            return context.FoodItems
                .FirstOrDefault(f => f.RestaurantId == restaurantId && f.FoodItemId == foodItemId);
        }


        public bool UpdateFoodItem(FoodItem updatedItem)
        {
            var existing = context.FoodItems
                .FirstOrDefault(f => f.FoodItemId == updatedItem.FoodItemId && f.RestaurantId == updatedItem.RestaurantId);

            if (existing == null)
                return false;

            existing.Name = updatedItem.Name;
            existing.Description = updatedItem.Description;
            existing.Price = updatedItem.Price;

            context.SaveChanges();

            return true;
        }


        public bool DeleteFoodItem(int restaurantId, int foodItemId)
        {
            var existing = context.FoodItems
                .FirstOrDefault(f => f.RestaurantId == restaurantId && f.FoodItemId == foodItemId);

            if (existing == null)
                return false;

            context.FoodItems.Remove(existing);
            context.SaveChanges();
            return true;
        }


        //-------------------------OrderDetails------------------------
        public OrderDetailsDTO AddOrder(OrderDetailsDTO order)
        {
            if (order == null || order.OrderLineItems == null || !order.OrderLineItems.Any())
                return null;

            double orderTotal = 0;

            var ord = new Order
            {
                OrderDate = order.OrderDate,
                Discount = order.Discount,
                gst = order.Gst,
                OrderStatus = order.OrderStatus,
                deliveredBy = order.DeliveredBy,
                UserId = order.UserId,
                AddressId = order.AddressId,
                RestId = order.RestId,
                ScheduleDeliveryAt = order.ScheduleDeliveryAt,
                orderLineItems = new List<OrderLineItem>()
            };

            foreach (var lidto in order.OrderLineItems)
            {
                double subtotal = lidto.Qty * lidto.UnitPrice;
                orderTotal += subtotal;

                var lineItem = new OrderLineItem
                {
                    Quantity = lidto.Qty,
                    FoodItemId = lidto.FoodItemId
                };

                ord.orderLineItems.Add(lineItem);
            }

            ord.OrderTotal = orderTotal;
            ord.FinalAmount = orderTotal - ord.Discount + ord.gst;

            context.Orders.Add(ord);
            context.SaveChanges(); // OrderId gets generated here

        

            return new OrderDetailsDTO
            {
                OrderId = ord.OrderId,
                OrderDate = ord.OrderDate,
                OrderTotal = ord.OrderTotal,
                Discount = ord.Discount,
                Gst = ord.gst,
                FinalAmount = ord.FinalAmount,
                OrderStatus = ord.OrderStatus,
                DeliveredBy = ord.deliveredBy,
                UserId = ord.UserId,
                AddressId = ord.AddressId,
                RestId = ord.RestId,
                ScheduleDeliveryAt = ord.ScheduleDeliveryAt,
                OrderLineItems = order.OrderLineItems // you can map from DB if needed
    };
}

        
        public OrderDetailsDTO GetOrder(int orderId)
        {
            var order = context.Orders
                .Include(o => o.orderLineItems)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null) return null;

            return new OrderDetailsDTO
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Discount = order.Discount,
                Gst = order.gst,
                FinalAmount = order.FinalAmount,
                OrderStatus = order.OrderStatus,
                DeliveredBy = order.deliveredBy,
                UserId = order.UserId,
                AddressId = order.AddressId,
                RestId = order.RestId,
                ScheduleDeliveryAt = order.ScheduleDeliveryAt,
                OrderTotal = order.OrderTotal,
                OrderLineItems = order.orderLineItems.Select(oli => new OrderLineItemDTO
                {
                    FoodItemId = oli.FoodItemId,
                    Qty = oli.Quantity,
                    UnitPrice = 0 // Set if you track price per item in orderLineItems or FoodItem entity
                }).ToList()
            };
        }


        public bool UpdateOrder(OrderDetailsDTO data)
        {
            var order = context.Orders
                .Include(o => o.orderLineItems)
                .FirstOrDefault(o => o.OrderId == data.OrderId);

            if (order == null) return false;

            // Update core order fields
            order.OrderDate = data.OrderDate;
            order.Discount = data.Discount;
            order.gst = data.Gst;
            order.OrderStatus = data.OrderStatus;
            order.deliveredBy = data.DeliveredBy;
            order.UserId = data.UserId;
            order.AddressId = data.AddressId;
            order.RestId = data.RestId;
            order.ScheduleDeliveryAt = data.ScheduleDeliveryAt;

            // Remove existing line items
            context.OrderLines.RemoveRange(order.orderLineItems);

            // Add updated line items
            order.orderLineItems = new List<OrderLineItem>();
            double total = 0;

            foreach (var item in data.OrderLineItems)
            {
                double subtotal = item.Qty * item.UnitPrice;
                total += subtotal;

                order.orderLineItems.Add(new OrderLineItem
                {
                    FoodItemId = item.FoodItemId,
                    Quantity = item.Qty,
                    //UnitPrice = item.UnitPrice
                });
            }

            order.OrderTotal = total;
            order.FinalAmount = total - order.Discount + order.gst;

            context.SaveChanges();
            return true;
        }

        public bool DeleteOrder(int orderId)
        {
            var order = context.Orders
                .Include(o => o.orderLineItems)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order == null) return false;

            // Step 1: Remove line items
            context.OrderLines.RemoveRange(order.orderLineItems);

            // Step 2: Remove order itself
            context.Orders.Remove(order);

            context.SaveChanges();
            return true;
        }



        //==================================Review======================================

        public bool AddReview(ReviewDTO data)   
        {
            var review = new Review
            {
                Rating = data.Rating,
                CreatedAt = data.CreatedAt,
                Comments = data.Comments,
                UserId = data.UserId,
                OrderId = data.OrderId,
                FoodItemId = data.FoodItemId
            };
            context.Reviews.Add(review);
            context.SaveChanges();
            return true;

        }

        public List<ReviewDTO> GetAllReviews()  
        {
            return context.Reviews
                .Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId,
                    Rating = r.Rating,
                    CreatedAt = r.CreatedAt,
                    Comments = r.Comments,
                    UserId = r.UserId,
                    OrderId = r.OrderId,
                    FoodItemId = r.FoodItemId
                }).ToList();
        }


        public ReviewDTO GetReviewsByFoodItem(int foodItemId)
        {
            var review = context.Reviews.FirstOrDefault(r => r.ReviewId == foodItemId);
            if (review == null)
                return null;

            return new ReviewDTO
            {
                ReviewId = review.ReviewId,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt,
                Comments = review.Comments,
                UserId = review.UserId,
                OrderId = review.OrderId,
                FoodItemId = review.FoodItemId
            };


        }

        public bool UpdateReviewByFoodItemId(ReviewDTO data)
        {
            var review = context.Reviews.FirstOrDefault(r => r.FoodItemId == data.FoodItemId);
            if (review == null) return false;

            review.Rating = data.Rating;
            review.Comments = data.Comments;
            review.CreatedAt = data.CreatedAt;
            review.UserId = data.UserId;
            review.OrderId = data.OrderId;
            context.SaveChanges();
             return true;
        }

        public bool DeleteReviewByFoodItemId(int foodItemId)
        {
            var review = context.Reviews.FirstOrDefault(r => r.FoodItemId == foodItemId);
            if (review == null) return false;

            context.Reviews.Remove(review);
            context.SaveChanges();
            return true;
        }




        //============================payment===============================
        public bool AddPayment(PaymentDTO data)
        {
            var payment = new Payment
            {
                PaymentMethod = data.PaymentMethod,
                Amount = data.Amount,
                PaymentStatus = data.PaymentStatus,
                OrderId = data.OrderId
            };

            context.Payments.Add(payment);
            context.SaveChanges();
            return true;
        }

        public PaymentDTO GetPaymentByOrderId(int orderId)
        {
            var payment = context.Payments.FirstOrDefault(p => p.OrderId == orderId);
            if (payment == null) return null;

            return new PaymentDTO
            {
                PaymentId = payment.PaymentId,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                OrderId = payment.OrderId
            };
        }

        public List<PaymentDTO> GetAllPayments()
        {
            return context.Payments.Select(p => new PaymentDTO
            {
                PaymentId = p.PaymentId,
                PaymentMethod = p.PaymentMethod,
                Amount = p.Amount,
                PaymentStatus = p.PaymentStatus,
                OrderId = p.OrderId
            }).ToList();
        }


        public bool UpdatePaymentByOrderId(PaymentDTO data)
        {
            var payment = context.Payments.FirstOrDefault(p => p.OrderId == data.OrderId);
            if (payment == null) return false;

            payment.PaymentMethod = data.PaymentMethod;
            payment.Amount = data.Amount;
            payment.PaymentStatus = data.PaymentStatus;

            context.SaveChanges();
            return true;
        }


        public bool DeletePaymentByOrderId(int orderId)
        {
            var payment = context.Payments.FirstOrDefault(p => p.OrderId == orderId);
            if (payment == null) return false;

            context.Payments.Remove(payment);
            context.SaveChanges();
            return true;
        }



    }


}




