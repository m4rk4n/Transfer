import { Agency } from "../agency";
import { agencyPartnersViewModel } from "../agencyPartnersViewModel";

export class AgencyUpdateModel {
  agency: Agency;
  agencyPartners: agencyPartnersViewModel[] = [];
}
