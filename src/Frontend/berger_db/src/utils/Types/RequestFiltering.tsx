export default class RequestFiltering {
  public readonly field: string;

  public filtering: string;

  constructor(field: string) {
    this.field = field;
    this.filtering = "";
  }

  toRequestString() {
    return this.filtering === "" ? "" : `&${this.field}=${this.filtering}`;
  }
}
