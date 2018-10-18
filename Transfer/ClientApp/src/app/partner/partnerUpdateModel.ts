import { Partner } from "./partner";
import { PartnerAgenciesViewModel } from "./partnerAgenciesViewModel";

export class PartnerUpdateModel {
  partner: Partner;
  partnerAgencies: PartnerAgenciesViewModel[] = [];
  vehicleId: number;
}
