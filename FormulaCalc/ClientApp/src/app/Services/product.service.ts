import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../Models';
import { Subject } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  currentProductSubject = new Subject<Product>();
  currentProduct: Product;

  currentListProductSubject = new Subject<Product[]>();
  currentListProduct: Product[];


  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string) { }

  getAll() {
    return this.http.post<Product[]>(this.baseUrl + 'product/getAll', null); }

    create(product: Product) {
     return this.http.post<Product>(this.baseUrl + 'product/create', product ).subscribe(result => {
        this.currentProduct = result;
        this.currentProductSubject.next(result);
        return true;
       }, error => console.error(error));
    }

    delete(product: Product) {
      let id = {name: product.productID};
     return this.http.post<Product[]>(this.baseUrl + 'product/delete',id ).subscribe(result => {
       if ( result) {
          this.currentListProductSubject.next(result);
          return true;
      }
      }, error => console.error(error));  }

      update(product: Product) {
        return this.http.post<Product>(this.baseUrl + 'product/update', product).subscribe(result => {
         this.currentProduct = result;
         this.currentProductSubject.next(result);
         return result;
        }, error => console.error(error));  }


}
