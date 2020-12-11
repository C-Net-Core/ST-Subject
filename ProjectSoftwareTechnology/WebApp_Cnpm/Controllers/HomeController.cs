
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp_Cnpm.Common;
using WebApp_Cnpm.Models;
using WebApp_Cnpm.Models.Helper;

namespace WebApp_Cnpm.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly IAdminDTOService adminService;
        private readonly IAccountDTOService accountService;
        private readonly IProductDTOService productService;
        private readonly IProductHasDeletedDTOService productHasDeletedDTOService;
        private readonly IProductTypeDTOService productTypeService;
        private readonly ISaleDTOService saleService;
        private readonly IRegionDTOService regionService;
        private readonly IProvinceDTOService provinceService;
        private readonly IWardDTOService wardService;
        private readonly IPayOrderDTOService payOrderService;
        private readonly IInfoOrderDTOService infoOrderService;
        private readonly IPayOrderCancelDTOService payOrderCancelService;
        private readonly IFunctionDTOService functionService;
        private readonly IAuthorizationDTOService authorizationService;
        #endregion

        #region Constructor
        public HomeController(IAdminDTOService adminService, IAccountDTOService accountService, IProductDTOService productService,
            IProductHasDeletedDTOService productHasDeletedDTOService, IProductTypeDTOService productTypeService,
            ISaleDTOService saleService, IRegionDTOService regionService, IProvinceDTOService provinceService,
            IWardDTOService wardService, IPayOrderDTOService payOrderService, IInfoOrderDTOService infoOrderService,
            IPayOrderCancelDTOService payOrderCancelService, IFunctionDTOService functionService,
            IAuthorizationDTOService authorizationService)
        {
            this.adminService = adminService;
            this.functionService = functionService;
            this.authorizationService = authorizationService;
            this.accountService = accountService;
            this.productService = productService;
            this.productHasDeletedDTOService = productHasDeletedDTOService;
            this.productTypeService = productTypeService;
            this.saleService = saleService;
            this.regionService = regionService;
            this.provinceService = provinceService;
            this.wardService = wardService;
            this.payOrderService = payOrderService;
            this.infoOrderService = infoOrderService;
            this.payOrderCancelService = payOrderCancelService;
        }
        #endregion

        #region Home page
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();

            ViewData["SESSION_LOGIN"] = "not null";
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            var SESSION_CART = HttpContext.Session.GetString("cart");

            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
                return View();
            }
            else
            {
                ViewData["SESSION_LOGIN"] = "not null";
                ViewData["COOKIE_USER"] = TempData.Peek("username");
                if (TempData.Peek("admin") != null)
                {
                    TempData["authorAdmin"] = TempData.Peek("admin");
                }
                return View();
            }
        }
        #endregion

        #region Product
        [AllowAnonymous]
        public IActionResult Shop_Grid(int id, String search, String maxPrice, String minPrice)
        {
            #region common
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            else
            {
                ViewData["SESSION_LOGIN"] = "not null";
                ViewData["COOKIE_USER"] = TempData.Peek("username");
                if (TempData.Peek("admin") != null)
                {
                    TempData["authorAdmin"] = TempData.Peek("admin");
                }
            }
            #endregion

            #region get products
            List<ProductDTO> products;
            if (search != null || search == "")
            {
                products = productService.GetAllProduct()
                                                    .Where(p => (p.Name.ToLower()).Contains(search.ToLower()))
                                                    .ToList();
            }
            else
            {
                if ((minPrice != null && maxPrice != null) || (minPrice == "" && maxPrice == ""))
                {
                    products = productService.GetAllProduct()
                                                    .Where(p => double.Parse(p.Cost) >= double.Parse(minPrice)
                                                                && double.Parse(p.Cost) <= double.Parse(maxPrice))
                                                    .ToList();
                }
                else
                {
                    products = productService.GetAllProduct()
                                                    .Where(p => p.IDLSP == id)
                                                    .ToList();
                }
            }
            return View(products);
            #endregion
        }
        #endregion

        #region Detail product
        [AllowAnonymous]
        public IActionResult Shop_Detail(int id)
        {
            ViewData["sp"] = productService.GetProductByID(id);
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();
            ViewData["productType"] = productTypeService.GetAllProductType();

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            else
            {
                ViewData["SESSION_LOGIN"] = "not null";
                ViewData["COOKIE_USER"] = TempData.Peek("username");
                if (TempData.Peek("admin") != null)
                {
                    TempData["authorAdmin"] = TempData.Peek("admin");
                }
            }


            if (string.IsNullOrEmpty(id.ToString()))
            {
                Response.Redirect("/Home/Shop_Grid");
            }
            if (ViewData["sp"] == null)
            {
                Response.Redirect("/Home/Shop_Grid");
            }
            return View();
        }
        #endregion

        #region Checkout & card
        // trang thanh toán
        [AllowAnonymous]
        public IActionResult Checkout()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();
            ViewData["province"] = provinceService.GetAllProvince();
            ViewData["ward"] = wardService.GetAllWard();

            var SESSION_CART = HttpContext.Session.GetString("cart");   //get key cart
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login

            if (TempData.Peek("username") == null)
            {
                if (SESSION_CART == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                if (SESSION_CART == null)
                {
                    return RedirectToAction(nameof(Shop_Grid));
                }
                else
                {
                    int tongtien = 0;
                    int dem = 0;
                    List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                    tongtien = dataCart.Sum(t => t.Total);
                    dem = dataCart.Count();

                    if (dem > 0)
                    {
                        ViewData["total_cart"] = tongtien;
                        ViewData["dem_item_cart"] = dem;

                        ViewBag.carts = dataCart;
                        ViewData["SESSION_LOGIN"] = "not null";
                        ViewData["COOKIE_USER"] = TempData.Peek("username");
                        if (TempData.Peek("admin") != null)
                        {
                            TempData["authorAdmin"] = TempData.Peek("admin");
                        }
                        return View();
                    }
                }
            }
            return RedirectToAction(nameof(Shop_Grid));
        }

        // trang giỏ hàng   
        [AllowAnonymous]
        public IActionResult Cart()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();

            var SESSION_CART = HttpContext.Session.GetString("cart");   //get key cart
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login

            if (TempData.Peek("username") == null)
            {
                if (SESSION_CART == null)
                {
                    ViewData["total_cart"] = 0;
                    ViewData["dem_item_cart"] = 0;

                    ViewBag.carts = null;
                    ViewData["SESSION_LOGIN"] = "null";
                    return View();
                }
                else
                {
                    int tongtien = 0;
                    int dem = 0;
                    List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                    tongtien = dataCart.Sum(t => t.Total);
                    dem = dataCart.Count();

                    if (dem > 0)
                    {
                        ViewData["total_cart"] = tongtien;
                        ViewData["dem_item_cart"] = dem;

                        ViewBag.carts = dataCart;
                        ViewData["SESSION_LOGIN"] = "null";
                        return View();
                    }
                }
            }
            else
            {
                if (SESSION_CART == null)
                {
                    ViewData["total_cart"] = 0;
                    ViewData["dem_item_cart"] = 0;

                    ViewBag.carts = null;
                    ViewData["SESSION_LOGIN"] = "not null";
                    ViewData["COOKIE_USER"] = TempData.Peek("username");
                    if (TempData.Peek("admin") != null)
                    {
                        TempData["authorAdmin"] = TempData.Peek("admin");
                    }
                    return View();
                }
                else
                {
                    int tongtien = 0;
                    int dem = 0;
                    List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                    tongtien = dataCart.Sum(t => t.Total);
                    dem = dataCart.Count();

                    if (dem > 0)
                    {
                        ViewData["total_cart"] = tongtien;
                        ViewData["dem_item_cart"] = dem;

                        ViewBag.carts = dataCart;
                        ViewData["SESSION_LOGIN"] = "not null";
                        ViewData["COOKIE_USER"] = TempData.Peek("username");
                        if (TempData.Peek("admin") != null)
                        {
                            TempData["authorAdmin"] = TempData.Peek("admin");
                        }
                        return View();
                    }
                }
            }
            return View();
        }

        // xử lý thêm vào giỏ hàng
        [AllowAnonymous]
        public IActionResult AddtoCart(int id)
        {
            var Session = HttpContext.Session;
            //var SESSION_LOGIN = Session.GetString("login"); // get ket login
            var SESSION_CART = Session.GetString("cart");   //get key cart

            ProductDTO sp = productService.GetProductByID(id);
            SaleDTO s = saleService.GetSaleByID(sp.IDKM);

            double cost = int.Parse(sp.Cost) - int.Parse(sp.Cost) * int.Parse(s.phantram) * 0.01;

            if (SESSION_CART == null)
            {
                // Lấy ra thông tin của sản phẩm khi click thêm vào giỏ hàng
                List<CartItems> list_cart = new List<CartItems>()
                {
                    new CartItems
                    {
                        Img = sp.Image,
                        Idsp = sp.IDSP,
                        Name = sp.Name,
                        Size = sp.Size,
                        Cost = cost.ToString(),
                        Amount = 1
                    }
                };
                Session.SetString("cart", JsonConvert.SerializeObject(list_cart));

                // nếu ko tồn tại thì trả về trang đăng nhập
                return RedirectToAction(nameof(Shop_Grid));
            }
            else
            {
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Idsp == id)
                    {
                        if ((dataCart[i].Amount + 1) > int.Parse(sp.Amount))
                        {
                            dataCart[i].Amount = int.Parse(sp.Amount);
                            check = false;
                        }
                        else
                        {
                            dataCart[i].Amount++;
                            check = false;
                        }
                    }
                }

                if (check == true)
                {
                    dataCart.Add(new CartItems
                    {
                        Img = sp.Image,
                        Idsp = sp.IDSP,
                        Name = sp.Name,
                        Size = sp.Size,
                        Cost = cost.ToString(),
                        Amount = 1
                    });
                }

                // cập nhật lại SESSION_CART
                Session.SetString("cart", JsonConvert.SerializeObject(dataCart));

                // nếu ko tồn tại thì trả về trang đăng nhập
                return RedirectToAction(nameof(Shop_Grid));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddtoCartInDetail([FromBody] DataAddtoCartInDetail data)
        {
            var idsp = data.idsp;
            var amountBuy = data.amountBuy;

            var Session = HttpContext.Session;
            //var SESSION_LOGIN = HttpContext.Session.GetString("login"); // get ket login
            var SESSION_CART = HttpContext.Session.GetString("cart");   //get key cart

            ProductDTO sp = productService.GetProductByID(idsp);
            SaleDTO s = saleService.GetSaleByID(sp.IDKM);

            double cost = int.Parse(sp.Cost) - int.Parse(sp.Cost) * int.Parse(s.phantram) * 0.01;

            if (SESSION_CART == null)
            {
                // Lấy ra thông tin của sản phẩm khi click thêm vào giỏ hàng
                List<CartItems> list_cart = new List<CartItems>()
                {
                    new CartItems
                    {
                        Img = sp.Image,
                        Idsp = sp.IDSP,
                        Name = sp.Name,
                        Size = sp.Size,
                        Cost = cost.ToString(),
                        Amount = int.Parse(amountBuy)
                    }
                };
                Session.SetString("cart", JsonConvert.SerializeObject(list_cart));
            }
            else
            {
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Idsp == idsp)
                    {
                        if ((dataCart[i].Amount + int.Parse(amountBuy)) > int.Parse(sp.Amount))
                        {
                            dataCart[i].Amount = int.Parse(sp.Amount);
                            check = false;
                        }
                        else
                        {
                            dataCart[i].Amount += int.Parse(amountBuy);
                            check = false;
                        }
                    }
                }
                if (check == true)
                {
                    dataCart.Add(new CartItems
                    {
                        Img = sp.Image,
                        Idsp = sp.IDSP,
                        Name = sp.Name,
                        Size = sp.Size,
                        Cost = cost.ToString(),
                        Amount = int.Parse(amountBuy)
                    });
                }

                // cập nhật lại SESSION_CART
                Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }

            return Json("Add to cart successed.");
        }

        // xóa sản phẩm trong giỏ hàng
        [HttpPost]
        [AllowAnonymous]
        public JsonResult RemoveItemInCart([FromBody] ValueID data)
        {
            var SESSION_CART = HttpContext.Session.GetString("cart");

            // lấy dữ liệu trong SESSION_CART ra đổ vào list
            List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);

            var idsp = data.idsp;
            if (dataCart.Count > 0)
            {
                //lấy ra item có idsp truyền vào
                CartItems item = dataCart.Where(c => c.Idsp == idsp).FirstOrDefault();

                // xóa đi item đó trong list Cart
                dataCart.Remove(item);

                // Cập nhật lại dữ liệu trong SESSION_CART
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            }
            return Json("Delete item seccessed.!!!");
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult UpdateCart([FromBody] ValueUpdateCart data)
        {
            int idsp = data.idsp;
            int amount = data.amount;
            var SESSION_CART = HttpContext.Session.GetString("cart");

            List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);

            ProductDTO sp = productService.GetProductByID(idsp);
            CartItems item = dataCart.Where(i => i.Idsp == idsp).FirstOrDefault();

            // Kiểm tra nếu số lượng mới tăng lớn hơn số lượng hiện có của sp thì sp trong giỏ hàng = số lượng hiện có của sp
            if (amount > int.Parse(sp.Amount))
            {
                item.Amount = int.Parse(sp.Amount);
            }
            else
            {
                item.Amount = amount;       // cập nhật số lượng mới cho sp đó
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
            return Json("OK");
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult CheckCart([FromBody] ValueTotal data)
        {
            int tongtien = 0;
            int total = data.Total;
            int dem = 0;
            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();
            }
            if (total == tongtien)
            {
                return Json("ok");
            }
            else
            {
                return Json("failt");
            }
        }
        #endregion

        #region Contact
        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();

            ViewData["SESSION_LOGIN"] = "not null";
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            var SESSION_CART = HttpContext.Session.GetString("cart");

            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
                return View();
            }
            else
            {
                ViewData["SESSION_LOGIN"] = "not null";
                ViewData["COOKIE_USER"] = TempData.Peek("username");
                if (TempData.Peek("admin") != null)
                {
                    TempData["authorAdmin"] = TempData.Peek("admin");
                }
                return View();
            }
        }
        #endregion

        #region Login - logout
        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();
            ViewData["sale"] = saleService.GetAllSale();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            #region using Session
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key cart
            //if (SESSION_LOGIN == null)
            //{
            //ViewData["SESSION_LOGIN"] = "null";
            //return View();
            //}
            #endregion

            #region using Cookie
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
                return View();
            }
            #endregion

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public void CheckLogin(String uid, String pwd)
        {
            if (String.IsNullOrEmpty(uid) || String.IsNullOrEmpty(pwd))
            {
                Response.Redirect("/Home/Login");
            }
            else
            {
                String pass = Encryptor.MD5Hash(pwd);
                AccountDTO acc = accountService.GetAllAccount()
                                            .Where(a => a.UID == uid && a.PW == pass && a.Status == "On")
                                            .FirstOrDefault();
                AdminDTO admin = adminService.GetAllAdmin()
                                            .Where(a => a.UID == uid && a.PW == pass && a.Status == "On")
                                            .FirstOrDefault();
                #region login customer
                if (acc != null)
                {
                    #region using Session
                    //var session = HttpContext.Session;
                    //var SESSION_LOGIN = session.GetString("login");

                    // chuyển đổi dữ liệu Object sang kiểu string của json rồi gán nó cho session[login]
                    //session.SetString("login", JsonConvert.SerializeObject(uid));
                    #endregion

                    #region using Cookie
                    var customerClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, acc.UID),
                        new Claim(ClaimTypes.Email, acc.Email),
                        new Claim(ClaimTypes.StreetAddress, acc.Address)
                    };

                    var customerIdentity = new ClaimsIdentity(customerClaims, "Customer Identity");

                    var customerPrincipal = new ClaimsPrincipal(new[] { customerIdentity });
                    //-----------------------------------------------------------
                    HttpContext.SignInAsync(customerPrincipal); // đăng nhập vào Cookie

                    // set value username
                    TempData["username"] = acc.UID;

                    #endregion

                    Response.Redirect("/Home/Index");
                }
                #endregion

                #region login admin

                if (admin != null)
                {
                    var roles = functionService.GetAll();
                    var authorRoles = authorizationService.GetAll();
                    #region using Cookie
                    var AuthorClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, admin.UID),
                        new Claim(ClaimTypes.DateOfBirth, "authorAdmin")
                    };

                    List<RoleMethod> roleMethods = new List<RoleMethod>();
                    foreach (var role in roles)
                    {
                        foreach (var authorRole in authorRoles)
                        {
                            if (authorRole.IDAdmin.Equals(admin.ID) && authorRole.IDCN.Equals(role.IDCN))
                            {
                                // add Claims
                                var claim = new Claim(ClaimTypes.Role, role.Name);
                                AuthorClaims.Add(claim);

                                // add roleMethod
                                roleMethods.Add(new RoleMethod { 
                                    Role = role.Name,
                                    Method = role.NameMethod
                                });
                            }
                        }
                    }

                    var AuthorIdentity = new ClaimsIdentity(AuthorClaims, "Author Identity");

                    var AuthorPrincipal = new ClaimsPrincipal(new[] { AuthorIdentity });
                    //-----------------------------------------------------------
                    HttpContext.SignInAsync(AuthorPrincipal); // đăng nhập vào Cookie

                    TempData["username"] = admin.UID;
                    TempData["admin"] = admin.UID;
                    TempData["roleMethod"] = JsonConvert.SerializeObject(roleMethods);

                    #endregion
                    Response.Redirect("/Home/Index");
                }

                #endregion
                else
                {
                    Response.Redirect("/Home/Login");
                }
            }
        }

        [AllowAnonymous]
        public void Logout()
        {
            #region using Session
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            //var SESSION_CART = HttpContext.Session.GetString("cart");

            //if (SESSION_LOGIN != null)
            //{
            //    if (SESSION_CART != null)
            //    {
            //        // xóa session cart
            //        HttpContext.Session.Remove("cart");
            //    }
            //    // xóa session login
            //    HttpContext.Session.Remove("login");

            //    // trả về trang index
            //    Response.Redirect("/Home/Index");
            //}
            #endregion

            #region using Cookie
            var SESSION_CART = HttpContext.Session.GetString("cart");

            if (TempData.Peek("username") != null)
            {
                if (SESSION_CART != null)
                {
                    // xóa session cart
                    HttpContext.Session.Remove("cart");
                }
                // xóa cookie login
                HttpContext.SignOutAsync();
                TempData.Remove("admin");
                TempData.Remove("username");
                TempData.Remove("roleMethod");

                // trả về trang index
                Response.Redirect("/Home/Index");
            }
            #endregion
        }
        #endregion

        #region Pay & info order
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ChangeWardWhenChangeProvince([FromBody] ValueIDProvinceStateChanged data)
        {
            int IDProvince = data.ID;
            IEnumerable<WardDTO> dataWards = wardService.GetAllWard()
                                                .Where(w => w.IdProvince == IDProvince)
                                                .ToList();
            return Json(dataWards);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetFeeShip([FromBody] ValueIDProvinceStateChanged data)
        {
            int IDProvince = data.ID;
            ProvinceDTO province = provinceService.GetProvinceByID(IDProvince);
            RegionDTO region = regionService.GetRegionByID(province.IdReion);
            return Json((region.FeeShip).ToString());
        }

        [HttpPost]
        [Authorize]
        public IActionResult PayOrder([FromBody] ValuePayOrder data)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            //String[] user = SESSION_LOGIN.Split('"');
            var email_user = accountService.getEmailUser(TempData.Peek("username").ToString());

            var SESSION_CART = HttpContext.Session.GetString("cart");

            List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);

            var IDprovince = data.IDprovince;
            //var UID = JsonConvert.DeserializeObject<String>(SESSION_LOGIN);
            var Receiver = data.FirstName + " " + data.LastName;

            var Phone = data.Phone;
            var DateOrder = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString();
            var Status = "Chờ xử lý";
            var StatusPay = "Chưa thanh toán";
            var Note = data.Note;

            // tính tổng tiền có kèm phí ship
            // B1: lấy ra thằng Province được lựa chọn
            ProvinceDTO province = provinceService.GetProvinceByID(IDprovince);
            var Address = data.Street + " Street, " + data.District + " Distrist, " + province.NameProvince + " City";

            // B2: kiểm tra province đó thuộc region nào, sau đó lấy ra region đó
            RegionDTO region = regionService.GetRegionByID(province.IdReion);

            // B3: còn lại chỉ việc cộng phí ship vô thôi...hehe
            var Total = dataCart.Sum(c => c.Total) + region.FeeShip;

            // =======================================================
            // thêm data vào table PayOrder
            PayOrderDTO payOrder = new PayOrderDTO();
            payOrder.UID = TempData.Peek("username").ToString();
            payOrder.Receiver = Receiver;
            payOrder.Address = Address;
            payOrder.Phone = Phone;
            payOrder.Total = Total.ToString();
            payOrder.DateOrder = DateOrder;
            payOrder.Status = Status;
            payOrder.StatusPay = StatusPay;
            payOrder.Note = Note;

            payOrderService.AddPayOrder(payOrder);

            #region send email
            string subject = "Your Order Infomation " + DateTime.Now.ToString();
            string text = "- Your Order ID: #" + payOrderService.GetAllPayOrder().Last().ID
                        + "\n- Customer: " + Receiver
                        + "\n- Address: " + Address
                        + "\n- Phone: " + Phone
                        + "\n- Total Money: " + Total.ToString("#,##0").Replace(",", ".") + " VNĐ"
                        + "\n- Date Order: " + DateTime.Now
                        + "\n- Status: Watting for processing"
                        + "\n- Status Pay: Unpaid "
                        + "\n- Note: " + Note;
            SendMail.sendMail(subject, email_user, text);
            #endregion

            // ========================================================================
            InfoOrderDTO infoOrder = new InfoOrderDTO();
            foreach (CartItems items in dataCart)
            {
                // thêm data vào table InfoOrder
                infoOrder.IDP = payOrderService.GetAllPayOrder().Last().ID;
                infoOrder.IDSP = items.Idsp;
                infoOrder.Amount = items.Amount.ToString();
                infoOrder.Size = items.Size;
                infoOrder.Price = items.Cost;
                infoOrder.IntoMoney = items.Total.ToString();

                infoOrderService.AddInfoOrder(infoOrder);
                // =====================================================================
                // cập nhật lại số lượng sản phẩm
                ProductDTO product = productService.GetProductByID(items.Idsp);
                product.Amount = (int.Parse(product.Amount) - items.Amount).ToString();

                productService.UpdateProduct(product);
            }

            //xóa session giỏ hàng sau khi đặt hàng thành công
            HttpContext.Session.Remove("cart");

            return Json("Order successfully.!!!");
        }

        [HttpPost]
        [Authorize]
        public IActionResult ToCancel([FromBody] ValueIDPayCancel data)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            //String[] user = SESSION_LOGIN.Split('"');
            var email_user = accountService.getEmailUser(TempData.Peek("username").ToString());

            int IDPAYCANCEL = data.IDPAYCANCEL;
            PayOrderDTO payOrder = payOrderService.GetPayOrderByID(IDPAYCANCEL);

            // cập nhật lại trạng thái đơn hàng thành 'đã hủy'
            payOrder.Status = "Đã hủy";
            payOrder.DateCancel = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString();
            payOrderService.UpdatePayOrder(payOrder);
            #region send email
            string subject = "Your Order Infomation Has Been Canceled " + DateTime.Now.ToString();
            string text = "- Your Order ID: #" + payOrderService.GetAllPayOrder().Last().ID
                        + "\n- Customer: " + payOrder.Receiver
                        + "\n- Address: " + payOrder.Address
                        + "\n- Phone: " + payOrder.Phone
                        + "\n- Total Money: " + int.Parse(payOrder.Total).ToString("#,##0").Replace(",", ".") + " VNĐ"
                        + "\n- Date Order: " + DateTime.Now
                        + "\n- Status: Canceled"
                        + "\n- Status Pay: Unpaid ";
            SendMail.sendMail(subject, email_user, text);
            #endregion

            // hoàn lại số lượng sản phẩm
            var infoOrderThisIDPs = infoOrderService.GetAllInfoOrder()
                                                        .Where(i => i.IDP == IDPAYCANCEL)
                                                        .ToList();
            foreach (var info in infoOrderThisIDPs)
            {
                var product = productService.GetProductByID(info.IDSP);
                // cộng dồn lại số lượng sản phẩm
                product.Amount = (int.Parse(product.Amount) + int.Parse(info.Amount)).ToString();
                // cập nhật lại sp
                productService.UpdateProduct(product);
            }

            // thêm đơn hàng hủy vào table quản lý các đơn hủy
            payOrderCancelService.AddPayOrderCancel(new PayOrderCancelDTO
            {
                IDP = payOrder.ID,
                UID = payOrder.UID,
                Receiver = payOrder.Receiver,
                Address = payOrder.Address,
                Phone = payOrder.Phone,
                Total = payOrder.Total,
                DateOrder = payOrder.DateOrder,
                DateCancel = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString(),
                Status = "Đã hủy",
                StatusPay = "Chưa thanh toán"
            });

            return Json("Your orders have been cancelled successfully.!!!");
        }

        [Authorize]
        public IActionResult History()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();
            ViewData["product"] = productService.GetAllProduct();

            ViewData["infoOrder"] = infoOrderService.GetAllInfoOrder();
            ViewData["payOrder"] = payOrderService.GetAllPayOrder();


            ViewData["SESSION_LOGIN"] = "not null";
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            var SESSION_CART = HttpContext.Session.GetString("cart");

            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["SESSION_LOGIN"] = "not null";
                //ViewData["UID"] = JsonConvert.DeserializeObject<String>(SESSION_LOGIN);
                ViewData["UID"] = TempData.Peek("username");
                ViewData["COOKIE_USER"] = TempData.Peek("username");
                if (TempData.Peek("admin") != null)
                {
                    TempData["authorAdmin"] = TempData.Peek("admin");
                }
                return View();
            }
        }

        [Authorize]
        public IActionResult DetailOrder(int id)
        {
            ViewData["productType"] = productTypeService.GetAllProductType();

            ViewData["SESSION_LOGIN"] = "not null";
            //var SESSION_LOGIN = HttpContext.Session.GetString("login");
            var SESSION_CART = HttpContext.Session.GetString("cart");

            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
                return RedirectToAction(nameof(Index));
            }

            ViewData["SESSION_LOGIN"] = "not null";
            ViewData["COOKIE_USER"] = TempData.Peek("username");
            if (TempData.Peek("admin") != null)
            {
                TempData["authorAdmin"] = TempData.Peek("admin");
            }

            PayOrderDTO payOrder = payOrderService.GetPayOrderByID(id);
            IEnumerable<InfoOrderDTO> listInfoOrder = infoOrderService.GetAllInfoOrder();
            List<DetailOrders> listDetailOrders = new List<DetailOrders>();
            foreach (InfoOrderDTO infoOrders in listInfoOrder)
            {
                if (infoOrders.IDP == id)
                {
                    DetailOrders detailOrder = new DetailOrders();
                    ProductDTO product = productService.GetProductByID(infoOrders.IDSP);

                    detailOrder.NameCustomer = payOrder.Receiver;
                    detailOrder.PhoneCustomer = payOrder.Phone;
                    detailOrder.AddressCustomer = payOrder.Address;
                    detailOrder.ID_PayOrder = payOrder.ID;
                    detailOrder.DateOrder = payOrder.DateOrder;
                    detailOrder.ImageProduct = product.Image;
                    detailOrder.NameProduct = product.Name;
                    detailOrder.PriceProduct = infoOrders.Price;
                    detailOrder.SizeProduct = infoOrders.Size;
                    detailOrder.AmountProduct = int.Parse(infoOrders.Amount);
                    var statusOrder = payOrder.Status;
                    if (statusOrder == "Chờ xử lý")
                    {
                        detailOrder.StatusOrder = "Waiting for progressing";
                    }
                    else
                    {
                        if (statusOrder == "Chờ giao hàng")
                        {
                            detailOrder.StatusOrder = "To ship";
                        }
                        else
                        {
                            if (statusOrder == "Chờ thanh toán")
                            {
                                detailOrder.StatusOrder = "To Pay";
                            }
                            else
                            {
                                if (statusOrder == "Đã giao hàng")
                                {
                                    detailOrder.StatusOrder = "Delivered";
                                }
                                else
                                {
                                    detailOrder.StatusOrder = "Cancelled";
                                }
                            }
                        }
                    }
                    var Total = infoOrderService.GetAllInfoOrder()
                                                .Where(s => s.IDP == id)
                                                .Sum(s => int.Parse(s.IntoMoney));

                    detailOrder.SubTotal = int.Parse(infoOrders.IntoMoney);
                    detailOrder.FeeShip = int.Parse(payOrder.Total) - Total;
                    detailOrder.Total = int.Parse(payOrder.Total);

                    listDetailOrders.Add(detailOrder);
                }
            }

            return View(listDetailOrders);
        }
        #endregion

        #region Register
        // b1
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }

            return View();
        }

        // b2
        public IActionResult RegisterConfirm(String user, String pwd, String repwd, String address, String email)
        {
            #region common
            ViewData["productType"] = productTypeService.GetAllProductType();
            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            #endregion
            AccountDTO acc = accountService.GetAllAccount()
                                            .Where(c => c.UID == user)
                                            .FirstOrDefault();

            if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(pwd)
                || String.IsNullOrEmpty(address) || String.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Register));
            }
            else
            {
                if (acc == null)
                {
                    if (pwd == repwd)
                    {
                        #region sendmail
                        Random random = new Random();
                        string verification = (random.Next(1000, 9999)).ToString();
                        string subject = "Verification Code";
                        string text = "- Your verification is: " + verification;
                        SendMail.sendMail(subject, email, text);
                        #endregion

                        NewRegister registerVM = new NewRegister
                        {
                            code = verification,
                            user = user,
                            password = pwd,
                            address = address,
                            email = email
                        };
                        return View(registerVM);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Register));
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Register));
                }
            }
        }

        //b3
        [HttpPost]
        [AllowAnonymous]
        public JsonResult CheckRegister([FromBody] NewRegister data)
        {
            if (data.code == data.verification)
            {
                AccountDTO ac = new AccountDTO();
                ac.UID = data.user;
                ac.PW = Encryptor.MD5Hash(data.password);
                ac.Status = "On";
                ac.Address = data.address;
                ac.Email = data.email;
                ac.DateActive = DateTime.Now.ToString();

                #region sendmail
                string subject = "Register Successed " + DateTime.Now;
                string text = "- Account " + data.user + " has been successfully registered at " + DateTime.Now;
                SendMail.sendMail(subject, data.email, text);
                #endregion

                accountService.AddAccount(ac);

                return Json("OK");
            }
            return Json("Fail");
        }

        // b4
        [AllowAnonymous]
        public IActionResult RegisterSuccessful()
        {
            ViewData["productType"] = productTypeService.GetAllProductType();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }

            return View();
        }
        #endregion

        #region Forgot Password
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            #region common
            ViewData["productType"] = productTypeService.GetAllProductType();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            #endregion
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CheckForgotPassword(string user, string email)
        {
            #region common
            ViewData["productType"] = productTypeService.GetAllProductType();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            #endregion

            if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(ForgotPassword));
            }
            else
            {
                var email_user = accountService.getEmailUser(user);
                if (email_user == null)
                {
                    return RedirectToAction(nameof(ForgotPassword));
                }
                else
                {
                    if (email_user != email)
                    {
                        return RedirectToAction(nameof(ForgotPassword));
                    }
                    else
                    {

                        #region sendmail
                        Random random = new Random();
                        string verification = (random.Next(1000, 9999)).ToString();
                        string subject = "Verification Code";
                        string text = "- Your verification is: " + verification;
                        SendMail.sendMail(subject, email, text);
                        #endregion

                        NewRegister viewModel = new NewRegister
                        {
                            user = user,
                            code = verification
                        };
                        return View(viewModel);
                    }
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult NewPassword(String user, String code, String verification)
        {
            #region common
            ViewData["productType"] = productTypeService.GetAllProductType();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            #endregion

            if (code == verification)
            {
                var viewModel = new NewRegister
                {
                    user = user
                };
                return View(viewModel);
            }
            return RedirectToAction(nameof(CheckForgotPassword));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult RePasswordSuccessful(string user, string newPw, string reNewPw)
        {
            #region common
            ViewData["productType"] = productTypeService.GetAllProductType();

            var SESSION_CART = HttpContext.Session.GetString("cart");
            if (SESSION_CART != null)
            {
                int tongtien = 0;
                int dem = 0;
                List<CartItems> dataCart = JsonConvert.DeserializeObject<List<CartItems>>(SESSION_CART);
                tongtien = dataCart.Sum(t => t.Total);
                dem = dataCart.Count();

                ViewData["total_cart"] = tongtien;
                ViewData["dem_item_cart"] = dem;
            }
            else
            {
                ViewData["total_cart"] = 0;
                ViewData["dem_item_cart"] = 0;
            }

            //var SESSION_LOGIN = HttpContext.Session.GetString("login");   //get key login
            if (TempData.Peek("username") == null)
            {
                ViewData["SESSION_LOGIN"] = "null";
            }
            #endregion
            if (newPw == reNewPw)
            {
                var acc = accountService.GetAllAccount().Where(a => a.UID == user).FirstOrDefault();
                acc.PW = Encryptor.MD5Hash(newPw);

                #region sendmail
                string subject = "Your password was successfully retrieved";
                string text = "- Account " + acc.UID + " was successfully retrieved at " + DateTime.Now;
                SendMail.sendMail(subject, acc.Email, text);
                #endregion

                accountService.UpdateAccount(acc);
                return View();
            }
            return RedirectToAction(nameof(NewPassword));
        }
        #endregion

        #region Chatbox

        [HttpPost]
        public IActionResult SendMessage([FromBody] KeyMessage keyData)
        {
            var session = HttpContext.Session;
            var session_message = session.GetString("message");
            if (session_message == null)
            {
                List<KeyMessage> dataMessage = new List<KeyMessage>();
                dataMessage.Add(new KeyMessage
                {
                    message = keyData.message,
                    author = "customer"
                });

                session.SetString("message", JsonConvert.SerializeObject(dataMessage));
            }
            else
            {
                List<KeyMessage> dataMessage = JsonConvert.DeserializeObject<List<KeyMessage>>(session_message);
                dataMessage.Add(new KeyMessage
                {
                    message = keyData.message,
                    author = "customer"
                });

                session.SetString("message", JsonConvert.SerializeObject(dataMessage));
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult CheckMessage([FromBody] KeyAmountElement keyData)
        {
            var n = keyData.amountElement;
            var session = HttpContext.Session;
            var session_message = session.GetString("message");
            if (session_message == null)
            {
                return Json("false");
            }
            else
            {
                List<KeyMessage> dataMessage = JsonConvert.DeserializeObject<List<KeyMessage>>(session_message);
                if (n < dataMessage.Count)
                {
                    string messageLast = dataMessage[dataMessage.Count - 1].message;

                    if (dataMessage[dataMessage.Count - 1].author == "customer")
                    {
                        string htmlData = "<div class='msg'><span id='msg'>" + messageLast + "</spand></div>";
                        return Json(htmlData);
                    }
                    else
                    {
                        string htmlData = "<div class='msg'><span id='msg_ad'>" + messageLast + "</spand></div>";
                        return Json(htmlData);
                    }
                }
                return Json("false");
            }
        }

        #endregion

        #region Error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
        
    }
}
