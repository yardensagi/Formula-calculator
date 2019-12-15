import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from '../Services/product.service';
import { Ingeredient, Product } from '../Models';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent implements OnInit {
  types = ['Powder', 'Liquid'];
  selectedOption: string;

    constructor(private toastr: ToastrService,
      private productService: ProductService) {
        this.getdata();
       }

        getdata() {
          this.product = this.productService.currentProduct;
          this.totalMass = this.product.mass;
          this.totalPrice = this.product.price;
          this.product.ingredients.forEach(ing => {
            this.newDynamic = {ingredientId: ing.ingredientId, name: ing.name, price: ing.price, 
              quantity: ing.quantity, type: ing.type,
            specificGravity: ing.specificGravity, massPercentage: ing.massPercentage, mass: ing.mass };
            this.dynamicArray.push(this.newDynamic);
            this.totalQuantity += ing.quantity;
          });
      }

    totalPrice = 0;
    totalQuantity = 0;
    totalMass = 0;
    dynamicArray: Array<Ingeredient> = [];
    newDynamic: any = {};
    product = new Product;

    ngOnInit(): void {

    }

    addRow(index) {
        this.newDynamic = {ingredientId: '', name: '', price: '', quantity: '', type: 'Powder',
        specificGravity: '', massPercentage: '', mass: '' };
        this.dynamicArray.push(this.newDynamic);
        this.toastr.success('New row added successfully', 'New Row');
        return true;
    }

    deleteRow(index) {
        if (this.dynamicArray.length === 1) {
          this.toastr.error('Can\'t delete the row when there is only one row', 'Warning');
            return false;
        } else {
            this.dynamicArray.splice(index, 1);
            this.makeChanges();
            this.toastr.warning('Row deleted successfully', 'Delete row');
            return true;
        }
    }

    getTotalPrice() {
      this.totalPrice = 0;
      this.dynamicArray.forEach(x => this.totalPrice  += (x.quantity * x.price) * 1000  );
      this.product.price = this.totalPrice;
    }

    getTotalQuantity() {
      this.totalQuantity = 0;
      this.dynamicArray.forEach(x => this.totalQuantity  += x.quantity  );
    }

    getMass() {
      this.totalMass = 0;
      this.dynamicArray.forEach(x => {
        if (x.type === 'Powder') {
          x.mass = x.quantity * 1000;
        }
        if (x.type === 'Liquid') {
          x.mass = x.quantity * 1000 * x.specificGravity;
        }
        x.massPercentage = (x.quantity * 100) / this.totalQuantity;
         this.totalMass += x.mass;
     });
      this.product.mass = this.totalMass;
    }

    makeChanges() {
      this.getTotalPrice();
      this.getTotalQuantity();
      this.getMass();
    }

    submit() {
      this.product.ingredients = this.dynamicArray;
      if(this.productService.update(this.product)){
        this.toastr.success('Product updated successfully');
        this.product = this.productService.currentProduct;
      }
    }

}
function delay(ms: number) {
  return new Promise( resolve => setTimeout(resolve, ms) );
}