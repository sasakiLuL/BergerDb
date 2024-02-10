import { IconButton } from "@mui/material";
import { ReactElement } from "react";

export interface MarkupButtonProps {
  onClick: () => void;
  icon: ReactElement;
  active?: boolean;
}

export default function MarkupButton({
  onClick,
  icon,
  active,
}: MarkupButtonProps) {
  return (
    <IconButton
      color={active ? "primary" : "default"}
      size="small"
      onClick={onClick}
    >
      {icon}
    </IconButton>
  );
}
