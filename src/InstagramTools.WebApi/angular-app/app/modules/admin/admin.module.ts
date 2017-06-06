import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { MaterialModule } from '@angular/material';
import { AdminRoutingModule } from './admin-routing.module';
import { MainPageComponent } from './main-page/main-page.component';
import { LoginPageComponent, SettingsDialog } from './login-page/login-page.component';

@NgModule({
    imports: [
        CommonModule,
        AdminRoutingModule,
        MaterialModule
    ],
    declarations: [
        MainPageComponent, LoginPageComponent, SettingsDialog
    ],
    entryComponents: [
        MainPageComponent, LoginPageComponent, SettingsDialog
    ]
})
export class AdminModule { }
