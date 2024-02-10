import {
  AppBar,
  Box,
  Button,
  Skeleton,
  Toolbar,
  Typography,
  useTheme,
} from "@mui/material";
import Logo from "../../assets/Logo_inverted.png";
import HomeIcon from "@mui/icons-material/Home";
import SettingsIcon from "@mui/icons-material/Settings";
import LogoutIcon from "@mui/icons-material/Logout";
import { useUser } from "../../features/Users/hooks/UserProvider";
import { useSignOut } from "react-auth-kit";
import { useNavigate } from "react-router-dom";

export interface MainNavBarProps {
  loginPath: string;
  homePath?: string;
  emailConfigurationPath?: string;
}

export default function MainNavBar({
  loginPath,
  homePath,
  emailConfigurationPath,
}: MainNavBarProps) {
  const { user } = useUser();
  const navigate = useNavigate();
  const signOut = useSignOut();
  const theme = useTheme();

  const handleSignOut = () => {
    signOut();
    navigate(loginPath);
  };

  const handleNavigateToHome = () => {
    homePath && navigate(homePath);
  };

  const handleNavigateToEmailConfiguration = () => {
    emailConfigurationPath && navigate(emailConfigurationPath);
  };

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="sticky">
        <Toolbar>
          <img
            style={{
              maxWidth: "200px",
              flexGrow: 1,
              marginRight: 20,
            }}
            src={Logo}
            alt="Logo.png"
          />
          <Button
            color="inherit"
            startIcon={<HomeIcon />}
            onClick={handleNavigateToHome}
          >
            Home
          </Button>
          <Button
            color="inherit"
            startIcon={<SettingsIcon />}
            onClick={handleNavigateToEmailConfiguration}
          >
            Configuration
          </Button>
          <Box sx={{ flexGrow: 1 }} />
          <Box sx={{ px: theme.spacing(2) }}>
            <Typography>
              Hello {user ? user.userName : <Skeleton variant="text" />}!
            </Typography>
          </Box>
          <Button
            color="inherit"
            startIcon={<LogoutIcon />}
            onClick={handleSignOut}
          >
            Logout
          </Button>
        </Toolbar>
      </AppBar>
    </Box>
  );
}
