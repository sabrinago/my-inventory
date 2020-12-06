﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MyInventory.Data;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var list = _context.Suppliers.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier record)
        {
            var item = new Supplier()
            {
                CompanyName = record.CompanyName,
                Representative = record.Representative,
                Code = record.Code,
                Address = record.Address,
                DateAdded = DateTime.Now,
                Type = record.Type
            };
            _context.Suppliers.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Suppliers.Where(i => i.SupplierId == id).SingleOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Supplier record)
        {
            var item = _context.Suppliers.Where(i => i.SupplierId == id).SingleOrDefault();
            item.CompanyName = record.CompanyName;
            item.Representative = record.Representative;
            item.Code = record.Code;
            item.Address = record.Address;
            item.DateAdded = DateTime.Now;
            item.Type = record.Type;

            _context.Suppliers.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Suppliers.Where(i => i.SupplierId == id).SingleOrDefault();
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            _context.Suppliers.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}

