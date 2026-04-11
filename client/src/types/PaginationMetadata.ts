export type PaginationMetadata = {
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  currentPage: number;
  totalPages: number;
};

export type PaginationResult<T> = {
  items: T[];
  metadata: PaginationMetadata;
};
