import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Lookup } from "src/models/LookUps/lookup.model";
import { BookDto } from "src/models/books/book.model"
import { BookDetailsDto } from "src/models/books/bookDetails.model";

@Injectable({
    providedIn: 'root'
  })

export class BookService {

    httpUrl :string = `https://localhost:7283/api/Books`
  
    constructor(private http: HttpClient) { }
  
    getBooks(): Observable<BookDetailsDto[]> {
  
      return this.http.get<BookDetailsDto[]>(`${this.httpUrl}/GetBooks`);
    }
  
    getBook(id: number): Observable<BookDetailsDto> {
  
      return this.http.get<BookDetailsDto>(`${this.httpUrl}/GetBook/${id}`);
    }

    getBookForEdit(id: number): Observable<BookDto> {
      return this.http.get<BookDto>(`${this.httpUrl}/GetBookForEdit/${id}`);
    }
  
    createBook(book: BookDto): Observable<BookDto> {
  
      return this.http.post<BookDto>(`${this.httpUrl}/CreateBook`, book);
    }
  
    editBook(book: BookDto): Observable<any> {
  
      return this.http.put<BookDto>(`${this.httpUrl}/EditBook/${book.id}`, book);
    }
  
    deleteBook(bookId: number): Observable<any> {
  
      return this.http.delete<BookDto>(`${this.httpUrl}/DeleteBook/${bookId}`);
    }
  
    getBooksLookup(): Observable<Lookup[]> {
  
      return this.http.get<Lookup[]>(`${this.httpUrl}/GetBooksLookup`);
    }
  
    addToCart(bookId: number): Observable<any> {
  
      return this.http.post<any>(`${this.httpUrl}/AddBookToCart?bookId=${bookId}`, bookId);
    }
  }