using PrateEX.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net;

namespace PrateEX.Controllers
{
    public class PirateController : Controller
    {
        PirateContext db = new PirateContext();
        // GET: Pirate
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Booty()
        {
            var list = db.Crews.GroupBy(g => g.PirateId).ToList();
            var jsonO = list.Select(gl => new { a = gl.First().Pirate.Name, b = gl.Sum(c => c.Booty) }).OrderByDescending(gl => gl.b).ToList();
            return Json(jsonO, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Pirate()
        {
            var jsonO = db.Pirates.ToList().Select(p => new { a = p.Name, b = p.Date }).OrderByDescending(p => p.b).ToList();
            return Json(jsonO, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Ship()
        {
            var jsonO = db.Ships.ToList().Select(s => new { a = s.Name, b = s.Tonnage, c = s.Type }).OrderByDescending(s => s.b).ToList();
            return Json(jsonO, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Crew()
        {
            var jsonO = db.Crews.ToList().Select(c => new { a = c.Id, b = c.Pirate.Name, c = c.Booty }).OrderByDescending(c => c.c);
            return Json(jsonO, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string name)
        {
            //Debug.WriteLine("from client: "+ name);
            var found = false;
            var info = "";
            //List<Pirate> plist = db.Pirates.Where(p => p.Name.ToUpper().Contains(name.ToUpper())).ToList(); ;
            List<string> namelist = null;
            //List<string> nlist = db.Pirates.Select(p => p.Name.ToUpper()).ToList();
            //if (plist.Count!=0)
            if (db.Pirates.ToList().Exists(p => p.Name.ToUpper().Contains(name.ToUpper())))
            {
                //namelist = plist.Select(pl => pl.Name).ToList();
                namelist = db.Pirates.Where(p => p.Name.ToUpper().Contains(name.ToUpper())).ToList().Select(x => x.Name).ToList();
                //foreach (var p in plist) { namelist.Add(p.Name); }
                var jsonO = new
                {
                    found = true,
                    info = "",
                    namelist

                };
                return Json(jsonO, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var jsonO = new
                {
                    found = false,
                    info = " name could not be found "
                };
                return Json(jsonO, JsonRequestBehavior.AllowGet);
            }


            //Debug.WriteLine("list length: "+ nameList.Count());

        }


        public ActionResult ShowPirates(int? page)
        {

            return View(db.Pirates.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult PirateDetail(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var pd = db.Pirates.Find(id);
            if (pd == null) { return HttpNotFound(); }
            return View(pd);
        }
        [Authorize]
        public ActionResult EditPirate(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var ep = db.Pirates.Find(id);
            if (ep == null) { return HttpNotFound(); }

            return View(ep);
        }
        [HttpPost]
        public ActionResult EditPirate(int? id, Pirate pirate)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var ep = db.Pirates.Find(id);
            if (TryUpdateModel(ep, "", new string[] { "Name", "Date" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("ShowPirates");
                }
                catch (Exception ex) { ModelState.AddModelError("", "Edit can not be saved"); }
            }
            return View(ep);
        }
        [Authorize]
        public ActionResult DeletePirate(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var dp = db.Pirates.Find(id);
            if (dp == null) { return HttpNotFound(); }
            return View(dp);
        }

        [HttpPost]
        public ActionResult DeletePirate(int? id, Pirate p)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var dp = db.Pirates.Find(id);
            try
            {
                db.Pirates.Remove(dp);
                db.SaveChanges();
                return RedirectToAction("ShowPirates");
            }
            catch (Exception ex) { ModelState.AddModelError("", "the input can not be deleterd"); }

            return View(dp);
        }
        [Authorize]
        public ActionResult CreatePirate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePirate(Pirate pirate)
        {
            if (ModelState.IsValid)
            {
                try {
                    db.Pirates.Add(pirate);
                    db.SaveChanges();
                    return RedirectToAction("ShowPirates");
                }
                catch (Exception e) { ModelState.AddModelError("","the input can not be created"); }
            }
            return View(pirate);
        }
        public ActionResult ShowShips(int? page)
        {
            return View(db.Ships.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult CreateShip()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateShip(Ship ship)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Ships.Add(ship);
                    db.SaveChanges();
                    return RedirectToAction("ShowShips");
                }
                catch (Exception ex) { ModelState.AddModelError("","input can not be added"); }
                
            }
            return View(ship);
        }

        public ActionResult DeleteShip(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var ship = db.Ships.Find(id);
            if (ship == null) { return HttpNotFound(); }
            return View(ship);
        }
        [HttpPost]
        public ActionResult DeleteShip(int? id, Ship s)
        {
            Ship ship = null;
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            try
            {
                ship = db.Ships.Find(id);
                db.Ships.Remove(ship);
                db.SaveChanges();
                return RedirectToAction("ShowShips");
            }
            catch(Exception ex) { ModelState.AddModelError("", "the input can not be removed"); }
            return View(ship);
        }

        public ActionResult EditShip(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Ship ship = db.Ships.Find(id);
            if ( ship == null) { return HttpNotFound(); }
            return View(ship);
        }
        [HttpPost]
        public ActionResult EditShip(int? id, Ship s)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Ship ship = db.Ships.Find(id);
            if (TryUpdateModel(ship, "", new string[]{"Name","Type","Tonnage" }))
            {
                try {
                    db.SaveChanges();
                    return RedirectToAction("ShowShips");
                } catch(Exception ex) { ModelState.AddModelError("","input can't be edited"); }
                
            }
            
            return View(ship);
        }
        public ActionResult ShipDetail(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Ship ship = db.Ships.Find(id);
            if (ship == null){return HttpNotFound();}
            return View(ship);
        }
        public ActionResult ShowCrew()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);

        }


    }
}