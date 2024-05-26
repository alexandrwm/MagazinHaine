using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeStreet.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProd _prods = new BusinesLogic().GetProdBL();
        public ActionResult Index()
        {
            ViewData["currentNav"] = "Product";

            var pdvm = _prods.GetAllProducts();

            if (pdvm == null) return HttpNotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(pdvm);
        }

        public ActionResult Create()
        {
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Suppliers"] = new SelectList(db.Suppliers, "SupId", "SupName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
                ViewData["Status"] = new SelectList(db.Statuses, "StatusId", "StatusName").ToList();
            }

            ViewData["currentNav"] = "Product/Create";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase formFile, Product obj)
        {
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Suppliers"] = new SelectList(db.Suppliers, "SupId", "SupName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
                ViewData["Status"] = new SelectList(db.Statuses, "StatusId", "StatusName").ToList();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var pd = _prods.CreateProduct(obj);

                    if (formFile.ContentLength > 0)
                    {
                        var FileName = "P" + pd + obj.ColorId + obj.SizeId;
                        var FileExtension = ".png";
                        var SaveFileName = FileName + FileExtension;
                        var SavePath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgpd");
                        var SaveFilePath = Path.Combine(SavePath, SaveFileName);
                        
                        formFile.SaveAs(SaveFilePath);
                    }
                    TempData["SuccessMessage"] = "Salvat cu succes!";

                    //return RedirectToAction("Edit", new { id = theid });

                    return RedirectToAction("Index");
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Eroare la salvare: " + ex.Message;

                return View(obj);
            }
            ViewBag.ErrorMessage = "Datele nu s-au salvat.";

            return View(obj);
        }
        public ActionResult Delete(int? id)
        {
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Suppliers"] = new SelectList(db.Suppliers, "SupId", "SupName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
                ViewData["Status"] = new SelectList(db.Statuses, "StatusId", "StatusName").ToList();
            }

            ViewData["currentNav"] = "Product";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Vă rugăm să specificați Id Nr.";
                return RedirectToAction("Index");
            }
            var obj = _prods.GetProductById(id);
            
            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            var fileName = "P" + obj.PdId + obj.ColorId + obj.SizeId + ".png";
            var imgPath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgpd");
            var filePath = Path.Combine(imgPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.ImgFile = "/wwwroot/imgpd/" + fileName;
            }
            else
            {
                ViewBag.ImgFile = "/wwwroot/imgpd/No_image2.png";
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int PdId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var prodDeleted = _prods.DeleteProductById(PdId);
                    if (!prodDeleted)
                    {
                        TempData["ErrorMessage"] = "Nu s-au șters datele.";
                        return RedirectToAction("Index");
                    }
                    
                    TempData["SuccessMessage"] = "Delete SuccessMessage";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Nu s-au șters datele: " + ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            ViewData["currentNav"] = "Product";
            
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Suppliers"] = new SelectList(db.Suppliers, "SupId", "SupName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
                ViewData["Status"] = new SelectList(db.Statuses, "StatusId", "StatusName").ToList();
            }
            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }

            var obj = _prods.GetProductById(id);

            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            var fileName = "P" + obj.PdId + obj.ColorId + obj.SizeId + ".png";
            var imgPath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgpd");
            var filePath = Path.Combine(imgPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.ImgFile = "/wwwroot/imgpd/" + fileName;
            }
            else
            {
                ViewBag.ImgFile = "/wwwroot/imgpd/No_image2.png";
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase formFile, Product obj)
        {
            ViewBag.formFile = formFile;
            ViewData["formFile"] = formFile;
            ViewData["currentNav"] = "Product";
            
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Suppliers"] = new SelectList(db.Suppliers, "SupId", "SupName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
                ViewData["Status"] = new SelectList(db.Statuses, "StatusId", "StatusName").ToList();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var theid = obj.PdId;
                    
                    _prods.UpdateProductById(theid, obj);

                    if (formFile?.ContentLength > 0)
                    {
                        var FileName = "P" + theid + obj.ColorId + obj.SizeId;
                        var FileExtension = ".png";
                        var SaveFileName = FileName + FileExtension;
                        var SavePath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgpd");
                        var SaveFilePath = Path.Combine(SavePath, SaveFileName);

                        formFile.SaveAs(SaveFilePath);
                    }

                    TempData["SuccessMessage"] = "Successfully!";
                    return RedirectToAction("Edit", new { id = theid });

                    //return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
            ViewBag.ErrorMessage = "Corectarea erorii.";
            //ViewData["Pdt"] = new SelectList(_db.ProductTypes, "PdtId", "PdtName", obj.PdId);
            //ViewData["Brand"] = new SelectList(_db.Brands, "BrandId", "BrandName", obj.BrandId);
            return View(obj);
        }

        public ActionResult Show(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Home");
            }

            var obj = _prods.GetProductById(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Home");
            }

            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
            
                //string[] parts = id.Split('-');
                //string PrefixId = parts[0] + "-" + parts[1];
                //string PrefixId = parts[0];

                var productFilter = from p in db.Products
                                    join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt
                                    from p_pt in join_p_pt.DefaultIfEmpty()

                                    join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
                                    from p_color in join_p_color.DefaultIfEmpty()

                                    join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
                                    from p_size in join_p_size.DefaultIfEmpty()

                                    join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
                                    from p_target in join_p_target.DefaultIfEmpty()

                                    join status in db.Statuses on p.StatusId equals status.StatusId into join_p_status
                                    from p_status in join_p_status.DefaultIfEmpty()
                                    where p.PdId == id

                                    select new PdFilterVM
                                    {
                                        PdId = p.PdId,
                                        ColorId = p.ColorId,
                                        ColorName = p_color.ColorName,
                                        SizeId = p.SizeId,
                                        SizeName = p_size.SizeName,
                                        TargetId = p.TargetId,
                                        TargetName = p_target.TargetName,
                                        PdName = p.PdName,
                                        PdtId = p.PdtId,
                                        PdtName = p_pt.PdtName,
                                        PdPrice = p.PdPrice,
                                        PdStk = p.PdStk,
                                        StatusName = p_status.StatusName,
                                    };

                var items = productFilter.ToList();
                ViewData["item"] = items;
            //ViewData["colors"] = colors.Distinct(); ;

            return View(obj);
            }
        }
    }
}