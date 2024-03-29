import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { PageMode } from 'src/app/enum/pageMode.enum';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/models/category/category.model';

@Component({
  selector: 'app-add-edit-category',
  templateUrl: './add-edit-category.component.html',
  styleUrls: ['./add-edit-category.component.css']
})
export class AddEditCategoryComponent implements OnInit {

  categoryForm!: FormGroup;

  categoryId!: number;
  category!: Category;
  pageMode: PageMode = PageMode.add;

  pageModeEnum = PageMode;

  constructor(
    private fb: FormBuilder,
    private catSvc: CategoryService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.buildForm();

    this.getCategoryIdFromUrl();

    if (this.pageMode == PageMode.edit) {

      this.loadCategory();
    }

  }

  submitForm() {

    if (this.categoryForm.valid) {

      if (this.pageMode == PageMode.add) {

        this.createCategory();
      }
      else {
        this.editCategory();
      }
    }
  }

  //#region Private Functions

  private buildForm(): void {

    this.categoryForm = this.fb.group({
      id: [0],
      name: ['', Validators.required]
    });
  }

  private getCategoryIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.categoryId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
      this.pageMode = PageMode.edit;
    }
  }

  private loadCategory(): void {

    this.catSvc.getCategory(this.categoryId).subscribe({
      next: (categoryFromApi: Category) => {

        this.category = categoryFromApi;
        this.patchCategoryForm();

      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }
  private patchCategoryForm() {

    this.categoryForm.patchValue({
      id: this.category.id,
      name: this.category.name
    });
  }

  private createCategory(): void {

    this.catSvc.createCategory(this.categoryForm.value).subscribe({
      next: (categoryFromApi: Category) => {
        this.snackBar.open("Category has been created Successfully");
        this.router.navigate(['categories']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  private editCategory(): void {

    this.catSvc.editCategory(this.categoryForm.value).subscribe({
      next: (categoryFromApi: Category) => {
        this.snackBar.open("Category has been updated Successfully");
        this.router.navigate(['categories']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#endregion

}
