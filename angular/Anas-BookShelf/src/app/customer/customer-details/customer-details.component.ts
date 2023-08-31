import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UploaderImage } from 'src/app/directives/image-uploader/UploaderImage.data';
import { UploaderStyle, UploaderMode, UploaderType } from 'src/app/directives/image-uploader/uploader.enum';
import { CustomerService } from 'src/app/services/customer.service';
import { CustomerDetails } from 'src/models/customer/customerDetails.model';
import { OrderList } from 'src/models/orders/orderList.model';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.css']
})
export class CustomerDetailsComponent implements OnInit {

  customerId!: number;
  customer?: CustomerDetails;

  orderDS: OrderList[] = [];
  orderColumns: string[] = ['id', 'totalPrice', 'note', 'orderDate', 'actions'];

  images: UploaderImage[] = [];
  uploaderConfig = new ImageUploaderConfig(UploaderStyle.Profile, UploaderMode.Details, UploaderType.Single);


  constructor(private customerSvc: CustomerService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {

    this.getCustomerIdFromUrl();

    if (this.customerId) {

      this.loadCustomer();
    }
  }

  //#region Private Functions

  private getCustomerIdFromUrl(): void {

    if (this.activatedRoute.snapshot.paramMap.get('id')) {

      this.customerId = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    }
  }

  private loadCustomer(): void {

    this.customerSvc.getCustomer(this.customerId).subscribe({
      next: (customerFromApi: CustomerDetails) => {
        this.customer = customerFromApi;
        this.orderDS = this.customer.orders;
        this.images = this.customer.images;
      },
      error: (err: HttpErrorResponse) => {
        console.error(err.error);
      }
    });
  }

  //#endregion

}
