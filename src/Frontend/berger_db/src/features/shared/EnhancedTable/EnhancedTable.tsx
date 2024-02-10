import {
  Collapse,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { ReactElement, ReactNode } from "react";
import ProgressBar from "../ProgressBar/ProgressBar";

export interface EnhancedTableColumn<T> {
  field: string;
  renderHeader?: () => ReactNode;
  renderFilter?: () => ReactNode;
  renderSorting?: () => ReactNode;
  renderColumn?: (item: T) => ReactNode;
  display: boolean;
}

export interface EnhancedTableProps<T> {
  columns: EnhancedTableColumn<T>[];
  rows: T[];
  isShownFilters?: boolean;
  isLoading?: boolean;
  pagination?: ReactElement;
  toolbar?: ReactElement;
}

export default function EnhancedTable<T>({
  rows,
  columns,
  pagination,
  isShownFilters,
  isLoading,
  toolbar,
}: EnhancedTableProps<T>) {
  return (
    <>
      <ProgressBar open={isLoading ? isLoading : false} />
      {toolbar}
      <TableContainer sx={{ minHeight: "600px" }}>
        <Table>
          <TableHead>
            <TableRow>
              {columns.map(
                (column, index) =>
                  column.display && (
                    <TableCell key={`col-filter-${column.field}-${index}`}>
                      <Collapse in={isShownFilters}>
                        {column.renderFilter && column.renderFilter()}
                      </Collapse>
                    </TableCell>
                  )
              )}
            </TableRow>
            <TableRow>
              {columns.map(
                (column, index) =>
                  column.display && (
                    <TableCell key={`col-${column.field}-${index}`}>
                      <Stack direction="row" alignItems="center">
                        {column.renderHeader && column.renderHeader()}
                        {column.renderSorting && column.renderSorting()}
                      </Stack>
                    </TableCell>
                  )
              )}
            </TableRow>
          </TableHead>
          <TableBody>
            {rows.map((row, index) => (
              <TableRow key={`row-${index}`}>
                {columns.map(
                  (column, index) =>
                    column.display && (
                      <TableCell key={`cell-${index}`}>
                        {column.renderColumn && column.renderColumn(row)}
                      </TableCell>
                    )
                )}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Table>
        <TableHead>
          <TableRow>{pagination}</TableRow>
        </TableHead>
      </Table>
    </>
  );
}
