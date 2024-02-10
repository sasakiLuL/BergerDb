import { Checkbox } from "@mui/material";
import { useEffect, useState } from "react";

export interface CustomersTableSelectAllButtonProps {
  selectAll: () => void;
  clear: () => void;
  selectedCustomersCount: number;
  totalCustomerCount: number;
}

export default function CustomersTableSelectAllButton({
  selectAll,
  clear,
  selectedCustomersCount,
  totalCustomerCount,
}: CustomersTableSelectAllButtonProps) {
  const [selected, setSelected] = useState<boolean>(false);

  useEffect(() => {
    if (selectedCustomersCount <= 0) setSelected(false);
    else setSelected(true);
  }, [selectedCustomersCount]);

  return (
    <Checkbox
      checked={selected}
      indeterminate={
        selectedCustomersCount > 0 &&
        selectedCustomersCount < totalCustomerCount
      }
      onChange={(_, checked) => {
        checked ? selectAll() : clear();
        setSelected(checked);
      }}
      inputProps={{ "aria-label": "controlled" }}
    />
  );
}
