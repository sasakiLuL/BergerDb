import { Button } from "@mui/material";
import { ReactElement } from "react";

export interface BlockButtonProps {
  onClick: () => void;
  icon: ReactElement | null;
  children: string;
  active?: boolean;
}

export default function BlockButton({
  onClick,
  icon,
  children,
  active,
}: BlockButtonProps) {
  return (
    <Button
      color={active ? "primary" : "inherit"}
      size="small"
      onClick={onClick}
      startIcon={icon}
    >
      {children}
    </Button>
  );
}
