import { BookDto } from "../books/book.model";

export interface OrderDetails {
  id: number;
  totalPrice: number;
  note: string;
  orderDate: string;
  customerFullName: string;
  books: BookDto[];
}