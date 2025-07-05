using eTickets.V8.Data.Cart;
using eTickets.V8.Data.Services;
using eTickets.V8.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.V8.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };

            return View(response);
        }


        //menggunakan RedirectToActionResult, jika sudah tau bahwa return pasti hanya menggunakan RedirectToActionResult, gabakal diubah untuk kedepannya
        public async Task<RedirectToActionResult> AddToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null) { 
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }
    }
}
