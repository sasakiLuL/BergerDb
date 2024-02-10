import { useEffect, useState } from "react";
import EnhancedTable, {
  EnhancedTableColumn,
} from "../../shared/EnhancedTable/EnhancedTable";
import { useBillingCustomers } from "../hooks/BillingCustomers/BillingCustomersProvider";
import { useLocalizationContext } from "@mui/x-date-pickers/internals";
import {
  Box,
  Button,
  IconButton,
  Paper,
  Tab,
  TablePagination,
  Tabs,
  Typography,
  useTheme,
} from "@mui/material";
import CustomersTableToolbar from "../CustomersTableComponents/CustomersTableToolbar";
import CustomerResponse from "../../../utils/Types/CustomerResponse";
import CustomersTableColumns from "../CustomersTableColumns/CustomersTableColumns";
import CustomersTableSelectAllButton from "../CustomersTableComponents/CustomersTableSelectAllButton";
import CustomersTableSelectButton from "../CustomersTableComponents/CustomersTableSelectButton";
import { useSelectedBillingCustomers } from "../hooks/BillingCustomers/SelectedBillingCustomersProvider";
import SendIcon from "@mui/icons-material/Send";
import DeleteSweepIcon from "@mui/icons-material/DeleteSweep";
import ManageAccountsIcon from "@mui/icons-material/ManageAccounts";
import UpdateCustomerDialog from "../UpdateCustomerDialog/UpdateCustomerDialog";
import { UpdateAddressProps } from "../UpdateCustomerDialog/UpdateAddressForm";
import { UpdateMembershipProps } from "../UpdateCustomerDialog/UpdateMembershipForm";
import { UpdateNotationProps } from "../UpdateCustomerDialog/UpdateNotationForm";
import { UpdatePersonalInfoProps } from "../UpdateCustomerDialog/UpdatePersonalInfoForm";
import axios from "axios";
import { useAuthHeader } from "react-auth-kit";
import { useErrorSnackbar } from "../../shared/hooks/ErrorSnackbarProvider";
import DeleteCustomersDialog from "../DeleteCustomersDialog";
import AddCustomerDialog, {
  AddCustomerProps,
} from "../AddCustomerDialog/AddCustomerDialog";
import { useApiUrl } from "../../shared/hooks/ApiUrlProvider";
import PersonAddAlt1Icon from "@mui/icons-material/PersonAddAlt1";
import SendEmailDialog from "./SendCustomerEmailDialog";
import { pdf } from "@react-pdf/renderer";
import InvoiceDocument from "../../Users/InvoiceDocument/InvoiceDocument";
import { useEmailConfiguration } from "../../Users/hooks/EmailConfigurationProvider";
import MarkEmailReadIcon from "@mui/icons-material/MarkEmailRead";
import DoneOutlineIcon from "@mui/icons-material/DoneOutline";
import dayjs from "dayjs";
import CustomersTableBooleanFilter from "../CustomersTableFilters/CustomersTableBooleanFilter";
import { Sex } from "../../../utils/Types/Customer";
import RemindingDocument from "../../Users/RemindingDocument/RemindingDocument";
import JSZip from "jszip";
import { saveAs } from "file-saver";
import CustomersTableDateFilter from "../CustomersTableFilters/CustomersTableDateFilter";
import CustomersTableSort from "../CustomersTableFilters/CustomersTableSort";

