import { IPagination } from '../../models/pagination';
import { IProduct } from '../../models/product';

export const product: IProduct = {
    id: 'testid',
    name: 'testname',
    category: 'testcategory',
    summary: 'testsummary',
    description: 'testdesc',
    mainImageUrl: 'testimg',
    price: 5,
    ageRating: 5,
    count: 5,
    averageRating: 5,
};

export const data = [product, product, product, product];

export const pagination: IPagination = {
    currentPage: 2,
    itemsPerPage: 2,
    totalItems: 8,
    totalPages: 4,
};
