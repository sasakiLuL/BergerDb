import { TableSortLabel } from "@mui/material";
import RequestSorting from "../../../utils/Types/RequestSorting";

export interface CustomersTableSortProps {
  field: string;
  sorting: RequestSorting | null;
  setSorting: (value: RequestSorting | null) => void;
}

export default function CustomersTableSort({
  field,
  sorting,
  setSorting,
}: CustomersTableSortProps) {
  const isSortingCurrentColumn = sorting?.column === field;
  const isDescSorting = sorting?.sorting === "desc";

  return (
    <TableSortLabel
      active={isSortingCurrentColumn}
      direction={
        !isSortingCurrentColumn ? undefined : isDescSorting ? "desc" : "asc"
      }
      onClick={() => {
        setSorting(
          new RequestSorting(
            field,
            !isSortingCurrentColumn ? "asc" : isDescSorting ? "asc" : "desc"
          )
        );
      }}
    />
  );
}
