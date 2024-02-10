import {
  Button,
  Chip,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import CustomerResponse from "../../../utils/Types/CustomerResponse";
import UpdatePersonalInfoForm, {
  UpdatePersonalInfoProps,
} from "./UpdatePersonalInfoForm";
import UpdateMembershipForm, {
  UpdateMembershipProps,
} from "./UpdateMembershipForm";
import UpdateAddressForm, { UpdateAddressProps } from "./UpdateAddressForm";
import UpdateNotationForm, { UpdateNotationProps } from "./UpdateNotationForm";

interface ViewCustomerDialogProps {
  customer: CustomerResponse;
  open: boolean;
  dateFormat: string;
  onPersonalInfoSubmit: ({
    firstName,
    lastName,
    email,
    sex,
    registrationDate,
  }: UpdatePersonalInfoProps) => void;
  onMembershipSubmit: ({
    memberType,
    institution,
    entryType,
    paymentType,
    amount,
    currentInvoiceSendedOn,
    lastInvoiceSendedOn,
    currentCreditReceivedOn,
    lastCreditReceivedOn,
    terminatedOn,
  }: UpdateMembershipProps) => void;
  onAddressSubmit: ({ street, zipCode, city }: UpdateAddressProps) => void;
  onNotationSubmit: ({ notation }: UpdateNotationProps) => void;
  onClose: () => void;
}

export default function ViewCustomerDialog({
  customer,
  open,
  onClose,
  dateFormat,
  onPersonalInfoSubmit,
  onAddressSubmit,
  onMembershipSubmit,
  onNotationSubmit,
}: ViewCustomerDialogProps) {
  return (
    <Dialog
      maxWidth="xl"
      onClose={onClose}
      aria-labelledby="customized-dialog-title"
      open={open}
    >
      <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
        <Typography component="div">Customer</Typography>
        <Chip label={customer.customer.id} />
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
      </DialogTitle>
      <FormControl>
        <Stack>
          <DialogContent dividers>
            <UpdatePersonalInfoForm
              defaultValues={{
                prefix: customer.customer.prefix,
                firstName: customer.customer.firstName,
                lastName: customer.customer.lastName,
                personalId: customer.customer.personalId,
                email: customer.customer.email,
                sex: customer.customer.sex,
                registrationDate: customer.customer.registrationDate,
              }}
              dateFormat={dateFormat}
              onSubmit={onPersonalInfoSubmit}
            ></UpdatePersonalInfoForm>
          </DialogContent>
          <DialogContent dividers>
            <UpdateAddressForm
              defaultValues={{
                city: customer.customer.city,
                street: customer.customer.street,
                zipCode: customer.customer.zipCode,
              }}
              onSubmit={onAddressSubmit}
            ></UpdateAddressForm>
          </DialogContent>
          <DialogContent dividers>
            <UpdateMembershipForm
              defaultValues={{
                memberType: customer.customer.memberType,
                institution: customer.customer.institution,
                entryType: customer.customer.entryType,
                paymentType: customer.customer.paymentType,
                amount: customer.customer.amount,
                currentInvoiceSendedOn:
                  customer.customer.currentInvoiceSendedOn,
                lastInvoiceSendedOn: customer.customer.lastInvoiceSendedOn,
                currentCreditReceivedOn:
                  customer.customer.currentCreditReceivedOn,
                lastCreditReceivedOn: customer.customer.lastCreditReceivedOn,
                terminatedOn: customer.customer.terminatedOn,
                dunningSendedOn: customer.customer.dunningSendedOn,
              }}
              onSubmit={onMembershipSubmit}
            ></UpdateMembershipForm>
          </DialogContent>
          <DialogContent>
            <UpdateNotationForm
              defaultValues={{ notation: customer.customer.notation }}
              onSubmit={onNotationSubmit}
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={onClose}>Close</Button>
          </DialogActions>
        </Stack>
      </FormControl>
    </Dialog>
  );
}
