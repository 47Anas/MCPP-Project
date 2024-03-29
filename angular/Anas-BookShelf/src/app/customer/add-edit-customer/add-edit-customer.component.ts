import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDatepicker } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageUploaderConfig } from 'src/app/directives/image-uploader/image-uploader.config';
import { UploaderStyle, UploaderMode, UploaderType } from 'src/app/directives/image-uploader/uploader.enum';
import { UploaderImage } from 'src/app/directives/image-uploader/UploaderImage.data';
import { PageMode } from 'src/app/enum/pageMode.enum';
import { CustomerService } from 'src/app/services/customer.service';
import { Customer } from 'src/models/customer/customer.model';

@Component({
  selector: 'app-add-edit-customer',
  templateUrl: './add-edit-customer.component.html',
  styleUrls: ['./add-edit-customer.component.css']
})
export class AddEditCustomerComponent implements OnInit {

  images: UploaderImage[] = [];
  uploaderConfig = new ImageUploaderConfig(UploaderStyle.Profile, UploaderMode.AddEdit, UploaderType.Single);


  customerForm!: FormGroup;

  customerId!: number;
  customer!: Customer;
  pageMode: PageMode = PageMode.add;

  pageModeEnum = PageMode;

  constructor(
    private fb: FormBuilder,
    private customerSvc: CustomerService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.buildForm();

    this.getCustomerIdFromUrl();

    if (this.pageMode == PageMode.edit) {

      this.loadCustomer();
    }
  }

  submitForm() {

    if (this.customerForm.valid) {

      if (this.pageMode == PageMode.add) {

        this.createCustomer();
      }
      else {
        this.editCustomer();
      }
    }
  }

  uploadFinished(uploaderImages: UploaderImage[]) {

    this.customerForm.patchValue({
      images: uploaderImages
    });
  }



  //#region Private Functions

  private buildForm(): void {

    this.customerForm = this.fb.group({
      id: [0],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      images: [[]]
    });
  }

  private getCustomerIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.customerId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
      this.pageMode = PageMode.edit;
    }
  }

  private loadCustomer(): void {

    this.customerSvc.getCustomerForEdit(this.customerId).subscribe({
      next: (customerFromApi: Customer) => {
        this.customer = customerFromApi;
        this.patchCustomerForm();

        if (customerFromApi.images) {
          this.images = customerFromApi.images;
        }
      },
      error: (err: HttpErrorResponse) => {
        console.error(err.error);
      }
    });
  }
  private patchCustomerForm() {

    this.customerForm.patchValue({
      id: this.customer.id,
      firstName: this.customer.firstName,
      lastName: this.customer.lastName,
      phoneNumber: this.customer.phoneNumber,
      dateOfBirth: this.customer.dateOfBirth,
      images: this.customer.images
    });
  }

  private createCustomer(): void {

    this.customerSvc.createCustomer(this.customerForm.value).subscribe({
      next: () => {
        this.snackBar.open("Customer has been created Successfully");
        this.router.navigate(['customer']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  private editCustomer(): void {

    this.customerSvc.editCustomer(this.customerForm.value).subscribe({
      next: () => {
        this.snackBar.open("Customer has been updated Successfully");
        this.router.navigate(['customer']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#endregion


}
