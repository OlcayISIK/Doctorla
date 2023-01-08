using Doctorla.Core.InternalDtos;
using Doctorla.Data.Members;
using Doctorla.Data.Shared;
using Doctorla.Dto;
using Doctorla.Dto.Payment;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Helpers
{
    public static class IyzicoHelper
    {
        private const string firstCategory = "Health"; 
        private const string secondCategory = "Online Health Service\""; 

        public static bool MakePayment(IyzicoAPI iyzicoApi, PaymentDto paymentDto, User user, Appointment appointment)
        {
            var result = SendPaymentRequest(iyzicoApi, paymentDto, user, appointment);

            //status == "failure" if failed
            if (result.Status == "success")
                return true;

            //log error
            return false;
        }
        
        //Todo - given ids are copied from old application, check if it needs to be normalized
        private static Payment SendPaymentRequest(IyzicoAPI iyzicoApi, PaymentDto paymentDto, User user, Appointment appointment)
        {
            var options = new Options();
            options.ApiKey = iyzicoApi.ApiKey;
            options.SecretKey = iyzicoApi.SecretKey;
            options.BaseUrl = iyzicoApi.Url;

            var request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = appointment.Id.ToString() + ":" + user.Id.ToString();
            request.Price = appointment.SessionPrice.ToString();
            //For commision
            request.PaidPrice = appointment.SessionPrice.ToString();
            request.Currency = Currency.TRY.ToString();
            //request.Installment = 1;
            request.BasketId = appointment.SessionKey;
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            var paymentCard = new PaymentCard();
            paymentCard.CardHolderName = paymentDto.CardDetailDto.CardHolderName;
            paymentCard.CardNumber = paymentDto.CardDetailDto.CardNumber;
            paymentCard.ExpireMonth = paymentDto.CardDetailDto.ExpireMonth;
            paymentCard.ExpireYear = paymentDto.CardDetailDto.ExpireYear;
            paymentCard.Cvc = paymentDto.CardDetailDto.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            var buyer = new Buyer();
            buyer.Id = user.Id.ToString();
            buyer.Name = user.Name;
            buyer.Surname = user.Surname;
            buyer.GsmNumber = user.PhoneNumber;
            buyer.Email = user.Email;
            buyer.IdentityNumber = user.IdentificationNumber.ToString();
            buyer.LastLoginDate = user.LastLoginDate.ConvertDateToIyzicoFormat(); //"2015-10-05 12:43:35"
            buyer.RegistrationDate = user.CreatedAt.ConvertDateToIyzicoFormat(); 
            buyer.RegistrationAddress = user.Address; //"Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = paymentDto.IpAddress;
            buyer.City = user.City; //"Istanbul";
            buyer.Country = user.Country; //"Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            var shippingAddress = new Address();
            shippingAddress.ContactName = paymentDto.BillingAddressDto.ContactName;
            shippingAddress.City = paymentDto.BillingAddressDto.City;
            shippingAddress.Country = paymentDto.BillingAddressDto.Country;
            shippingAddress.Description = paymentDto.BillingAddressDto.Description;
            shippingAddress.ZipCode = paymentDto.BillingAddressDto.ZipCode;
            request.ShippingAddress = shippingAddress;

            var billingAddress = new Address();
            billingAddress.ContactName = paymentDto.BillingAddressDto.ContactName;
            billingAddress.City = paymentDto.BillingAddressDto.City;
            billingAddress.Country = paymentDto.BillingAddressDto.Country;
            billingAddress.Description = paymentDto.BillingAddressDto.Description;
            billingAddress.ZipCode = paymentDto.BillingAddressDto.ZipCode;
            request.BillingAddress = billingAddress;

            var basketItems = new List<BasketItem>();

            var basketItem = new BasketItem();
            basketItem.Id = appointment.SessionKey + appointment.Id.ToString();
            basketItem.Name = appointment.Id.ToString();
            basketItem.Category1 = firstCategory;
            basketItem.Category2 = secondCategory;
            basketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            //Todo - check if this asks for 9 as 900 (like stripe)
            basketItem.Price = appointment.SessionPrice.ToString();
            basketItems.Add(basketItem);

            request.BasketItems = basketItems;
            
            return Payment.Create(request, options);
        }
        private static string ConvertDateToIyzicoFormat(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
            
    }
}
