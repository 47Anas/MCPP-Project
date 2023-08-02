import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { PageMode } from 'src/app/enum/pageMode.enum';
import { Lookup } from 'src/models/LookUps/lookup.model';
import { Book } from 'src/models/books/book.model';
import { CategoryService } from 'src/app/services/category.service';
import { BookService } from 'src/app/services/book.service';


@Component({
  selector: 'app-add-edit-book',
  templateUrl: './add-edit-book.component.html',
  styleUrls: ['./add-edit-book.component.css']
})
export class AddEditBookComponent implements OnInit {

  // images: UploaderImage[] = [];
  // uploaderConfig = new ImageUploaderConfig(UploaderStyle.Normal, UploaderMode.AddEdit, UploaderType.Multiple);

  bookForm!: FormGroup;
  categoryLookup: Lookup[] = [];

  bookId!: number;
  book!: Book;
  pageMode: PageMode = PageMode.add;

  pageModeEnum = PageMode;
  images: any;

  constructor(
    private fb: FormBuilder,
    private bookSvc: BookService,
    private categorySvc: CategoryService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.buildForm();

    this.loadCategories();

    this.getBookIdFromUrl();

    if (this.pageMode == PageMode.edit) {

      this.loadBook();
    }

  }

  create(): void {

    if (this.bookForm.valid) {
      this.createBook();
      this.router.navigate(['book']);
    }
  }

  save(): void {

    if (this.bookForm.valid) {
      this.saveBook();
      this.router.navigate(['book']);
    }
  }

  // uploadFinished(uploaderImages: UploaderImage[]) {

  //   this.bookForm.patchValue({
  //     images: uploaderImages
  //   });
  // }

  //#region Private Functions

  private buildForm(): void {

    this.bookForm = this.fb.group({
      id: [0],
      name: ['', Validators.required],
      price: ['', Validators.required],
      categoryIds: [[], Validators.required],
      description: ['']
      // images: [[]]
    });
  }

  private loadCategories() : void {

    this.categorySvc.getCategoriesLookup().subscribe({
      next: (categoryDtoFromApi: Lookup[]) => {
        this.categoryLookup = categoryDtoFromApi;
      }
    });
  }

  private getBookIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.bookId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
      this.pageMode = PageMode.edit;
    }
  }

  private loadBook(): void {

    this.bookSvc.getBook(this.bookId).subscribe({
      next: (bookFromApi: Book) => {

        this.book = bookFromApi;
        this.patchBookForm();

        // if (bookFromApi.images) {
        //   this.images = bookFromApi.images;
        // }

      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  private patchBookForm() {

    this.bookForm.patchValue({
      id: this.book.id,
      name: this.book.name,
      price: this.book.price,
      categoryIds: this.book.categoryIds,
      description: this.book.description
    });
  }

  private createBook(): void {

    this.bookSvc.createBook(this.bookForm.value).subscribe({
      next: (bookFromApi: Book) => {
        this.snackBar.open("Book has been created Successfully");
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  private saveBook(): void {

    this.bookSvc.editBook(this.bookForm.value).subscribe({
      next: (bookFromApi: Book) => {
        this.snackBar.open("Book has been updated Successfully");
        this.router.navigate(['book']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#endregion

}
