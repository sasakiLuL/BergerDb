import {
  ReactNode,
  createContext,
  useContext,
  useEffect,
  useState,
} from "react";
import RequestPagination from "../../../../utils/Types/RequestPagination";
import RequestFiltering from "../../../../utils/Types/RequestFiltering";
import RequestSorting from "../../../../utils/Types/RequestSorting";
import CustomerResponse from "../../../../utils/Types/CustomerResponse";
import { useAuthHeader } from "react-auth-kit";
import { useApiUrl } from "../../../shared/hooks/ApiUrlProvider";
import axios from "axios";
import { useErrorSnackbar } from "../../../shared/hooks/ErrorSnackbarProvider";
import { PaymentType } from "../../../../utils/Types/Customer";
import CustomersRequestFilters from "../../../../utils/CustomersRequestFilters/CustomersRequestFilters";

export interface DirectDebitingCustomersProviderProps {
  children: ReactNode;
}

const CustomersContext = createContext<{
  pagination: RequestPagination;
  setPagination: (value: RequestPagination) => void;
  filtering: RequestFiltering[];
  setFiltering: (value: RequestFiltering[]) => void;
  sorting: RequestSorting | null;
  setSorting: (value: RequestSorting | null) => void;
  customers: CustomerResponse[];
  update: () => Promise<void>;
  isLoading: boolean;
}>({
  pagination: new RequestPagination(),
  setPagination: () => {},
  filtering: [],
  setFiltering: () => {},
  sorting: null,
  setSorting: () => {},
  customers: [],
  update: () => new Promise<void>(() => {}),
  isLoading: false,
});

export const useDirectDebitingCustomers = () => useContext(CustomersContext);

export default function DirectDebitingCustomersProvider({
  children,
}: DirectDebitingCustomersProviderProps) {
  const authHeader = useAuthHeader();
  const apiUrl = useApiUrl();
  const [sorting, setSorting] = useState<RequestSorting | null>(null);
  const [filtering, setFiltering] = useState<RequestFiltering[]>(
    CustomersRequestFilters.filtering
  );
  const [pagination, setPagination] = useState(new RequestPagination());
  const [customers, setCustomers] = useState<CustomerResponse[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [openErrorSnackbar] = useErrorSnackbar();

  const getCustomers = () => {
    let filteringString = "";
    for (const filter of filtering) {
      filteringString += filter.toRequestString();
    }

    console.log(filteringString);

    setIsLoading(true);
    return axios
      .get(
        `${apiUrl}/customers/?paymentType=${
          PaymentType.DirectDebiting
        }&${pagination.toRequestString()}&${
          sorting ? sorting.toRequestString() : ""
        }${filteringString}`,
        {
          headers: { Authorization: authHeader() },
        }
      )
      .then((response) => {
        setPagination(
          new RequestPagination(
            response.data.page,
            response.data.pageSize,
            response.data.totalCount
          )
        );
        setCustomers(
          response.data.items.map((customer: any) => {
            return { customer: customer, links: customer.links };
          })
        );
      })
      .catch((error) => {
        openErrorSnackbar(error);
      })
      .finally(() => {
        setIsLoading(false);
      });
  };

  useEffect(() => {
    getCustomers();
  }, [
    sorting?.column,
    sorting?.sorting,
    pagination.page,
    pagination.pageSize,
    filtering,
  ]);

  return (
    <CustomersContext.Provider
      value={{
        pagination: pagination,
        setPagination: setPagination,
        filtering: filtering,
        setFiltering: setFiltering,
        sorting: sorting,
        setSorting: setSorting,
        customers: customers,
        update: getCustomers,
        isLoading: isLoading,
      }}
    >
      {children}
    </CustomersContext.Provider>
  );
}
