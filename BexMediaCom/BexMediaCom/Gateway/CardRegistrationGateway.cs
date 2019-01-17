using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BexMediaCom.Models;

namespace BexMediaCom.Gateway
{
    public class CardRegistrationGateway : CommonGateway
    {
        public int Save(CardRegistration cardRegistration)
        {
            try
            {
                Query = "INSERT INTO CardReg(Number, Status) VALUES(@Number, @Status)";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Number", SqlDbType.VarChar);
                Command.Parameters["Number"].Value = cardRegistration.Number;
                Command.Parameters.Add("Status", SqlDbType.VarChar);
                Command.Parameters["Status"].Value = cardRegistration.Status;
                Connection.Open();
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }

        }

        public bool IsNumberExists(string number)
        {
            try
            {
                bool isNumberExists = false;
                Query = "SELECT*FROM CardReg WHERE Number='" + number + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    isNumberExists = true;
                }
                return isNumberExists;
            }
            catch (Exception exception)
            {
                return false;
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<CardRegistration> ShowAllCardNumber()
        {
            try
            {
                Query = "SELECT*FROM CardReg";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CardRegistration> cardDivisions = new List<CardRegistration>();
                int number = 1;
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CardRegistration cardRegistration = new CardRegistration();
                        cardRegistration.Id = Convert.ToInt32(Reader["Id"].ToString());
                        cardRegistration.Number = Reader["Number"].ToString();
                        cardRegistration.Status = Reader["Status"].ToString();
                        cardRegistration.SNumber = number;
                        cardDivisions.Add(cardRegistration);
                        number++;
                    }
                    Reader.Close();
                }
                return cardDivisions;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

        }

        public List<CardRegistration> GetAllCardRegistrations()
        {
            try
            {
                Query = "SELECT*FROM CardReg";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                List<CardRegistration> cardRegistrations = new List<CardRegistration>();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CardRegistration cardRegistration = new CardRegistration();
                        cardRegistration.Id = Convert.ToInt32(Reader["Id"].ToString());
                        cardRegistration.Number = Reader["Number"].ToString();
                        cardRegistrations.Add(cardRegistration);
                    }
                    Reader.Close();
                }
                return cardRegistrations;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

        }

        public CardRegistration GetCardNumberById(int? id)
        {
            try
            {
                Query = "SELECT * FROM CardReg Where Id = " + id + "";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Connection.Open();
                Reader = Command.ExecuteReader();
                CardRegistration cardRegistrations = new CardRegistration();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        CardRegistration cardRegistration = new CardRegistration();
                        cardRegistration.Id = Convert.ToInt32(Reader["Id"]);
                        cardRegistration.Number = Reader["Number"].ToString();
                        cardRegistrations = cardRegistration;
                    }
                    Reader.Close();
                }
                return cardRegistrations;
            }
            catch (Exception exception)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }

        }

        public int UpdateCardNumber(CardRegistration cardRegistration)
        {
            try
            {
                Query = @"UPDATE CardReg
   SET [Number] = @Number
 WHERE Id = '" + cardRegistration.Id + "'";
                Command.CommandText = Query;
                Command.Connection = Connection;
                Command.Parameters.Clear();
                Command.Parameters.Add("Number", SqlDbType.VarChar);
                Command.Parameters["Number"].Value = cardRegistration.Number;
                Connection.Open();
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception exception)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }

        }
    }
}