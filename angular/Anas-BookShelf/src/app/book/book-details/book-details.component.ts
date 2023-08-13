import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { BookService } from 'src/app/services/book.service';
import { BookDetailsDto } from 'src/models/books/bookDetails.model';


@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {

  // images: UploaderImage[] = [];
  //uploaderConfig = new ImageUploaderConfig(UploaderStyle.Normal, UploaderMode.Details, UploaderType.Multiple);

  constructor(
    private bookSvc: BookService,
    private activatedRoute: ActivatedRoute) { }

  bookId!: number;
  book?: BookDetailsDto;

  ngOnInit(): void {

    this.getBooksIdFromUrl();

    if (this.bookId) {

      this.loadBooks();
    }
  }

  //#region Private Functions

  private getBooksIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.bookId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    }
  }

  private loadBooks(): void {

    this.bookSvc.getBook(this.bookId).subscribe({
      next: (booksFromApi: BookDetailsDto) => {
        this.book = booksFromApi;

        // if (booksFromApi.images) {
        //   this.images = booksFromApi.images;
        // }

      },
      error: (err: HttpErrorResponse) => {
        console.error(err.error);
      }
    });
  }

  //#endregion


}
