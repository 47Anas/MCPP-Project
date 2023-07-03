import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEditCategoryComponent } from './categories/add-edit-category/add-edit-category.component';
import { CategoriesComponent } from './categories/categories.component';
import { CategoryDetailsComponent } from './categories/category-details/category-details.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path : 'home', component: HomeComponent },

  { path: 'categories', component: CategoriesComponent },
  { path: 'category/details/:id', component: CategoryDetailsComponent, },
  { path: 'category/add', component: AddEditCategoryComponent, },
  { path: 'category/edit/:id', component: AddEditCategoryComponent, },

  // { path: 'customer', component: CustomerComponent, },
  // { path: 'customer/details/:id', component: CustomerDetailsComponent, },
  // { path: 'customer/add', component: AddEditCustomerComponent, },
  // { path: 'customer/edit/:id', component: AddEditCustomerComponent, },

  // { path: 'book', component: BookComponent },
  // { path: 'book/details/:id', component: BookDetailsComponent, },
  // { path: 'book/add', component: AddEditBookComponent, },
  // { path: 'book/edit/:id', component: AddEditBookComponent, },

  // { path: 'cart', component: CartComponent, },


  { path: '', redirectTo: '/home', pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
