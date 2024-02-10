import { useState } from "react";
import { Stack } from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers";
import RequestFiltering from "../../../utils/Types/RequestFiltering";

export interface CustomersTableDateFilterProps {
  startDateField: string;
  endDateField: string;
  filtering: RequestFiltering[];
  setFiltering: (value: RequestFiltering[]) => void;
}

export default function CustomersTableDateFilter({
  startDateField,
  endDateField,
  filtering,
  setFiltering,
}: CustomersTableDateFilterProps) {
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);

  const onChange = (start: Date | null, end: Date | null) => {
    setFiltering(
      filtering.map((filter) => {
        if (filter.field === startDateField)
          filter.filtering = start?.toISOString() ?? "";
        return filter;
      })
    );

    setFiltering(
      filtering.map((filter) => {
        if (filter.field === endDateField)
          filter.filtering = end?.toISOString() ?? "";
        return filter;
      })
    );
  };

  const onStartDateChange = (value: Date | null) => {
    setStartDate(value);
    onChange(value, endDate);
  };

  const onEndDateChange = (value: Date | null) => {
    setEndDate(value);
    onChange(startDate, value);
  };

  return (
    <Stack spacing={1}>
      <DatePicker
        value={startDate}
        sx={{ width: 180 }}
        slotProps={{
          field: { clearable: true },
          textField: { size: "small" },
        }}
        maxDate={endDate === null ? undefined : endDate}
        label="Von"
        onChange={onStartDateChange}
      />
      <DatePicker
        value={endDate}
        sx={{ width: 180 }}
        slotProps={{
          field: { clearable: true },
          textField: { size: "small" },
        }}
        label="Bis"
        minDate={startDate === null ? undefined : startDate}
        onChange={onEndDateChange}
      />
    </Stack>
  );
}
