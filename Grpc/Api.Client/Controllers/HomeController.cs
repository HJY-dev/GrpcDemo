using Api.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly Greeter.GreeterClient _greeterClient;
        readonly Orders.OrdersClient _ordersClient;
        public HomeController(Greeter.GreeterClient greeterClient, Orders.OrdersClient ordersClient)
        {
            _greeterClient = greeterClient;
            _ordersClient = ordersClient;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("testGrpc")]
        public string TestGrpc()
        {
            var result = _greeterClient.SayHello(new HelloRequest { Name = "Mike" });
            return result.Message;
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns></returns>
        [HttpGet("getOrderDataGrpc")]
        public string GetOrderDataGrpc(int page,int size)
        {
            var result = _ordersClient.GetOrderList(new OrderRequest { Page = page, Size= size });
            return result.Message;
        }

    }
}
