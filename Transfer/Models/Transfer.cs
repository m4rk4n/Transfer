using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool AgencyTravel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string TransferFrom { get; set; }
        public string TransferTo { get; set; }
        public string TransferDuration { get; set; }
        public string PickUpAddress { get; set; } //
        public string FlightNumber { get; set; } // a number?
        public DateTime DepartureDate { get; set; }
        public DateTime PickUpDepartureTime { get; set; }
        public bool RoundTrip { get; set; }
        public int NumberOfPassengers { get; set; }
        public int ChildSeats { get; set; }
        public int Boosters { get; set; } //what's a booster
        public string Notice { get; set; }
        public string  Message { get; set; }
        public bool DoINeedSendMessageToClient { get; set; }
        public bool DidISendMessageToComment { get; set; }
        public bool Paid { get; set; }
        public string PaymentMethod { get; set; } // maybe something more extensive here
        public string PaypalEmail { get; set; }
        public string PaymentMethodNotice { get; set; }
        public double PriceInKn { get; set; }
        public double PriceInEur { get; set; }
        public bool ConfirmTransfer { get; set; }

        public int? AgencyId { get; set; }
        public Agency Agency { get; set; } 
        public int? PartnerId { get; set; } // should it be nullable?
        public Partner Partner { get; set; }
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } // not needed, will be removed
    }
}
