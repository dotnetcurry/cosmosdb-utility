using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CosmosDBUtilitiesExample.Models;
using CosmosDBUtilitiesExample.Repository;
using CosmosDBUtilitiesExample.DTOs;

namespace CosmosDBUtilitiesExample.Controllers
{
    public class HomeController : Controller
    {
        ToDoItemsRepository repo;
        public HomeController(ToDoItemsRepository repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {

            var vm = await repo.GetAllItems();
            return View(vm);
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    repo.Delete<string>(id);
                    await repo.SaveChanges();
                }
                    
            }
            catch {
                ViewData["error"] = "an error occurred";
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                 var vm = await 
                    repo.GetById<DetailItemDTO, string>(id);
                return View(vm);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public IActionResult Create()
        {
                return View("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DetailItemDTO  vm)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                if (vm.SubItems != null)
                    vm.SubItems.Where(x => x.Id == null)
                        .All(x =>
                        {
                            x.Id = Guid.NewGuid().ToString();
                            return true;
                        });
                try
                {
                    vm.Owner = User.Identity.Name;
                    repo.Update(false, vm);
                    
                    await repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewData["error"] = "an error occurred";
                }
            }
            return View(vm);
        }
        [HttpPost]
    public async Task<IActionResult> Create(DetailItemDTO vm)
    {
        if (ModelState.IsValid)
        {
            ModelState.Clear();
            if (vm.SubItems != null)
                vm.SubItems.Where(x => x.Id == null)
                    .All(x =>
                    {
                        x.Id = Guid.NewGuid().ToString();
                        return true;
                    });
            try
            {
                vm.Owner = User.Identity.Name;
                vm.Id = Guid.NewGuid().ToString();

                repo.Add(false, vm);
                    
                await repo.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData["error"] = "an error occurred";
            }
        }
        return View("Edit", vm);
    }
        public async Task<IActionResult> AllSubitems ()
        {
            var members = await repo.AllMembers();
            var vm=await repo.AllSubItems();
            return View(vm);
        }
        public async Task<IActionResult> AllTeamMembers()
        {
            var vm = await repo.AllMembers();
            return View(vm);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
