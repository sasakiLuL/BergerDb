import MainNavBar from "../MainNavBar/MainNavBar";
import {
  Button,
  Collapse,
  Container,
  Grid,
  IconButton,
  Stack,
  TextField,
  Tooltip,
  Typography,
  useTheme,
} from "@mui/material";
import { Font, PDFViewer } from "@react-pdf/renderer";
import Customer, {
  EntryType,
  MemberType,
  PaymentType,
  Sex,
} from "../../utils/Types/Customer";
import InvoiceDocument from "../../features/Users/InvoiceDocument/InvoiceDocument";
import RichTextField from "../../features/shared/RichTextField/RichTextField";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { PdfStyleProvider } from "../../features/shared/hooks/PdfStylesProvider";
import reguralFont from "../../assets/pdf-font/NotoSans-Regular.ttf";
import boldFont from "../../assets/pdf-font/NotoSans-Bold.ttf";
import italicFont from "../../assets/pdf-font/NotoSans-Italic.ttf";
import italicBoldFont from "../../assets/pdf-font/NotoSans-BoldItalic.ttf";
import { useFormik } from "formik";
import { useEmailConfiguration } from "../../features/Users/hooks/EmailConfigurationProvider";
import EmailConfiguraion from "../../utils/Types/EmailConfiguration";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import SaveIcon from "@mui/icons-material/Save";
import { useEffect, useState } from "react";
import { EditorState } from "draft-js";
import RefundRemindingDocument from "../../features/Users/RefundRemindingDocument/RefundRemindingDocument";
import RemindingDocument from "../../features/Users/RemindingDocument/RemindingDocument";
import { useLocalizationContext } from "@mui/x-date-pickers/internals";
import LoadingBackdrop from "../../features/shared/LoadingBackdrop/LoadingBackdrop";

export interface EmailConfigurationProps {
  loginPath: string;
  homePath: string;
}

Font.register({
  family: "NotoSans",
  fonts: [
    { src: reguralFont },
    { src: boldFont, fontWeight: 700 },
    { src: italicFont, fontStyle: "italic" },
    {
      src: italicBoldFont,
      fontStyle: "italic",
      fontWeight: 700,
    },
  ],
});

