import { Category } from "../category/category.model";

export interface BookDto {
    // images: any;
    id: number;
    name: string;
    description :string;
    price: number;
    categoryIds: number[];
}