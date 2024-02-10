import {
  FormControl,
  FormHelperText,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Typography,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import dayjs from "dayjs";

interface AddPersonalInfoFormProps {
  formik: any;
}

export default function AddPersonalInfoForm({
  formik,
}: AddPersonalInfoFormProps) {
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Typography variant="h3">Persönliche Informationen</Typography>
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
            field: { clearable: true },
            textField: {
              variant: "outlined",
              error:
                formik.touched.registrationDate &&
                Boolean(formik.errors.registrationDate),
              helperText:
                formik.touched.registrationDate &&
                formik.errors.registrationDate,
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
    </Grid>
  );
}
