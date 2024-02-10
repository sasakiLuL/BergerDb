import {
  Button,
  Collapse,
  FormControl,
  FormHelperText,
  Grid,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  Stack,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import SaveIcon from "@mui/icons-material/Save";
import { useFormik } from "formik";
import * as yup from "yup";
import dayjs from "dayjs";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import { useState } from "react";
import CustomerValidationSchema from "../../../utils/ValidationSchemas/CustomerValidationSchema";
import { Sex } from "../../../utils/Types/Customer";
import { DatePicker } from "@mui/x-date-pickers";

export interface UpdatePersonalInfoProps {
  prefix: string;
  firstName: string;
  lastName: string;
  personalId: number;
  email: string;
  sex: Sex;
  registrationDate: Date;
}

interface UpdatePersonalInfoFormProps {
  defaultValues: UpdatePersonalInfoProps;
  onSubmit: (value: UpdatePersonalInfoProps) => void;
  dateFormat: string;
}

export default function PersonalInfoForm({
  defaultValues,
  onSubmit,
}: UpdatePersonalInfoFormProps) {
  const [initValues, setInitValues] =
    useState<UpdatePersonalInfoProps>(defaultValues);

  const formik = useFormik({
    initialValues: initValues,
    validationSchema: yup.object({
      prefix: CustomerValidationSchema.prefix,
      firstName: CustomerValidationSchema.firstName,
      personalId: CustomerValidationSchema.personalId,
      lastName: CustomerValidationSchema.lastName,
      email: CustomerValidationSchema.email,
      sex: CustomerValidationSchema.sex,
      registrationDate: CustomerValidationSchema.registrationDate,
    }),
    onSubmit: (values) => {
      setInitValues(values);
      console.log(values);
      onSubmit(values);
    },
    enableReinitialize: true,
  });

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Stack direction="row" alignItems="baseline">
          <Typography variant="h3">Persönliche Informationen</Typography>
          <Tooltip title="Zurücksetzen" sx={{ ml: 1 }}>
            <IconButton type="reset" onClick={formik.handleReset}>
              <RestartAltIcon />
            </IconButton>
          </Tooltip>
        </Stack>
      </Grid>
      <Grid item xs={1}>
        <TextField
          id="title-field"
          label="Titel"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("prefix")}
          error={formik.touched.prefix && Boolean(formik.errors.prefix)}
          helperText={formik.touched.prefix && formik.errors.prefix}
        />
      </Grid>
      <Grid item xs>
        <TextField
          id="firstname-field"
          label="Vorname"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("firstName")}
          error={formik.touched.firstName && Boolean(formik.errors.firstName)}
          helperText={formik.touched.firstName && formik.errors.firstName}
        />
      </Grid>
      <Grid item xs>
        <TextField
          id="lastname-field"
          label="Nachname"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("lastName")}
          error={formik.touched.lastName && Boolean(formik.errors.lastName)}
          helperText={formik.touched.lastName && formik.errors.lastName}
        />
      </Grid>
      <Grid item xs={3}>
        <TextField
          id="personalId-field"
          label="Persönlicher ID"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("personalId")}
          error={formik.touched.personalId && Boolean(formik.errors.personalId)}
          helperText={formik.touched.personalId && formik.errors.personalId}
        />
      </Grid>
      <Grid item xs={2}>
        <DatePicker
          autoFocus={false}
          label="Registriert am"
          onChange={(value) =>
            formik.setFieldValue(
              "registrationDate",
              value?.toDate() ?? null,
              true
            )
          }
          value={
            formik.values.registrationDate !== null
              ? dayjs(formik.values.registrationDate)
              : null
          }
          sx={{ width: "100%" }}
          slotProps={{
            textField: {
              variant: "outlined",
              error:
                formik.touched.registrationDate &&
                Boolean(formik.errors.registrationDate),
            },
          }}
        />
      </Grid>
      <Grid item xs={10}>
        <TextField
          id="email-field"
          label="Email"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("email")}
          error={formik.touched.email && Boolean(formik.errors.email)}
          helperText={formik.touched.email && formik.errors.email}
        />
      </Grid>
      <Grid item xs={2}>
        <FormControl fullWidth>
          <InputLabel id="sex-select-label">Geschlecht</InputLabel>
          <Select
            labelId="sex-select-label"
            id="sex-select"
            label="Geschlecht"
            {...formik.getFieldProps("sex")}
            error={formik.touched.sex && Boolean(formik.errors.sex)}
          >
            <MenuItem value={0}>Herr</MenuItem>
            <MenuItem value={1}>Frau</MenuItem>
          </Select>
          {formik.touched.sex && Boolean(formik.errors.sex) && (
            <FormHelperText>{formik.errors.email}</FormHelperText>
          )}
        </FormControl>
      </Grid>
      <Grid item xs={4} component="form" onSubmit={formik.handleSubmit}>
        <Collapse in={formik.dirty}>
          <Button variant="contained" type="submit" startIcon={<SaveIcon />}>
            Änderungen speichern
          </Button>
        </Collapse>
      </Grid>
    </Grid>
  );
}
