using Grpc.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Server.Services
{
    public class OrderService : Orders.OrdersBase
    {
        public override Task<OrderReply> GetOrderList(OrderRequest request, ServerCallContext context)
        {
            var orderList = new List<object>() { 
                new { orderid = 1, orderno = "No1", user = "张三", address = "北京" },
                new { orderid = 2, orderno = "No2", user = "李四", address = "上海" },
                new { orderid = 3, orderno = "No3", user = "五五", address = "广州" },
                new { orderid = 4, orderno = "No4", user = "赵六", address = "深圳" },
                new { orderid = 5, orderno = "No5", user = "钱七", address = "天津" }
            };
            var result = orderList.Skip((request.Page - 1) * request.Size).Take(request.Size);

            return Task.FromResult(new OrderReply
            {
                Message = JsonConvert.SerializeObject(result)
            }) ;

            
        }
    }
}