export default function BillingCustomersTable() {
  const localization = useLocalizationContext();
  const theme = useTheme();
  const apiUrl = useApiUrl();
  const authHeader = useAuthHeader();
  const [openErrorSnackbar] = useErrorSnackbar();
  const [isShownFilters, setIsShownFilters] = useState<boolean>(false);
  const {
    filtering,
    setFiltering,
    sorting,
    setSorting,
    pagination,
    setPagination,
    customers,
    update,
  } = useBillingCustomers();
  const { selectAll, clear, selectedCustomers, setSelectedCustomers } =
    useSelectedBillingCustomers();
  const [customersType, setCustomersType] = useState<"normal" | "debtor">(
    "normal"
  );
  const [columns, setColumns] = useState<
    EnhancedTableColumn<CustomerResponse>[]
  >([
    {
      field: "selectAll",
      renderHeader: () => (
        <Box sx={{ width: "50px" }}>
          {customers.length !== 0 && (
            <CustomersTableSelectAllButton
              selectedCustomersCount={selectedCustomers.length}
              totalCustomerCount={pagination.totalItemsCount}
              selectAll={selectAll}
              clear={clear}
            />
          )}
        </Box>
      ),
      renderColumn: (item: CustomerResponse) => (
        <CustomersTableSelectButton
          selectedCustomers={selectedCustomers}
          setSelectedCustomers={setSelectedCustomers}
          item={item}
        />
      ),
      display: true,
    },
    {
      field: "edit",
      renderHeader: () => <Box sx={{ width: "50px" }}></Box>,
      renderColumn: (item: CustomerResponse) => (
        <IconButton
          onClick={() => {
            setSingleSelectedCustomer(item);
            setIsUpdateCustomerDialogOpen(true);
          }}
        >
          <ManageAccountsIcon />
        </IconButton>
      ),
      display: true,
    },
    {
      field: "isRecivedInvoice",
      renderHeader: () => (
        <>
          <Typography fontWeight={700}>Die Rechnung erhalten?</Typography>
          <MarkEmailReadIcon sx={{ m: theme.spacing(1) }} color="primary" />
        </>
      ),
      renderColumn: (item: CustomerResponse) => (
        <Box sx={{ textAlign: "center" }}>
          {item.customer.isRecivedInvoice ? (
            <DoneOutlineIcon color="success"></DoneOutlineIcon>
          ) : (
            <></>
          )}
        </Box>
      ),
      renderFilter: () => (
        <CustomersTableBooleanFilter
          filtering={filtering}
          setFiltering={setFiltering}
          field="isRecivedInvoice"
        />
      ),
      display: customersType === "normal",
    },
    {
      field: "isRecivedDunning",
      renderHeader: () => (
        <>
          <Typography
            color="warning"
            sx={{ mx: theme.spacing(1) }}
            fontWeight={700}
          >
            Die Mahnung erhalten?
          </Typography>
          <MarkEmailReadIcon color="warning" />
        </>
      ),
      renderColumn: (item: CustomerResponse) =>
        item.customer.isRecivedDunning ? (
          <DoneOutlineIcon color="success"></DoneOutlineIcon>
        ) : (
          <></>
        ),
      renderFilter: () => (
        <CustomersTableBooleanFilter
          filtering={filtering}
          setFiltering={setFiltering}
          field="isRecivedDunning"
        />
      ),
      display: customersType === "debtor",
    },
    ...new CustomersTableColumns(
      filtering,
      setFiltering,
      sorting,
      setSorting,
      localization.utils.formats.fullDate
    ).columns,
    {
      field: "dunningSendedOn",
      renderHeader: () => (
        <Typography fontWeight={700}>Mahnung datum</Typography>
      ),
      renderFilter: () => (
        <CustomersTableDateFilter
          filtering={filtering}
          setFiltering={setFiltering}
          startDateField="dunningSendedOnGte"
          endDateField="dunningSendedOnLte"
        />
      ),
      renderSorting: () => (
        <CustomersTableSort
          sorting={sorting}
          setSorting={setSorting}
          field="dunningSendedOn"
        />
      ),
      renderColumn: (item: CustomerResponse) => (
        <>
          {item.customer.dunningSendedOn !== null
            ? dayjs(item.customer.dunningSendedOn).format(
                localization.utils.formats.fullDate
              )
            : ""}
        </>
      ),
      display: customersType === "debtor",
    },
  ]);
  const [isUpdateCustomerDialogOpen, setIsUpdateCustomerDialogOpen] =
    useState<boolean>(false);
  const [isDeleteCustomersDialogOpen, setIsDeleteCustomersDialogOpen] =
    useState<boolean>(false);
  const [singleSelectedCustomer, setSingleSelectedCustomer] =
    useState<CustomerResponse | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isAddCustomerDialogOpen, setIsAddCustomerDialogOpen] =
    useState<boolean>(false);
  const [isSendInvoiceDialogOpen, setIsSendInvoiceDialogOpen] =
    useState<boolean>(false);
  const [isSendDunningDialogOpen, setIsSendDunningDialogOpen] =
    useState<boolean>(false);

  const { emailConfiguration } = useEmailConfiguration();

  useEffect(() => {
    setColumns([
      {
        field: "selectAll",
        renderHeader: () => (
          <Box sx={{ width: "50px" }}>
            {customers.length !== 0 && (
              <CustomersTableSelectAllButton
                selectedCustomersCount={selectedCustomers.length}
                totalCustomerCount={pagination.totalItemsCount}
                selectAll={selectAll}
                clear={clear}
              />
            )}
          </Box>
        ),
        renderColumn: (item: CustomerResponse) => (
          <CustomersTableSelectButton
            selectedCustomers={selectedCustomers}
            setSelectedCustomers={setSelectedCustomers}
            item={item}
          />
        ),
        display: true,
      },
      {
        field: "edit",
        renderHeader: () => <Box sx={{ width: "50px" }}></Box>,
        renderColumn: (item: CustomerResponse) => (
          <IconButton
            onClick={() => {
              setSingleSelectedCustomer(item);
              setIsUpdateCustomerDialogOpen(true);
            }}
          >
            <ManageAccountsIcon />
          </IconButton>
        ),
        display: true,
      },
      {
        field: "isRecivedInvoice",
        renderHeader: () => (
          <>
            <Typography fontWeight={700}>
              Wurde die Rechnung verschickt?
            </Typography>
            <MarkEmailReadIcon sx={{ m: theme.spacing(1) }} color="primary" />
          </>
        ),
        renderColumn: (item: CustomerResponse) => (
          <Box sx={{ textAlign: "center" }}>
            {item.customer.isRecivedInvoice ? (
              <DoneOutlineIcon color="success"></DoneOutlineIcon>
            ) : (
              <></>
            )}
          </Box>
        ),
        renderFilter: () => (
          <CustomersTableBooleanFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="isRecivedInvoice"
          />
        ),
        display: customersType === "normal",
      },
      {
        field: "isRecivedDunning",
        renderHeader: () => (
          <>
            <Typography
              color="warning"
              sx={{ mx: theme.spacing(1) }}
              fontWeight={700}
            >
              Wurde die Mahnung verschickt?
            </Typography>
            <MarkEmailReadIcon color="warning" />
          </>
        ),
        renderColumn: (item: CustomerResponse) =>
          item.customer.isRecivedDunning ? (
            <DoneOutlineIcon color="success"></DoneOutlineIcon>
          ) : (
            <></>
          ),
        renderFilter: () => (
          <CustomersTableBooleanFilter
            filtering={filtering}
            setFiltering={setFiltering}
            field="isRecivedDunning"
          />
        ),
        display: customersType === "debtor",
      },
      ...new CustomersTableColumns(
        filtering,
        setFiltering,
        sorting,
        setSorting,
        localization.utils.formats.fullDate
      ).columns.map((column) => {
        return {
          ...column,
          display: columns.filter((item) => item.field === column.field)[0]
            .display,
        };
      }),
      {
        field: "dunningSendedOn",
        renderHeader: () => (
          <Typography fontWeight={700}>Mahnung datum</Typography>
        ),
        renderFilter: () => (
          <CustomersTableDateFilter
            filtering={filtering}
            setFiltering={setFiltering}
            startDateField="dunningSendedOnGte"
            endDateField="dunningSendedOnLte"
          />
        ),
        renderSorting: () => (
          <CustomersTableSort
            sorting={sorting}
            setSorting={setSorting}
            field="dunningSendedOn"
          />
        ),
        renderColumn: (item: CustomerResponse) => (
          <>
            {item.customer.dunningSendedOn !== null
              ? dayjs(item.customer.dunningSendedOn).format(
                  localization.utils.formats.fullDate
                )
              : ""}
          </>
        ),
        display: customersType === "debtor",
      },
    ]);
  }, [selectedCustomers]);

  useEffect(() => {
    clear();
  }, [
    filtering,
    pagination.pageSize,
    pagination.totalItemsCount,
    sorting?.column,
    sorting?.sorting,
  ]);

  return (
    <>
      {singleSelectedCustomer !== null && (
        <UpdateCustomerDialog
          customer={singleSelectedCustomer}
          open={isUpdateCustomerDialogOpen}
          dateFormat={localization.utils.formats.fullDate}
          onPersonalInfoSubmit={(value: UpdatePersonalInfoProps) => {
            axios
              .put(
                singleSelectedCustomer!.links.find(
                  (link) => link.rel === "update-customer-data"
                )!.href,
                value,
                {
                  headers: { Authorization: authHeader() },
                }
              )
              .catch((error) => {
                if (error.response) {
                  openErrorSnackbar(
                    error.response.data.errors.map((err: any) => err.message)
                  );
                }
              });
          }}
          onMembershipSubmit={(value: UpdateMembershipProps) => {
            axios
              .put(
                singleSelectedCustomer!.links.find(
                  (link) => link.rel === "update-customer-membership"
                )!.href,
                value,
                {
                  headers: { Authorization: authHeader() },
                }
              )
              .catch((error) => {
                if (error.response) {
                  openErrorSnackbar(
                    error.response.data.errors.map((err: any) => err.message)
                  );
                }
              });
          }}
          onAddressSubmit={(value: UpdateAddressProps) => {
            axios
              .put(
                singleSelectedCustomer!.links.find(
                  (link) => link.rel === "update-customer-address"
                )!.href,
                value,
                {
                  headers: { Authorization: authHeader() },
                }
              )
              .catch((error) => {
                if (error.response) {
                  openErrorSnackbar(
                    error.response.data.errors.map((err: any) => err.message)
                  );
                }
              });
          }}
          onNotationSubmit={(value: UpdateNotationProps) => {
            axios
              .put(
                singleSelectedCustomer!.links.find(
                  (link) => link.rel === "update-customer-notation"
                )!.href,
                value,
                {
                  headers: { Authorization: authHeader() },
                }
              )
              .catch((error) => {
                if (error.response) {
                  openErrorSnackbar(
                    error.response.data.errors.map((err: any) => err.message)
                  );
                }
              });
          }}
          onClose={() => {
            setIsUpdateCustomerDialogOpen(false);
            update().finally(() => {
              setIsLoading(false);
            });
          }}
        ></UpdateCustomerDialog>
      )}

      <DeleteCustomersDialog
        title={"Kunde löschen"}
        message={`Sie sind sicher, dass Sie ${selectedCustomers.length} Kunden(Kunde) löschen möchten`}
        open={isDeleteCustomersDialogOpen}
        handleAcceptClose={() => {
          setIsLoading(true);
          Promise.all(
            selectedCustomers.map((customer) =>
              axios
                .delete(
                  customer.links.find((link) => link.rel === "delete-customer")!
                    .href,
                  {
                    headers: { Authorization: authHeader() },
                  }
                )
                .catch((error) => {
                  if (error.response) {
                    openErrorSnackbar(
                      error.response.data.errors.map((err: any) => err.message)
                    );
                  }
                })
            )
          ).finally(() => {
            setSelectedCustomers([]);
            setIsDeleteCustomersDialogOpen(false);
            update().finally(() => {
              setIsLoading(false);
            });
          });
        }}
        handleCancelClose={() => setIsDeleteCustomersDialogOpen(false)}
      ></DeleteCustomersDialog>

      <AddCustomerDialog
        open={isAddCustomerDialogOpen}
        onSubmit={(value: AddCustomerProps) => {
          setIsLoading(true);
          axios
            .post(`${apiUrl}/customers`, value, {
              headers: { Authorization: authHeader() },
            })
            .catch((error) => {
              if (error.response) {
                openErrorSnackbar(
                  error.response.data.errors.map((err: any) => err.message)
                );
              }
            })
            .finally(() => {
              update().finally(() => {
                setIsLoading(false);
              });
              setIsAddCustomerDialogOpen(false);
            });
        }}
        onClose={() => {
          setIsAddCustomerDialogOpen(false);
        }}
      ></AddCustomerDialog>

      <Button
        onClick={() => {
          setIsAddCustomerDialogOpen(true);
        }}
        size="large"
        variant="contained"
        startIcon={<PersonAddAlt1Icon />}
        sx={{ m: theme.spacing(2) }}
      >
        Kunde hinzufügen
      </Button>

      <Typography variant="h3">Rechnung</Typography>
      <SendEmailDialog
        open={isSendInvoiceDialogOpen}
        customers={selectedCustomers}
        onSubmit={() => {
          setIsLoading(true);
          Promise.all(
            selectedCustomers.map((customer) => {
              const fileName = `${customer.customer.personalId}-${dayjs(
                new Date()
              ).format("YYMMDD")}`;
              const formData = new FormData();
              formData.append(
                "Subject",
                emailConfiguration!.invoiceEmailSubject
              );
              formData.append(
                "BodyText",
                `${
                  customer.customer.sex === Sex.Male
                    ? "Sehr geehrter Herr"
                    : "Sehr geehrte Frau"
                } ${
                  customer.customer.prefix !== ""
                    ? customer.customer.prefix + " "
                    : ""
                }${customer.customer.lastName}\n${
                  emailConfiguration!.invoiceEmailBody
                }`
              );
              formData.append("FileName", fileName);
              return pdf(
                <InvoiceDocument
                  emailConfiguration={emailConfiguration!}
                  customer={customer.customer}
                  dateFormat={localization.utils.formats.fullDate}
                />
              )
                .toBlob()
                .then((blob) => {
                  formData.append("PdfFile", blob, `${fileName}.pdf`);
                })
                .then(() =>
                  axios
                    .post(
                      customer.links.find(
                        (link) => link.rel === "send-invoice"
                      )!.href,
                      formData,
                      {
                        headers: {
                          Authorization: authHeader(),
                          "Content-Type": "multipart/form-data",
                        },
                      }
                    )
                    .catch((error) => {
                      if (error.response) {
                        openErrorSnackbar(
                          error.response.data.errors.map(
                            (err: any) => err.message
                          )
                        );
                      }
                    })
                );
            })
          ).finally(() => {
            update().finally(() => {
              setSelectedCustomers([]);
              setIsLoading(false);
            });
            setIsSendInvoiceDialogOpen(false);
          });
        }}
        onClose={() => {
          setIsSendInvoiceDialogOpen(false);
        }}
        onDownloadButtonClick={() => {
          setIsLoading(true);
          const zip = new JSZip();
          Promise.all(
            selectedCustomers.map((customer, index) => {
              const fileName = `${customer.customer.personalId}-${dayjs(
                new Date()
              ).format("YYMMDD")}`;
              return pdf(
                <InvoiceDocument
                  emailConfiguration={emailConfiguration!}
                  customer={customer.customer}
                  dateFormat={localization.utils.formats.fullDate}
                />
              )
                .toBlob()
                .then((blob) => {
                  console.log(fileName);
                  zip.file(`${fileName}(${index}).pdf`, blob);
                })
                .catch((error) => openErrorSnackbar(error));
            })
          ).then(() => {
            zip
              .generateAsync({ type: "blob" })
              .then((content) => {
                saveAs(content, "Rechnungen.zip");
              })
              .finally(() => {
                setIsLoading(false);
              });
          });
        }}
      ></SendEmailDialog>

      <SendEmailDialog
        open={isSendDunningDialogOpen}
        customers={selectedCustomers}
        onSubmit={() => {
          setIsLoading(true);
          Promise.all(
            selectedCustomers.map((customer) => {
              const fileName = `${customer.customer.personalId}-${dayjs(
                new Date()
              ).format("YYMMDD")}`;
              const formData = new FormData();
              formData.append(
                "Subject",
                emailConfiguration!.billingRemindingEmailSubject
              );
              formData.append(
                "BodyText",
                `${
                  customer.customer.sex === Sex.Male
                    ? "Sehr geehrter Herr"
                    : "Sehr geehrte Frau"
                } ${
                  customer.customer.prefix !== ""
                    ? customer.customer.prefix + " "
                    : ""
                }${customer.customer.lastName}\n${
                  emailConfiguration!.billingRemindingEmailBody
                }`
              );
              formData.append("FileName", fileName);
              return pdf(
                <RemindingDocument
                  emailConfiguration={emailConfiguration!}
                  customer={customer.customer}
                  dateFormat={localization.utils.formats.fullDate}
                />
              )
                .toBlob()
                .then((blob) => {
                  formData.append("PdfFile", blob, `${fileName}.pdf`);
                })
                .then(() =>
                  axios
                    .post(
                      customer.links.find(
                        (link) => link.rel === "send-dunning"
                      )!.href,
                      formData,
                      {
                        headers: {
                          Authorization: authHeader(),
                          "Content-Type": "multipart/form-data",
                        },
                      }
                    )
                    .catch((error) => {
                      if (error.response) {
                        openErrorSnackbar(
                          error.response.data.errors.map(
                            (err: any) => err.message
                          )
                        );
                      }
                    })
                );
            })
          ).finally(() => {
            update().finally(() => {
              setSelectedCustomers([]);
              setIsLoading(false);
            });
            setIsSendDunningDialogOpen(false);
          });
        }}
        onClose={() => {
          setIsSendDunningDialogOpen(false);
        }}
        onDownloadButtonClick={() => {
          setIsLoading(true);
          const zip = new JSZip();
          Promise.all(
            selectedCustomers.map((customer, index) => {
              const fileName = `${customer.customer.personalId}-${dayjs(
                new Date()
              ).format("YYMMDD")}`;
              return pdf(
                <RemindingDocument
                  emailConfiguration={emailConfiguration!}
                  customer={customer.customer}
                  dateFormat={localization.utils.formats.fullDate}
                />
              )
                .toBlob()
                .then((blob) => {
                  zip.file(`${fileName}(${index}).pdf`, blob);
                });
            })
          )
            .catch((error) => openErrorSnackbar(error))
            .then(() => {
              zip.generateAsync({ type: "blob" }).then((content) => {
                saveAs(content, "Rechnungen.zip");
              });
            })
            .finally(() => {
              setIsLoading(false);
            });
        }}
      ></SendEmailDialog>

      <Paper>
        <EnhancedTable
          columns={columns}
          rows={customers}
          isShownFilters={isShownFilters}
          isLoading={isLoading}
          toolbar={
            <CustomersTableToolbar
              columns={columns}
              setColumns={setColumns}
              isShownFilters={isShownFilters}
              setIsShownFilters={setIsShownFilters}
            >
              {selectedCustomers.length > 0 ? (
                <>
                  <Typography
                    sx={{ flex: "1 1 100%", mx: "10px" }}
                    color="inherit"
                    variant="h6"
                    component="div"
                  >
                    {selectedCustomers.length} ausgewählt
                  </Typography>
                  <Button
                    sx={{ flex: "1 1 100%", mx: "10px" }}
                    variant="contained"
                    color={customersType === "debtor" ? "warning" : "primary"}
                    size="small"
                    disabled={
                      selectedCustomers.filter((customer) =>
                        customersType === "debtor"
                          ? customer.customer.isRecivedDunning
                          : customer.customer.isRecivedInvoice
                      ).length !== 0
                    }
                    startIcon={<SendIcon />}
                    onClick={() => {
                      customersType === "debtor"
                        ? setIsSendDunningDialogOpen(true)
                        : setIsSendInvoiceDialogOpen(true);
                    }}
                  >
                    {customersType === "debtor" ? "Mahnung" : "Rechnungen"}{" "}
                    versenden
                  </Button>
                  <Button
                    sx={{ flex: "1 1 100%" }}
                    variant="contained"
                    color="error"
                    size="small"
                    startIcon={<DeleteSweepIcon />}
                    onClick={() => {
                      setIsDeleteCustomersDialogOpen(true);
                    }}
                  >
                    Kunden löschen
                  </Button>
                </>
              ) : (
                <>
                  <Tabs
                    variant="fullWidth"
                    sx={{ flex: "1 1 100%", mx: "10px" }}
                    TabIndicatorProps={{
                      style: { background: theme.palette.text.disabled },
                    }}
                    value={customersType}
                    onChange={(_, newValue) => {
                      setCustomersType(newValue);
                      setFiltering(
                        filtering.map((filter) => {
                          if (filter.field === "isDebtor")
                            filter.filtering =
                              newValue === "normal" ? "false" : "true";
                          return filter;
                        })
                      );
                    }}
                  >
                    <Tab value={"normal"} label="Kunden"></Tab>
                    <Tab
                      value={"debtor"}
                      sx={{
                        color: theme.palette.warning.main,
                        "&:hover": {
                          color: theme.palette.warning.main,
                        },
                      }}
                      label="Erinnerung"
                    ></Tab>
                  </Tabs>
                </>
              )}
            </CustomersTableToolbar>
          }
          pagination={
            <TablePagination
              showFirstButton
              showLastButton
              rowsPerPageOptions={[5, 10, 25]}
              count={pagination.totalItemsCount}
              rowsPerPage={pagination.pageSize}
              page={pagination.page - 1}
              onPageChange={(_, newPage) => {
                setPagination({
                  ...pagination,
                  page: newPage + 1,
                  toRequestString: pagination.toRequestString,
                });
              }}
              onRowsPerPageChange={(event) => {
                setPagination({
                  ...pagination,
                  page: 1,
                  pageSize: parseInt(event.target.value),
                  toRequestString: pagination.toRequestString,
                });
              }}
            />
          }
        />
      </Paper>
    </>
  );
}
