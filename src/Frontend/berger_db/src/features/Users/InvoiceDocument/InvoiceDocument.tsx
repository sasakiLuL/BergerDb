import {
  Page,
  Document,
  View,
  Line,
  Text,
  Svg,
  Image,
  StyleSheet,
} from "@react-pdf/renderer";
import Customer, { PaymentType, Sex } from "../../../utils/Types/Customer";
import dayjs from "dayjs";
import Logo from "../../../assets/Logo_lila.png";
import RichText from "../../shared/PdfRichText/PdfRichText";
import { usePdfStyle } from "../../shared/hooks/PdfStylesProvider";
import EmailConfiguraion from "../../../utils/Types/EmailConfiguration";
import { convertToRaw } from "draft-js";

export interface InvoiceDocumentProps {
  emailConfiguration: EmailConfiguraion;
  customer: Customer;
  dateFormat: string;
}

export default function InvoiceDocument({
  dateFormat,
  customer,
  emailConfiguration,
}: InvoiceDocumentProps) {
  const pdfStyle = usePdfStyle();
  const {
    fontFamily,
    mainFontSize,
    subFontSize,
    headerFontSize,
    subTextColor,
    mainTextColor,
    blockPaddings,
  } = pdfStyle;

  const styles = StyleSheet.create({
    page: {
      fontFamily: fontFamily,
      paddingHorizontal: "1.27cm",
      paddingVertical: "1cm",
      display: "flex",
    },
    subFontSize: {
      fontSize: subFontSize,
    },
    mainFontSize: {
      fontSize: mainFontSize,
    },
    headerFontSize: {
      fontSize: headerFontSize,
    },
    subFontColor: {
      color: subTextColor,
    },
    boldFont: {
      fontWeight: 700,
    },
    italicFont: {
      fontStyle: "italic",
    },
    mainFontColor: {
      color: mainTextColor,
    },
    view: {
      marginVertical: blockPaddings,
    },
  });

  const taxesPercentage = 0.19;
  const taxes = customer.amount * taxesPercentage;
  const amountWithoutTaxes = customer.amount - taxes;
  const {
    city,
    zipCode,
    street,
    phoneNumber,
    email,
    homePage,
    accountName,
    iban,
    bic,
    gid,
    taxIdentificationNumber,
    invoicePdfBody,
  } = emailConfiguration;

  return (
    <Document title="Rechnung">
      <Page
        size="A4"
        style={[styles.page, styles.mainFontColor, styles.mainFontSize]}
      >
        <View
          style={{
            height: "100%",
            display: "flex",
            flexDirection: "column",
            justifyContent: "space-between",
          }}
        >
          <View
            style={{
              display: "flex",
              flexDirection: "column",
              justifyContent: "flex-start",
              alignItems: "flex-end",
              height: "90%",
            }}
          >
            <View
              style={{
                width: "100%",
                height: "50%",
                display: "flex",
                flexDirection: "row",
              }}
            >
              <View style={{ width: "50%", alignSelf: "flex-end" }}>
                <View>
                  <View
                    style={[
                      styles.view,
                      styles.subFontColor,
                      styles.subFontSize,
                    ]}
                  >
                    <Text>Deutsches Netzwerk für Homöopathie</Text>
                    <Text>
                      {street} • {zipCode} {city}
                    </Text>
                  </View>
                  <View style={[styles.view, styles.headerFontSize]}>
                    <Text>{customer.sex === Sex.Female ? "Frau" : "Herr"}</Text>
                    <Text>
                      {customer.prefix !== "" && customer.prefix + " "}
                      {customer.firstName} {customer.lastName}
                    </Text>
                    <Text>{customer.street}</Text>
                  </View>
                  <View style={[styles.view, styles.headerFontSize]}>
                    <Text>
                      {customer.zipCode} {customer.city}
                    </Text>
                  </View>
                </View>
                <View
                  style={[
                    styles.view,
                    styles.boldFont,
                    styles.headerFontSize,
                    { paddingTop: "28pt", paddingBottom: "14pt" },
                  ]}
                >
                  <Text style={[styles.boldFont, styles.headerFontSize]}>
                    {customer.paymentType === PaymentType.Billing
                      ? "Rechnung"
                      : "Beleg für Ihre Unterlagen"}
                  </Text>
                </View>
                <View>
                  <Text style={styles.boldFont}>
                    {"Ihre Mitgliedschaft bei \n"}
                    <Text
                      style={[
                        styles.subFontColor,
                        styles.boldFont,
                        { textDecoration: "underline" },
                      ]}
                    >
                      {homePage}
                    </Text>
                  </Text>
                </View>
                <View style={styles.view}>
                  <View
                    style={{
                      display: "flex",
                      flexDirection: "row",
                      justifyContent: "space-between",
                      minWidth: "100%",
                    }}
                  >
                    <Text style={{ minWidth: "50%" }}>{city}, den</Text>
                    <Text style={{ minWidth: "50%" }}>
                      {dayjs(new Date()).format(dateFormat)}
                    </Text>
                  </View>
                  <View
                    style={{
                      display: "flex",
                      flexDirection: "row",
                      justifyContent: "space-between",
                      minWidth: "100%",
                    }}
                  >
                    <Text style={{ minWidth: "50%" }}>Rechnungsnummer:</Text>
                    <Text style={{ minWidth: "50%" }}>
                      {customer.personalId}-{dayjs(new Date()).format("YYMMDD")}
                    </Text>
                  </View>
                  <View
                    style={{
                      display: "flex",
                      flexDirection: "row",
                      justifyContent: "space-between",
                      minWidth: "100%",
                    }}
                  >
                    <Text style={{ minWidth: "50%" }}>
                      Abrechnungszeitraum:
                    </Text>
                    <Text style={{ minWidth: "50%" }}>
                      {`${dayjs(customer.registrationDate)
                        .year(new Date().getFullYear())
                        .format(dateFormat)} bis ${dayjs(
                        customer.registrationDate
                      )
                        .year(new Date().getFullYear())
                        .add(1, "y")
                        .subtract(1, "d")
                        .format(dateFormat)}`}
                    </Text>
                  </View>
                </View>
              </View>
              <View style={{ width: "50%" }}>
                <Image source={Logo} style={{ width: "100%" }} />
              </View>
            </View>
            <View style={{ width: "100%" }}>
              <View style={styles.view}>
                <Text>
                  {`Sehr ${
                    customer.sex === Sex.Female
                      ? "geehrte Frau"
                      : "geehrter Herr"
                  } ${customer.prefix !== "" ? customer.prefix + " " : ""}${
                    customer.lastName
                  },`}
                </Text>
              </View>
              <View style={[styles.view]}>
                <RichText
                  styleSheet={pdfStyle}
                  rawContentState={convertToRaw(
                    invoicePdfBody.getCurrentContent()
                  )}
                />
              </View>
            </View>
            <View style={{ width: "50%", textAlign: "right" }}>
              <View
                style={{
                  display: "flex",
                  flexDirection: "row",
                  justifyContent: "flex-start",
                  width: "100%",
                }}
              >
                <View
                  style={{
                    width: "70%",
                    display: "flex",
                    justifyContent: "space-between",
                    flexDirection: "row",
                  }}
                >
                  <Text>Jahresbeitrag</Text>
                  <Text>€</Text>
                </View>
                <Text style={{ width: "15%" }}>
                  {amountWithoutTaxes.toFixed(2)}
                </Text>
              </View>
              <View
                style={{
                  display: "flex",
                  flexDirection: "row",
                  justifyContent: "flex-start",
                  width: "100%",
                }}
              >
                <View
                  style={{
                    width: "70%",
                    display: "flex",
                    justifyContent: "space-between",
                    flexDirection: "row",
                  }}
                >
                  <Text>zzgl. 19% MwSt.</Text>
                  <Text>€</Text>
                </View>
                <Text style={{ width: "15%" }}>{taxes.toFixed(2)}</Text>
              </View>
              <View
                style={[
                  styles.view,
                  styles.boldFont,
                  {
                    display: "flex",
                    flexWrap: "wrap",
                    flexDirection: "row",
                    justifyContent: "flex-start",
                    width: "100%",
                  },
                ]}
              >
                <Svg width="85%" height="4">
                  <Line
                    x1="0"
                    y1="0"
                    x2="500"
                    y2="0"
                    strokeWidth={2}
                    stroke="rgb(0,0,0)"
                  />
                </Svg>
                <View
                  style={{
                    width: "70%",
                    display: "flex",
                    justifyContent: "space-between",
                    flexDirection: "row",
                  }}
                >
                  <Text>Rechnungssumme:</Text>
                  <Text>€</Text>
                </View>
                <Text style={{ width: "15%" }}>
                  {customer.amount.toFixed(2)}
                </Text>
              </View>
            </View>
            <View style={{ width: "100%" }}>
              <View style={styles.view}>
                {customer.paymentType === PaymentType.Billing ? (
                  <Text>
                    Bitte überweisen Sie den offenen Betrag in den nächsten 14
                    Tagen. Für Fragen stehen wir gerne zur Verfügung.
                  </Text>
                ) : (
                  <Text>
                    Der Betrag wird in den nächsten
                    <Text style={styles.boldFont}>
                      {" Tagen per Lastschrift "}
                    </Text>
                    von Ihrem Konto abgebucht. Für Fragen stehen wir gerne zur
                    Verfügung.
                  </Text>
                )}
              </View>
              <View style={styles.view}>
                <Text>Mit freundlichen Grüßen</Text>
              </View>
              <View style={styles.view}>
                <Text>Verena Berger</Text>
              </View>
            </View>
          </View>
          <View
            style={[
              {
                height: "10%",
                width: "100%",
                display: "flex",
                flexDirection: "row",
                justifyContent: "space-between",
                alignItems: "flex-start",
              },
              styles.subFontColor,
              styles.subFontSize,
            ]}
          >
            <View style={{ width: "30%" }}>
              <View style={styles.view}>
                <Text>Deutsches Netzwerk für Homöopathie</Text>
                <Text>{street}</Text>
                <Text>
                  {zipCode} {city}
                </Text>
              </View>
            </View>
            <View style={{ width: "30%" }}>
              <View style={styles.view}>
                <Text>Tel: {phoneNumber}</Text>
              </View>
              <View style={styles.view}>
                <Text>{homePage}</Text>
                <Text>{email}</Text>
              </View>
            </View>
            <View style={{ width: "30%" }}>
              <View style={styles.view}>
                <Text>{accountName}</Text>
                <Text>IBAN: {iban}</Text>
                <Text>BIC: {bic}</Text>
                <Text>G-ID: {gid}</Text>
              </View>
              <View style={styles.view}>
                <Text>Steuernummer: {taxIdentificationNumber}</Text>
              </View>
            </View>
          </View>
        </View>
      </Page>
    </Document>
  );
}
