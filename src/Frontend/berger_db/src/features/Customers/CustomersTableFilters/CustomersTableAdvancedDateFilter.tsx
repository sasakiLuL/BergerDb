import { DatePicker } from "@mui/x-date-pickers";
import { useState } from "react";
import RequestFiltering from "../../../utils/Types/RequestFiltering";
import { Stack } from "@mui/material";

export interface CustomersTableAdvancedDateFilterProps {
  startMonth: string;
  endMonth: string;
  filtering: RequestFiltering[];
  setFiltering: (value: RequestFiltering[]) => void;
}

export default function CustomersTableAdvancedDateFilter({
  startMonth,
  endMonth,
  filtering,
  setFiltering,
}: CustomersTableAdvancedDateFilterProps) {
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);

  const onChange = (start: Date | null, end: Date | null) => {
    setFiltering(
      filtering.map((filter) => {
        if (filter.field === startMonth)
          filter.filtering = start?.toISOString() ?? "";
        return filter;
      })
    );
    setFiltering(
      filtering.map((filter) => {
        if (filter.field === endMonth)
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
        openTo="month"
        views={["month"]}
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
        openTo="month"
        views={["month"]}
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
