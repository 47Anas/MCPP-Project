import { UploaderImage } from "src/app/directives/image-uploader/UploaderImage.data";
import { Category } from "../category/category.model";

export interface BookDto {
    // images: any;
    id: number;
    name: string;
    description :string;
    price: number;
    categoryIds: number[];
    images: UploaderImage[];
}