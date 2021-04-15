using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using NetCoreModelBinder.Models;
using System;

namespace NetCoreModelBinder
{
    /// <summary>
    ///
    /// </summary>
    public class ProductEntityBinderProvider : IModelBinderProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(ProductModel))
            {
                return new BinderTypeModelBinder(typeof(ProductEntityBinder));
            }

            return null;
        }
    }
}