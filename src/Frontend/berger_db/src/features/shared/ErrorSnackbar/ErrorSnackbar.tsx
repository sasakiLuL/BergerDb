import { Alert, Chip, Snackbar } from "@mui/material";
import { AxiosError } from "axios";
import { useEffect, useState } from "react";

export interface ErrorSnackbarProps {
  open: boolean;
  error: any;
}

export default function ErrorSnackbar({ open, error }: ErrorSnackbarProps) {
  const [code, setCode] = useState<string>("");
  const [message, setMessage] = useState<string>("");

  useEffect(() => {
    if (error) {
      console.log(error);
      switch (error) {
        case error as AxiosError:
          const errorResponse = (error as AxiosError).response?.data as any;
          setCode(
            (errorResponse.title as string) +
              " " +
              (errorResponse.status as string)
          );
          setMessage(errorResponse.errors[0].message as string);
          break;
        default:
          setCode("Error");
          setMessage("Something went wrong...");
          break;
      }
    }
  }, []);

  return (
    <Snackbar open={open} autoHideDuration={2000} message={message}>
      <Alert severity="error" variant="filled" sx={{ width: "100%" }}>
        <Chip label={code} />
        {message}
      </Alert>
    </Snackbar>
  );
}
