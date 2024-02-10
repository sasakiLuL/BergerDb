import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";
import EmailConfiguraion from "../../../utils/Types/EmailConfiguration";
import Link from "../../../utils/Types/Link";
import { useAuthHeader } from "react-auth-kit";
import { useUser } from "./UserProvider";
import axios from "axios";
import { EditorState, convertFromRaw, convertToRaw } from "draft-js";
import { useErrorSnackbar } from "../../shared/hooks/ErrorSnackbarProvider";

export interface EmailConfigurationProviderProps {
  children: ReactNode;
}

export type EmailConfigurationSetter = (value: EmailConfiguraion) => void;

const EmailConfigurationContext = createContext<{
  emailConfiguration: EmailConfiguraion | null;
  setEmailConfiguration: EmailConfigurationSetter;
  links: Link[];
}>({
  emailConfiguration: null,
  setEmailConfiguration: () => {},
  links: [],
});

export const useEmailConfiguration = () =>
  useContext(EmailConfigurationContext);

export function EmailConfigurationProvider({
  children,
}: EmailConfigurationProviderProps) {
  const authHeader = useAuthHeader();
  const [openErrorSnackbar] = useErrorSnackbar();
  const { user, links: userLinks } = useUser();
  const [emailConfiguration, setEmailConfiguration] =
    useState<EmailConfiguraion | null>(null);
  const [links, setLinks] = useState<Link[]>([]);

  useEffect(() => {
    getEmailConfigurationAndLinks();
  }, [user]);

  const getEmailConfigurationAndLinks = () => {
    if (user) {
      axios
        .get(
          userLinks.find((link) => link.rel === "get-user-email-configuration")!
            .href,
          {
            headers: { Authorization: authHeader() },
          }
        )
        .then((response) => {
          const convertedEmailConfiguration: EmailConfiguraion = {
            ...response.data,
            invoicePdfBody: EditorState.createWithContent(
              convertFromRaw(JSON.parse(response.data.invoicePdfBody))
            ),
          };
          setEmailConfiguration(convertedEmailConfiguration);
          setLinks(response.data.links);
        })
        .catch((error) => {
          openErrorSnackbar(error);
        });
    }
  };

  const putEmailConfigurationAndLinks = (value: EmailConfiguraion) => {
    if (user) {
      axios
        .put(
          userLinks.find(
            (link) => link.rel === "update-user-email-configuration"
          )!.href,
          {
            ...value,
            invoicePdfBody: JSON.stringify(
              convertToRaw(value.invoicePdfBody.getCurrentContent())
            ),
          },
          {
            headers: { Authorization: authHeader() },
          }
        )
        .then(() => {
          setEmailConfiguration(value);
        })
        .catch((error) => {
          openErrorSnackbar(error);
        });
    }
  };

  return (
    <EmailConfigurationContext.Provider
      value={{
        emailConfiguration: emailConfiguration,
        setEmailConfiguration: putEmailConfigurationAndLinks,
        links: links,
      }}
    >
      {children}
    </EmailConfigurationContext.Provider>
  );
}
