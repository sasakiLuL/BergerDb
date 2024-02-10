import { Grid, TextField, Typography } from "@mui/material";

interface AddNotationFormProps {
  formik: any;
}

export default function AddNotationForm({ formik }: AddNotationFormProps) {
  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Typography variant="h3">Notation</Typography>
      </Grid>
      <Grid item xs={12}>
        <TextField
          id="notation-field"
          label="Notation"
          multiline
          rows={3}
          fullWidth
          variant="standard"
          {...formik.getFieldProps("notation")}
          error={formik.touched.notation && Boolean(formik.errors.notation)}
          helperText={formik.touched.notation && formik.errors.notation}
        />
      </Grid>
    </Grid>
  );
}
