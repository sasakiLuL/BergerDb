import { ReactNode, createContext, useContext, useState } from "react";

const ApiUrlContext = createContext<string>("");

export const useApiUrl = () => useContext(ApiUrlContext);

export interface ApiUrlProviderProps {
  children: ReactNode;
  url: string;
}

export function ApiUrlProvider({ children, url }: ApiUrlProviderProps) {
  const [apiUrl] = useState<string>(url);

  return (
    <ApiUrlContext.Provider value={apiUrl}>{children}</ApiUrlContext.Provider>
  );
}
