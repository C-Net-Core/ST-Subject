
using Application.DTOs;
using Application.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using WebApp_Cnpm.Common;
using WebApp_Cnpm.Controllers.Handling;
using WebApp_Cnpm.Models;
using WebApp_Cnpm.Models.Helper;
using WebApp_Cnpm.ViewModels;
using WebApp_Cnpm.ViewModels.AdminViewModels;

namespace WebApp_Cnpm.Controllers
{
    public class AdminController : Controller
    {
        #region Interfaces Service Fields
        private readonly IAdminDTOService adminService;
        private readonly IAccountDTOService accountService;
        private readonly IProductDTOService productService;
        private readonly IProductHasDeletedDTOService productHasDeletedService;
        private readonly IProductTypeDTOService productTypeService;
        private readonly ISaleDTOService saleService;
        private readonly IRegionDTOService regionService;
        private readonly IProvinceDTOService provinceService;
        private readonly IWardDTOService wardService;
        private readonly IPayOrderDTOService payOrderService;
        private readonly IInfoOrderDTOService infoOrderService;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IFunctionDTOService functionService;
        private readonly IAuthorizationDTOService authorService;

        #endregion

        #region Cons
        public AdminController(IAdminDTOService adminService, IAccountDTOService accountService,
                    IProductDTOService productService, IProductHasDeletedDTOService productHasDeletedService,
                    IProductTypeDTOService productTypeService, ISaleDTOService saleService,
                    IRegionDTOService regionService, IProvinceDTOService provinceService,
                    IWardDTOService wardService, IPayOrderDTOService payOrderService,
                    IInfoOrderDTOService infoOrderService, IWebHostEnvironment hostEnvironment,
                    IFunctionDTOService functionService, IAuthorizationDTOService authorService
            )
        {
            this.adminService = adminService;
            this.accountService = accountService;
            this.productService = productService;
            this.productHasDeletedService = productHasDeletedService;
            this.productTypeService = productTypeService;
            this.saleService = saleService;
            this.regionService = regionService;
            this.provinceService = provinceService;
            this.wardService = wardService;
            this.payOrderService = payOrderService;
            this.infoOrderService = infoOrderService;
            this.hostEnvironment = hostEnvironment;
            this.functionService = functionService;
            this.authorService = authorService;
        }
        #endregion

        #region Functions of Login

        [AllowAnonymous]
        //public IActionResult Login()
        //{
        //    //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
        //    if (TempData.Peek("admin") != null)
        //    {
        //        ViewData["sayHiAdmin"] = TempData.Peek("admin");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View();
        //}
        //public void CheckLogin(String user, String pass)
        //{
        //    if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(pass))
        //    {
        //        Response.Redirect("/Admin/Login");
        //    }
        //    else
        //    {
        //        var password = Encryptor.MD5Hash(pass);
        //        AdminDTO admin = adminService.GetAllAdmin()
        //                                    .Where(a => a.UID == user && a.PW == password && a.Status == "On")
        //                                    .FirstOrDefault();
        //        if (admin != null)
        //        {
        //            //HttpContext.Session.SetString("admin", JsonConvert.SerializeObject(user));

        //            #region using Cookie
        //            var adminClaims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.Name, admin.UID),
        //                new Claim(ClaimTypes.Role,"Manager")
        //            };

        //            var adminIdentity = new ClaimsIdentity(adminClaims, "Admin Identity");

        //            var adminPrincipal = new ClaimsPrincipal(new[] { adminIdentity });
        //            //-----------------------------------------------------------
        //            HttpContext.SignInAsync(adminPrincipal); // đăng nhập vào Cookie

        //            ViewData["sayHiAdmin"] = user;
        //            #endregion

        //            Response.Redirect("/Admin/Index");
        //        }
        //        else
        //        {
        //            Response.Redirect("/Admin/Login");
        //        }
        //    }
        //}
        #endregion

        #region Function of Logout
        [Authorize]
        public void Logout()
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");

            if (TempData.Peek("admin") != null)
            {
                //xóa session login
                //HttpContext.Session.Remove("admin");

                // xóa cookie login
                HttpContext.SignOutAsync();
                TempData.Remove("admin");
                TempData.Remove("username");
                TempData.Remove("roleMethod");

                // trả về trang index
                Response.Redirect("/Home/Index");
            }
        }
        #endregion

        #region Function of Index

