using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class PayOrderProfile
    {

        public static PayOrderDTO MappingPayOrdertDto(this PayOrder payOrder)
        {
            var payOrderDTO = new PayOrderDTO
            {
                ID = payOrder.ID,
                UID = payOrder.UID,
                Receiver = payOrder.Receiver,
                Address = payOrder.Address,
                Phone = payOrder.Phone,
                Total = payOrder.Total,
                DateOrder = payOrder.DateOrder,
                Status = payOrder.Status,
                StatusPay = payOrder.StatusPay,
                Note = payOrder.Note,
                DateConfirm = payOrder.DateConfirm,
                DateToShip = payOrder.DateToShip,
                DateToPay = payOrder.DateToPay,
                DateCancel = payOrder.DateCancel
            };
            return payOrderDTO;
        }



        public static PayOrder MappingPayOrder(this PayOrderDTO payOrderDTO)
        {
            var payOrder = new PayOrder
            {
                ID = payOrderDTO.ID,
                UID = payOrderDTO.UID,
                Receiver = payOrderDTO.Receiver,
                Address = payOrderDTO.Address,
                Phone = payOrderDTO.Phone,
                Total = payOrderDTO.Total,
                DateOrder = payOrderDTO.DateOrder,
                Status = payOrderDTO.Status,
                StatusPay = payOrderDTO.StatusPay,
                Note = payOrderDTO.Note,
                DateConfirm = payOrderDTO.DateConfirm,
                DateToShip = payOrderDTO.DateToShip,
                DateToPay = payOrderDTO.DateToPay,
                DateCancel = payOrderDTO.DateCancel
            };
            return payOrder;
        }



        public static void MappingPayOrder(this PayOrderDTO payOrderDTO, PayOrder payOrder)
        {
            payOrder.ID = payOrderDTO.ID;
            payOrder.UID = payOrderDTO.UID;
            payOrder.Receiver = payOrderDTO.Receiver;
            payOrder.Address = payOrderDTO.Address;
            payOrder.Phone = payOrderDTO.Phone;
            payOrder.Total = payOrderDTO.Total;
            payOrder.DateOrder = payOrderDTO.DateOrder;
            payOrder.Status = payOrderDTO.Status;
            payOrder.StatusPay = payOrderDTO.StatusPay;
            payOrder.Note = payOrderDTO.Note;
            payOrder.DateConfirm = payOrderDTO.DateConfirm;
            payOrder.DateToShip = payOrderDTO.DateToShip;
            payOrder.DateToPay = payOrderDTO.DateToPay;
            payOrder.DateCancel = payOrderDTO.DateCancel;
        }


        public static IEnumerable<PayOrderDTO> MappingPayOrderDtos(this IEnumerable<PayOrder> payOrders)
        {
            foreach (var payOrder in payOrders)
            {
                yield return payOrder.MappingPayOrdertDto();
            }
        }
    }
}
