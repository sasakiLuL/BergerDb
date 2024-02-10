import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  Typography,
  useTheme,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import SendIcon from "@mui/icons-material/Send";
import CustomerResponse from "../../../utils/Types/CustomerResponse";

interface SendEmailDialogProps {
  open: boolean;
  customers: CustomerResponse[];
  onSubmit: () => void;
  onClose: () => void;
  onDownloadButtonClick: () => void;
}

export default function SendEmailDialog({
  open,
  customers,
  onSubmit,
  onClose,
  onDownloadButtonClick,
}: SendEmailDialogProps) {
  const theme = useTheme();

  return (
    <Dialog
      maxWidth="xl"
      onClose={onClose}
      aria-labelledby="create-customer-dialog-title"
      open={open}
    >
      <DialogTitle sx={{ m: 0, p: 2 }} id="create-customer-dialog-title">
        <Typography component="div">E-Mails senden</Typography>
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
      <DialogContent
        dividers
        style={{ display: "flex", flexDirection: "column" }}
      >
        <Box sx={{ my: theme.spacing(2) }}>
          Die Anzahl der E-Mails:{" "}
          <span style={{ fontWeight: "bold" }}>{customers.length}</span>
        </Box>
        <Button variant="contained" onClick={() => onDownloadButtonClick()}>
          Alle E-Mails herunterladen
        </Button>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Schließen</Button>
        <Button
          variant="contained"
          type="submit"
          onClick={() => onSubmit()}
          endIcon={<SendIcon />}
        >
          Senden
        </Button>
      </DialogActions>
    </Dialog>
  );
}
