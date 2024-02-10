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

export interface RefundRemindingDocumentProps {
  emailConfiguration: EmailConfiguraion;
  customer: Customer;
  returnedDate: Date;
  commission: number;
  waitingWeeks: number;
  dateFormat: string;
}

export default function RefundRemindingDocument({
  customer,
  returnedDate,
  commission,
  waitingWeeks,
  dateFormat,
  emailConfiguration,
}: RefundRemindingDocumentProps) {
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
    <Document title="Mahnung">
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
                    Mahnung
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
                    {`Wir haben Ihnen am ${dayjs(
                      customer.currentInvoiceSendedOn
                    ).format(
                      dateFormat
                    )} die Rechnung für Ihre Mitgliedschaft zugestellt und den offenen Betrag, wie gewünscht, am ${dayjs(
                      customer.currentCreditReceivedOn
                    ).format(
                      dateFormat
                    )} von Ihrem Konto eingezogen. Leider wurde dieser Betrag von Ihnen am ${dayjs(
                      returnedDate
                    ).format(
                      dateFormat
                    )} zurückgebucht. Dabei entstanden zusätzliche Kosten von ${commission.toFixed(
                      2
                    )}€`}
                  </Text>
                </View>
                <View style={[styles.view]}>
                  <Text>
                    Wir möchten Sie nun höflichst bitten, uns den noch offenen
                    Betrag von{" "}
                    <Text style={[styles.boldFont]}>
                      {customer.amount},-€ zuzüglich der Rückbuchungsgebühr von{" "}
                      {commission.toFixed(2)}€
                      <Text style={[styles.boldFont, styles.subFontColor]}>
                        {" "}
                        (Gesamtbetrag{" "}
                        {(commission + customer.amount).toFixed(2)}€){" "}
                      </Text>
                      bis zum{" "}
                      {dayjs(new Date())
                        .add(waitingWeeks, "weeks")
                        .format(dateFormat)}{" "}
                    </Text>
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
