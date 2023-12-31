﻿using December.Core.Entities.Areas;
using December.Core.Entities;

namespace FinalProjectsDecmeberUI.ViewModels.HomeVMs
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; } = null!;
        public List<Category> Categories { get; set; } = null!;
        public List<Collection> Collections { get; set; } = null!;
        public List<Product> Products { get; set; } = null!;
        public List<Brand> Brands { get; set; } = null!;
        public List<Image> Images { get; set; } = null!;
        public int MenProductCount { get; set; }
        public int KidsProductCount { get; set; }
        public int WomenProductCount { get; set; }
        public int AccesoriesProductCount { get; set; }
        public int JwelleryProductCount { get; set; }
        public int NewProductCount { get; set; }

    }
}
