using Microsoft.AspNetCore.Mvc;

namespace NetCoreModelBinder.Models
{
    /// <summary>
    ///
    /// </summary>
    [ModelBinder(BinderType = typeof(ProductEntityBinder))]
    public class ProductModel
    {
        /// <summary>
        ///
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Category { get; set; }
    }
}