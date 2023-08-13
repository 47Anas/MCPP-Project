import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { } from "src/models/books/book.model"
import { BookDetailsDto } from 'src/models/books/bookDetails.model';
import { BookService } from '../services/book.service';
import { DeleteBookComponent } from './delete-book/delete-book.component';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  bookDS: BookDetailsDto[] = [];
  bookColumns: string[] = ['id', 'name', 'actions'];

  constructor(
    private bookSvc: BookService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.loadBooks();
  }

  openDeleteDialog(book: BookDetailsDto) {

    const dialogRef = this.dialog.open(DeleteBookComponent, {
      data: book
    });

    dialogRef.afterClosed().subscribe({
      next: (result: boolean) => {

        if (result) {
          this.bookSvc.deleteBook(book.id).subscribe({
            next: () => {
              this.loadBooks();
              this.snackBar.open(`${book.name} has been deleted successfully`);
            },
            error: (err: HttpErrorResponse) => {
              this.snackBar.open(`${book.name} cannot be deleted. ${err.message}`);
            }
          });
        }

      }
    });
  }

  addToCart(book: BookDetailsDto): void {

    this.bookSvc.addToCart(book.id).subscribe({
      next: () => {
        this.snackBar.open(`${book.name} has been added to cart successfully`);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#region Private Functions

  private loadBooks(): void {

    this.bookSvc.getBooks().subscribe({
      next: (booksFromApi: BookDetailsDto[]) => {
        this.bookDS = booksFromApi;
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#endregion

}
