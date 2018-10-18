import { Agency } from "../agency/agency";
import { Partner } from "../partner/partner";
import { Vehicle } from "../vehicle/vehicle";

export class Transfer {
  reservationTime: Date = new Date();
  agencyTravel: boolean;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  country: string;
  transferFrom: string;
  transferTo: string;
  transferDuration: string;
  pickupAddress: string;
  flightNumber: string;
  departureDate: Date = new Date();
  pickupDepartureTime: Date = new Date();
  roundTrip: boolean;
  numberOfPassengers: number;
  childSeats: number;
  boosters: number;
  notice: string;
  message: string;
  paid: boolean;
  paymentMethod: string;
  paymentMethodNotice: string;
  priceInKn: string;
  priceInEur: string;
  confirmTransfer: boolean;

  agency: Agency = new Agency();
  agencyId: number;
  partner: Partner = new Partner();
  partnerId: number;
  vehicle: Vehicle;
  vehicleId: number;
}
