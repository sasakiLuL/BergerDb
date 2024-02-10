import { Text, View } from "@react-pdf/renderer";
import { RawDraftContentState } from "draft-js";
import { PdfStyleSheet } from "../hooks/PdfStylesProvider";
// @ts-ignore
import redraft from "redraft";

export interface PdfRichTextProps {
  rawContentState: RawDraftContentState;
  styleSheet: PdfStyleSheet;
}

export default function PdfRichText({
  rawContentState,
  styleSheet,
}: PdfRichTextProps) {
  const renderers = {
    inline: {
      BOLD: (children: any, { key }: any) => (
        <Text key={`bold-${key}`} style={{ fontWeight: 700 }}>
          {children}
        </Text>
      ),
      ITALIC: (children: any, { key }: any) => (
        <Text key={`italic-${key}`} style={{ fontStyle: "italic" }}>
          {children}
        </Text>
      ),
      UNDERLINE: (children: any, { key }: any) => (
        <Text key={`underline-${key}`} style={{ textDecoration: "underline" }}>
          {children}
        </Text>
      ),
      HIGHLIGHT: (children: any, { key }: any) => (
        <Text
          key={`underline-${key}`}
          style={{ color: styleSheet.subTextColor }}
        >
          {children}
        </Text>
      ),
    },
    blocks: {
      unstyled: (children: any, { keys }: any) => {
        return children.map((child: any, index: any) => {
          return (
            <View
              style={{ marginVertical: styleSheet.blockPaddings }}
              key={keys[index]}
            >
              <Text style={{ fontSize: styleSheet.mainFontSize }}>{child}</Text>
            </View>
          );
        });
      },
      "sub-block": (children: any, { keys }: any) => {
        return children.map((child: any, index: any) => {
          return (
            <View
              style={{ marginVertical: styleSheet.blockPaddings }}
              key={keys[index]}
            >
              <Text style={{ fontSize: styleSheet.subFontSize }}>{child}</Text>
            </View>
          );
        });
      },
      "header-block": (children: any, { keys }: any) => {
        return children.map((child: any, index: any) => {
          return (
            <View
              style={{ marginVertical: styleSheet.blockPaddings }}
              key={keys[index]}
            >
              <Text style={{ fontSize: styleSheet.headerFontSize }}>
                {child}
              </Text>
            </View>
          );
        });
      },
    },
  };

  return redraft(rawContentState, renderers, { blockFallback: "unstyled" });
}
