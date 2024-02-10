import { FormControl, MenuItem, Select } from "@mui/material";
import RequestFiltering from "../../../utils/Types/RequestFiltering";

export interface CustomersTableSelectFilterProps {
  field: string;
  defaultOption: string;
  options: Array<[value: number, title: string]>;
  filtering: RequestFiltering[];
  setFiltering: (value: RequestFiltering[]) => void;
}

export default function CustomersTableSelectFilter({
  field,
  defaultOption,
  options,
  filtering,
  setFiltering,
}: CustomersTableSelectFilterProps) {
  return (
    <FormControl>
      <Select
        size="small"
        displayEmpty
        sx={{ maxWidth: 150 }}
        onChange={(event) => {
          setFiltering(
            filtering.map((filter) => {
              if (filter.field === field) filter.filtering = event.target.value;
              return filter;
            })
          );
        }}
        inputProps={{ "aria-label": "Without label" }}
        defaultValue={""}
      >
        <MenuItem value="">
          <em>{defaultOption}</em>
        </MenuItem>
        {options.map((option) => (
          <MenuItem key={`${field}-${option[0]}}`} value={option[0]}>
            {option[1]}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
}
