export default class RequestSorting {
  column: string;
  sorting: "asc" | "desc";

  constructor(column: string, sorting: "asc" | "desc") {
    this.column = column;
    this.sorting = sorting;
  }

  toRequestString() {
    return `&sortColumn=${this.column}&sortOrder=${this.sorting}`;
  }
}
