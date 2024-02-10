import { Checkbox } from "@mui/material";
import CustomerResponse from "../../../utils/Types/CustomerResponse";

export interface CustomersTableSelectButtonProps {
  selectedCustomers: CustomerResponse[];
  setSelectedCustomers: (value: CustomerResponse[]) => void;
  item: CustomerResponse;
}

export default function CustomersTableSelectButton({
  item,
  selectedCustomers,
  setSelectedCustomers,
}: CustomersTableSelectButtonProps) {
  return (
    <Checkbox
      checked={
        selectedCustomers.find(
          (value) => value.customer.id === item.customer.id
        ) !== undefined
      }
      onChange={(_, checked) => {
        if (checked) {
          setSelectedCustomers([...selectedCustomers, item]);
        } else {
          setSelectedCustomers(
            selectedCustomers.filter(
              (value) => value.customer.id !== item.customer.id
            )
          );
        }
      }}
      inputProps={{ "aria-label": "controlled" }}
    />
  );
}
