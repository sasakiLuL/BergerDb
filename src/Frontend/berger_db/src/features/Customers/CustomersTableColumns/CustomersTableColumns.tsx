import { EnhancedTableColumn } from "../../shared/EnhancedTable/EnhancedTable";
import CustomerResponse from "../../../utils/Types/CustomerResponse";
import { Typography } from "@mui/material";
import dayjs from "dayjs";
import { Sex, MemberType, EntryType } from "../../../utils/Types/Customer";
import CustomersTableDateFilter from "../CustomersTableFilters/CustomersTableDateFilter";
import CustomersTableSelectFilter from "../CustomersTableFilters/CustomersTableSelectFilter";
import CustomersTableSort from "../CustomersTableFilters/CustomersTableSort";
import CustomersTableStringFilter from "../CustomersTableFilters/CustomersTableStringFilter";
import RequestFiltering from "../../../utils/Types/RequestFiltering";
import RequestSorting from "../../../utils/Types/RequestSorting";

export default class CustomersTableColumns {
  public columns: EnhancedTableColumn<CustomerResponse>[];

  constructor(
    filtering: RequestFiltering[],
    setFiltering: (value: RequestFiltering[]) => void,
    sorting: RequestSorting | null,
    setSorting: (sorting: RequestSorting | null) => void,
    dateFormat: string
  ) {
    this.columns = [
      {
        field: "id",
        renderHeader: () => <Typography fontWeight={700}>Id</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            field="id"
            placeholder="c801ec22-11bc-4687-9ec9-60d35b3606d4"
            filtering={filtering}
            setFiltering={setFiltering}
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="id"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.id}</>,
        display: false,
      },
      {
        field: "prefix",
        renderHeader: () => <Typography fontWeight={700}>Titel</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="prefix"
            placeholder="Dr."
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="prefix"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.prefix}</>,
        display: false,
      },
      {
        field: "firstName",
        renderHeader: () => <Typography fontWeight={700}>Vorname</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="firstName"
            placeholder="Thomas"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="firstName"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>{item.customer.firstName}</>
        ),
        display: true,
      },
      {
        field: "lastName",
        renderHeader: () => <Typography fontWeight={700}>Nachname</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="lastName"
            placeholder="Meyer"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="lastName"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.lastName}</>,
        display: true,
      },
      {
        field: "personalId",
        renderHeader: () => (
          <Typography fontWeight={700}>Personal Id</Typography>
        ),
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="personalId"
            placeholder="123"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="personalId"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>{item.customer.personalId}</>
        ),
        display: false,
      },
      {
        field: "email",
        renderHeader: () => <Typography fontWeight={700}>Email</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="email"
            placeholder="example@mail.com"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="email"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.email}</>,
        display: false,
      },
      {
        field: "sex",
        renderHeader: () => (
          <Typography fontWeight={700}>Geschlecht</Typography>
        ),
        renderFilter: () => (
          <CustomersTableSelectFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="sex"
            defaultOption="Alle"
            options={[
              [0, "Herr"],
              [1, "Frau"],
            ]}
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="sex"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>{item.customer.sex === Sex.Male ? "Herr" : "Frau"}</>
        ),
        display: false,
      },
      {
        field: "registrationDate",
        renderHeader: () => (
          <Typography fontWeight={700}>Registriert am</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="registrationDateGte"
            endDateField="registrationDateLte"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="registrationDate"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.registrationDate !== null
              ? dayjs(item.customer.registrationDate).format(dateFormat)
              : ""}
          </>
        ),
        display: true,
      },
      {
        field: "street",
        renderHeader: () => <Typography fontWeight={700}>Straße</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="street"
            placeholder="Arndtstr. 16"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="street"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.street}</>,
        display: false,
      },
      {
        field: "zipCode",
        renderHeader: () => <Typography fontWeight={700}>PLZ</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="zipCode"
            placeholder="22085"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="zipCode"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.zipCode}</>,
        display: false,
      },
      {
        field: "city",
        renderHeader: () => <Typography fontWeight={700}>Stadt</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="city"
            placeholder="Hamburg"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="city"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.city}</>,
        display: false,
      },
      {
        field: "memberType",
        renderHeader: () => <Typography fontWeight={700}>Mitglied</Typography>,
        renderFilter: () => (
          <CustomersTableSelectFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="memberType"
            defaultOption="Alle"
            options={[
              [0, "Apo"],
              [1, "Laie"],
              [2, "Arzt"],
              [3, "Heilpraktiker"],
            ]}
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="memberType"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.memberType === MemberType.Apothecary
              ? "Apo"
              : item.customer.memberType === MemberType.LayPerson
              ? "Laie"
              : item.customer.memberType === MemberType.Doctor
              ? "Arzt"
              : "Heilpraktiker"}
          </>
        ),
        display: true,
      },
      {
        field: "institution",
        renderHeader: () => (
          <Typography fontWeight={700}>Einrichtung</Typography>
        ),
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="institution"
            placeholder="Homoeopathie heute"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="institution"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>{item.customer.institution}</>
        ),
        display: false,
      },
      {
        field: "entryType",
        renderHeader: () => <Typography fontWeight={700}>Eintrag</Typography>,
        renderFilter: () => (
          <CustomersTableSelectFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="entryType"
            defaultOption="Alle"
            options={[
              [0, "GE"],
              [1, "EE"],
            ]}
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="entryType"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>{EntryType[item.customer.entryType]}</>
        ),
        display: true,
      },
      {
        field: "amount",
        renderHeader: () => <Typography fontWeight={700}>Betrag</Typography>,
        renderFilter: () => (
          <CustomersTableStringFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="amount"
            placeholder="50"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="amount"
          />
        ),
        renderColumn: (item: CustomerResponse) => <>{item.customer.amount}</>,
        display: false,
      },
      {
        field: "currentInvoiceSendedOn",
        renderHeader: () => (
          <Typography fontWeight={700}>Aktuelle Rechnung</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="currentInvoiceSendedOnGte"
            endDateField="currentInvoiceSendedOnLte"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="currentInvoiceSendedOn"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.currentInvoiceSendedOn !== null
              ? dayjs(item.customer.currentInvoiceSendedOn).format(dateFormat)
              : ""}
          </>
        ),
        display: true,
      },
      {
        field: "lastInvoiceSendedOn",
        renderHeader: () => (
          <Typography fontWeight={700}>Letzte Rechnung</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="lastInvoiceSendedOnGte"
            endDateField="lastInvoiceSendedOnLte"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="lastInvoiceSendedOn"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.lastInvoiceSendedOn !== null
              ? dayjs(item.customer.lastInvoiceSendedOn).format(dateFormat)
              : ""}
          </>
        ),
        display: false,
      },
      {
        field: "currentCreditReceivedOn",
        renderHeader: () => (
          <Typography fontWeight={700}>Aktuelle Gutschrift</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="currentCreditReceivedOnGte"
            endDateField="currentCreditReceivedOnLte"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="currentCreditReceivedOn"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.currentCreditReceivedOn !== null
              ? dayjs(item.customer.currentCreditReceivedOn).format(dateFormat)
              : ""}
          </>
        ),
        display: true,
      },
      {
        field: "lastCreditReceivedOn",
        renderHeader: () => (
          <Typography fontWeight={700}>Letzte Gutschrift</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="lastCreditReceivedOnGte"
            endDateField="lastCreditReceivedOnLte"
          />
        ),

        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="lastCreditReceivedOn"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.lastCreditReceivedOn !== null
              ? dayjs(item.customer.lastCreditReceivedOn).format(dateFormat)
              : ""}
          </>
        ),
        display: false,
      },
      {
        field: "terminatedOn",
        renderHeader: () => (
          <Typography fontWeight={700}>Kuendigen am</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="terminatedOnGte"
            endDateField="terminatedOnLte"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="terminatedOn"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.terminatedOn !== null
              ? dayjs(item.customer.terminatedOn).format(dateFormat)
              : ""}
          </>
        ),
        display: false,
      },
    ];
  }
}
