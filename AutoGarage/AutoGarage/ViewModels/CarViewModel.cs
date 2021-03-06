﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage.ViewModels
{
    public class CarViewModel
    {
        public int ID { get; set; }
        public string DRN { get; set; }
        public string ModelName { get; set; }

        public override string ToString()
        {
            return $"Reg.#: {DRN} / Vehicle Model: {ModelName}";
        }
    }
}
