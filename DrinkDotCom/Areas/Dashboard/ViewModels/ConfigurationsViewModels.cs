﻿using DrinkDotCom.Entities;
using DrinkDotCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkDotCom.Areas.Dashboard.ViewModels
{
    public class ConfigurationsListingViewModels : PageViewModel
    {
        public List<Configuration> Configurations { get; set; }

        public int? ConfigurationType { get; set; }
        public string SearchTerm { get; set; }

        public bool isPartial { get; set; }

        public Pager Pager { get; set; }
    }
}