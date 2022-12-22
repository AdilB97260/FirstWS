using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PaymentController
/// </summary>
public class PaymentController
{
    #region [DECLRATION]
    RaffleEntities _entities = new RaffleEntities();
    #endregion

    public PaymentController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int AddBillingInfo(tblBillingInformation billingInfo)
    {
        _entities.tblBillingInformations.AddObject(billingInfo);
        _entities.SaveChanges();
        return billingInfo.billing_pk;
    }

    public string GetDistNameByTicketNumber(int _fromTicket, int _toTicket)
    {
        OnlineTicketDistribution obj=  _entities.OnlineTicketDistributions.ToList().Where(x => x.from_ticket <= _fromTicket && x.to_number >= _toTicket).FirstOrDefault();
        if(obj !=null)
        {
            return obj.dist_name;
        }
        else
        {
            OnlineTicketDistribution obj1 = _entities.OnlineTicketDistributions.ToList().Where(x => x.to_number == _fromTicket).FirstOrDefault();

            if (obj1 != null)
            {
                return obj1.dist_name;
            }
            else
            {
                return "";
            }
        }
            
    }


    public int AddTicketBuyer(List<tblTicketBuy> lstTicketBuy)
    {
        foreach (tblTicketBuy obj in lstTicketBuy)
        {
            obj.created_date = DateTime.Now;
            _entities.tblTicketBuys.AddObject(obj);
            _entities.SaveChanges();
            return 1;
        }
        return 0;
    }

    public int GetTicketBuyerID()
    {
        tblTicketBuyerID obj = new tblTicketBuyerID();
        obj.created_date = DateTime.Now;
        _entities.tblTicketBuyerIDs.AddObject(obj);
        _entities.SaveChanges();
        return obj.ticket_buyer_id;
    }

    public int AddPaymentInfo(PaymentInfo paymentInfo)
    {
        _entities.PaymentInfoes.AddObject(paymentInfo);
        _entities.SaveChanges();
        return paymentInfo.payment_id;
    }


    public int UpdatePaymentInfo(PaymentInfo paymentInfo)
    {
        PaymentInfo objPay = _entities.PaymentInfoes.Where(x => x.payment_id == paymentInfo.payment_id).FirstOrDefault();
        if (objPay != null)
        {
            objPay.stire_payment_id = paymentInfo.stire_payment_id;
            objPay.payment_status = "Paid";
            objPay.confirmation_no = paymentInfo.confirmation_no;
            objPay.modified_date = DateTime.Now;
            _entities.SaveChanges();
            return paymentInfo.payment_id;
        }
        else
        {
            return 0;
        }
    }

    public int UpdateFailedPaymentInfo(PaymentInfo paymentInfo)
    {
        PaymentInfo objPay = _entities.PaymentInfoes.Where(x => x.payment_id == paymentInfo.payment_id).FirstOrDefault();
        if (objPay != null)
        {
            objPay.payment_error = paymentInfo.payment_error;
            objPay.payment_status = "Failed";
            objPay.modified_date = DateTime.Now;
            _entities.SaveChanges();
            return paymentInfo.payment_id;
        }
        else
        {
            return 0;
        }
    }

}