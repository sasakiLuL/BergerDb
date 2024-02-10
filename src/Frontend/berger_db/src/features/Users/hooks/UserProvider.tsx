import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";
import User from "../../../utils/Types/User";
import Link from "../../../utils/Types/Link";
import { useAuthHeader, useAuthUser } from "react-auth-kit";
import axios from "axios";
import { useErrorSnackbar } from "../../shared/hooks/ErrorSnackbarProvider";

export interface UserProviderProps {
  children: ReactNode;
}

export type UserSetter = (value: User) => void;

const UserContext = createContext<{
  user: User | null;
  setUser: UserSetter;
  links: Link[];
}>({ user: null, setUser: () => {}, links: [] });

export const useUser = () => useContext(UserContext);

export default function UserProvider({ children }: UserProviderProps) {
  const authUser = useAuthUser();
  const authHeader = useAuthHeader();
  const [openErrorSnackbar] = useErrorSnackbar();
  const [user, setUser] = useState<User | null>(null);
  const [links, setLinks] = useState<Link[]>([]);

  const getUserAndLinks = () => {
    axios
      .get((authUser() as any).userGetUrl, {
        headers: { Authorization: authHeader() },
      })
      .then((response) => {
        setUser(response.data);
        setLinks(response.data.links);
      })
      .catch((error) => {
        openErrorSnackbar(error);
      });
  };

  useEffect(() => {
    getUserAndLinks();
  }, []);

  return (
    <UserContext.Provider
      value={{ user: user, setUser: setUser, links: links }}
    >
      {children}
    </UserContext.Provider>
  );
}
