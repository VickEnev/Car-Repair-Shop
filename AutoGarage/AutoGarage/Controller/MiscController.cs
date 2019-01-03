﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGarage.ViewModels;
using AutoGarage.Data;
using AutoGarage.DataModel.AutomobileDataModels;
using AutoGarage.DataModel.SparePartsDataModels;

namespace AutoGarage.Controller
{
    public class MiscController : Controller
    {
    

        public MiscController(AutomobileDbContext context) : base (context)
        {
            this.context = context;
        }

        // read methods 
        public List<object> GetAllBrands()
        {
            try
            {
                if (context.Brands != null)
                {
                    var result = new List<object>();
                    foreach (var b in context.Brands)
                    {
                        result.Add(new BrandViewModel() { Id = b.Id, Name = b.Name });
                    }
                    return result;
                }
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }


            return null;
        }
        /// <summary>
        /// Gets all car models in the database.
        /// </summary>
        /// <returns></returns>
        public List<CarModelViewModel> GetAllModels()
        {
            try
            {
                if (context.Models != null)
                {
                    var result = new List<CarModelViewModel>();
                    foreach (var b in context.Models)
                    {
                        result.Add(new CarModelViewModel() { Id = b.Id, Name = b.Name });
                    }
                    return result;
                }

            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }


            return null;
        }
        /// <summary>
        /// Gets all car models for a specific brand.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public List<CarModelViewModel> GetAllModels(string brandName)
        {
            try
            {
                if (context.Models != null)
                {
                    var result = new List<CarModelViewModel>();
                    foreach (var b in context.Models)
                    {
                        if (b.CarBrand.Name == brandName)
                            result.Add(new CarModelViewModel() { Id = b.Id, Name = b.Name });
                    }
                    return result;
                }

            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }


            return null;
        }

        public List<ColorViewModel> GetAllColors()
        {
            try
            {
                if (context.Colors != null)
                {
                    var result = new List<ColorViewModel>();
                    foreach (var b in context.Colors)
                    {
                        result.Add(new ColorViewModel() { Id = b.Id, Name = b.Name });
                    }
                    return result;
                }
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }



            return null;
        }


        public List<EngineViewModel> GetAllEnginesByModel(string ModelName)
        {
            try
            {
                if (context.Engines != null)
                {
                    var result = new List<EngineViewModel>();
                    foreach (var b in context.Engines.Where(e => e.CarModel.Name == ModelName))
                    {
                        result.Add(new EngineViewModel() { Id = b.Id, EngineNumber = b.EngineNumber, Volume = b.Volume });
                    }
                    return result;
                }
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }



            return null;
        }

        public CarModelDataModel GetModelByName(string modelName, string brandName)
        {
            try
            {
                CarModelDataModel result = new CarModelDataModel();
                result = context.Models.FirstOrDefault(m => m.Name == modelName && m.CarBrand.Name == brandName);
                return result;
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }
            return null;
        }

        // write methods

        public void WriteBrandDataModelToDatabase(BrandDataModel model)
        {
            context.Brands.Add(model);
            context.SaveChanges();
        }

        public void WriteColorsDataModelToDatabase(List<ColorDataModel> models)
        { 
            context.Colors.AddRange(models);
            context.SaveChanges();
        }

        public void WriteBrandDataModelToDatabase(List<BrandDataModel> models)
        {
            context.Brands.AddRange(models);
            context.SaveChanges();
        }

        public void WriteCarModelsDataModelsToDatabase(List<CarModelDataModel> models)
        {
            context.Models.AddRange(models);
            context.SaveChanges();
        }

        public void WriteEngineDataModelToDatabase(EngineDataModel model)
        {
            context.Engines.Add(model);
            context.SaveChanges();
        }

        public void AddOrUpdateParts(SparePartsDataModel model)
        {
            if (context.Spare_Parts.Contains(model))
            {
                var m = context.Spare_Parts.First(p => p.Id == model.Id);
                m.Name = model.Name;
                m.Price = model.Price;
                m.IsDeleted = model.IsDeleted;
                
                this.EditEntity<SparePartsDataModel>(model);
            }
            else
            {
                context.Spare_Parts.Add(model);
                context.SaveChanges();
            }

        }

    }
}
