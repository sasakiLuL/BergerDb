import { Box, Divider, Paper, Stack, useTheme } from "@mui/material";
import {
  DefaultDraftBlockRenderMap,
  DraftBlockRenderMap,
  Editor,
  EditorState,
  RichUtils,
} from "draft-js";
import { Map } from "immutable";
import MarkupButton from "./MarkupButton";

import FormatBoldIcon from "@mui/icons-material/FormatBold";
import FormatItalicIcon from "@mui/icons-material/FormatItalic";
import FormatUnderlinedIcon from "@mui/icons-material/FormatUnderlined";
import FormatPaintIcon from "@mui/icons-material/FormatPaint";
import BlockButton from "./BlockButton";
import HMobiledataIcon from "@mui/icons-material/HMobiledata";
import { usePdfStyle } from "../hooks/PdfStylesProvider";

export interface RichTextFieldProps {
  editorState: EditorState;
  setEditorState: (value: EditorState) => void;
}

export default function RichTextField({
  editorState,
  setEditorState,
}: RichTextFieldProps) {
  const onBoldClick = () => {
    setEditorState(RichUtils.toggleInlineStyle(editorState, "BOLD"));
  };
  const onItalicClick = () => {
    setEditorState(RichUtils.toggleInlineStyle(editorState, "ITALIC"));
  };
  const onUnderlineClick = () => {
    setEditorState(RichUtils.toggleInlineStyle(editorState, "UNDERLINE"));
  };
  const onHighlightClick = () => {
    setEditorState(RichUtils.toggleInlineStyle(editorState, "HIGHLIGHT"));
  };
  const onSubClick = () => {
    setEditorState(RichUtils.toggleBlockType(editorState, "sub-block"));
  };
  const onHeaderClick = () => {
    setEditorState(RichUtils.toggleBlockType(editorState, "header-block"));
  };

  const theme = useTheme();
  const pdfStyle = usePdfStyle();

  const blockRenderMap: DraftBlockRenderMap = Map({
    "sub-block": {
      element: "div",
      wrapper: (
        <Box
          sx={{ my: pdfStyle.blockPaddings, fontSize: pdfStyle.subFontSize }}
        />
      ),
    },
    "header-block": {
      element: "div",
      wrapper: (
        <Box
          sx={{
            my: pdfStyle.blockPaddings,
            fontSize: pdfStyle.headerFontSize,
          }}
        />
      ),
    },
    unstyled: {
      element: "div",
      wrapper: (
        <Box
          sx={{ my: pdfStyle.blockPaddings, fontSize: pdfStyle.mainFontSize }}
        />
      ),
    },
  });

  const styleMap = {
    HIGHLIGHT: {
      color: pdfStyle.subTextColor,
    },
  };

  const extendedBlockRenderMap: DraftBlockRenderMap =
    DefaultDraftBlockRenderMap.merge(blockRenderMap);

  return (
    <Paper variant="outlined">
      <Stack direction={"row"} sx={{ p: theme.spacing(1) }} spacing={1}>
        <MarkupButton
          active={editorState.getCurrentInlineStyle().has("BOLD")}
          onClick={onBoldClick}
          icon={<FormatBoldIcon />}
        />
        <MarkupButton
          active={editorState.getCurrentInlineStyle().has("ITALIC")}
          onClick={onItalicClick}
          icon={<FormatItalicIcon />}
        />
        <MarkupButton
          active={editorState.getCurrentInlineStyle().has("UNDERLINE")}
          onClick={onUnderlineClick}
          icon={<FormatUnderlinedIcon />}
        />
        <MarkupButton
          active={editorState.getCurrentInlineStyle().has("HIGHLIGHT")}
          onClick={onHighlightClick}
          icon={<FormatPaintIcon />}
        />
        <BlockButton
          active={
            editorState
              .getCurrentContent()
              .getBlockForKey(editorState.getSelection().getStartKey())
              .getType() === "header-block"
          }
          onClick={onHeaderClick}
          icon={<HMobiledataIcon />}
        >
          Heading
        </BlockButton>
        <BlockButton
          active={
            editorState
              .getCurrentContent()
              .getBlockForKey(editorState.getSelection().getStartKey())
              .getType() === "sub-block"
          }
          onClick={onSubClick}
          icon={<HMobiledataIcon />}
        >
          Sub
        </BlockButton>
      </Stack>
      <Divider></Divider>
      <Box sx={{ p: theme.spacing(2) }}>
        <Editor
          editorState={editorState}
          customStyleMap={styleMap}
          blockRenderMap={extendedBlockRenderMap}
          onChange={(editor) => setEditorState(editor)}
        />
      </Box>
    </Paper>
  );
}
