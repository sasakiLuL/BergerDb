import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";
import CustomerResponse from "../../../../utils/Types/CustomerResponse";
import RequestPagination from "../../../../utils/Types/RequestPagination";
import axios from "axios";
import { useAuthHeader } from "react-auth-kit";
import { useApiUrl } from "../../../shared/hooks/ApiUrlProvider";
import { useErrorSnackbar } from "../../../shared/hooks/ErrorSnackbarProvider";
import { useDirectDebitingCustomers } from "./DirectDebitingCustomersProvider";
import { PaymentType } from "../../../../utils/Types/Customer";

export interface SelectedDirectDebitingProviderProps {
  children: ReactNode;
}

const SelectedCustomersContext = createContext<{
  selectedCustomers: CustomerResponse[];
  setSelectedCustomers: (value: CustomerResponse[]) => void;
  selectAll: () => void;
  clear: () => void;
  select: (value: CustomerResponse) => void;
}>({
  selectedCustomers: [],
  setSelectedCustomers: () => {},
  selectAll: () => {},
  clear: () => {},
  select: () => {},
});

export const useSelectedDirectDebitingCustomers = () =>
  useContext(SelectedCustomersContext);

export default function SelectedDirectDebitingCustomersProvider({
  children,
}: SelectedDirectDebitingProviderProps) {
  const [selectedCustomers, setSelectedCustomers] = useState<
    CustomerResponse[]
  >([]);
  const authHeader = useAuthHeader();
  const apiUrl = useApiUrl();
  const { pagination, sorting, filtering } = useDirectDebitingCustomers();
  const [openErrorSnackbar] = useErrorSnackbar();

  const getAll = () => {
    if (pagination.totalItemsCount <= 0) return;
    let filteringString = "";
    for (const filter of filtering) {
      filteringString += filter.toRequestString();
    }
    axios
      .get(
        `${apiUrl}/customers/?paymentType=${
          PaymentType.DirectDebiting
        }&${new RequestPagination(
          1,
          pagination.totalItemsCount
        ).toRequestString()}&${
          sorting ? sorting.toRequestString() : ""
        }${filteringString}`,
        {
          headers: { Authorization: authHeader() },
        }
      )
      .then((response) => {
        setSelectedCustomers(
          response.data.items.map((customer: any) => {
            return { customer: customer, links: customer.links };
          })
        );
      })
      .catch((error) => {
        openErrorSnackbar(error);
      });
  };

  const clear = () => {
    setSelectedCustomers([]);
  };

  useEffect(() => {}, [
    sorting?.column,
    sorting?.sorting,
    pagination.page,
    pagination.pageSize,
    filtering,
  ]);

  return (
    <SelectedCustomersContext.Provider
      value={{
        selectedCustomers: selectedCustomers,
        setSelectedCustomers: setSelectedCustomers,
        selectAll: getAll,
        clear: clear,
        select: () => {},
      }}
    >
      {children}
    </SelectedCustomersContext.Provider>
  );
}
