using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BexMediaCom.Gateway;
using BexMediaCom.Models;

namespace BexMediaCom.Manager
{
    public class CardRegistrationManager
    {
        CardRegistrationGateway cardRegistrationGateway = new CardRegistrationGateway();

        public bool Save(CardRegistration cardRegistration)
        {

            if (IsNumberExists(cardRegistration.Number))
            {
                throw new Exception("Sorry!!!Card Number Already Exists");
            }

            return cardRegistrationGateway.Save(cardRegistration) > 0;
        }

        public bool IsNumberExists(string Number)
        {
            return cardRegistrationGateway.IsNumberExists(Number);
        }

        public List<CardRegistration> ShowAllCardNumber()
        {
            return cardRegistrationGateway.ShowAllCardNumber();
        }

        public List<CardRegistration> GetAllCardRegistrations()
        {
            return cardRegistrationGateway.GetAllCardRegistrations();
        }
        public CardRegistration GetCardNumberById(int? id)
        {
            return cardRegistrationGateway.GetCardNumberById(id);
        }

        public int UpdateCardNumber(CardRegistration cardRegistration)
        {
            return cardRegistrationGateway.UpdateCardNumber(cardRegistration);
        }
    }
}