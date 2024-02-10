import { TextField } from "@mui/material";
import RequestFiltering from "../../../utils/Types/RequestFiltering";

export interface CustomersTableStringFilterProps {
  field: string;
  placeholder: string;
  filtering: RequestFiltering[];
  setFiltering: (value: RequestFiltering[]) => void;
}

export default function CustomersTableStringFilter({
  field,
  placeholder,
  filtering,
  setFiltering,
}: CustomersTableStringFilterProps) {
  return (
    <TextField
      size="small"
      sx={{ width: 150 }}
      id="filled-hidden-label-normal"
      placeholder={placeholder}
      onChange={(event) => {
        setFiltering(
          filtering.map((filter) => {
            if (filter.field === field) filter.filtering = event.target.value;
            return filter;
          })
        );
      }}
    />
  );
}
