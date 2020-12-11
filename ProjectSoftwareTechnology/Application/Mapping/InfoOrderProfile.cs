using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class InfoOrderProfile
    {

        public static InfoOrderDTO MappingInfoOrderDto(this InfoOrder infoOrder)
        {
            var value = new InfoOrderDTO
            {
                IDP = infoOrder.IDP,
                IDSP = infoOrder.IDSP,
                Amount = infoOrder.Amount,
                Size = infoOrder.Size,
                Price = infoOrder.Price,
                IntoMoney = infoOrder.IntoMoney
            };
            return value;
        }


        public static InfoOrder MappingInfoOrder(this InfoOrderDTO infoOrderDto)
        {
            var infoOrder = new InfoOrder
            {
                IDP = infoOrderDto.IDP,
                IDSP = infoOrderDto.IDSP,
                Amount = infoOrderDto.Amount,
                Size = infoOrderDto.Size,
                Price = infoOrderDto.Price,
                IntoMoney = infoOrderDto.IntoMoney
            };
            return infoOrder;
        }


        public static void MappingInfoOrder(this InfoOrderDTO infoOrderDto, InfoOrder infoOrder)
        {
            infoOrder.IDP = infoOrderDto.IDP;
            infoOrder.IDSP = infoOrderDto.IDSP;
            infoOrder.Amount = infoOrderDto.Amount;
            infoOrder.Size = infoOrderDto.Size;
            infoOrder.Price = infoOrderDto.Price;
            infoOrder.IntoMoney = infoOrderDto.IntoMoney;
        }



        // hàm trả về danh sách ProductDTO và thực thi gán value của từng đối tượng Product cho ProductDTO
        public static IEnumerable<InfoOrderDTO> MappingInfoOrderDtos(this IEnumerable<InfoOrder> infoOrders)
        {
            foreach (var infoOrder in infoOrders)
            {
                yield return infoOrder.MappingInfoOrderDto();
            }
        }
    }
}
