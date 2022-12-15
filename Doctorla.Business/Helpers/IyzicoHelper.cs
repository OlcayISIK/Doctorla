using Doctorla.Core.InternalDtos;
using Doctorla.Data.Members;
using Doctorla.Dto;
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
        public static bool MakePayment(IyzicoAPI iyzicoApi)
        {
            var result = SendPaymentRequest(iyzicoApi);

            //status == "failure" if failed
            if (result.Status == "success")
                return true;

            //log error
            return false;
        }

        private static Payment SendPaymentRequest(IyzicoAPI iyzicoApi, User user)
        {
            var options = new Options();
            options.ApiKey = iyzicoApi.ApiKey;
            options.SecretKey = iyzicoApi.SecretKey;
            options.BaseUrl = iyzicoApi.Url;

            var request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            var paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            var buyer = new Buyer();
            buyer.Id = user.Id.ToString();
            buyer.Name = user.Name;
            buyer.Surname = user.Surname;
            buyer.GsmNumber = user.PhoneNumber;
            buyer.Email = user.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            var shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            var billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            var basketItems = new List<BasketItem>();

            var basketItem = new BasketItem();
            basketItem.Id = "BI101";
            basketItem.Name = "Binocular";
            basketItem.Category1 = "Collectibles";
            basketItem.Category2 = "Accessories";
            basketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            basketItem.Price = "1";
            basketItems.Add(basketItem);

            request.BasketItems = basketItems;
            
            return Payment.Create(request, options);
        }
    }
}
