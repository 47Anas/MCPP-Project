import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { BookDto } from 'src/models/books/book.model';
import { OrderDetails } from 'src/models/orders/orderDetails.model';

@Component({
  selector: 'app-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {

  orderId!: number;
  order?: OrderDetails;

  bookDS: BookDto[] = [];
  bookColumns: string[] = ['name', 'categoryName'];

  constructor(
    private orderSvc: OrderService,
    private activatedRoute: ActivatedRoute,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.getOrdersIdFromUrl();

    if (this.orderId) {

      this.loadOrders();
    }
  }

  //#region Private Functions

  private getOrdersIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.orderId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    }
  }

  private loadOrders(): void {

    this.orderSvc.getOrder(this.orderId).subscribe({
      next: (ordersFromApi: OrderDetails) => {
        this.order = ordersFromApi;
        this.bookDS = this.order.books;
      },
      error: (err: HttpErrorResponse) => {
        this.snackBar.open(err.message);
      }
    });
  }

  //#endregion

}
