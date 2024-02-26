import {
  Button,
  Collapse,
  FormControl,
  FormHelperText,
  Grid,
  IconButton,
  Input,
  InputAdornment,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import SaveIcon from "@mui/icons-material/Save";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import * as yup from "yup";
import { useFormik } from "formik";
import dayjs from "dayjs";
import { useState } from "react";
import {
  EntryType,
  MemberType,
  PaymentType,
} from "../../../utils/Types/Customer";
import CustomerValidationSchema from "../../../utils/ValidationSchemas/CustomerValidationSchema";

export interface UpdateMembershipProps {
  memberType: MemberType;
  institution: string;
  entryType: EntryType;
  paymentType: PaymentType;
  amount: number;
  currentInvoiceSendedOn: Date | null;
  lastInvoiceSendedOn: Date | null;
  currentCreditReceivedOn: Date | null;
  lastCreditReceivedOn: Date | null;
  terminatedOn: Date | null;
  dunningSendedOn: Date | null;
}

interface UpdateMembershipFormProps {
  defaultValues: UpdateMembershipProps;
  onSubmit: (value: UpdateMembershipProps) => void;
}

export default function UpdateMembershipForm({
  defaultValues,
  onSubmit,
}: UpdateMembershipFormProps) {
  const [initValues, setInitValues] =
    useState<UpdateMembershipProps>(defaultValues);

  const formik = useFormik({
    initialValues: initValues,
    validationSchema: yup.object({
      memberType: CustomerValidationSchema.memberType,
      institution: CustomerValidationSchema.institution,
      entryType: CustomerValidationSchema.entryType,
      paymentType: CustomerValidationSchema.paymentType,
      amount: CustomerValidationSchema.amount,
      currentInvoiceSendedOn: CustomerValidationSchema.currentInvoiceSendedOn,
      lastInvoiceSendedOn: CustomerValidationSchema.lastInvoiceSendedOn,
      currentCreditReceivedOn: CustomerValidationSchema.currentCreditReceivedOn,
      lastCreditReceivedOn: CustomerValidationSchema.lastCreditReceivedOn,
      terminatedOn: CustomerValidationSchema.terminatedOn,
      dunningSendedOn: CustomerValidationSchema.dunningSendedOn,
    }),
    onSubmit: (values) => {
      setInitValues(values);
      onSubmit(values);
    },
    enableReinitialize: true,
  });

  return (
    <Grid
      container
      spacing={2}
      component="form"
      onSubmit={formik.handleSubmit}
      onReset={formik.handleReset}
    >
      <Grid item xs={12}>
        <Stack direction="row" alignItems="baseline">
          <Typography variant="h3">Mitgliedschaft</Typography>
          <Tooltip title="Zurücksetzen" sx={{ ml: 1 }}>
            <IconButton type="reset">
              <RestartAltIcon />
            </IconButton>
          </Tooltip>
        </Stack>
      </Grid>
      <Grid item xs={4}>
        <FormControl fullWidth>
          <InputLabel id="membership-select-label">Mitglied</InputLabel>
          <Select
            labelId="membership-select-label"
            id="membership-select"
            label="Mitglied"
            {...formik.getFieldProps("memberType")}
            error={
              formik.touched.memberType && Boolean(formik.errors.memberType)
            }
          >
            <MenuItem value={0}>Apo</MenuItem>
            <MenuItem value={1}>Laie</MenuItem>
            <MenuItem value={2}>Arzt</MenuItem>
            <MenuItem value={3}>Heilpraktiker</MenuItem>
          </Select>
          {formik.touched.memberType && Boolean(formik.errors.memberType) && (
            <FormHelperText>{formik.errors.memberType}</FormHelperText>
          )}
        </FormControl>
      </Grid>
      <Grid item xs={4}>
        <TextField
          id="institution-field"
          label="Einrichtung"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("institution")}
          error={
            formik.touched.institution && Boolean(formik.errors.institution)
          }
          helperText={formik.touched.institution && formik.errors.institution}
        />
      </Grid>
      <Grid item xs={4}>
        <DatePicker
          autoFocus={false}
          label="Kuendigen am"
          onChange={(value) =>
            formik.setFieldValue("terminatedOn", value?.toDate() ?? null, true)
          }
          value={
            formik.values.terminatedOn !== null
              ? dayjs(formik.values.terminatedOn)
              : null
          }
          sx={{ width: "100%" }}
          slotProps={{
            field: { clearable: true },
            textField: {
              variant: "outlined",
              error:
                formik.touched.terminatedOn &&
                Boolean(formik.errors.terminatedOn),
              helperText:
                formik.touched.terminatedOn && formik.errors.terminatedOn,
            },
          }}
        />
      </Grid>
      <Grid item xs={12} container spacing={2}>
        <Grid item xs={2}>
          <FormControl fullWidth>
            <InputLabel id="entry-type-select-label">Eintrag</InputLabel>
            <Select
              labelId="entry-type-select-label"
              id="entry-type-select"
              label="Eintrag"
              {...formik.getFieldProps("entryType")}
              error={
                formik.touched.entryType && Boolean(formik.errors.entryType)
              }
            >
              <MenuItem value={0}>GE</MenuItem>
              <MenuItem value={1}>EE</MenuItem>
            </Select>
            {formik.touched.entryType && Boolean(formik.errors.entryType) && (
              <FormHelperText>{formik.errors.entryType}</FormHelperText>
            )}
          </FormControl>
        </Grid>
        <Grid item xs={3}>
          <FormControl fullWidth>
            <InputLabel id="payment-method-select-label">
              Zahlungsart
            </InputLabel>
            <Select
              labelId="payment-method-select-label"
              id="payment-method-select"
              label="Zahlungsart"
              {...formik.getFieldProps("paymentType")}
              error={
                formik.touched.paymentType && Boolean(formik.errors.paymentType)
              }
            >
              <MenuItem value={0}>Rechnung</MenuItem>
              <MenuItem value={1}>Einzug</MenuItem>
            </Select>
            {formik.touched.paymentType &&
              Boolean(formik.errors.paymentType) && (
                <FormHelperText>{formik.errors.paymentType}</FormHelperText>
              )}
          </FormControl>
        </Grid>
        <Grid item xs={3}>
          <FormControl fullWidth variant="standard">
            <InputLabel htmlFor="standard-adornment-amount">Betrag</InputLabel>
            <Input
              id="standard-adornment-amount"
              startAdornment={
                <InputAdornment position="start">$</InputAdornment>
              }
              {...formik.getFieldProps("amount")}
              error={formik.touched.amount && Boolean(formik.errors.amount)}
              inputProps={{
                inputMode: "numeric",
              }}
            />
            {formik.touched.amount && Boolean(formik.errors.amount) && (
              <FormHelperText>{formik.errors.amount}</FormHelperText>
            )}
          </FormControl>
        </Grid>
        <Grid item xs={4}>
          <DatePicker
            autoFocus={false}
            label="Mahnung datum"
            onChange={(value) =>
              formik.setFieldValue(
                "dunningSendedOn",
                value?.toDate() ?? null,
                true
              )
            }
            value={
              formik.values.dunningSendedOn !== null
                ? dayjs(formik.values.dunningSendedOn)
                : null
            }
            sx={{ width: "100%" }}
            slotProps={{
              field: { clearable: true },
              textField: {
                variant: "outlined",
                error:
                  formik.touched.dunningSendedOn &&
                  Boolean(formik.errors.dunningSendedOn),
                helperText:
                  formik.touched.dunningSendedOn &&
                  formik.errors.dunningSendedOn,
              },
            }}
          />
        </Grid>
      </Grid>
      <Grid item xs={6} container spacing={2}>
        <Grid item xs={12}>
          <Typography variant="h6">Rechnung</Typography>
        </Grid>
        <Grid item xs={6}>
          <DatePicker
            label="Aktuelle Rechnung"
            minDate={
              formik.values.lastInvoiceSendedOn !== null
                ? dayjs(formik.values.lastInvoiceSendedOn)
                : null
            }
            onChange={(value) =>
              formik.setFieldValue(
                "currentInvoiceSendedOn",
                value?.toDate() ?? null,
                true
              )
            }
            value={
              formik.values.currentInvoiceSendedOn !== null
                ? dayjs(formik.values.currentInvoiceSendedOn)
                : null
            }
            sx={{ width: "100%" }}
            slotProps={{
              field: { clearable: true },
              textField: {
                variant: "outlined",
                error:
                  formik.touched.currentInvoiceSendedOn &&
                  Boolean(formik.errors.currentInvoiceSendedOn),
                helperText:
                  formik.touched.currentInvoiceSendedOn &&
                  formik.errors.currentInvoiceSendedOn,
              },
            }}
          />
        </Grid>
        <Grid item xs={6}>
          <DatePicker
            label="Letzte Rechnung"
            maxDate={
              formik.values.currentInvoiceSendedOn !== null
                ? dayjs(formik.values.currentInvoiceSendedOn)
                : null
            }
            onChange={(value) =>
              formik.setFieldValue(
                "lastInvoiceSendedOn",
                value?.toDate() ?? null,
                true
              )
            }
            value={
              formik.values.lastInvoiceSendedOn !== null
                ? dayjs(formik.values.lastInvoiceSendedOn)
                : null
            }
            sx={{ width: "100%" }}
            slotProps={{
              field: { clearable: true },
              textField: {
                variant: "outlined",
                error:
                  formik.touched.lastInvoiceSendedOn &&
                  Boolean(formik.errors.lastInvoiceSendedOn),
                helperText:
                  formik.touched.lastInvoiceSendedOn &&
                  formik.errors.lastInvoiceSendedOn,
              },
            }}
          />
        </Grid>
      </Grid>
      <Grid item xs={6} container spacing={2}>
        <Grid item xs={12}>
          <Typography variant="h6">Gutschrift</Typography>
        </Grid>
        <Grid item xs={6}>
          <DatePicker
            label="Aktuelle Gutschrift"
            minDate={
              formik.values.lastCreditReceivedOn !== undefined
                ? dayjs(formik.values.lastCreditReceivedOn)
                : undefined
            }
            onChange={(value) =>
              formik.setFieldValue(
                "currentCreditReceivedOn",
                value?.toDate() ?? null,
                true
              )
            }
            value={
              formik.values.currentCreditReceivedOn !== null
                ? dayjs(formik.values.currentCreditReceivedOn)
                : null
            }
            sx={{ width: "100%" }}
            slotProps={{
              field: { clearable: true },
              textField: {
                variant: "outlined",
                error:
                  formik.touched.currentCreditReceivedOn &&
                  Boolean(formik.errors.currentCreditReceivedOn),
                helperText:
                  formik.touched.currentCreditReceivedOn &&
                  formik.errors.currentCreditReceivedOn,
              },
            }}
          />
        </Grid>
        <Grid item xs={6}>
          <DatePicker
            label="Letzte Gutschrift"
            maxDate={
              formik.values.currentCreditReceivedOn !== null
                ? dayjs(formik.values.currentCreditReceivedOn)
                : null
            }
            onChange={(value) =>
              formik.setFieldValue(
                "lastCreditReceivedOn",
                value?.toDate() ?? null,
                true
              )
            }
            value={
              formik.values.lastCreditReceivedOn !== null
                ? dayjs(formik.values.lastCreditReceivedOn)
                : null
            }
            sx={{ width: "100%" }}
            slotProps={{
              field: { clearable: true },
              textField: {
                variant: "outlined",
                error:
                  formik.touched.lastCreditReceivedOn &&
                  Boolean(formik.errors.lastCreditReceivedOn),
                helperText:
                  formik.touched.lastCreditReceivedOn &&
                  formik.errors.lastCreditReceivedOn,
              },
            }}
          />
        </Grid>
      </Grid>
      <Grid item xs={4}>
        <Collapse in={formik.dirty}>
          <Button variant="contained" type="submit" startIcon={<SaveIcon />}>
            Änderungen speichern
          </Button>
        </Collapse>
      </Grid>
    </Grid>
  );
}
