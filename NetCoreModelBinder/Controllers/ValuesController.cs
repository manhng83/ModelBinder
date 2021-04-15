using Microsoft.AspNetCore.Mvc;
using NetCoreModelBinder.Models;
using System.Diagnostics;

namespace NetCoreModelBinder.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// GET api/Values/Get
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IActionResult Get(ProductModel product)
        {
            if (product == null)
            {
                return NotFound();
            }
            Debug.WriteLine(product.Name);
            return Ok(product);
        }

        /// <summary>
        /// GET api/Values/Get1
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get1")]
        public IActionResult Get1([ModelBinder(typeof(ProductEntityBinder))] ProductModel product)
        {
            if (product == null)
            {
                return NotFound();
            }
            Debug.WriteLine(product.Name);
            return Ok(product);
        }

        /// <summary>
        /// GET api/Values/Get2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get2")]
        public IActionResult Get2(
                string id,
                string name,
                decimal price,
                string category
            )
        {
            Debug.WriteLine(name);
            return Ok(name);
        }

        

        /// <summary>
        /// POST api/Values/Post
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post")]
        public IActionResult Post([FromBody] ProductModel product)
        {
            if (product == null)
            {
                return NotFound();
            }
            Debug.WriteLine(product.Name);
            return Ok(product);
        }

        /// <summary>
        /// POST api/Values/Post1
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post1")]
        public IActionResult Post1(ProductModel product)
        {
            if (product == null)
            {
                return NotFound();
            }
            Debug.WriteLine(product.Name);
            return Ok(product);
        }

        /// <summary>
        /// POST api/Values/Post2
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post2")]
        public IActionResult Post2([ModelBinder(typeof(ProductEntityBinder))] ProductModel product)
        {
            if (product == null)
            {
                return NotFound();
            }
            Debug.WriteLine(product.Name);
            return Ok(product);
        }

        /// <summary>
        /// POST api/Values/Test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Test")]
        public IActionResult Test(
                string id,
                string name,
                decimal price,
                string category
            )
        {
            Debug.WriteLine(name);
            return Ok(name);
        }
    }
}