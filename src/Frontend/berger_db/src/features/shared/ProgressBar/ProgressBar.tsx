import { Box } from "@mui/material";
import LinearProgress, {
  linearProgressClasses,
} from "@mui/material/LinearProgress";
import { styled } from "@mui/material/styles";

export interface ProgressBarProps {
  open: boolean;
}

export default function ProgressBar({ open }: ProgressBarProps) {
  const BorderLinearProgress = styled(LinearProgress)(({ theme }) => ({
    borderRadius: 5,
    [`&.${linearProgressClasses.colorPrimary}`]: {
      backgroundColor:
        theme.palette.grey[theme.palette.mode === "light" ? 200 : 800],
    },
    [`& .${linearProgressClasses.bar}`]: {
      borderRadius: 5,
      backgroundColor: theme.palette.mode === "light" ? "#1a90ff" : "#308fe8",
    },
  }));
  return (
    <Box sx={{ flexGrow: 1, height: "3px" }}>
      {open && <BorderLinearProgress variant="indeterminate" />}
    </Box>
  );
}
