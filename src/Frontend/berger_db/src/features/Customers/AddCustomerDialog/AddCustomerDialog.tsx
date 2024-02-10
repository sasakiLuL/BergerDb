import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  Stack,
  Tooltip,
  Typography,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import SaveIcon from "@mui/icons-material/Save";
import AddPersonalInfoForm from "./AddPersonalInfoForm";
import AddAddressForm from "./AddAddressForm";
import AddMembershipForm from "./AddMembershipForm";
import * as yup from "yup";
import { useFormik } from "formik";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import AddNotationForm from "./AddNotationForm";
import {
  EntryType,
  MemberType,
  PaymentType,
  Sex,
} from "../../../utils/Types/Customer";
import CustomerValidationSchema from "../../../utils/ValidationSchemas/CustomerValidationSchema";

export interface AddCustomerProps {
  prefix: string;
  firstName: string;
  lastName: string;
  email: string;
  personalId: number;
  notation: string;
  sex: Sex;
  street: string;
  zipCode: string;
  city: string;
  memberType: MemberType;
  institution: string;
  entryType: EntryType;
  paymentType: PaymentType;
  amount: number;
  currentInvoiceDate: Date | null;
  lastInvoiceDate: Date | null;
  currentCreditDate: Date | null;
  lastCreditDate: Date | null;
}

interface AddCustomerDialogProps {
  open: boolean;
  onSubmit: (value: AddCustomerProps) => void;
  onClose: () => void;
}

export default function AddCustomerDialog({
  open,
  onSubmit,
  onClose,
}: AddCustomerDialogProps) {
  const initialValues: AddCustomerProps = {
    prefix: "",
    firstName: "",
    lastName: "",
    email: "",
    personalId: 0,
    notation: "",
    sex: Sex.Male,
    street: "",
    zipCode: "",
    city: "",
    memberType: MemberType.Apothecary,
    institution: "",
    entryType: EntryType.GE,
    paymentType: PaymentType.Billing,
    amount: 0,
    currentInvoiceDate: null,
    lastInvoiceDate: null,
    currentCreditDate: null,
    lastCreditDate: null,
  };

  const formik = useFormik({
    initialValues: initialValues,
    validationSchema: yup.object({
      prefix: CustomerValidationSchema.prefix,
      personalId: CustomerValidationSchema.personalId,
      notation: CustomerValidationSchema.notation,
      firstName: CustomerValidationSchema.firstName,
      lastName: CustomerValidationSchema.lastName,
      email: CustomerValidationSchema.email,
      sex: CustomerValidationSchema.sex,
      registrationDate: CustomerValidationSchema.registrationDate,
      street: CustomerValidationSchema.street,
      zipCode: CustomerValidationSchema.zipCode,
      city: CustomerValidationSchema.city,
      memberType: CustomerValidationSchema.memberType,
      institution: CustomerValidationSchema.institution,
      entryType: CustomerValidationSchema.entryType,
      paymentType: CustomerValidationSchema.paymentType,
      amount: CustomerValidationSchema.amount,
      currentInvoiceSendedOn: CustomerValidationSchema.currentInvoiceSendedOn,
      lastInvoiceSendedOn: CustomerValidationSchema.lastInvoiceSendedOn,
      currentCreditReceivedOn: CustomerValidationSchema.currentCreditReceivedOn,
      lastCreditReceivedOn: CustomerValidationSchema.lastCreditReceivedOn,
    }),
    onSubmit: onSubmit,
    validateOnChange: false,
  });

  return (
    <Dialog
      maxWidth="xl"
      onClose={onClose}
      aria-labelledby="create-customer-dialog-title"
      open={open}
    >
      <Box
        component="form"
        onSubmit={formik.handleSubmit}
        onReset={formik.handleReset}
      >
        <DialogTitle sx={{ m: 0, p: 2 }} id="create-customer-dialog-title">
          <Stack direction="row" alignItems="center">
            <Typography component="div">Kunde hinzufügen</Typography>
            <IconButton
              aria-label="close"
              onClick={onClose}
              sx={{
                position: "absolute",
                right: 8,
                top: 8,
                color: (theme) => theme.palette.grey[500],
              }}
            >
              <CloseIcon />
            </IconButton>
            <Tooltip title="Reset" sx={{ ml: 1 }}>
              <IconButton type="reset" onClick={formik.handleReset}>
                <RestartAltIcon />
              </IconButton>
            </Tooltip>
          </Stack>
        </DialogTitle>
        <Stack>
          <DialogContent dividers>
            <AddPersonalInfoForm formik={formik} />
          </DialogContent>
          <DialogContent dividers>
            <AddAddressForm formik={formik} />
          </DialogContent>
          <DialogContent dividers>
            <AddMembershipForm formik={formik} />
          </DialogContent>
          <DialogContent dividers>
            <AddNotationForm formik={formik} />
          </DialogContent>
          <DialogActions>
            <Button onClick={onClose}>Schließen</Button>
            <Button variant="contained" type="submit" startIcon={<SaveIcon />}>
              Erstellen
            </Button>
          </DialogActions>
        </Stack>
      </Box>
    </Dialog>
  );
}
