using GrupoThera.WebUI.Controllers.api.Filters;
using GrupoThera.Business.Interfaces;
using GrupoThera.WebUI.Controllers.api.Filters;
using GrupoThera.BusinessModel.Converters.Mock;
using GrupoThera.Core.Envelope;
using GrupoThera.Core.ErrorsApiHandling;
using GrupoThera.Entities.Entity.Mock;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Web.Http.Description;


namespace GrupoThera.WebUI.Controllers.api
{
    [MyExceptionFilter]
    [RoutePrefix("rs")]
    public class MockController : ApiController
    {
        private IProductService _productManager;  

        public MockController(IProductService productManager)
        {
            _productManager = productManager; 
        }

        /// <summary>
        /// Get the list of products
        /// </summary> 
        /// <returns></returns>
        [HttpGet]
        [Route("products")]
        [ResponseType(typeof(Product))]
        [GenericAuthenticationFilter(true)]
        public IHttpActionResult getAllProducts()
        { 
            var products = _productManager.getAllProducts();
            if (products == null)
            {
                return BadRequest(GrupoTheraErrors.LIST_EMPTY.errorMessage);
            }
            DataEnvelope<Product> envelope = Envelope<Product>.createDataEnvelope(products);
            JObject jsonObject = new DataEnvelope2JsonConverter<Product>().convertToJson(envelope, new Product2JsonConverter());
            return Ok(jsonObject);
        }

        /// <summary>
        /// Get the product by id
        /// </summary> 
        /// <returns></returns>
        [HttpGet]
        [Route("products/{id}")]
        [ResponseType(typeof(Product))]
        [GenericAuthenticationFilter(true)]
        public IHttpActionResult getProductById(int id)
        {
            var products = _productManager.getProductById(id);
            if (products == null)
            {
                return BadRequest(GrupoTheraErrors.LIST_EMPTY.errorMessage);
            }
            DataEnvelope<Product> envelope = Envelope<Product>.createDataEnvelope(products);
            JObject jsonObject = new DataEnvelope2JsonConverter<Product>().convertToJson(envelope, new Product2JsonConverter());
            return Ok(jsonObject);
        }

        /// <summary>
        /// Create a new product
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        [Route("products")]
        [ResponseType(typeof(Product))]
        [GenericAuthenticationFilter(true)]
        public IHttpActionResult createProduct([FromBody]dynamic data)
        {
            #region listObjects
            //send with a list of objects 
            //dynamic jobject = JsonConvert.DeserializeObject<List<Product>>(data.ToString());
            /* Example : 
                           { 
                                { 
    	                            "id": 98,
                                    "name": "productPost1",
                                    "categoryId": 1
                                },
                                { 
                                    "id": 99,
                                    "name": "productPost2",
                                    "categoryId": 1
                                } 
                           }
             */
            #endregion listObjects 

            #region listObjectsComposed 
            //dynamic jobject = JsonConvert.DeserializeObject<productDto>(data.ToString());
            /* Example :              
                          {
                             "products": [
                                  { 
                                      "id": 98,
                                      "name": "productPost1",
                                      "categoryId": 1
                                  },
                                  { 
                                      "id": 99,
                                      "name": "productPost2",
                                      "categoryId": 1
                                  }
                              ]
                           }
            */
            #endregion listObjectsComposed

            #region SimpleObjectsComposed 

            /* Example class Product             
              { 
                "id": 1,
                "name": "productPost2",
                "categoryId": 1
              } 
            */


            Product jobject = JsonConvert.DeserializeObject<Product>(data.ToString());
            _productManager.createProduct(jobject);

            #endregion SimpleObjectsComposed  

            return Ok();
        }

        /// <summary>
        /// Update The product
        /// </summary> 
        /// <returns></returns>
        [HttpPut]
        [Route("products")]
        [ResponseType(typeof(Product))]
        [GenericAuthenticationFilter(true)]
        public IHttpActionResult updateProduct([FromBody]dynamic data)
        {
            Product jobject = JsonConvert.DeserializeObject<Product>(data.ToString());
            _productManager.updateProduct(jobject);
            return Ok();
        }

        /// <summary>
        /// Get the product by id
        /// </summary> 
        /// <returns></returns>
        [HttpDelete]
        [Route("products/{idDeleted}")]
        [ResponseType(typeof(Product))]
        [GenericAuthenticationFilter(true)]
        public IHttpActionResult deleteProduct(int idDeleted)
        {            
            _productManager.deleteProduct(idDeleted);
            return Ok();
        } 

    }
}
