import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../Services/product.service';
import { Product } from '../Models';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  panelOpenState = false;
  search = '';
  products: Product[];
  constructor(private productService: ProductService,
    private router: Router,
    private toastr: ToastrService) {
    this.getall();
}

ngOnInit() {

}

   getall() {
   this.productService.getAll().subscribe(result => {
    this.products = result;
    console.log(result);
    this.productService.currentListProductSubject.next(result);
   }, error => console.error(error));
}
  delete(product: Product) {
    if (this.productService.delete(product)) {
            this.productService.currentListProductSubject.subscribe( pList => {
                this.products = pList;
                this.toastr.success('Product deleted from database');
              });
    }
  }

   async update(product: Product) {
     this.productService.currentProduct = product;
      this.productService.currentProductSubject.next(product)
     await delay(600);
      this.router.navigate(['update-product']);
    }
}

function delay(ms: number) {
  return new Promise( resolve => setTimeout(resolve, ms) );
}
