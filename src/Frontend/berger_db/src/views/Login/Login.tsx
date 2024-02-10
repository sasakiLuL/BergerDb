import axios, { AxiosError } from "axios";
import { useEffect, useState } from "react";
import { useSignIn } from "react-auth-kit";
import * as yup from "yup";
import {
  Button,
  FormControl,
  FormHelperText,
  Grid,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  Stack,
  Typography,
} from "@mui/material";
import { Person, Visibility, VisibilityOff } from "@mui/icons-material";
import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";
import { useIsAuthenticated } from "react-auth-kit";
import LoginValidationSchema from "../../utils/ValidationSchemas/LoginValidationSchema";
import { useApiUrl } from "../../features/shared/hooks/ApiUrlProvider";
import Logo from "../../assets/Logo_lila.png";
import LoadingBackdrop from "../../features/shared/LoadingBackdrop/LoadingBackdrop";

export interface LoginProps {
  homePath: string;
}

export default function Login({ homePath }: LoginProps) {
  const handleLoginingSubmit = (value: { email: string; password: string }) => {
    setIsLoading(true);
    axios
      .post(apiUrl + "/authentication/login", value)
      .then((response) => {
        signIn({
          token: response.data.token,
          tokenType: "Bearer",
          expiresIn: 360,
          authState: {
            userId: response.data.id,
            userGetUrl: response.data.links.find(
              (link: any) => link.rel === "get-user"
            ).href,
          },
        });
        navigate(homePath);
      })
      .catch((error) => {
        if (error instanceof AxiosError) {
          setErrors([
            error.response?.data.errors.map((err: any) => err.message),
          ]);
        }
      })
      .finally(() => {
        setIsLoading(false);
      });
  };
  const handleClickShowPassword = () => setShowPassword((show) => !show);
  const handleMouseDownPassword = (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    event.preventDefault();
  };

  const [errors, setErrors] = useState<string[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const navigate = useNavigate();
  const isAuthenticated = useIsAuthenticated();
  const signIn = useSignIn();
  const apiUrl = useApiUrl();
  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
    },
    validationSchema: yup.object({
      email: LoginValidationSchema.email,
      password: LoginValidationSchema.password,
    }),
    onSubmit: handleLoginingSubmit,
  });
  const [showPassword, setShowPassword] = useState<boolean>();
  useEffect(() => {
    if (isAuthenticated()) {
      navigate(homePath);
    }
  }, [isAuthenticated]);

  return (
    <>
      <LoadingBackdrop open={isLoading} />
      <Stack alignItems="center" spacing={10}>
        <Stack alignItems="center">
          <Typography variant="h1">Berger DB</Typography>
          <img src={Logo} loading="lazy" />
        </Stack>
        <Grid
          item
          xs={2}
          component="form"
          container
          direction="column"
          justifyContent="center"
          spacing={2}
          onSubmit={formik.handleSubmit}
        >
          <Grid item xs={12}>
            <h2>Login</h2>
          </Grid>
          <Grid item xs={6}>
            <FormControl
              error={
                formik.touched.email &&
                (Boolean(formik.errors.email) || errors.length !== 0)
              }
              variant="outlined"
              fullWidth
            >
              <InputLabel htmlFor="outlined-adornment-email">Email</InputLabel>
              <OutlinedInput
                itemID="email"
                id="outlined-adornment-email"
                type="text"
                {...formik.getFieldProps("email")}
                endAdornment={
                  <InputAdornment position="start">
                    <IconButton>
                      <Person />
                    </IconButton>
                  </InputAdornment>
                }
                label="Email"
              />
              <FormHelperText id="component-error-text">
                {formik.touched.email && formik.errors.email}
              </FormHelperText>
            </FormControl>
          </Grid>
          <Grid item xs={6}>
            <FormControl
              error={
                formik.touched.password &&
                (Boolean(formik.errors.password) || errors.length !== 0)
              }
              variant="outlined"
              fullWidth
            >
              <InputLabel htmlFor="outlined-adornment-password">
                Password
              </InputLabel>
              <OutlinedInput
                fullWidth
                itemID="password"
                id="outlined-adornment-password"
                type={showPassword ? "text" : "password"}
                {...formik.getFieldProps("password")}
                endAdornment={
                  <InputAdornment position="end">
                    <IconButton
                      aria-label="toggle password visibility"
                      onClick={handleClickShowPassword}
                      onMouseDown={handleMouseDownPassword}
                      edge="end"
                    >
                      {showPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                }
                label="Password"
              />
              <FormHelperText id="component-error-text">
                {formik.touched.password && formik.errors.password}
              </FormHelperText>
            </FormControl>
          </Grid>
          <Grid item xs={12}>
            <Button type="submit" fullWidth variant="outlined">
              Login
            </Button>
          </Grid>
          {errors.length !== 0 &&
            errors.map((error) => (
              <Grid item xs={12}>
                <FormHelperText error>{error}</FormHelperText>
              </Grid>
            ))}
        </Grid>
      </Stack>
    </>
  );
}
