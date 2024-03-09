using Common;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using VietMark.Models;
using VietMark.Orthers;

namespace VietMark.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFormSession();
            if (ShoppingCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = ShoppingCart.Sum(p => p.Quantity);
            ViewBag.Tongtien = ShoppingCart.Sum(p => p.Quantity * p.Price);
            return View(ShoppingCart);
        }
        public List<CartItem> GetShoppingCartFormSession()
        {
            var lstShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (lstShoppingCart == null)
            {
                lstShoppingCart = new List<CartItem>();
                Session["ShoppingCart"] = lstShoppingCart;
            }
            return lstShoppingCart;
        }
        public RedirectToRouteResult AddToCart(int masp)
        {
            DB_ThuongMaiDienTu db = new DB_ThuongMaiDienTu();
            List<CartItem> ShoppingCart = GetShoppingCartFormSession();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m.MaSP == masp);
            if (findCartItem == null)
            {
                SanPham findSP = db.SanPhams.First(m => m.Id_SanPham == masp);
                var giatien = (findSP.GiaBanBanDau * (100 - findSP.PhanTramGiamGia)) / 100;
                CartItem newItem = new CartItem()
                {
                    MaSP = findSP.Id_SanPham,
                    TenSP = findSP.TenSP,
                    Quantity = 1,
                    Image = findSP.Image,
                    Price = (decimal)giatien,
                    Id_CuaHang = findSP.Id_CuaHang,
                    SoLuongTon = (int)findSP.SoLuongTon,
                    
                };
                ShoppingCart.Add(newItem);
            }
            else
            {
                findCartItem.Quantity++;
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

         public RedirectToRouteResult UpdateCart(int masp, int txtQuantity)
         {
             var itemFind = GetShoppingCartFormSession().FirstOrDefault(p => p.MaSP == masp);
             if (itemFind != null)
             {
                 itemFind.Quantity = txtQuantity;
             }
             return RedirectToAction("Index");
         }


          public JsonResult Plus(int masp)
          {
              bool result = false;
              var itemFind = GetShoppingCartFormSession().FirstOrDefault(p => p.MaSP == masp);
              if (itemFind != null)
              {
                  itemFind.Quantity++;
                itemFind.SoLuongTon--;
              }
              return Json(result, JsonRequestBehavior.AllowGet);
          }
        public JsonResult Minus(int masp)
        {
            bool result = false;
            var itemFind = GetShoppingCartFormSession().FirstOrDefault(p => p.MaSP == masp);
            if (itemFind != null)
            {
                itemFind.Quantity--;
                itemFind.SoLuongTon++;
                if (itemFind.Quantity < 1)
                {
                    RemoveCartItem(masp);
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       

        public ActionResult CartSumary()
        {
            ViewBag.CartCount = GetShoppingCartFormSession().Count();
            return PartialView("CartSumary");
        }
      
        public JsonResult RemoveCartItem(int masp)
        {
            bool result = false;
            var itemFind = GetShoppingCartFormSession().FirstOrDefault(p => p.MaSP == masp);
            GetShoppingCartFormSession().Remove(itemFind);            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete()
        {
            GetShoppingCartFormSession().Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ThanhToan()
        {
            string currentUserId = User.Identity.GetUserId();           

            if (currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult VnPay(CartItem model)
        {
            List<CartItem> ShoppingCart = GetShoppingCartFormSession();
            decimal tongtien = ShoppingCart.Sum(p => p.Quantity * p.Price);
            int sum = (int)tongtien;
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            VnPayLibrary pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (sum * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        public ActionResult ConfirmThanhToan()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                VnPayLibrary pay = new VnPayLibrary();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }

        public ActionResult Order(string fullname, string number_phone, string address_sum, string pay_at_home)
        {           
            DB_ThuongMaiDienTu context = new DB_ThuongMaiDienTu();

            List<CartItem> listCartItems = GetShoppingCartFormSession();
            var TongSoLuong = listCartItems.Sum(p => p.Quantity);
            var Tongtien = listCartItems.Sum(p => p.Quantity * p.Price);          

            var list_sanpham = new List<SanPham>();
            for (int i = 0; i < listCartItems.Count; i++)
            {
                var masp = listCartItems[i].MaSP;
                var item = context.SanPhams.Where(p => p.Id_SanPham == masp).FirstOrDefault();
                list_sanpham.Add(item);
            }

            var list_id_cuahang = new List<string>();
            for (int i = 0; i < list_sanpham.Count; i++)
            {
                var id = list_sanpham[i].Id_CuaHang;
                list_id_cuahang.Add(id);
            }
            /*int id_sanpham = listCartItems[0].MaSP;*/
            /*var sanPham = context.SanPhams.Where(p => p.Id_SanPham == id_sanpham).FirstOrDefault();*/
            /*var id_cuahang = sanPham.Id_CuaHang;*/

            string currentUserId = User.Identity.GetUserId();
            KhachHang kh = context.KhachHangs.Where(p => p.Id_KhachHang == currentUserId).FirstOrDefault();

           /* if (currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }*/
            string hinhThucThanhToan = "";
            if(pay_at_home != null)
            {
                hinhThucThanhToan = "Thanh toán tại nhà";

            }
            else
            {
                hinhThucThanhToan = "Thanh toán online";
            }
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    
                  
                    for (int i = 0; i < list_id_cuahang.Count; i++)
                    {
                        DonDatHang donDatHang = new DonDatHang();
                        donDatHang.Id_KhachHang = currentUserId;
                        donDatHang.NgayDatHang = DateTime.Now;
                        donDatHang.NgayGiaoHang = DateTime.Today.AddDays(7);
                        donDatHang.DiaChiGiaoHang = address_sum;
                        donDatHang.PhiGiaoHang = 15000;                        
                        donDatHang.TinhTrangDonDatHang = "Hoàn tất giao";
                        donDatHang.HinhThucThanhToan = hinhThucThanhToan;
                        donDatHang.Id_CuaHang = list_id_cuahang[i];
                        donDatHang.TenNguoiNhan = fullname;
                        donDatHang.SDTNguoiNhan = number_phone;
                        context.DonDatHangs.Add(donDatHang);
                        context.SaveChanges();

                        foreach (var item in listCartItems)
                        {
                            if(item.Id_CuaHang == donDatHang.Id_CuaHang)
                            {
                                ChiTietDonDatHang chiTietDonDatHang = new ChiTietDonDatHang()
                                {
                                    Id_SanPham = item.MaSP,
                                    Id_DonDatHang = donDatHang.Id_DonDatHang,
                                    SoLuong = item.Quantity,
                                    GiaTien = (double)item.Price,
                                    TinhTrangChiTietDonHang = "Hoàn Thành",                                    
                                };
                                context.ChiTietDonDatHangs.Add(chiTietDonDatHang);
                                context.SaveChanges();
                                var findSP = context.SanPhams.Where(p => p.Id_SanPham == item.MaSP).FirstOrDefault();
                                if (findSP != null)
                                {
                                    findSP.SoLuongTon -= item.Quantity;
                                }
                                context.Entry(findSP).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                        }

                    }
                                                       
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    
                    transaction.Rollback();
                    return Content("Gặp lỗi khi đặt hàng: " + ex.Message);
                }
            }

            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            string list_product = "";
            list_product += "<table border=\"1px\" cellspacing=\"0\" cellpadding=\"20px\">\r\n <thead>\r\n <tr>\r\n                   <th>Tên sản phẩm</th>\r\n                    <th>Giá sản phẩm</th>\r\n                   <th>Số lượng</th>\r\n      <th>Thành tiền</th>\r\n          </tr>\r\n            </thead>\r\n            <tbody>";
            foreach (var item in listCartItems)
            {
                list_product += "<tr> <td>"+ item.TenSP + "</td> <td> "+ String.Format(info, "{0:c}", item.Price) + " </td> <td>" + item.Quantity +"</td> <td>"+ String.Format(info, "{0:c}", item.Money) + "</td> </tr>";
            }
            list_product += "</tbody>\r\n        </table>";

            GetShoppingCartFormSession().Clear();

            //SEND EMAIL
            string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

            content = content.Replace("{{listProduct}}", list_product);
            content = content.Replace("{{CustomerName}}", kh.TenKh);
            content = content.Replace("{{Phone}}", kh.SDT);           
            content = content.Replace("{{Address}}", kh.DiaChi);
            content = content.Replace("{{Total}}", String.Format(info, "{0:c}", Tongtien));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Viet Mark Shop", content);

            return RedirectToAction("ConfirmOrder", "ShoppingCart");
        }
        public ActionResult ConfirmOrder()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                VnPayLibrary pay = new VnPayLibrary();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            GetShoppingCartFormSession().Clear();
            return View();
        }
        public ActionResult Payment()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFormSession();
            var sum = ShoppingCart.Sum(p => p.Quantity * p.Price);
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "Đơn hàng từ VietMark";
            string returnUrl = "https://localhost:44348/ShoppingCart/ThanhToan";
            string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/ShoppingCart/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = sum.ToString();
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient(Result result)
        {
            //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
            string rMessage = result.message;
            string rOrderId = result.orderId;
            string rErrorCode = result.errorCode; // = 0: thanh toán thành công
            return View();
        }
    }
}