using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaffleModel;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for RaffleContext
/// </summary>
/// 
namespace RaffleModel
{
    public class RaffleContext
    {
        public RaffleContext()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }


    public partial class User
    {
        public int distFk { get; set; }

    }

    public partial class TicketSold
    {
        //public string LastDistUserName { get; set; }
        //public int LastDistUserFk { get; set; }
        public string RaffleName { get; set; }
        public string ChurchName { get; set; }
        public string MemberName { get; set; }
        public string SalesBy { get; set; }
        public string StrTicketSoldDate { get; set; }
    }


    public partial class DIST_USERS
    {
        public DIST_USERS()
        {

        }
        public string FromTicket { get; set; }
        public string ToTicket { get; set; }
        public int TotalTicket { get; set; }
        public string status { get; set; }
        public string FullAddress { get; set; }

    }

    public partial class TicketsDistribution
    {
        public string DistUserName { get; set; }
        [DataMember]
        public string LastDistUserName { get; set; }
        [DataMember]
        public int LastDistUserFk { get; set; }
        public int FTick { get; set; }
        public int TTick { get; set; }

        public string FromDistUserName { get; set; }
        public string ToDistUserName { get; set; }

        public string strCreatedDate { get; set; }

        [DataMember]
        public string Address { get; set; }
        
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string email { get; set; }

        public decimal Amount { get; set; }

        public int TicketDistribution_PK { get; set; }

    }
    


}