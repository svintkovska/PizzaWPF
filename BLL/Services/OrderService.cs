using AutoMapper;
using BLL.ModelsDTO;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService
    {
        EFAppContext context = new EFAppContext();
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly BasketRepository _basketRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository(context);
            _orderItemsRepository = new OrderItemsRepository(context);
            _basketRepository = new BasketRepository(context);
        }

        public async Task AddToBasket(BasketDTO item)
        {
            if (item != null)
            {

                var translateObj = new MapperConfiguration(map => map.CreateMap<BasketDTO, BasketEntity>()).CreateMapper();
                var entity = translateObj.Map<BasketDTO, BasketEntity>(item);

                await _basketRepository.Create(entity);
            }
        }

        public async Task UpdateBasket(BasketDTO item)
        {
            if (item != null)
            {
                var translateObj = new MapperConfiguration(map => map.CreateMap<BasketDTO, BasketEntity>()).CreateMapper();
                var entity = translateObj.Map<BasketDTO, BasketEntity>(item);
                await _basketRepository.Update(entity);
            }
        }

        public async Task DeleteFromBasket(int userId, int productId)
        {
            await _basketRepository.Delete(userId, productId);
        }

        public async Task<int> CreateOrder(int userId)
        {
            var order = new OrderDTO()
            {
                UserId = userId,
                DateCreated = DateTime.Now
            };
            var translateObj = new MapperConfiguration(map => map.CreateMap<OrderDTO, OrderEntity>()).CreateMapper();
            var entity = translateObj.Map<OrderDTO, OrderEntity>(order);

            await _orderRepository.Create(entity);
            return entity.Id;
        }

        public async Task CreateOrderItems(int userId)
        {
            var orderQuery = _orderRepository.GetAll().Where(u => u.UserId == userId).LastOrDefault();

            var basketQuery = _basketRepository.GetAll().Where(u => u.UserId == userId);

            var orderItems = new List<OrderItemDTO>();

            foreach (var item in basketQuery)
            {
                var product = context.Products.AsQueryable().Where(p => p.Id == item.ProductId).FirstOrDefault();
                var price = product.IsOnDiscount ? product.DiscountPrice : product.Price;
                var order = new OrderItemDTO()
                {
                    ProductId = item.ProductId,
                    DateCreated = DateTime.Now,
                    Count = item.Count,
                    Price = price,
                    OrderId = orderQuery.Id
                };
                var translateObj = new MapperConfiguration(map => map.CreateMap<OrderItemDTO, OrderItemEntity>()).CreateMapper();
                var entity = translateObj.Map<OrderItemDTO, OrderItemEntity>(order);
                await _orderItemsRepository.Create(entity);
            }

            foreach (var item in basketQuery)
            {
                await DeleteFromBasket(userId, item.ProductId);
            }
        }

        public List<OrderDTO> GetOrderHistory(int userId)
        {
            var orders = _orderRepository.GetAll().Where(u => u.UserId == userId);
            var list = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var translateObj = new MapperConfiguration(map => map.CreateMap<OrderEntity, OrderDTO>()).CreateMapper();
                var orderDTO = translateObj.Map<OrderEntity, OrderDTO>(order);
                list.Add(orderDTO);
            }
            return list;
        }

        public List<OrderItemDTO> GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = _orderItemsRepository.GetAll().Where(o => o.OrderId == orderId);
            var list = new List<OrderItemDTO>();
            foreach (var item in orderItems)
            {
                var translateObj = new MapperConfiguration(map => map.CreateMap<OrderItemEntity, OrderItemDTO>()).CreateMapper();
                var itemDTO = translateObj.Map<OrderItemEntity, OrderItemDTO>(item);
                list.Add(itemDTO);
            }
            return list;
        }
    }
}
