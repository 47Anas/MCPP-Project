import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerList } from 'src/models/customer/customeList.model';
import { Customer } from 'src/models/customer/customer.model';
import { CustomerDetails } from 'src/models/customer/customerDetails.model';
import { Lookup } from 'src/models/LookUps/lookup.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  
  httpUrl :string = `https://localhost:7283/api/Customers`


  constructor(private http: HttpClient) { }

  getCustomers(): Observable<CustomerList[]> {

    return this.http.get<CustomerList[]>(`${this.httpUrl}/GetCustomers`);
  }

  getCustomer(customerId: number): Observable<CustomerDetails> {

    return this.http.get<CustomerDetails>(`${this.httpUrl}/GetCustomer/${customerId}`);
  }

  createCustomer(customer: Customer): Observable<any> {

    return this.http.post<any>(`${this.httpUrl}/CreateCustomer`, customer);
  }

  getCustomerForEdit(customerId: number): Observable<Customer> {

    return this.http.get<Customer>(`${this.httpUrl}/GetCustomerForEdit/${customerId}`);
  }

  editCustomer(customer: Customer): Observable<any> {

    return this.http.put<any>(`${this.httpUrl}/EditCustomer/${customer.id}`, customer);
  }

  deleteCustomer(customerId: number): Observable<any> {

    return this.http.delete<any>(`${this.httpUrl}/DeleteCustomer/${customerId}`);
  }

  getCustomersLookup(): Observable<Lookup[]> {

    return this.http.get<Lookup[]>(`${this.httpUrl}/GetCustomersLookup`);
  }
}
