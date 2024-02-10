import { FormControl, MenuItem, Select } from "@mui/material";
import RequestFiltering from "../../../utils/Types/RequestFiltering";

export interface CustomersTableBooleanFilterProps {
  field: string;
  filtering: RequestFiltering[];
  setFiltering: (value: RequestFiltering[]) => void;
}

export default function CustomersTableBooleanFilter({
  field,
  filtering,
  setFiltering,
}: CustomersTableBooleanFilterProps) {
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
          <em>Alle</em>
        </MenuItem>
        <MenuItem key={`${field}-true`} value={"true"}>
          Ja
        </MenuItem>
        <MenuItem key={`${field}-false`} value={"false"}>
          Nein
        </MenuItem>
      </Select>
    </FormControl>
  );
}
