import { IconButton, Toolbar, ToolbarProps, Tooltip } from "@mui/material";
import { EnhancedTableColumn } from "../../shared/EnhancedTable/EnhancedTable";
import FilterListIcon from "@mui/icons-material/FilterList";
import CustomersTableColumnSelect from "./CustomersTableColumnSelect";
import CustomerResponse from "../../../utils/Types/CustomerResponse";
import { ReactNode } from "react";

export interface CustomersTableToolbarProps extends ToolbarProps {
  columns: EnhancedTableColumn<CustomerResponse>[];
  setColumns: (value: EnhancedTableColumn<CustomerResponse>[]) => void;
  isShownFilters: boolean;
  setIsShownFilters: (value: boolean) => void;
  children: ReactNode;
}

export default function CustomersTableToolbar({
  columns,
  setColumns,
  isShownFilters,
  setIsShownFilters,
  children,
  ...props
}: CustomersTableToolbarProps) {
  return (
    <Toolbar
      sx={{
        pl: { sm: 2 },
        pr: { xs: 1, sm: 1 },
      }}
      {...props}
    >
      {children}
      <Tooltip title="Filterliste">
        <IconButton
          onClick={() => {
            setIsShownFilters(!isShownFilters);
          }}
        >
          <FilterListIcon />
        </IconButton>
      </Tooltip>
      <CustomersTableColumnSelect setColums={setColumns} columns={columns} />
    </Toolbar>
  );
}
