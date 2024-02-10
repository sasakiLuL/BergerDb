import { EditorState } from "draft-js";

export default interface EmailConfiguraion {
  street: string;
  city: string;
  zipCode: string;
  phoneNumber: string;
  email: string;
  homePage: string;
  accountName: string;
  iban: string;
  bic: string;
  gid: string;
  taxIdentificationNumber: string;
  invoicePdfBody: EditorState;
  invoiceEmailSubject: string;
  invoiceEmailBody: string;
  billingRemindingEmailSubject: string;
  billingRemindingEmailBody: string;
  directDebitingRemindingEmailSubject: string;
  directDebitingRemindingEmailBody: string;
}
