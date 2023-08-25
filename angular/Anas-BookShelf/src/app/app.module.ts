import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { AddEditCategoryComponent } from './categories/add-edit-category/add-edit-category.component';
import { CategoryDetailsComponent } from './categories/category-details/category-details.component';
import { DeleteCategoryComponent } from './categories/delete-category/delete-category.component';
import { CategoriesComponent } from './categories/categories.component';
import { CustomerComponent } from './customer/customer.component';
import { CustomerDetailsComponent } from './customer/customer-details/customer-details.component';
import { AddEditCustomerComponent } from './customer/add-edit-customer/add-edit-customer.component';
import { DeleteCustomerComponent } from './customer/delete-customer/delete-customer.component';
import { BookComponent } from './book/book.component';
import { DeleteBookComponent } from './book/delete-book/delete-book.component';
import { AddEditBookComponent } from './book/add-edit-book/add-edit-book.component';
import { BookDetailsComponent } from './book/book-details/book-details.component';
import { OrderComponent } from './orders/orders.component';
import { OrderDetailsComponent } from './orders/order-details/order-details.component';
import { AddEditOrderComponent } from './orders/add-edit-order/add-edit-order.component';
import { DeleteOrderComponent } from './orders/delete-order/delete-order.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CategoriesComponent,
    AddEditCategoryComponent,
    CategoryDetailsComponent,
    DeleteCategoryComponent,
    CustomerComponent,
    CustomerDetailsComponent,
    AddEditCustomerComponent,
    DeleteCustomerComponent,
    BookComponent,
    DeleteBookComponent,
    AddEditBookComponent,
    BookDetailsComponent,
    OrderComponent,
    OrderDetailsComponent,
    AddEditOrderComponent,
    DeleteOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
