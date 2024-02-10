import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormHelperText,
  IconButton,
  Input,
  InputLabel,
  Stack,
  Typography,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import SendIcon from "@mui/icons-material/Send";
import { PDFViewer, pdf } from "@react-pdf/renderer";
import { useFormik } from "formik";
import dayjs from "dayjs";
import RefundRemindingDocument from "../../Users/RefundRemindingDocument/RefundRemindingDocument";
import { useEmailConfiguration } from "../../Users/hooks/EmailConfigurationProvider";
import { useLocalizationContext } from "@mui/x-date-pickers/internals";
import * as yup from "yup";
import { DatePicker } from "@mui/x-date-pickers";
import CustomerResponse from "../../../utils/Types/CustomerResponse";

export interface DirectDebitingCustomerDunningDialogProps {
  open: boolean;
  customer: CustomerResponse;
  onSubmit: (
    blob: Blob | null,
    returnedDate: Date,
    commission: number,
    waitingWeeks: number
  ) => void;
  onClose: () => void;
}

export default function DirectDebitingCustomerDunningDialog({
  open,
  customer,
  onSubmit,
  onClose,
}: DirectDebitingCustomerDunningDialogProps) {
  const init = {
    returnedDate: new Date(),
    commission: 4.48,
    waitingWeeks: 4,
  };
  const { emailConfiguration } = useEmailConfiguration();
  const localization = useLocalizationContext();
  const formik = useFormik({
    initialValues: init,
    validationSchema: yup.object({
      returnedDate: yup.date().required("Refund date is required"),
      commission: yup
        .number()
        .positive("Commission should be positive")
        .required("Additional costs are required"),
      waitingWeeks: yup
        .number()
        .positive("Waiting weeks should be positive")
        .required("Weeks of waiting is required"),
    }),
    onSubmit: (values) => {
      pdf(
        <RefundRemindingDocument
          emailConfiguration={emailConfiguration!}
          customer={customer.customer}
          returnedDate={values.returnedDate}
          commission={Number(values.commission)}
          waitingWeeks={Number(values.waitingWeeks)}
          dateFormat={localization.utils.formats.fullDate}
        />
      )
        .toBlob()
        .then((blob) => {
          onSubmit(
            blob,
            values.returnedDate,
            values.commission,
            values.waitingWeeks
          );
        });
    },
  });

  return (
    <Dialog
      maxWidth="xl"
      onClose={onClose}
      aria-labelledby="create-customer-dialog-title"
      open={open}
    >
      <Box component={"form"} onSubmit={formik.handleSubmit}>
        <DialogTitle sx={{ m: 0, p: 2 }} id="create-customer-dialog-title">
          <Typography component="div">E-Mail senden</Typography>
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
        <DialogContent dividers>
          <Stack direction="row" spacing={2} width={"70vw"}>
            <Stack spacing={2} width={"50%"}>
              <DatePicker
                autoFocus={true}
                label="Datum der Rückerstattung"
                onChange={(value) => {
                  console.log(value);
                  formik.setFieldValue("returnedDate", value!.toDate());
                }}
                value={dayjs(formik.values.returnedDate)}
                sx={{ width: "100%" }}
              />
              <InputLabel htmlFor="standard-adornment-amount">
                Zusätzliche Kosten
              </InputLabel>
              <Input
                id="standard-adornment-amount"
                {...formik.getFieldProps("commission")}
                error={
                  formik.touched.commission && Boolean(formik.errors.commission)
                }
                inputProps={{
                  inputMode: "numeric",
                }}
              />
              {formik.touched.commission &&
                Boolean(formik.errors.commission) && (
                  <FormHelperText>{formik.errors.commission}</FormHelperText>
                )}
              <InputLabel htmlFor="standard-adornment-amount">
                Wochenlange Wartezeit
              </InputLabel>
              <Input
                id="standard-adornment-amount"
                {...formik.getFieldProps("waitingWeeks")}
                error={
                  formik.touched.waitingWeeks &&
                  Boolean(formik.errors.waitingWeeks)
                }
                inputProps={{
                  inputMode: "numeric",
                }}
              />
              {formik.touched.waitingWeeks &&
                Boolean(formik.errors.waitingWeeks) && (
                  <FormHelperText>{formik.errors.waitingWeeks}</FormHelperText>
                )}
            </Stack>
            <PDFViewer showToolbar style={{ width: "800px", height: "70vh" }}>
              <RefundRemindingDocument
                emailConfiguration={emailConfiguration!}
                customer={customer.customer}
                returnedDate={formik.values.returnedDate}
                commission={Number(formik.values.commission)}
                waitingWeeks={Number(formik.values.waitingWeeks)}
                dateFormat={localization.utils.formats.fullDate}
              />
            </PDFViewer>
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Schließen</Button>
          <Button variant="contained" type="submit" endIcon={<SendIcon />}>
            Senden
          </Button>
        </DialogActions>
      </Box>
    </Dialog>
  );
}
