import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CreateProductComponent } from './create-product/create-product.component';
import { AppRoutingModule } from './app-routing.module';


import { AppMateiralModule } from './material.module';
import { UpdateProductComponent } from './update-product/update-product.component';
import { FilterPipe } from './filter.pipe';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CreateProductComponent,
    UpdateProductComponent,
    FilterPipe,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    AppMateiralModule,
    ToastrModule.forRoot()
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
