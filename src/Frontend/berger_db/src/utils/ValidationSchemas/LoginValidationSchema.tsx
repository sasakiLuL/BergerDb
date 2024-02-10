import * as yup from "yup";

export default class LoginValidationSchema {
  static email: yup.StringSchema = yup
    .string()
    .email("Enter a valid email")
    .required("Email is required");

  static password: yup.StringSchema = yup
    .string()
    .min(8, "Password should be of minimum 8 characters length")
    .max(30, "Password should be of maximum 30 characters length")
    .matches(/^[A-Za-z\d@$!%*#?&]+$/, "Password format is invalid.")
    .matches(/^(?=.*[A-Za-z])/, "Password requires at least one letter")
    .matches(/^(?=.*\d)/, "Password requires at least one digit ")
    .required("Password is required");
}
