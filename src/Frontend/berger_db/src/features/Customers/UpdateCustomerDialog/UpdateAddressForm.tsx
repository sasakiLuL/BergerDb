import {
  Button,
  Collapse,
  Grid,
  IconButton,
  Stack,
  TextField,
  Tooltip,
  Typography,
} from "@mui/material";
import { useFormik } from "formik";
import * as yup from "yup";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import SaveIcon from "@mui/icons-material/Save";
import { useState } from "react";
import CustomerValidationSchema from "../../../utils/ValidationSchemas/CustomerValidationSchema";

export interface UpdateAddressProps {
  street: string;
  zipCode: string;
  city: string;
}

interface UpdateAddressFormProps {
  defaultValues: UpdateAddressProps;
  onSubmit: (value: UpdateAddressProps) => void;
}

export default function UpdateAddressForm({
  defaultValues,
  onSubmit,
}: UpdateAddressFormProps) {
  const [initValues, setInitValues] =
    useState<UpdateAddressProps>(defaultValues);

  const formik = useFormik({
    initialValues: initValues,
    validationSchema: yup.object({
      street: CustomerValidationSchema.street,
      city: CustomerValidationSchema.city,
      zipCode: CustomerValidationSchema.zipCode,
    }),
    onSubmit: (values) => {
      setInitValues(values);
      onSubmit(values);
    },
    enableReinitialize: true,
  });

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Stack direction="row" alignItems="baseline">
          <Typography variant="h3">Adresse</Typography>
          <Tooltip title="Zurücksetzen" sx={{ ml: 1 }}>
            <IconButton type="reset" onClick={formik.handleReset}>
              <RestartAltIcon />
            </IconButton>
          </Tooltip>
        </Stack>
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
      <Grid item xs={4} component="form" onSubmit={formik.handleSubmit}>
        <Collapse in={formik.dirty}>
          <Button type="submit" variant="contained" startIcon={<SaveIcon />}>
            Änderungen speichern
          </Button>
        </Collapse>
      </Grid>
    </Grid>
  );
}
