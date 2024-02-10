import Customer from "./Customer";
import Link from "./Link";

export default interface CustomerResponse {
  customer: Customer;
  links: Link[];
}
