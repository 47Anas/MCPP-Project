export interface Book {
    // images: any;
    id: number;
    name: string;
    description :string;
    price: number;
    categoryIds: number[];
    categoryName: string;
}