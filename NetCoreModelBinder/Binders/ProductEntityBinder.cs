using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCoreModelBinder.Models;
using System;
using System.Threading.Tasks;

namespace NetCoreModelBinder
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductEntityBinder : IModelBinder
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductEntityBinder()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(value, out var id))
            {
                // Non-integer arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    modelName, "Author Id must be an integer.");

                return Task.CompletedTask;
            }

            var productModel = new ProductModel();

            HttpRequest request = bindingContext.HttpContext.Request;

            productModel.Id = request.Form["Id"];
            productModel.Name = request.Form["Name"];
            productModel.Category = request.Form["Category"];
            productModel.Price = Convert.ToDecimal(request.Form["Price"]);

            bindingContext.Result = ModelBindingResult.Success(productModel);

            return Task.CompletedTask;
        }
    }
}