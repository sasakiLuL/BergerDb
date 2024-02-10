import { ReactNode, createContext, useContext, useState } from "react";

export interface PdfStyleSheet {
  fontFamily: string;
  mainFontSize: string;
  subFontSize: string;
  headerFontSize: string;
  subTextColor: string;
  mainTextColor: string;
  blockPaddings: string;
}

export interface PdfStyleProviderProps {
  styleSheet?: PdfStyleSheet;
  children: ReactNode;
}

const defaultValue: PdfStyleSheet = {
  fontFamily: "NotoSans",
  mainFontSize: "10pt",
  subFontSize: "8pt",
  headerFontSize: "12pt",
  subTextColor: "#dfbfe9",
  mainTextColor: "#000000",
  blockPaddings: "6pt",
};

const PdfStylesContext = createContext<PdfStyleSheet>(defaultValue);

const PdfStylesUpdateContext = createContext<
  ((value: PdfStyleSheet) => void) | undefined
>(undefined);

export const usePdfStyle = () => useContext(PdfStylesContext);

export const usePdfStyleUpdate = () => useContext(PdfStylesUpdateContext);

export function PdfStyleProvider({ children }: PdfStyleProviderProps) {
  const [pdfStyle, setPdfStyle] = useState<PdfStyleSheet>(defaultValue);
  return (
    <PdfStylesContext.Provider value={pdfStyle}>
      <PdfStylesUpdateContext.Provider value={setPdfStyle}>
        {children}
      </PdfStylesUpdateContext.Provider>
    </PdfStylesContext.Provider>
  );
}
