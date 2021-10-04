export interface IPagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
    private readonly _data: T;
    private readonly _pagination: IPagination;

    public get data() {
        return this._data;
    }

    public get pagination() {
        return this._pagination;
    }

    constructor(data: T, pagination: IPagination) {
        this._data = data;
        this._pagination = pagination;
    }
}

export class PagingParams {
    private readonly _pageNumber;
    private readonly _pageSize;

    public get pageNumber() {
        return this._pageNumber;
    }

    public get pageSize() {
        return this._pageSize;
    }

    constructor(pageNumber = 1, pageSize = 6) {
        this._pageNumber = pageNumber;
        this._pageSize = pageSize;
    }
}
