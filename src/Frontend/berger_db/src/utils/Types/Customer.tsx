export enum Sex {
  Male,
  Female,
}

export enum MemberType {
  Apothecary,
  LayPerson,
  Doctor,
  NonmedicalPractitioner,
}

export enum EntryType {
  GE,
  EE,
}

export enum PaymentType {
  Billing,
  DirectDebiting,
}

export default interface Customer {
  id: string;
  prefix: string;
  firstName: string;
  lastName: string;
  personalId: number;
  notation: string;
  email: string;
  registrationDate: Date;
  sex: Sex;
  street: string;
  zipCode: string;
  city: string;
  memberType: MemberType;
  institution: string;
  entryType: EntryType;
  paymentType: PaymentType;
  amount: number;
  isRecivedInvoice: boolean;
  isRecivedDunning: boolean;
  isDebtor: boolean;
  currentInvoiceSendedOn: Date | null;
  lastInvoiceSendedOn: Date | null;
  currentCreditReceivedOn: Date | null;
  lastCreditReceivedOn: Date | null;
  dunningSendedOn: Date | null;
  terminatedOn: Date | null;
}
