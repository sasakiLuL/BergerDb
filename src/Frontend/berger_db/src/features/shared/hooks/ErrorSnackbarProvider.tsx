import { ReactNode, createContext, useContext, useState } from "react";
import ErrorSnackbar from "../ErrorSnackbar/ErrorSnackbar";

export interface ErrorSnackbarProviderProps {
  children: ReactNode;
}

const ErrorSnackbarContext = createContext<
  [open: (error: any) => void, isOpen: boolean]
>([() => {}, false]);

export const useErrorSnackbar = () => useContext(ErrorSnackbarContext);

export default function ErrorSnackbarProvider({
  children,
}: ErrorSnackbarProviderProps) {
  const [open, setOpen] = useState<boolean>(false);
  const [error, setError] = useState<any>(null);

  const handleOpen = (error: any) => {
    setError(error);
    setOpen(true);
  };

  return (
    <ErrorSnackbarContext.Provider value={[handleOpen, open]}>
      <ErrorSnackbar error={error} open={open} />
      {children}
    </ErrorSnackbarContext.Provider>
  );
}
