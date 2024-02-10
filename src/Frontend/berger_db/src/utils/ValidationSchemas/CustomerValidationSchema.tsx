import { EntryType, MemberType, PaymentType, Sex } from "../Types/Customer";

import * as yup from "yup";

export default class CustomerValidationSchema {
  static notation: yup.StringSchema = yup.string().max(3000);

  static prefix: yup.StringSchema = yup
    .string()
    .max(50, "The name title is too long")
    .matches(/^[\p{L}0-9, \\\.\/-]*$/u, "The name title format is invalid");

  static personalId: yup.NumberSchema = yup
    .number()
    .positive("Id can be only positive number")
    .required("Personal id is required");

  static firstName: yup.StringSchema = yup
    .string()
    .required("Firstname is required")
    .matches(
      /^[\p{L}0-9 ,\.\/!@#$%&*()_-]*$/u,
      "The firstname can contain only letters"
    )
    .max(100, "The first name is too short");

  static lastName: yup.StringSchema = yup
    .string()
    .required("Lastname is required")
    .matches(
      /^[\p{L}0-9 ,\.\/!@#$%&*()_-]*$/u,
      "The lastname can contain only letters"
    )
    .max(100, "The last name is too short");

  static email: yup.StringSchema = yup
    .string()
    .email("Enter a valid email")
    .required("Email is required");

  static sex: yup.MixedSchema = yup
    .mixed()
    .oneOf(Object.values(Sex), "Wrong type")
    .required("The sex value is requred");

  static registrationDate: yup.AnySchema = yup.date();

  static street: yup.StringSchema = yup
    .string()
    .required("Street is required")
    .max(100, "The address name is too long")
    .matches(
      /^[\p{L}0-9, \\\.\/-]*$/u,
      "The address name can not contain special symbols"
    );

  static city: yup.StringSchema = yup
    .string()
    .required("City is required")
    .max(100, "The address name is too long")
    .matches(
      /^[\p{L}0-9, \\\.\/-]*$/u,
      "The address name can not contain special symbols"
    );

  static zipCode: yup.StringSchema = yup
    .string()
    .required("Zip code is required")
    .max(5, "The zip code should be 5 chars or less")
    .matches(/^[\d]+$/, "The zip code format is invalid");

  static memberType: yup.MixedSchema = yup
    .mixed()
    .oneOf(Object.values(MemberType), "Wrong type")
    .required("The member type is requred");

  static institution: yup.StringSchema = yup
    .string()
    .max(100, "The institution name is too long")
    .matches(
      /^[\p{L}0-9 ,\.\/!@#$%&*()_-]*$/u,
      "The institution name can not contain special symbols"
    );
  static entryType: yup.MixedSchema = yup
    .mixed()
    .oneOf(Object.values(EntryType), "Wrong type")
    .required("The entry type is requred");

  static paymentType: yup.MixedSchema = yup
    .mixed()
    .oneOf(Object.values(PaymentType), "Wrong type")
    .required("The payment type is requred");

  static amount: yup.NumberSchema = yup
    .number()
    .moreThan(-1, "Wrong value")
    .required("The amount is required");

  static currentInvoiceSendedOn: yup.AnySchema = yup.date().nullable();

  static lastInvoiceSendedOn: yup.AnySchema = yup.date().nullable();

  static currentCreditReceivedOn: yup.AnySchema = yup.date().nullable();

  static lastCreditReceivedOn: yup.AnySchema = yup.date().nullable();

  static terminatedOn: yup.AnySchema = yup.date().nullable();

  static dunningSendedOn: yup.AnySchema = yup.date().nullable();
}
