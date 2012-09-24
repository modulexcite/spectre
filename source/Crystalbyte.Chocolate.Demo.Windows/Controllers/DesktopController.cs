﻿using System;
using System.Web.Mvc;
using Crystalbyte.Chocolate.Models;
using Crystalbyte.Chocolate.UI;

namespace Crystalbyte.Chocolate.Controllers
{
    public sealed class DesktopController : Controller
    {
        public ViewResult Index() {
            return View(new DesktopModel());
        }
    }
}
