import {
  Page,
  Document,
  View,
  Text,
  Image,
  StyleSheet,
} from "@react-pdf/renderer";
import Customer, { Sex } from "../../../utils/Types/Customer";
import dayjs from "dayjs";
import Logo from "../../../assets/Logo_lila.png";
import { usePdfStyle } from "../../shared/hooks/PdfStylesProvider";
import EmailConfiguraion from "../../../utils/Types/EmailConfiguration";

export interface RemindingDocumentProps {
  emailConfiguration: EmailConfiguraion;
  customer: Customer;
  dateFormat: string;
}

export default function RemindingDocument({
  dateFormat,
  customer,
  emailConfiguration,
}: RemindingDocumentProps) {
  const {
    fontFamily,
    mainFontSize,
    subFontSize,
    headerFontSize,
    subTextColor,
    mainTextColor,
    blockPaddings,
  } = usePdfStyle();

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
  } = emailConfiguration;

  return (
    <Document title="Erinnerung">
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
                <View style={[styles.view, { paddingTop: "28pt" }]}>
                  <Text style={[styles.boldFont, styles.headerFontSize]}>
                    Erinnerung
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
                    <Text style={{ minWidth: "50%" }}>Betr.:</Text>
                    <Text style={[{ minWidth: "50%" }, styles.boldFont]}>
                      ReNr. {customer.personalId}-
                      {dayjs(new Date()).format("YYMMDD")}
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
              <View>
                <View style={[styles.view]}>
                  <Text>
                    Sie sind bei uns im Homöopathie-Portal{" "}
                    <Text
                      style={[
                        styles.boldFont,
                        styles.subFontColor,
                        { textDecoration: "underline" },
                      ]}
                    >
                      {homePage}
                    </Text>{" "}
                    als Mitglied eingetragen.
                  </Text>
                </View>
                <View style={[styles.view]}>
                  <Text>
                    Die Kosten für Ihre Mitgliedschaft und Ihren Eintrag im
                    Homöopathie-Portal belaufen sich auf {customer.amount},-€ /
                    Jahr.
                  </Text>
                </View>
                <View style={[styles.view]}>
                  <Text>
                    Am{" "}
                    {dayjs(customer.currentInvoiceSendedOn).format(dateFormat)}{" "}
                    haben wir Ihnen Ihre Rechnung per Email zugestellt. Leider
                    ist der offene Betrag bis heute nicht bei uns eingegangen.
                    Haben wir irgendetwas übersehen?
                  </Text>
                </View>
                <View style={[styles.view]}>
                  <Text>
                    Andernfalls möchten wir Sie freundlichst daran erinnern und
                    bitten, den noch offenen Betrag von{" "}
                    <Text style={[styles.boldFont]}>
                      {customer.amount},-€ bis zum{" "}
                      {dayjs(new Date()).add(14, "day").format(dateFormat)}
                    </Text>{" "}
                    auf unser unten aufgeführtes Konto zu überweisen.
                  </Text>
                </View>
                <View style={[styles.view]}>
                  <Text>
                    Sollte es Fragen geben, stehen wir gerne zur Verfügung. Am
                    besten erreicht man uns immer per Email.
                  </Text>
                </View>
                <View style={styles.view}>
                  <Text>Mit freundlichen Grüßen</Text>
                </View>
                <View style={styles.view}>
                  <Text>Verena Berger</Text>
                </View>
                <View style={styles.view}>
                  <Text>
                    PS.: Falls gewünscht, können Sie uns Ihre Kontoverbindung
                    zum Einzugsverfahren übermitteln. Einen Vordruck senden wir
                    Ihnen gerne per E-Mail zu.
                  </Text>
                </View>
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