export default function EmailConfiguration({
  loginPath,
  homePath,
}: EmailConfigurationProps) {
  const theme = useTheme();
  const localization = useLocalizationContext();
  const { emailConfiguration, setEmailConfiguration } = useEmailConfiguration();

  let initialValues: EmailConfiguraion = !emailConfiguration
    ? {
        street: "",
        city: "",
        zipCode: "",
        phoneNumber: "",
        email: "",
        homePage: "",
        accountName: "",
        iban: "",
        bic: "",
        gid: "",
        taxIdentificationNumber: "",
        invoicePdfBody: EditorState.createEmpty(),
        invoiceEmailSubject: "",
        invoiceEmailBody: "",
        billingRemindingEmailSubject: "",
        billingRemindingEmailBody: "",
        directDebitingRemindingEmailSubject: "",
        directDebitingRemindingEmailBody: "",
      }
    : emailConfiguration;

  const handleSubmit = (value: EmailConfiguraion) => {
    setIsLoading(true);
    setEmailConfiguration(value);
    setIsLoading(false);
  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: handleSubmit,
    enableReinitialize: true,
    validateOnChange: false,
    validateOnBlur: false,
    validateOnMount: false,
  });

  const [isLoading, setIsLoading] = useState<boolean>(false);
  useEffect(() => {
    if (emailConfiguration) initialValues = emailConfiguration;
  }, [emailConfiguration]);

  const testCustomer: Customer = {
    id: "",
    prefix: "Dr.",
    firstName: "Oleksandr",
    lastName: "Kryvko",
    personalId: 123,
    notation: "",
    email: "",
    sex: Sex.Male,
    street: "Westadts. Garten 2",
    city: "Lueneburg",
    zipCode: "21335",
    registrationDate: new Date(),
    institution: "",
    memberType: MemberType.Doctor,
    entryType: EntryType.EE,
    paymentType: PaymentType.Billing,
    amount: 42,
    currentInvoiceSendedOn: new Date(),
    lastInvoiceSendedOn: new Date(),
    currentCreditReceivedOn: new Date(),
    lastCreditReceivedOn: new Date(),
    terminatedOn: new Date(),
    dunningSendedOn: new Date(),
    isRecivedInvoice: false,
    isRecivedDunning: false,
    isDebtor: false,
  };

  return (
    <>
      <LoadingBackdrop open={isLoading} />
      <MainNavBar loginPath={loginPath} homePath={homePath} />
      <Container maxWidth="xl">
        <Stack
          component="form"
          onSubmit={formik.handleSubmit}
          sx={{ p: theme.spacing(2) }}
        >
          <Stack direction="row" alignItems="center">
            <Typography sx={{ my: theme.spacing(2) }} typography="h3">
              Konfigurationen
            </Typography>
            <Tooltip title="Zurücksetzen" sx={{ ml: 1 }}>
              <IconButton type="reset" onClick={formik.handleReset}>
                <RestartAltIcon />
              </IconButton>
            </Tooltip>
            <Collapse in={formik.dirty}>
              <Button
                type="submit"
                variant="contained"
                startIcon={<SaveIcon />}
              >
                Änderungen speichern
              </Button>
            </Collapse>
          </Stack>
          <Grid container spacing={2}>
            <Grid xs={4} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Rechnung email</Typography>
              </Grid>
              <Grid item>
                <TextField
                  id="standard-multiline-flexible-invoiceSubject"
                  label="Betreff"
                  multiline
                  fullWidth
                  {...formik.getFieldProps("invoiceEmailSubject")}
                  variant="standard"
                />
              </Grid>
              <Grid item>
                <TextField
                  id="standard-multiline-flexible-invoiceEmailBody"
                  label="Text"
                  multiline
                  fullWidth
                  {...formik.getFieldProps("invoiceEmailBody")}
                  rows={10}
                  variant="standard"
                />
              </Grid>
            </Grid>
            <Grid xs={4} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Erinnerung email</Typography>
              </Grid>
              <Grid item>
                <TextField
                  id="standard-multiline-flexible-admonitionSubject"
                  label="Betreff"
                  multiline
                  fullWidth
                  {...formik.getFieldProps("billingRemindingEmailSubject")}
                  variant="standard"
                />
              </Grid>
              <Grid item>
                <TextField
                  id="standard-multiline-flexible-admonitionEmailBody"
                  label="Text"
                  multiline
                  fullWidth
                  {...formik.getFieldProps("billingRemindingEmailBody")}
                  rows={10}
                  variant="standard"
                />
              </Grid>
            </Grid>
            <Grid xs={4} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Mahnung email</Typography>
              </Grid>
              <Grid item>
                <TextField
                  id="standard-multiline-flexible-admonitionSubject"
                  label="Betreff"
                  multiline
                  fullWidth
                  {...formik.getFieldProps(
                    "directDebitingRemindingEmailSubject"
                  )}
                  variant="standard"
                />
              </Grid>
              <Grid item>
                <TextField
                  id="standard-multiline-flexible-admonitionEmailBody"
                  label="Text"
                  multiline
                  fullWidth
                  {...formik.getFieldProps("directDebitingRemindingEmailBody")}
                  rows={10}
                  variant="standard"
                />
              </Grid>
            </Grid>
          </Grid>
          <Grid container spacing={2}>
            <Grid xs={6} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Felder</Typography>
              </Grid>
              <Grid item container direction="row" spacing={2}>
                <Grid item xs={6}>
                  <TextField
                    id="street-field"
                    label="Straße"
                    fullWidth
                    variant="standard"
                    {...formik.getFieldProps("street")}
                  />
                </Grid>
                <Grid item xs={4}>
                  <TextField
                    id="city-field"
                    label="Stadt"
                    fullWidth
                    variant="standard"
                    {...formik.getFieldProps("city")}
                  />
                </Grid>
                <Grid item xs={2}>
                  <TextField
                    id="postal-code-field"
                    label="PLZ"
                    fullWidth
                    variant="standard"
                    {...formik.getFieldProps("zipCode")}
                  />
                </Grid>
              </Grid>
              <Grid item container direction="row" spacing={2}>
                <Grid item xs={3}>
                  <TextField
                    id="phone-number-field"
                    label="Telefonnummer"
                    fullWidth
                    variant="standard"
                    {...formik.getFieldProps("phoneNumber")}
                  />
                </Grid>
                <Grid item xs={4.5}>
                  <TextField
                    id="email-field"
                    label="Email"
                    fullWidth
                    variant="standard"
                    {...formik.getFieldProps("email")}
                  />
                </Grid>
                <Grid item xs={4.5}>
                  <TextField
                    id="home-page-field"
                    label="Home page"
                    fullWidth
                    variant="standard"
                    {...formik.getFieldProps("homePage")}
                  />
                </Grid>
              </Grid>
              <Grid item>
                <TextField
                  id="account-name-field"
                  label="Konto"
                  fullWidth
                  variant="standard"
                  {...formik.getFieldProps("accountName")}
                />
              </Grid>
              <Grid item>
                <TextField
                  id="iban-field"
                  label="IBAN"
                  fullWidth
                  variant="standard"
                  {...formik.getFieldProps("iban")}
                />
              </Grid>
              <Grid item>
                <TextField
                  id="bic-field"
                  label="BIC"
                  fullWidth
                  variant="standard"
                  {...formik.getFieldProps("bic")}
                />
              </Grid>
              <Grid item>
                <TextField
                  id="gid-field"
                  label="GID"
                  fullWidth
                  variant="standard"
                  {...formik.getFieldProps("gid")}
                />
              </Grid>
              <Grid item>
                <TextField
                  id="tin-field"
                  label="Steuernummer"
                  fullWidth
                  variant="standard"
                  {...formik.getFieldProps("taxIdentificationNumber")}
                />
              </Grid>
            </Grid>
            <Grid xs={6} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Rechnung-Körper</Typography>
              </Grid>
              <Grid item>
                <PdfStyleProvider>
                  <RichTextField
                    editorState={formik.values.invoicePdfBody}
                    setEditorState={(event) => {
                      formik.setFieldValue("invoicePdfBody", event);
                    }}
                  />
                </PdfStyleProvider>
              </Grid>
            </Grid>
          </Grid>
          <Typography sx={{ my: theme.spacing(2) }} typography="h3">
            Beispile des PDF's
          </Typography>
          <Grid container spacing={2}>
            <Grid xs={4} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Rechnung</Typography>
              </Grid>
              <Grid item>
                <PDFViewer
                  showToolbar={true}
                  style={{ width: "100%", height: "50vh" }}
                >
                  <LocalizationProvider
                    dateAdapter={AdapterDayjs}
                    dateFormats={{ fullDate: "DD.MM.YYYY" }}
                  >
                    <PdfStyleProvider>
                      <InvoiceDocument
                        dateFormat={localization.utils.formats.fullDate}
                        emailConfiguration={formik.values}
                        customer={testCustomer}
                      />
                    </PdfStyleProvider>
                  </LocalizationProvider>
                </PDFViewer>
              </Grid>
            </Grid>
            <Grid xs={4} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Erinnerung</Typography>
              </Grid>
              <Grid item>
                <PDFViewer
                  showToolbar={true}
                  style={{ width: "100%", height: "50vh" }}
                >
                  <LocalizationProvider
                    dateAdapter={AdapterDayjs}
                    dateFormats={{ fullDate: "DD.MM.YYYY" }}
                  >
                    <PdfStyleProvider>
                      <RemindingDocument
                        dateFormat={localization.utils.formats.fullDate}
                        emailConfiguration={formik.values}
                        customer={testCustomer}
                      />
                    </PdfStyleProvider>
                  </LocalizationProvider>
                </PDFViewer>
              </Grid>
            </Grid>
            <Grid xs={4} item container direction="column" spacing={2}>
              <Grid item>
                <Typography typography="h4">Mahnung</Typography>
              </Grid>
              <Grid item>
                <PDFViewer
                  showToolbar={true}
                  style={{ width: "100%", height: "50vh" }}
                >
                  <LocalizationProvider
                    dateAdapter={AdapterDayjs}
                    dateFormats={{ fullDate: "DD.MM.YYYY" }}
                  >
                    <PdfStyleProvider>
                      <RefundRemindingDocument
                        dateFormat={localization.utils.formats.fullDate}
                        emailConfiguration={formik.values}
                        returnedDate={new Date()}
                        commission={4.2145}
                        waitingWeeks={4}
                        customer={testCustomer}
                      />
                    </PdfStyleProvider>
                  </LocalizationProvider>
                </PDFViewer>
              </Grid>
            </Grid>
          </Grid>
        </Stack>
      </Container>
    </>
  );
}
