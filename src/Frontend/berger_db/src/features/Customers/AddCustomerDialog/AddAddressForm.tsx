import { Grid, TextField, Typography } from "@mui/material";

interface AddAddressFormProps {
  formik: any;
}

export default function AddAddressForm({ formik }: AddAddressFormProps) {
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Typography variant="h3">Adresse</Typography>
      </Grid>
      <Grid item xs={6}>
        <TextField
          id="street-field"
          label="Straße"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("street")}
          error={formik.touched.street && Boolean(formik.errors.street)}
          helperText={formik.touched.street && formik.errors.street}
        />
      </Grid>
      <Grid item xs={4}>
        <TextField
          id="city-field"
          label="Stadt"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("city")}
          error={formik.touched.city && Boolean(formik.errors.city)}
          helperText={formik.touched.city && formik.errors.city}
        />
      </Grid>
      <Grid item xs={2}>
        <TextField
          id="postal-code-field"
          label="PLZ"
          fullWidth
          variant="standard"
          {...formik.getFieldProps("zipCode")}
          error={formik.touched.zipCode && Boolean(formik.errors.zipCode)}
          helperText={formik.touched.zipCode && formik.errors.zipCode}
        />
      </Grid>
    </Grid>
  );
}
