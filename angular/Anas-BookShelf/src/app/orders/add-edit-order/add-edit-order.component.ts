import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { PageMode } from 'src/app/enum/pageMode.enum';
import { BookService } from 'src/app/services/book.service';
import { CustomerService } from 'src/app/services/customer.service';
import { OrderService } from 'src/app/services/order.service';
import { Lookup } from 'src/models/LookUps/lookup.model';
import { Order } from 'src/models/orders/order.model';

@Component({
  selector: 'app-add-edit-order',
  templateUrl: './add-edit-order.component.html',
  styleUrls: ['./add-edit-order.component.css']
})
export class AddEditOrderComponent implements OnInit {

  orderForm!: FormGroup;
  booksLookup: Lookup[] = [];
  customersLookup: Lookup[] = [];

  orderId!: number;
  order!: Order;
  pageMode: PageMode = PageMode.add;

  pageModeEnum = PageMode;

  constructor(
    private fb: FormBuilder,
    private orderSvc: OrderService,
    private bookSvc: BookService,
    private customerSvc: CustomerService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.buildForm();

    this.loadCustomersLookup();
    this.loadBooksLookup();

    this.getOrderIdFromUrl();

    if (this.pageMode == PageMode.edit) {

      this.loadOrder();
    }

  }

  submitForm() {

    if (this.orderForm.valid) {

      if (this.pageMode == PageMode.add) {

        this.createOrder();
      }
      else {
        this.saveOrder();
      }
    }
  }

  //#region Private Functions

  private buildForm(): void {

    this.orderForm = this.fb.group({
      id: [0],
      note: [''],
      customerId: ['', Validators.required],
      bookIds: ['', Validators.required]
    });
  }

  private loadCustomersLookup() {

    this.customerSvc.getCustomersLookup().subscribe({
      next: (customersLookupFromApi: Lookup[]) => {
        this.customersLookup = customersLookupFromApi;
      }
    });
  }

  private loadBooksLookup() {

    this.bookSvc.getBooksLookup().subscribe({
      next: (booksLookupFromApi: Lookup[]) => {
        this.booksLookup = booksLookupFromApi;
      }
    });
  }

  private getOrderIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.orderId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
      this.pageMode = PageMode.edit;
    }
  }

  private loadOrder(): void {

    this.orderSvc.getOrderForEdit(this.orderId).subscribe({
      next: (orderFromApi: Order) => {

        this.order = orderFromApi;
        this.patchOrderForm();

      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  private patchOrderForm() {

    this.orderForm.patchValue({
      id: this.order.id,
      note: this.order.note,
      customerId: this.order.customerId,
      bookIds: this.order.bookIds
    });
  }

  private createOrder(): void {

    this.orderSvc.createOrder(this.orderForm.value).subscribe({
      next: (orderFromApi: Order) => {
        this.snackBar.open("Order has been created Successfully");
        this.router.navigate(['order']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  private saveOrder(): void {

    this.orderSvc.editOrder(this.orderForm.value).subscribe({
      next: (orderFromApi: Order) => {
        this.snackBar.open("Order has been updated Successfully");
        this.router.navigate(['order']);
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#endregion

}
