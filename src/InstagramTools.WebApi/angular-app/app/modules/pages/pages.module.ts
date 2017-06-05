import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { p404Component } from './404.component';

import { PagesRoutingModule } from './pages-routing.module';

@NgModule({
  declarations: [
    p404Component
  ],
  imports: [
    HttpModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PagesRoutingModule
  ]
})

export class PagesModule { }
