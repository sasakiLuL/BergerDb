import { CssBaseline, ThemeProvider, createTheme } from "@mui/material";
import { LocalizationProvider, deDE } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { deDE as dataGridDeDE } from "@mui/x-data-grid";
import { deDE as coreDeDE } from "@mui/material/locale";
import "dayjs/locale/de";
import { RequireAuth, AuthProvider } from "react-auth-kit";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "./views/Login/Login";
import Home from "./views/Home/Home";
import Configuration from "./views/EmailConfiguration/EmailsConfiguration";
import { ApiUrlProvider } from "./features/shared/hooks/ApiUrlProvider";
import { EmailConfigurationProvider } from "./features/Users/hooks/EmailConfigurationProvider";
import UserProvider from "./features/Users/hooks/UserProvider";
import ErrorSnackbarProvider from "./features/shared/hooks/ErrorSnackbarProvider";

const theme = createTheme(
  {
    palette: {
      mode: "light",
    },
  },
  deDE,
  dataGridDeDE,
  coreDeDE
);

const App = () => {
  return (
    <AuthProvider
      authType={"cookie"}
      authName={"_auth"}
      cookieDomain={window.location.hostname}
      cookieSecure
    >
      <ApiUrlProvider url="https://localhost:7152/api">
        <ThemeProvider theme={theme}>
          <CssBaseline />
          <LocalizationProvider
            dateAdapter={AdapterDayjs}
            dateFormats={{ fullDate: "DD.MM.YYYY", fullTime: "hh:mm:ss" }}
            adapterLocale="de"
          >
            <ErrorSnackbarProvider>
              <BrowserRouter>
                <Routes>
                  <Route path="/login" element={<Login homePath="/home" />} />
                  <Route
                    path="/configuration"
                    element={
                      <RequireAuth loginPath="/login">
                        <UserProvider>
                          <EmailConfigurationProvider>
                            <Configuration
                              loginPath="/login"
                              homePath="/home"
                            />
                          </EmailConfigurationProvider>
                        </UserProvider>
                      </RequireAuth>
                    }
                  />
                  <Route
                    path="/home"
                    element={
                      <RequireAuth loginPath="/login">
                        <UserProvider>
                          <EmailConfigurationProvider>
                            <Home
                              loginPath="/login"
                              configurationPath="/configuration"
                            />
                          </EmailConfigurationProvider>
                        </UserProvider>
                      </RequireAuth>
                    }
                  />
                  <Route
                    path="*"
                    element={
                      <RequireAuth loginPath="/login">
                        <UserProvider>
                          <EmailConfigurationProvider>
                            <Home
                              loginPath="/login"
                              configurationPath="/configuration"
                            />
                          </EmailConfigurationProvider>
                        </UserProvider>
                      </RequireAuth>
                    }
                  />
                </Routes>
              </BrowserRouter>
            </ErrorSnackbarProvider>
          </LocalizationProvider>
        </ThemeProvider>
      </ApiUrlProvider>
    </AuthProvider>
  );
};

export default App;
