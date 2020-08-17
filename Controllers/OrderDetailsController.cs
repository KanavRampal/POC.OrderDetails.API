using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace POC.OrderDetails.API.Controllers
{
    [Route("api/orderdetails")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public OrderDetailsController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet, Route("{id}")]
        public OrderDetails Get(int id)
        {
            OrderDetails retVal = null;
            if (id > 0)
            {
                retVal = GetOrderDetailsData(id);
            }
            return retVal;
        }


        private OrderDetails GetOrderDetailsData(int id)
        {
            OrderDetails retVal = new OrderDetails();

            // Get user data first if user exists get orders too.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetConnectionString(Constants.UserAPIConnectionUrl));

                var responseTask = client.GetAsync(id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    retVal.User = JsonConvert.DeserializeObject<User>(readTask.Result);
                }
            }
            if (retVal.User != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetConnectionString(Constants.OrderAPIConnectionUrl));

                    var responseTask = client.GetAsync("");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();

                        retVal.OrderList = JsonConvert.DeserializeObject<List<Order>>(readTask.Result);
                    }
                }
            }
            // Get OrdersData
            
            return retVal;
        }
    }
}