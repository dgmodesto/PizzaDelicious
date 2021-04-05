import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { MenuLoginComponent } from './menu-login/menu-login.component';
import { MenuComponent } from './menu/menu.component';

const components = [
  HomeComponent,
  MenuComponent,
  MenuLoginComponent,
  FooterComponent
];

@NgModule({
  declarations: [...components],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule
  ],
  exports: [...components]
})
export class NavigationModule { }
