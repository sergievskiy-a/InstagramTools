import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FullLayoutComponent, SimpleLayoutComponent } from './layouts/index';


export const routes: Routes = [
  {
    path: 'pages',
    component: SimpleLayoutComponent,
    data: {
      title: 'Pages'
    },
    children: [
      {
        path: '',
        loadChildren: './modules/pages/pages.module#PagesModule',
      }
    ]
  },
  {
    path: 'admin',
    // Loading by relative path didn't seem to work here
    //loadChildren: './modules/admin/admin.module#AdminModule'
    loadChildren: './modules/admin/admin.module#AdminModule'
  },
  {
    //Page not found
    path: '', redirectTo: '/pages/404', pathMatch: 'prefix'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }