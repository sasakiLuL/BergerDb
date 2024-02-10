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
import SaveIcon from "@mui/icons-material/Save";
import RestartAltIcon from "@mui/icons-material/RestartAlt";
import * as yup from "yup";
import { useState } from "react";
import CustomerValidationSchema from "../../../utils/ValidationSchemas/CustomerValidationSchema";

export interface UpdateNotationProps {
  notation: string;
}

interface UpdateNotationFormProps {
  defaultValues: UpdateNotationProps;
  onSubmit: (value: UpdateNotationProps) => void;
}

export default function UpdateNotationForm({
  defaultValues,
  onSubmit,
}: UpdateNotationFormProps) {
  const [initValues, setInitValues] =
    useState<UpdateNotationProps>(defaultValues);

  const formik = useFormik({
    initialValues: initValues,
    validationSchema: yup.object({
      notation: CustomerValidationSchema.notation,
    }),
    onSubmit: (values) => {
      onSubmit(values);
      setInitValues(values);
    },
    enableReinitialize: true,
  });

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        <Stack direction="row" alignItems="baseline">
          <Typography variant="h3">Notation</Typography>
          <Tooltip title="Zurücksetzen" sx={{ ml: 1 }}>
            <IconButton type="reset" onClick={formik.handleReset}>
              <RestartAltIcon />
            </IconButton>
          </Tooltip>
        </Stack>
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
