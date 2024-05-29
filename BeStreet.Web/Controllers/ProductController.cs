using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using BeStreet.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace BeStreet.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProd _prods = new BusinesLogic().GetProdBL();
        
        [AllowAdmin]
        public ActionResult Index()
        {
            SessionStatus();
            ViewData["currentNav"] = "Product";

            var pdvm = _prods.GetAllProducts();

            if (pdvm == null) return HttpNotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(pdvm);
        }

        [AllowAdmin]
        public ActionResult Create()
        {
            SessionStatus();
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
            }

            ViewData["currentNav"] = "Product/Create";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAdmin]
        public ActionResult Create(HttpPostedFileBase formFile, Product obj)
        {
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
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
        
        [AllowAdmin]
        public ActionResult Delete(int? id)
        {
            SessionStatus();
            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
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
        [AllowAdmin]
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

        [AllowAdmin]
        public ActionResult Edit(int? id)
        {
            SessionStatus();
            ViewData["currentNav"] = "Product";

            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
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
        [AllowAdmin]
        public ActionResult Edit(HttpPostedFileBase formFile, Product obj)
        {
            ViewBag.formFile = formFile;
            ViewData["formFile"] = formFile;
            ViewData["currentNav"] = "Product";

            using (var db = new BeStreetContext())
            {
                ViewData["Pdt"] = new SelectList(db.ProductTypes, "PdtId", "PdtName").ToList();
                ViewData["Size"] = new SelectList(db.Sizes, "SizeId", "SizeName").ToList();
                ViewData["Color"] = new SelectList(db.Colors, "ColorId", "ColorName").ToList();
                ViewData["Target"] = new SelectList(db.Targets, "TargetId", "TargetName").ToList();
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

            ViewData["item"] = _prods.GetDetailedProductById((int)id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult GetFilteredProducts(int[] typeIds, int[] sizeIds, int[] colorIds, string targetName)
        {
            var productFilter = _prods.GetFilteredProducts(typeIds, sizeIds, colorIds, targetName);

            return PartialView("_ProductList", productFilter);
        }
    }
}