import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

interface DeleteCustomersDialogProps {
  title: string;
  message: string;
  open: boolean;
  handleAcceptClose: () => void;
  handleCancelClose: () => void;
}

export default function DeleteCustomersDialog({
  title,
  message,
  open,
  handleAcceptClose,
  handleCancelClose,
}: DeleteCustomersDialogProps) {
  return (
    <Dialog
      onClose={handleCancelClose}
      aria-labelledby="customized-dialog-title"
      open={open}
    >
      <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
        {title}
      </DialogTitle>
      <IconButton
        aria-label="close"
        onClick={handleCancelClose}
        sx={{
          position: "absolute",
          right: 8,
          top: 8,
          color: (theme) => theme.palette.grey[500],
        }}
      >
        <CloseIcon />
      </IconButton>
      <DialogContent dividers>{message}</DialogContent>
      <DialogActions>
        <Button onClick={handleCancelClose}>Abbrechen</Button>
        <Button color="error" onClick={handleAcceptClose}>
          Löschen
        </Button>
      </DialogActions>
    </Dialog>
  );
}
