using Microsoft.AspNetCore.Mvc;
using KEStoreApi.Bussiness;
using KEStoreApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KEStoreApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private StoreLogic storeLogic = new StoreLogic();

        [HttpPost("cart")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCartAsync([FromBody] CartRequest request)
        {
            if (request == null || request.Cart == null)
            {
                return BadRequest(new { error = "The request and Cart fields are required." });
            }

            var cart = request.Cart;

            bool isCartEmpty = cart.Product == null || cart.Product.Count == 0;
            if (isCartEmpty)
            {
                return BadRequest(new { error = "El carrito debe contener al menos un producto." });
            }

            if (!StoreLogic.IsValidAddress(cart.Address))
            {
                return BadRequest(new { error = "La dirección de entrega no es válida." });
            }

            try
            {
                var saleTask = storeLogic.PurchaseAsync(cart);
                var sale = await saleTask;
                var purchaseNumber = sale.PurchaseNumber;
                var response = new { purchaseNumber = purchaseNumber };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    public class CartRequest
    {
        public Cart Cart { get; set; }
    }
}
