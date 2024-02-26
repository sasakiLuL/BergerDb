import { Container, Stack, useTheme } from "@mui/material";
import MainNavBar from "../MainNavBar/MainNavBar";
import { useUser } from "../../features/Users/hooks/UserProvider";
import LoadingBackdrop from "../../features/shared/LoadingBackdrop/LoadingBackdrop";
import { useEffect, useState } from "react";
import BillingCustomersProvider from "../../features/Customers/hooks/BillingCustomers/BillingCustomersProvider";
import SelectedBillingCustomersProvider from "../../features/Customers/hooks/BillingCustomers/SelectedBillingCustomersProvider";
import BillingCustomersTable from "../../features/Customers/BillingCustomersTable/BillingCustomersTable";
import { LocalizationProvider } from "@mui/x-date-pickers";
import DirectDebitingCustomersProvider from "../../features/Customers/hooks/DirectDebitingCustomers/DirectDebitingCustomersProvider";
import SelectedDirectDebitingCustomersProvider from "../../features/Customers/hooks/DirectDebitingCustomers/SelectedDirectDebitingCustomersProvider";
import DirectDebitingCustomersTable from "../../features/Customers/DirectDebitingCustomersTable/DirectDebitingCustomersTable";

export interface HomeProps {
  loginPath: string;
  configurationPath: string;
}

export default function Home({ loginPath, configurationPath }: HomeProps) {
  const theme = useTheme();
  const { user } = useUser();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    setIsLoading(true);
    setIsLoading(!user || false);
  }, [!user]);

  return (
    <LocalizationProvider>
      <LoadingBackdrop open={isLoading} />
      <MainNavBar
        loginPath={loginPath}
        emailConfigurationPath={configurationPath}
      />

      <Container maxWidth="xl" sx={{ p: theme.spacing(2) }}>
        <Stack spacing={2}>
          <DirectDebitingCustomersProvider>
            <SelectedDirectDebitingCustomersProvider>
              <DirectDebitingCustomersTable />
            </SelectedDirectDebitingCustomersProvider>
          </DirectDebitingCustomersProvider>
          <BillingCustomersProvider>
            <SelectedBillingCustomersProvider>
              <BillingCustomersTable />
            </SelectedBillingCustomersProvider>
          </BillingCustomersProvider>
        </Stack>
      </Container>
    </LocalizationProvider>
  );
}
