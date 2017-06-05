import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FullLayoutComponent, SimpleLayoutComponent } from './layouts/index';

@NgModule({
    declarations: [
        AppComponent,
        FullLayoutComponent,
        SimpleLayoutComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule
    ],
    exports: [RouterModule],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
