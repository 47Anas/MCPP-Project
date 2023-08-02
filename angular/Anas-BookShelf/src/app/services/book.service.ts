import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Lookup } from "src/models/LookUps/lookup.model";
import { Book } from "src/models/books/book.model"

@Injectable({
    providedIn: 'root'
  })

export class BookService {

    httpUrl :string = `https://localhost:7283/api/Books`
  
    constructor(private http: HttpClient) { }
  
    getBooks(): Observable<Book[]> {
  
      return this.http.get<Book[]>(`${this.httpUrl}/GetBooks`);
    }
  
    getBook(id: number): Observable<Book> {
  
      return this.http.get<Book>(`${this.httpUrl}/GetBook/${id}`);
    }
  
    createBook(book: Book): Observable<Book> {
  
      return this.http.post<Book>(`${this.httpUrl}/CreateBook`, book);
    }
  
    editBook(book: Book): Observable<any> {
  
      return this.http.put<Book>(`${this.httpUrl}/EditBook/${book.id}`, book);
    }
  
    deleteBook(bookId: number): Observable<any> {
  
      return this.http.delete<Book>(`${this.httpUrl}/DeleteBook/${bookId}`);
    }
  
    getBooksLookup(): Observable<Lookup[]> {
  
      return this.http.get<Lookup[]>(`${this.httpUrl}/GetBooksLookup`);
    }
  
    addToCart(bookId: number): Observable<any> {
  
      return this.http.post<any>(`${this.httpUrl}/AddBookToCart?bookId=${bookId}`, bookId);
    }
  }