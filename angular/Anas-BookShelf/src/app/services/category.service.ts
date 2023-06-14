import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Category } from "src/models/category/category.model";


@Injectable({
    providedIn: 'root'
  })

export class CategoryService {

    httpUrl :string = `https://localhost:7283/api/Categories/`

    constructor(private http: HttpClient){}


    getCategories(): Observable<Category[]>{

        return this.http.get<Category[]>(`${this.httpUrl}/GetCategories`)
    }
    
    
      getCategory(id: number): Observable<Category> {
    
        return this.http.get<Category>(`${this.httpUrl}/GetCategory/${id}`);
      }
    
      createCategory(category: Category): Observable<Category> {
    
        return this.http.post<Category>(`${this.httpUrl}/CreateCategory`, category);
      }
    
      editCategory(category: Category): Observable<any> {
    
        return this.http.put<Category>(`${this.httpUrl}/EditCategory/${category.id}`, category);
      }
}