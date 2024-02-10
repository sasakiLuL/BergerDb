import { useState } from "react";
import {
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  FormLabel,
  IconButton,
  Popover,
  Tooltip,
} from "@mui/material";
import ViewColumnIcon from "@mui/icons-material/ViewColumn";
import { EnhancedTableColumn } from "../../shared/EnhancedTable/EnhancedTable";
import CustomerResponse from "../../../utils/Types/CustomerResponse";

export interface CustomersTableColumnSelectProps {
  columns: EnhancedTableColumn<CustomerResponse>[];
  setColums: (value: EnhancedTableColumn<CustomerResponse>[]) => void;
}

export default function CustomersTableColumnSelect({
  columns,
  setColums,
}: CustomersTableColumnSelectProps) {
  const [anchorElement, setAnchorElement] = useState<HTMLButtonElement | null>(
    null
  );

  return (
    <>
      <Tooltip title="Spalten">
        <IconButton
          onClick={(event) => {
            setAnchorElement(event.currentTarget);
          }}
        >
          <ViewColumnIcon />
        </IconButton>
      </Tooltip>
      <Popover
        id={Boolean(anchorElement) ? "column-select-popover" : undefined}
        open={Boolean(anchorElement)}
        anchorEl={anchorElement}
        onClose={() => setAnchorElement(null)}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "left",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "left",
        }}
      >
        <FormControl sx={{ m: 3 }} component="fieldset" variant="standard">
          <FormLabel component="legend">Spalten</FormLabel>
          <FormGroup>
            {columns.map((column) => (
              <FormControlLabel
                control={
                  <Checkbox
                    checked={column.display}
                    onChange={(event) => {
                      setColums(
                        columns.map((item) =>
                          event.target.name === item.field
                            ? { ...item, display: !item.display }
                            : item
                        )
                      );
                    }}
                    name={column.field}
                  />
                }
                key={`select-column-${column.field}`}
                label={column.renderHeader && column.renderHeader()}
              />
            ))}
          </FormGroup>
        </FormControl>
      </Popover>
    </>
  );
}