        [Authorize(Policy = "Claim.DoB")]
        public IActionResult Index()
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.countProducts = productService.GetAllProduct().Count();
                ViewBag.countProductTypes = productTypeService.GetAllProductType().Count();
                ViewBag.countInventories = productService.GetAllProduct().Sum(s => int.Parse(s.Amount));
                ViewBag.countOrders = payOrderService.GetAllPayOrder().Count();
                ViewBag.countAccounts = accountService.GetAllAccount().Count();
                ViewBag.countRevenue = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.StatusPay == "Đã thanh toán")
                                                    .Sum(p => int.Parse(p.Total));

                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                return View();
            }
        }
        #endregion

        #region Function Product Management

        [Authorize(Roles = "Product Management")]
        public IActionResult ProductManagement(int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                int pageSize = 3;
                int count;
                ViewData["products"] = productService.GetAllProduct();
                ViewData["productTypes"] = productTypeService.GetAllProductType();
                ViewData["sales"] = saleService.GetAllSale();

                var products = productService.GetAllProduct()
                                                .Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize).ToList();
                count = products.Count();

                var addProductVM = new AddProductsModel()
                {
                    Products = new PaginatedList<ProductDTO>(products, count, pageIndex, pageSize)
                };

                return View(addProductVM);
            }
        }
        #endregion

        #region Functions Add - Edit - Delete Products

        [Authorize(Roles = "Product Management")]
        public void AddNewProduct(AddProductsModel data)
        {
            if (data.Keys.image != null)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = data.Keys.image.FileName;
                string path = Path.Combine(wwwRootPath + "/img/product/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    data.Keys.image.CopyTo(fileStream);
                }

                ProductDTO product = new ProductDTO();

                product.Name = data.Keys.name;
                product.Cost = data.Keys.cost;
                product.Descrip = data.Keys.decription;
                product.Image = fileName;
                product.Amount = "0";
                product.IDKM = int.Parse(data.Keys.saleID);
                product.Size = data.Keys.size;
                product.IDLSP = int.Parse(data.Keys.productTypeID);

                productService.AddProduct(product);

                //var GET_ID_NEW_PRODUCT_FROM_DATABASE = productService.GetAllProduct().Select(p => p.IDSP).Last();

                Response.Redirect("/Admin/ProductManagement");
            }
            else
            {
                Response.Redirect("/Admin/ErrorValue");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Product Management")]
        public JsonResult DeleteProduct([FromBody] ValueID data)
        {
            var id = data.idsp;

            // get product and brand you need delete
            var product = productService.GetProductByID(id);
            var brand = productTypeService.GetProductTypeByID(product.IDLSP);

            // add product deleted by admin to table data
            productHasDeletedService.Add(
                new ProductHasDeletedDTO
                {
                    IDSP = id,
                    IDLSP = product.IDLSP,
                    Name = product.Name,
                    NameBrand = brand.Name,
                    Cost = product.Cost,
                    Descrip = product.Descrip,
                    Image = product.Image,
                    Amount = product.Amount,
                    IDKM = product.IDKM,
                    Size = product.Size
                }
            );

            productService.DeleteProduct(id);
            return Json("Delete Successed.");
        }

        [HttpPost]
        [Authorize(Roles = "Product Management")]
        public JsonResult LoadDataEditProduct([FromBody] ValueID data)
        {
            var id = data.idsp;

            ProductDTO product = productService.GetProductByID(id);

            // get link image of product
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = product.Image;
            string path = Path.Combine(wwwRootPath + "/img/product/" + fileName);

            // create new item send to json
            ItemTempEditProduct item = new ItemTempEditProduct
            {
                id = product.IDSP.ToString(),
                name = product.Name,
                cost = product.Cost,
                decription = product.Descrip,
                path = product.Image
            };
            return Json(item);
        }

        [Authorize(Roles = "Product Management")]
        public void EditProduct(AddProductsModel data)
        {
            if (data.EditKeys.imageEdit != null)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = data.EditKeys.imageEdit.FileName;
                string path = Path.Combine(wwwRootPath + "/img/product/" + fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    data.EditKeys.imageEdit.CopyTo(fileStream);
                }

                var idsp = int.Parse(data.EditKeys.productIDEdit);

                // get Product need edit
                ProductDTO product = productService.GetProductByID(idsp);

                // set new value for product
                product.Name = data.EditKeys.nameEdit;
                product.Cost = data.EditKeys.costEdit;
                product.Descrip = data.EditKeys.decriptionEdit;
                product.Image = fileName;
                product.Amount = "0";
                product.IDKM = int.Parse(data.EditKeys.saleIDEdit);
                product.Size = data.EditKeys.sizeEdit;
                product.IDLSP = int.Parse(data.EditKeys.productTypeIDEdit);

                productService.UpdateProduct(product);
            }
            else
            {
                var idsp = int.Parse(data.EditKeys.productIDEdit);
                var idlsp = int.Parse(data.EditKeys.productTypeIDEdit);

                // get Product need edit
                ProductDTO product = productService.GetProductByID(idsp);

                // set new value for product
                product.Name = data.EditKeys.nameEdit;
                product.Cost = data.EditKeys.costEdit;
                product.Descrip = data.EditKeys.decriptionEdit;
                product.Image = product.Image;
                product.Amount = "0";
                product.IDKM = int.Parse(data.EditKeys.saleIDEdit);
                product.Size = data.EditKeys.sizeEdit;
                product.IDLSP = int.Parse(data.EditKeys.productTypeIDEdit);

                productService.UpdateProduct(product);
            }

            Response.Redirect("/Admin/ProductManagement");
        }

        #endregion

        #region Functions Add - Edit - Delete Product Types

        [Authorize(Roles = "Brand Management")]
        public IActionResult ProductTypeManagement(int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                int pageSize = 5;
                int count;

                var productTypes = productTypeService.GetAllProductType()
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize).ToList();

                var products = productService.GetAllProduct();

                count = productTypes.Count();

                var productTypeVM = new ProductTypeViewModel()
                {
                    ProductTypes = new PaginatedList<ProductTypeDTO>(productTypes, count, pageIndex, pageSize),
                    Products = products
                };
                return View(productTypeVM);
            }

        }

        [Authorize(Roles = "Brand Management")]
        public void AddNewBrand(ProductTypeViewModel data)
        {
            String name = data.BrandKeys.Name;
            if (String.IsNullOrEmpty(name))
            {
                Response.Redirect("/Admin/ErrorValue");
            }
            else
            {
                productTypeService.AddProductType(new ProductTypeDTO
                {
                    Name = name,
                    Filter = name.ToLower()
                });
                Response.Redirect("/Admin/ProductTypeManagement");
            }
        }

        [Authorize(Roles = "Brand Management")]
        public JsonResult LoadDataEditBrand([FromBody] ValueID data)
        {
            int ID = data.idsp;
            var item = productTypeService.GetProductTypeByID(ID);

            AddBrandKeys value = new AddBrandKeys()
            {
                ID = item.IDLSP,
                Name = item.Name
            };

            return Json(value);
        }

        [Authorize(Roles = "Brand Management")]
        public void EditBrand(ProductTypeViewModel data)
        {
            int ID = data.EditBrandKeys.ID;
            string Name = data.EditBrandKeys.Name;


            if (String.IsNullOrEmpty(Name))
            {
                Response.Redirect("/Admin/ProductTypeManagement");
            }
            else
            {
                ProductTypeDTO brand = productTypeService.GetProductTypeByID(ID);
                brand.Name = Name;
                brand.Filter = Name.ToLower();

                productTypeService.UpdateProductType(brand);

                Response.Redirect("/Admin/ProductTypeManagement");
            }
        }

        [Authorize(Roles = "Brand Management")]
        public JsonResult DeleteBrand([FromBody] ValueID data)
        {
            productTypeService.DeleteProductType(data.idsp);
            return Json("Delete Successed");
        }

        #endregion

        #region Functions Of Inventory Management

        [Authorize(Roles = "Inventory Management")]
        public IActionResult TheFirstInventoryManagement(InventoryViewModel data, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                int pageSize = 3;
                var getProducts = productService.GetAllProduct();
                var selectCount = getProducts.Sum(p => int.Parse(p.Amount));
                var getAmountProductSold_In_InfoOrder = infoOrderService.GetAllInfoOrder()
                                                                        .Sum(o => int.Parse(o.Amount));

                if (data.Search == null)
                {
                    var amountProductsSearchNull = productService.GetAllProduct()
                                                .Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize).ToList();
                    var amountProduct = amountProductsSearchNull.Count();

                    var inventoryVM = new InventoryViewModel()
                    {
                        AmountProducts = new PaginatedList<ProductDTO>(amountProductsSearchNull, amountProduct, pageIndex, pageSize),
                        countProducts = selectCount,
                        productDTOs = getProducts,
                        AllDeliver = getAmountProductSold_In_InfoOrder,
                        Search = data.Search,
                    };

                    return View(inventoryVM);
                }
                else
                {
                    var pageMax = 5;
                    var amountProductsSearch = productService.GetAllProduct()
                                                        .Where(p => p.Name.Contains(data.Search))
                                                        .Skip((pageIndex - 1) * pageMax)
                                                        .Take(pageMax).ToList();

                    var amountProduct = amountProductsSearch.Count();

                    var inventoryVM = new InventoryViewModel()
                    {
                        AmountProducts = new PaginatedList<ProductDTO>(amountProductsSearch, amountProduct, pageIndex, pageMax),
                        countProducts = selectCount,
                        productDTOs = getProducts,
                        AllDeliver = getAmountProductSold_In_InfoOrder,
                        Search = data.Search,
                    };

                    return View(inventoryVM);
                }
            }
        }

        [Authorize(Roles = "Inventory Management")]
        public IActionResult TheSecondInventoryManagement(int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");

                int pageSize = 3;

                // hiển thị tổng sản phẩm có trong kho
                var products = productService.GetAllProduct()
                                                .Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize).ToList();
                var countProducts = products.Count();

                var sales = saleService.GetAllSale();
                var getProducts = productService.GetAllProduct();

                var selectCount = getProducts.Sum(p => int.Parse(p.Amount));
                var getAmountProductSold_In_InfoOrder = infoOrderService.GetAllInfoOrder()
                                                                        .Sum(o => int.Parse(o.Amount));

                var inventoryVM = new InventoryViewModel()
                {
                    AllProduct = new PaginatedList<ProductDTO>(products, countProducts, pageIndex, pageSize),
                    Sales = sales,
                    countProducts = selectCount,
                    productDTOs = getProducts,
                    AllDeliver = getAmountProductSold_In_InfoOrder,
                };
                return View(inventoryVM);
            }
        }

        [Authorize(Roles = "Inventory Management")]
        public IActionResult TheThirthInventoryManagement(int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");

                var pageSize = 3;
                var getAmountProductSold_In_InfoOrder = infoOrderService.GetAllInfoOrder()
                                                                        .Sum(o => int.Parse(o.Amount));
                var getProducts = productService.GetAllProduct();

                // create new List Delivered
                List<ToDelivered> delivereds = new List<ToDelivered>();
                foreach (var product in getProducts)
                {
                    var sold = infoOrderService.GetAllInfoOrder()
                                                .Where(i => i.IDSP == product.IDSP)
                                                .Sum(s => int.Parse(s.Amount));
                    var remain = int.Parse(product.Amount) - sold;
                    if (remain <= 0)
                    {
                        delivereds.Add(new ToDelivered()
                        {
                            ID = product.IDSP,
                            Name = product.Name,
                            Sold = sold,
                            Remain = 0
                        });
                    }
                    else
                    {
                        delivereds.Add(new ToDelivered()
                        {
                            ID = product.IDSP,
                            Name = product.Name,
                            Sold = sold,
                            Remain = int.Parse(product.Amount) - sold
                        });
                    }
                }

                var getDelivereds = delivereds.Skip((pageIndex - 1) * pageSize)
                                                .Take(pageSize).ToList();

                var countDelivereds = getDelivereds.Count();

                var selectCount = getProducts.Sum(p => int.Parse(p.Amount));

                var inventoryVM = new InventoryViewModel()
                {
                    countProducts = selectCount,
                    productDTOs = getProducts,
                    AllDeliver = getAmountProductSold_In_InfoOrder,
                    Delivereds = new PaginatedList<ToDelivered>(getDelivereds, countDelivereds, pageIndex, pageSize),
                };

                return View(inventoryVM);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Inventory Management")]
        public JsonResult ToReceive([FromBody] InventoryViewModel data)
        {
            var idsp = data.idToReceive;
            var amountToReceive = int.Parse(data.amountToReceive);

            var getProductToReceive = productService.GetProductByID(int.Parse(idsp));
            var amountCurrent = int.Parse(getProductToReceive.Amount);

            getProductToReceive.Amount = (amountCurrent + amountToReceive).ToString();

            productService.UpdateProduct(getProductToReceive);

            return Json("To receive success.");
        }
        #endregion

        #region Functions Of Order Management

        // 5 status in order
        #region status confirm

        [Authorize(Roles = "Order Management")]
        public IActionResult TheFirstStatus(String searchID, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                var pageSize = 3;
                if (searchID == null)
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ xử lý")
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize).ToList();

                    var countGetOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ xử lý")
                                                    .Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder
                    };
                    return View(orderVM);
                }
                else
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ xử lý" && p.ID == int.Parse(searchID))
                                                    .ToList();

                    var countGetOrderPage = getOrderPage.Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder
                    };

                    return View(orderVM);
                }
            }
        }

        [Authorize(Roles = "Order Management")]
        public JsonResult Confirm([FromBody] OrderManagement data)
        {
            var id_order = data.ID_Order;
            var payOrder = payOrderService.GetPayOrderByID(id_order);

            payOrder.Status = "Chờ giao hàng";
            payOrder.DateConfirm = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString();

            #region send email
            string email_user = accountService.getEmailUser(payOrder.UID);
            string subject = "Your order has been confirmed " + DateTime.Now.ToString();
            string text = "- Your Order ID: #" + id_order
                        + "\n- Customer: " + payOrder.Receiver
                        + "\n- Address: " + payOrder.Address
                        + "\n- Phone: " + payOrder.Phone
                        + "\n- Total Money: " + int.Parse(payOrder.Total).ToString("#,##0").Replace(",", ".") + " VNĐ"
                        + "\n- Date Order: " + payOrder.DateOrder
                        + "\n- Date Confirm: " + DateTime.Now
                        + "\n- Status: Shipping"
                        + "\n- Status Pay: Unpaid ";
            SendMail.sendMail(subject, email_user, text);
            #endregion

            payOrderService.UpdatePayOrder(payOrder);


            return Json("Confirm successes");
        }
        #endregion

        #region status to ship

        [Authorize(Roles = "Order Management")]
        public IActionResult TheSecondStatus(String searchID, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");

                var pageSize = 3;
                if (searchID == null)
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ giao hàng")
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize).ToList();

                    var countGetOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ giao hàng")
                                                    .Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder
                    };
                    return View(orderVM);
                }
                else
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ giao hàng" && p.ID == int.Parse(searchID))
                                                    .ToList();

                    var countGetOrderPage = getOrderPage.Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder,
                        Search = searchID
                    };

                    return View(orderVM);
                }
            }
        }

        [Authorize(Roles = "Order Management")]
        public JsonResult ToDeliver([FromBody] OrderManagement data)
        {
            var id_order = data.ID_Order;
            var payOrder = payOrderService.GetPayOrderByID(id_order);

            payOrder.Status = "Chờ thanh toán";
            payOrder.DateToShip = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString();
            #region send email
            string email_user = accountService.getEmailUser(payOrder.UID);
            string subject = "Your order is being shipped by a courier " + DateTime.Now.ToString();
            string text = "- Your Order ID: #" + id_order
                        + "\n- Customer: " + payOrder.Receiver
                        + "\n- Address: " + payOrder.Address
                        + "\n- Phone: " + payOrder.Phone
                        + "\n- Total Money: " + int.Parse(payOrder.Total).ToString("#,##0").Replace(",", ".") + " VNĐ"
                        + "\n- Date Order: " + payOrder.DateOrder
                        + "\n- Date Confirm: " + payOrder.DateConfirm
                        + "\n- Date To Ship: " + DateTime.Now
                        + "\n- Status: Wait for pay"
                        + "\n- Status Pay: Unpaid ";
            SendMail.sendMail(subject, email_user, text);
            #endregion

            payOrderService.UpdatePayOrder(payOrder);
            return Json("Confirm to deliver successes");
        }
        #endregion

        #region status to pay

        [Authorize(Roles = "Order Management")]
        public IActionResult TheThirthStatus(String searchID, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                var pageSize = 3;
                if (searchID == null)
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ thanh toán")
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize).ToList();

                    var countGetOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ xử lý")
                                                    .Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder
                    };
                    return View(orderVM);
                }
                else
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ thanh toán" && p.ID == int.Parse(searchID))
                                                    .ToList();

                    var countGetOrderPage = getOrderPage.Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder,
                        Search = searchID
                    };

                    return View(orderVM);
                }
            }
        }

        [Authorize(Roles = "Order Management")]
        public JsonResult Delivered([FromBody] OrderManagement data)
        {
            var id_order = data.ID_Order;
            var payOrder = payOrderService.GetPayOrderByID(id_order);

            payOrder.Status = "Đã giao hàng";
            payOrder.StatusPay = "Đã thanh toán";
            payOrder.DateToPay = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString();
            #region send email
            string email_user = accountService.getEmailUser(payOrder.UID);
            string subject = "Your order has been successfully delivered " + DateTime.Now.ToString();
            string text = "- Your Order ID: #" + id_order
                        + "\n- Customer: " + payOrder.Receiver
                        + "\n- Address: " + payOrder.Address
                        + "\n- Phone: " + payOrder.Phone
                        + "\n- Total Money: " + int.Parse(payOrder.Total).ToString("#,##0").Replace(",", ".") + " VNĐ"
                        + "\n- Date Order: " + payOrder.DateOrder
                        + "\n- Date Confirm: " + payOrder.DateConfirm
                        + "\n- Date To Ship: " + payOrder.DateToShip
                        + "\n- Date To Pay: " + DateTime.Now
                        + "\n- Status: Successfully Delivered"
                        + "\n- Status Pay: Paid ";
            SendMail.sendMail(subject, email_user, text);
            #endregion

            payOrderService.UpdatePayOrder(payOrder);

            return Json("Confirm delivered successes");
        }
        #endregion

        #region fourth status

        [Authorize(Roles = "Order Management")]
        public IActionResult TheFourthStatus(String searchID, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                var pageSize = 3;
                if (searchID == null)
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Đã giao hàng")
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize).ToList();

                    var countGetOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ xử lý")
                                                    .Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder
                    };
                    return View(orderVM);
                }
                else
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Đã giao hàng" && p.ID == int.Parse(searchID))
                                                    .ToList();

                    var countGetOrderPage = getOrderPage.Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder,
                        Search = searchID
                    };

                    return View(orderVM);
                }
            }
        }

        #endregion

        #region fifth status

        [Authorize(Roles = "Order Management")]
        public IActionResult TheFifthStatus(String searchID, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                var pageSize = 3;
                if (searchID == null)
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Đã hủy")
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize).ToList();

                    var countGetOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Chờ xử lý")
                                                    .Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder
                    };
                    return View(orderVM);
                }
                else
                {
                    var getOrderPage = payOrderService.GetAllPayOrder()
                                                    .Where(p => p.Status == "Đã hủy" && p.ID == int.Parse(searchID))
                                                    .ToList();

                    var countGetOrderPage = getOrderPage.Count();

                    var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                    var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                    var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                    var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                    var count_Cancel = payOrderService.countStatus("Đã hủy");
                    var countAllOrder = payOrderService.countAllOrder();

                    var orderVM = new OrderManagement()
                    {
                        getOrderPage = new PaginatedList<PayOrderDTO>(getOrderPage, countGetOrderPage, pageIndex, pageSize),

                        count_Watting_For_Progressing = count_Watting_For_Progressing,
                        count_To_Ship = count_To_Ship,
                        count_To_Pay = count_To_Pay,
                        count_Delivered = count_Delivered,
                        count_Cancel = count_Cancel,
                        countAllOrder = countAllOrder,
                        Search = searchID
                    };

                    return View(orderVM);
                }
            }
        }
        #endregion

        #region show info detail Order

        [Authorize(Roles = "Order Management")]
        public IActionResult InfoOrder(int ID_Order)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                var count_Watting_For_Progressing = payOrderService.countStatus("Chờ xử lý");
                var count_To_Ship = payOrderService.countStatus("Chờ giao hàng");
                var count_To_Pay = payOrderService.countStatus("Chờ thanh toán");
                var count_Delivered = payOrderService.countStatus("Đã giao hàng");
                var count_Cancel = payOrderService.countStatus("Đã hủy");
                var countAllOrder = payOrderService.countAllOrder();

                List<InfoOrderManagement> detailOrder = new List<InfoOrderManagement>();
                var idDetailOrder = 0;
                foreach (var item in infoOrderService.GetAllInfoOrder())
                {
                    if (item.IDP == ID_Order)
                    {
                        var product = productService.GetProductByID(item.IDSP);
                        detailOrder.Add(new InfoOrderManagement
                        {
                            ID = idDetailOrder++,
                            IDP = ID_Order,
                            IDSP = product.IDSP,
                            NameProduct = product.Name,
                            Amount = item.Amount,
                            Size = item.Size,
                            Price = item.Price,
                            IntoMoney = item.IntoMoney
                        });
                    }
                }

                var orderVM = new OrderManagement()
                {
                    detailOrders = detailOrder,
                    count_Watting_For_Progressing = count_Watting_For_Progressing,
                    count_To_Ship = count_To_Ship,
                    count_To_Pay = count_To_Pay,
                    count_Delivered = count_Delivered,
                    count_Cancel = count_Cancel,
                    countAllOrder = countAllOrder
                };
                return View(orderVM);
            }
        }
        #endregion
        #endregion

        #region Functions of Account Management

        #region user

        [Authorize(Roles = "Account Management")]
        public IActionResult UserManagement(String username, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                if (username == null)
                {
                    var pageSize = 3;
                    var accounts = accountService.GetAllAccount().Skip((pageIndex - 1) * pageSize)
                                                                .Take(pageSize).ToList();
                    var countAccounts = accounts.Count();
                    var countUser = accountService.GetAllAccount().Count();
                    var countAdmin = adminService.GetAllAdmin().Count();
                    var countFunction = functionService.GetAll().Count();

                    var accountVM = new AccountViewModel
                    {
                        Accounts = new PaginatedList<AccountDTO>(accounts, countAccounts, pageIndex, pageSize),
                        countUser = countUser,
                        countAdmin = countAdmin,
                        countFunction = countFunction
                    };
                    return View(accountVM);
                }
                else
                {
                    var pageSize = 3;
                    var accounts = accountService.SearchByUser(username).Skip((pageIndex - 1) * pageSize)
                                                                .Take(pageSize).ToList();
                    var countAccounts = accounts.Count();

                    var countUser = accountService.GetAllAccount().Count();
                    var countAdmin = adminService.GetAllAdmin().Count();
                    var countFunction = functionService.GetAll().Count();

                    var accountVM = new AccountViewModel
                    {
                        Accounts = new PaginatedList<AccountDTO>(accounts, countAccounts, pageIndex, pageSize),
                        countUser = countUser,
                        countAdmin = countAdmin,
                        countFunction = countFunction
                    };
                    return View(accountVM);
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult LockUser([FromBody] AccountViewModel data)
        {
            var idUser = data.ID_User;
            var statusUser = data.Status_User;
            if (statusUser == "lock")
            {
                var account = accountService.GetAccountByID(idUser);
                account.Status = "Locked";

                #region send email
                string email_user = account.Email;
                string subject = "Your account has been locked by administrator " + DateTime.Now.ToString();
                string text = "- Since you have been inactive for a long time, we have now decided to close your account." +
                                " Please contact us through email vatvashop20@gmail.com." +
                                "\n- Thank you in the past time for trusting shopping at our website." +
                                "\n-------- DATE LOCKED: " + DateTime.Now + " --------";
                SendMail.sendMail(subject, email_user, text);
                #endregion

                accountService.UpdateAccount(account);
                return Json("This account has been locked successful");
            }
            else
            {
                var account = accountService.GetAccountByID(idUser);
                account.Status = "On";

                #region send email
                string email_user = account.Email;
                string subject = "Your account has been unlocked by administrator " + DateTime.Now.ToString();
                string text = "- Welcome back to us. Thank you for your trust in shopping on our website" +
                                "\n- Have a nice shopping day." +
                                "\n-------- DATE UNLOCKED: " + DateTime.Now + " --------";
                SendMail.sendMail(subject, email_user, text);
                #endregion

                accountService.UpdateAccount(account);
                return Json("This account has been unlocked successful");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult filterForStatus([FromBody] AccountViewModel data)
        {
            var status = data.Status_User;
            var accounts = accountService.GetAccountByStatus(status);
            return Json(accounts);
        }
        #endregion

        #region admin

        [Authorize(Roles = "Account Management")]
        public IActionResult AdminManagement(String username, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                //String[] user = SESSION_LOGIN.Split('"');
                var permission = adminService.getPermission(TempData.Peek("admin").ToString());
                var ID_ADMIN = adminService.getIDAdmin(TempData.Peek("admin").ToString());
                if (username == null)
                {
                    var pageSize = 3;
                    var admins = adminService.GetAllAdmin().Skip((pageIndex - 1) * pageSize)
                                                                .Take(pageSize).ToList();

                    var countAdmins = admins.Count();
                    var countUser = accountService.GetAllAccount().Count();
                    var countAdmin = adminService.GetAllAdmin().Count();
                    var countFunction = functionService.GetAll().Count();

                    var accountVM = new AccountViewModel
                    {
                        Admins = new PaginatedList<AdminDTO>(admins, countAdmins, pageIndex, pageSize),
                        countUser = countUser,
                        countAdmin = countAdmin,
                        countFunction = countFunction,
                        Permission = permission,
                        ID_Admin = ID_ADMIN,
                    };
                    return View(accountVM);
                }
                else
                {
                    var pageSize = 3;
                    var admins = adminService.SearchByUser(username).Skip((pageIndex - 1) * pageSize)
                                                                .Take(pageSize).ToList();
                    var countAdmins = admins.Count();

                    var countUser = accountService.GetAllAccount().Count();
                    var countAdmin = adminService.GetAllAdmin().Count();
                    var countFunction = functionService.GetAll().Count();

                    var accountVM = new AccountViewModel
                    {
                        Admins = new PaginatedList<AdminDTO>(admins, countAdmins, pageIndex, pageSize),
                        countUser = countUser,
                        countAdmin = countAdmin,
                        countFunction = countFunction,
                        Permission = permission,
                        ID_Admin = ID_ADMIN,
                    };
                    return View(accountVM);
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult LockAdmin([FromBody] AccountViewModel data)
        {
            var id = data.ID_Admin;
            var stt = data.Status_Admin;
            var admin = adminService.GetAdminByID(id);
            if (stt == "lock")
            {
                admin.Status = "Locked";
                adminService.UpdateAdmin(admin);
                return Json("Locked Successful");
            }
            else
            {
                admin.Status = "On";
                adminService.UpdateAdmin(admin);
                return Json("Unlocked Successful");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult ChangePasswordAdmin([FromBody] ChangePasswordKeys data)
        {
            var id = int.Parse(data.ID);
            var newPass = Encryptor.MD5Hash(data.New_Pass);

            var admin = adminService.GetAdminByID(id);
            if (admin.PW == newPass)
            {
                return Json("Password has been exited");
            }
            else
            {
                admin.PW = newPass;
                adminService.UpdateAdmin(admin);
                return Json("Your password has been changed successful");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult ChangePosition([FromBody] ChangePositionKeys data)
        {
            var id = int.Parse(data.ID);
            var newPosition = data.New_Position;
            var admin = adminService.GetAdminByID(id);
            admin.Permission = newPosition;
            adminService.UpdateAdmin(admin);

            return Json("Position of staff has been changed.!!!");
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult ChangePasswordYourSelfAdmin([FromBody] ChangeYourSelfAdminPassKeys data)
        {
            var id = int.Parse(data.ID);
            var newPass = Encryptor.MD5Hash(data.New_Pass);

            var admin = adminService.GetAdminByID(id);
            if (admin.PW == newPass)
            {
                return Json("Password has been exited");
            }
            else
            {
                admin.PW = newPass;
                adminService.UpdateAdmin(admin);
                return Json("Your password has been changed successful");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult GetValueChecked([FromBody] PermissionAdminKeys data)
        {
            var id = data.ID;
            List<PermissionAdminKeys> sendData = new List<PermissionAdminKeys>();
            var permissions = functionService.GetAll();
            foreach (var item in permissions)
            {
                var author = authorService.GetAuthor(id, item.IDCN);
                PermissionAdminKeys obj = new PermissionAdminKeys();
                if (author == false)
                {
                    obj.ID = item.IDCN;
                    obj.Permission = item.Name;
                    obj.booleanChecked = "false";
                }
                else
                {
                    obj.ID = item.IDCN;
                    obj.Permission = item.Name;
                    obj.booleanChecked = "true";
                }
                sendData.Add(obj);
            }
            return Json(sendData);
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult ChangePermission([FromBody] ChangePermission data)
        {
            var adminID = int.Parse(data.ID_Admin);
            var arrayCN = data.ID_CN.Split(" ");

            // delete all author has permission with adminID from ajax send
            // B1: get author have idAdmin from ajax send
            var authors = authorService.GetAll().Where(a => a.IDAdmin == adminID).ToList();
            // B2: use foreach to Remove this Object of Author
            foreach (var item in authors)
            {
                authorService.Remove(item);
            }

            // create new permission for author but the last array is " " so i will run i to array -1
            for (var i = 0; i < arrayCN.Length - 1; i++)
            {
                AuthorizationDTO newAuthor = new AuthorizationDTO();
                newAuthor.IDAdmin = adminID;
                newAuthor.IDCN = int.Parse(arrayCN[i]);

                authorService.Create(newAuthor);
            }

            return Json("Saved successful");
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult CreateNewAccountAdmin([FromBody] CreateNewAccountAdminKeys data)
        {
            var user = data.Username;
            var pass = Encryptor.MD5Hash(data.Password);
            var position = data.Position;
            if (adminService.checkExistAdmin(user) == true)
            {
                return Json("Existed");
            }
            else
            {
                adminService.AddAdmin(new AdminDTO
                {
                    UID = user,
                    PW = pass,
                    Permission = position,
                    DateActive = DateTime.Now.Day.ToString() + "-"
                                    + DateTime.Now.Month.ToString() + "-"
                                    + DateTime.Now.Year.ToString(),
                    Status = "On"
                });

                if (position == "MANAGER")
                {
                    var id_Last_Admin = adminService.GetAllAdmin().LastOrDefault().ID;
                    foreach (var permission in functionService.GetAll())
                    {
                        authorService.Create(new AuthorizationDTO
                        {
                            IDAdmin = id_Last_Admin,
                            IDCN = permission.IDCN
                        });
                    }
                }
                return Json("OK");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult FilterAdminForStatus([FromBody] AccountViewModel data)
        {
            var status = data.Status_Admin;
            var admins = adminService.GetByStatus(status);
            return Json(admins);
        }
        #endregion

        #region function

        [Authorize(Roles = "Account Management")]
        public IActionResult FunctionManagement(String nameFunction, int pageIndex = 1)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                //String[] user = SESSION_LOGIN.Split('"');
                var permission = adminService.getPermission(TempData.Peek("admin").ToString());
                if (nameFunction == null)
                {
                    var pageSize = 3;
                    var functions = functionService.GetAll().Skip((pageIndex - 1) * pageSize)
                                                                .Take(pageSize).ToList();
                    var countFunctions = functions.Count();

                    var countUser = accountService.GetAllAccount().Count();
                    var countAdmin = adminService.GetAllAdmin().Count();
                    var countFunction = functionService.GetAll().Count();

                    var accountVM = new AccountViewModel
                    {
                        Functions = new PaginatedList<FunctionDTO>(functions, countFunctions, pageIndex, pageSize),
                        countUser = countUser,
                        countAdmin = countAdmin,
                        countFunction = countFunction,
                        Permission = permission,
                    };
                    return View(accountVM);
                }
                else
                {
                    var pageSize = 3;
                    var functions = functionService.GetAll()
                                                    .Where(f => f.Name.Contains(nameFunction))
                                                    .Skip((pageIndex - 1) * pageSize)
                                                    .Take(pageSize).ToList();
                    var countFunctions = functions.Count();

                    var countUser = accountService.GetAllAccount().Count();
                    var countAdmin = adminService.GetAllAdmin().Count();
                    var countFunction = functionService.GetAll().Count();

                    var accountVM = new AccountViewModel
                    {
                        Functions = new PaginatedList<FunctionDTO>(functions, countFunctions, pageIndex, pageSize),
                        countUser = countUser,
                        countAdmin = countAdmin,
                        countFunction = countFunction,
                        Permission = permission,
                    };
                    return View(accountVM);
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult AddFunction([FromBody] AccountViewModel data)
        {
            var name = data.Name_Function;
            var is_exist_function = functionService.Is_Exist_Function_ByName(name);
            if (is_exist_function == true)
            {
                return Json("existed");
            }
            else
            {
                functionService.Create(new FunctionDTO
                {
                    Name = name
                });
                return Json("Create successful.!!!");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult DeleteFunction([FromBody] AccountViewModel data)
        {
            var authors = authorService.GetAll();
            foreach (var item in authors)
            {
                if (item.IDCN == int.Parse(data.ID_Function))
                {
                    authorService.RemoveByIDCN(item);
                }
            }
            functionService.Remove(int.Parse(data.ID_Function));

            return Json("Deleted successful.!!!");
        }

        [HttpPost]
        [Authorize(Roles = "Account Management")]
        public JsonResult EditFunction([FromBody] AccountViewModel data)
        {
            var function = functionService.GetByID(int.Parse(data.ID_Function));
            function.Name = data.Name_Function_Edit;
            functionService.Update(function);
            return Json("Edited successful");
        }
        #endregion

        #endregion

        #region Shipping Management

        [Authorize(Roles = "Shipping Fee Management")]
        public IActionResult ShippingFeeManagement()
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                var getRegions = regionService.GetAllRegion().ToList();
                var getProvinces = provinceService.GetAllProvince().ToList();
                var getWards = wardService.GetAllWard().ToList();
                var dstpMienBac = "";
                var dstpMienTrung = "";
                var dstpMienNam = "";
                var demTPMienBac = 0;
                var demQuanMienBac = 0;
                var demTPMienTrung = 0;
                var demQuanMienTrung = 0;
                var demTPMienNam = 0;
                var demQuanMienNam = 0;
                foreach (var province in getProvinces)
                {
                    foreach (var ward in getWards)
                    {
                        // ds các tp thuộc khu vực
                        if ((ward.IdProvince == province.IdProvince) && province.IdReion == 4)
                        {
                            dstpMienBac += ward.NameWard + ", ";
                            demQuanMienBac++;
                        }
                        if ((ward.IdProvince == province.IdProvince) && province.IdReion == 3)
                        {
                            dstpMienTrung += ward.NameWard + ", ";
                            demQuanMienTrung++;
                        }
                        if ((ward.IdProvince == province.IdProvince) && province.IdReion == 2)
                        {
                            dstpMienNam += ward.NameWard + ", ";
                            demQuanMienNam++;
                        }

                        // đếm các quận thuộc khu vực

                    }
                }
                demTPMienBac = provinceService.GetAllProvince().Where(p => p.IdReion == 4).Count();
                demTPMienTrung = provinceService.GetAllProvince().Where(p => p.IdReion == 3).Count();
                demTPMienNam = provinceService.GetAllProvince().Where(p => p.IdReion == 2).Count();
                var shippingFeeVM = new ShippingFeeViewModel
                {
                    Regions = getRegions,
                    Provinces = getProvinces,
                    Wards = getWards,
                    dsMienBac = dstpMienBac,
                    dsMienTrung = dstpMienTrung,
                    dsMienNam = dstpMienNam,
                    demTPMienBac = demTPMienBac,
                    demTPMienTrung = demTPMienTrung,
                    demTPMienNam = demTPMienNam,
                    demQuanMienBac = demQuanMienBac,
                    demQuanMienTrung = demQuanMienTrung,
                    demQuanMienNam = demQuanMienNam
                };
                return View(shippingFeeVM);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Shipping Fee Management")]
        public JsonResult ChangeShippingFee([FromBody] ShippingFeeViewModel data)
        {
            var id = int.Parse(data.idRegion);
            var feeship = data.feeship;
            var region = regionService.GetRegionByID(id);
            region.FeeShip = int.Parse(feeship);
            regionService.UpdateRegion(region);

            return Json("Changed shipping fee successful.!!!");
        }
        #endregion

        #region Statisticals
        // Doanh thu : 1
        [Authorize(Roles = "Statistical")]
        public IActionResult Turnover(String year)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                if (year == null)
                {
                    var statisticalVM = new StatisticalViewModel
                    {
                        #region Turnover
                        TurnoverOfYears = StatisticalProcess.TurnoverOfYear(payOrderService, "2020"),
                        countTotalStatistics = StatisticalProcess.countTotalStatistics(payOrderService, "2020"),
                        HighestRevenue = StatisticalProcess.HighestRevenue(payOrderService, "2020"),
                        LowestRevenue = StatisticalProcess.LowestRevenue(payOrderService, "2020"),
                        countTurnoverGreater1Milion = StatisticalProcess.countTurnoverGreater1Milion(payOrderService, "2020"),
                        countTurnoverLess1Milion = StatisticalProcess.countTurnoverLess1Milion(payOrderService, "2020"),
                        year = "2020",
                        #endregion
                    };
                    return View(statisticalVM);
                }
                else
                {
                    var turnovers = StatisticalProcess.TurnoverOfYear(payOrderService, year);
                    if (turnovers != null)
                    {
                        var statisticalVM = new StatisticalViewModel
                        {
                            #region Turnover
                            TurnoverOfYears = turnovers,
                            countTotalStatistics = StatisticalProcess.countTotalStatistics(payOrderService, year),
                            HighestRevenue = StatisticalProcess.HighestRevenue(payOrderService, year),
                            LowestRevenue = StatisticalProcess.LowestRevenue(payOrderService, year),
                            countTurnoverGreater1Milion = StatisticalProcess.countTurnoverGreater1Milion(payOrderService, year),
                            countTurnoverLess1Milion = StatisticalProcess.countTurnoverLess1Milion(payOrderService, year),
                            year = year,
                            #endregion
                        };
                        return View(statisticalVM);
                    }
                    else
                    {
                        var statisticalVM = new StatisticalViewModel
                        {
                            #region Turnover
                            TurnoverOfYears = StatisticalProcess.TurnoverOfYear(payOrderService, "2020"),
                            countTotalStatistics = StatisticalProcess.countTotalStatistics(payOrderService, "2020"),
                            HighestRevenue = StatisticalProcess.HighestRevenue(payOrderService, "2020"),
                            LowestRevenue = StatisticalProcess.LowestRevenue(payOrderService, "2020"),
                            countTurnoverGreater1Milion = StatisticalProcess.countTurnoverGreater1Milion(payOrderService, "2020"),
                            countTurnoverLess1Milion = StatisticalProcess.countTurnoverLess1Milion(payOrderService, "2020"),
                            year = "2020",
                            #endregion
                        };
                        return View(statisticalVM);
                    }
                }
            }
        }

        // Top product : 2
        [Authorize(Roles = "Statistical")]
        public IActionResult TopProduct(String month, String year)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                if (year == null && month == null)
                {
                    var nowMonth = DateTime.Now.Month.ToString();
                    var statisticalVM = new StatisticalViewModel
                    {
                        #region Statistical Product
                        TopSoldProducts_Greater = StatisticalProduct.TopSoldProducts_GreaterInYear(productService, infoOrderService, payOrderService, "2020"),
                        countProductSold = StatisticalProduct.countProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                        countMoney_ProductSold = StatisticalProduct.countMoney_ProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                        year = "2020",
                        month = ""
                        #endregion
                    };
                    return View(statisticalVM);
                }
                else
                {
                    var topSoldProducts_Greater = StatisticalProduct.TopSoldProducts_Greater(productService, infoOrderService, payOrderService, month, year);
                    if (topSoldProducts_Greater == null)
                    {
                        // is null
                        var statisticalVM = new StatisticalViewModel
                        {
                            #region Statistical Product
                            TopSoldProducts_Greater = StatisticalProduct.TopSoldProducts_GreaterInYear(productService, infoOrderService, payOrderService, "2020"),
                            countProductSold = StatisticalProduct.countProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                            countMoney_ProductSold = StatisticalProduct.countMoney_ProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                            year = "2020",
                            month = ""
                            #endregion
                        };
                        return View(statisticalVM);
                    }
                    else
                    {
                        // list != null
                        var statisticalVM = new StatisticalViewModel
                        {
                            #region Statistical Product
                            TopSoldProducts_Greater = StatisticalProduct.TopSoldProducts_Greater(productService, infoOrderService, payOrderService, month, year),
                            countProductSold = StatisticalProduct.countProductSold(productService, infoOrderService, payOrderService, month, year),
                            countMoney_ProductSold = StatisticalProduct.countMoney_ProductSold(productService, infoOrderService, payOrderService, month, year),
                            year = year,
                            month = month,
                            #endregion
                        };
                        return View(statisticalVM);
                    }
                }
            }
        }

        // Product Sell Slowly : 3
        [Authorize(Roles = "Statistical")]
        public IActionResult ProductSellSlowly(String month, String year)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                if (year == null && month == null)
                {
                    var nowMonth = DateTime.Now.Month.ToString();
                    var statisticalVM = new StatisticalViewModel
                    {
                        #region Statistical Product
                        TopSoldProducts_Greater = StatisticalProductSellSlowly.TopSoldProducts_GreaterInYear(productService, infoOrderService, payOrderService, "2020"),
                        countProductSold = StatisticalProductSellSlowly.countProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                        countMoney_ProductSold = StatisticalProductSellSlowly.countMoney_ProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                        year = "2020",
                        month = ""
                        #endregion
                    };
                    return View(statisticalVM);
                }
                else
                {
                    var topSoldProducts_Greater = StatisticalProductSellSlowly.TopSoldProducts_Greater(productService, infoOrderService, payOrderService, month, year);
                    if (topSoldProducts_Greater == null)
                    {
                        // is null
                        var statisticalVM = new StatisticalViewModel
                        {
                            #region Statistical Product
                            TopSoldProducts_Greater = StatisticalProductSellSlowly.TopSoldProducts_GreaterInYear(productService, infoOrderService, payOrderService, "2020"),
                            countProductSold = StatisticalProductSellSlowly.countProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                            countMoney_ProductSold = StatisticalProductSellSlowly.countMoney_ProductSoldInYear(productService, infoOrderService, payOrderService, "2020"),
                            year = "2020",
                            month = ""
                            #endregion
                        };
                        return View(statisticalVM);
                    }
                    else
                    {
                        // list != null
                        var statisticalVM = new StatisticalViewModel
                        {
                            #region Statistical Product
                            TopSoldProducts_Greater = StatisticalProductSellSlowly.TopSoldProducts_Greater(productService, infoOrderService, payOrderService, month, year),
                            countProductSold = StatisticalProductSellSlowly.countProductSold(productService, infoOrderService, payOrderService, month, year),
                            countMoney_ProductSold = StatisticalProductSellSlowly.countMoney_ProductSold(productService, infoOrderService, payOrderService, month, year),
                            year = year,
                            month = month,
                            #endregion
                        };
                        return View(statisticalVM);
                    }
                }
            }
        }

        // Loyal Customers : 4
        [Authorize(Roles = "Statistical")]
        public IActionResult LoyalCustomers(String sort, String month, String year)
        {
           // var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                if (sort == null)
                {
                    var statisticalVM = new StatisticalViewModel
                    {
                        StatisticalCustomers = StatisticalCustommer.statisticalCustomers_8(accountService, payOrderService,
                                                                                                month, year),
                        month = month,
                        year = year,
                    };
                    return View(statisticalVM);
                }
                else
                {
                    var valueSort = int.Parse(sort.Split('_')[1]);
                    var statisticalVM = new StatisticalViewModel
                    {
                        StatisticalCustomers = StatisticalCustommer.control(accountService, payOrderService,
                                                                                month, year, valueSort),
                        month = month,
                        year = year,
                    };
                    return View(statisticalVM);
                }
            }
        }

        // thống kê đơn hàng : 5
        [Authorize(Roles = "Statistical")]
        public IActionResult OrderStatistics(String sort, String year)
        {
            //var SESSION_LOGIN = HttpContext.Session.GetString("admin");
            if (TempData.Peek("admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                ViewData["sayHiAdmin"] = TempData.Peek("admin");
                if (sort == null && year == null)
                {
                    var statisticalVM = new StatisticalViewModel
                    {
                        StatisticalOrders = StatisticalOrder.statisticalOrders(payOrderService, "2020"),
                        totalOrders = StatisticalOrder.totalOrders(payOrderService, "2020"),
                        totalOrderSuccesses = StatisticalOrder.totalOrderSuccesses(payOrderService, "2020"),
                        totalOrderCancel = StatisticalOrder.totalOrderCancel(payOrderService, "2020"),
                        totalMoney = StatisticalOrder.totalMoney(payOrderService, "2020"),
                    };
                    return View(statisticalVM);
                }
                else
                {
                    if (sort == "asc")
                    {
                        var statisticalVM = new StatisticalViewModel
                        {
                            StatisticalOrders = StatisticalOrder.statisticalOrders_Asc(payOrderService, year),
                            totalOrders = StatisticalOrder.totalOrders(payOrderService, year),
                            totalOrderSuccesses = StatisticalOrder.totalOrderSuccesses(payOrderService, year),
                            totalOrderCancel = StatisticalOrder.totalOrderCancel(payOrderService, year),
                            totalMoney = StatisticalOrder.totalMoney(payOrderService, year),
                            year = year,
                        };
                        return View(statisticalVM);

                    }
                    else
                    {
                        var statisticalVM = new StatisticalViewModel
                        {
                            StatisticalOrders = StatisticalOrder.statisticalOrders_Dec(payOrderService, year),
                            totalOrders = StatisticalOrder.totalOrders(payOrderService, year),
                            totalOrderSuccesses = StatisticalOrder.totalOrderSuccesses(payOrderService, year),
                            totalOrderCancel = StatisticalOrder.totalOrderCancel(payOrderService, year),
                            totalMoney = StatisticalOrder.totalMoney(payOrderService, year),
                            year = year,
                        };
                        return View(statisticalVM);
                    }
                }
            }
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
                    author = "ad"
                });

                session.SetString("message", JsonConvert.SerializeObject(dataMessage));
            }
            else
            {
                List<KeyMessage> dataMessage = JsonConvert.DeserializeObject<List<KeyMessage>>(session_message);
                dataMessage.Add(new KeyMessage
                {
                    message = keyData.message,
                    author = "ad"
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

        #region View Error Value Input
        public IActionResult ErrorValue()
        {
            return View();
        }
        #endregion

    }
}
