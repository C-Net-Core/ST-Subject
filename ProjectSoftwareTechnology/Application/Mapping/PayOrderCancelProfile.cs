using Application.DTOs;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Mapping
{
    public static class PayOrderCancelProfile
    {

        public static PayOrderCancelDTO MappingPayOrdertCancelDto(this PayOrderCancel payOrderCancel)
        {
            var payOrderCancelDTO = new PayOrderCancelDTO
            {
                ID = payOrderCancel.ID,
                IDP = payOrderCancel.IDP,
                UID = payOrderCancel.UID,
                Receiver = payOrderCancel.Receiver,
                Address = payOrderCancel.Address,
                Phone = payOrderCancel.Phone,
                Total = payOrderCancel.Total,
                DateOrder = payOrderCancel.DateOrder,
                DateCancel = payOrderCancel.DateCancel,
                Status = payOrderCancel.Status,
                StatusPay = payOrderCancel.StatusPay,
                Note = payOrderCancel.Note
            };
            return payOrderCancelDTO;
        }



        public static PayOrderCancel MappingPayOrderCancel(this PayOrderCancelDTO payOrderCancelDTO)
        {
            var payOrderCancel = new PayOrderCancel
            {
                ID = payOrderCancelDTO.ID,
                IDP = payOrderCancelDTO.IDP,
                UID = payOrderCancelDTO.UID,
                Receiver = payOrderCancelDTO.Receiver,
                Address = payOrderCancelDTO.Address,
                Phone = payOrderCancelDTO.Phone,
                Total = payOrderCancelDTO.Total,
                DateOrder = payOrderCancelDTO.DateOrder,
                DateCancel = payOrderCancelDTO.DateCancel,
                Status = payOrderCancelDTO.Status,
                StatusPay = payOrderCancelDTO.StatusPay,
                Note = payOrderCancelDTO.Note
            };
            return payOrderCancel;
        }



        public static void MappingPayOrderCancel(this PayOrderCancelDTO payOrderCancelDTO, PayOrderCancel payOrderCancel)
        {
            payOrderCancel.ID = payOrderCancelDTO.ID;
            payOrderCancel.IDP = payOrderCancelDTO.IDP;
            payOrderCancel.UID = payOrderCancelDTO.UID;
            payOrderCancel.Receiver = payOrderCancelDTO.Receiver;
            payOrderCancel.Address = payOrderCancelDTO.Address;
            payOrderCancel.Phone = payOrderCancelDTO.Phone;
            payOrderCancel.Total = payOrderCancelDTO.Total;
            payOrderCancel.DateOrder = payOrderCancelDTO.DateOrder;
            payOrderCancel.DateCancel = payOrderCancelDTO.DateCancel;
            payOrderCancel.Status = payOrderCancelDTO.Status;
            payOrderCancel.StatusPay = payOrderCancelDTO.StatusPay;
            payOrderCancel.Note = payOrderCancelDTO.Note;
        }


        public static IEnumerable<PayOrderCancelDTO> MappingPayOrderCancelDtos(this IEnumerable<PayOrderCancel> payOrderCancels)
        {
            foreach (var payOrderCancel in payOrderCancels)
            {
                yield return payOrderCancel.MappingPayOrdertCancelDto();
            }
        }
    }
}
