export interface IPagination<I> {
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  items: Array<I>;
  pageNumber: number;
  totalCount: number;
  totalPages: number;
}
