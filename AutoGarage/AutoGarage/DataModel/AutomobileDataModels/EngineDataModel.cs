﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGarage.DataModel.AutomobileDataModels
{
    public class EngineDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Volume { get; set; }
        public string EngineNumber { get; set; }
        public int Horsepower { get; set; }
        
        public int CarModelId { get; set; }
        public CarModelDataModel CarModel { get; set; }
       
    }
}
