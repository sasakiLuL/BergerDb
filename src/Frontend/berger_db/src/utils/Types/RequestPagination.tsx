export default class RequestPagination {
  public page: number;
  public pageSize: number;
  public totalItemsCount: number;

  constructor(
    page: number = 1,
    pageSize: number = 5,
    totalItemsCount: number = 0
  ) {
    this.page = page;
    this.pageSize = pageSize;
    this.totalItemsCount = totalItemsCount;
  }

  toRequestString() {
    return `page=${this.page}&pageSize=${this.pageSize}`;
  }
}
