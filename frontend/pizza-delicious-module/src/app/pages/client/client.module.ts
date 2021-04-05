import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TextMaskModule } from 'angular2-text-mask';
import { NgBrazil } from 'ng-brazil';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ClientAppComponent } from 'src/app/pages/client/client.app.component';
import { ClientResolve } from 'src/app/shared/services/client/client.resolve';
import { ClientService } from 'src/app/shared/services/client/client.service';
import { ClientRoutingModule } from './client-routing.module';
import { DeleteComponent } from './delete/delete.component';
import { DetailComponent } from './detail/detail.component';
import { EditComponent } from './edit/edit.component';
import { ListComponent } from './list/list.component';
import { NewComponent } from './new/new.component';



@NgModule({
  declarations: [
    ClientAppComponent,
    NewComponent,
    EditComponent,
    ListComponent,
    DeleteComponent,
    DetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ClientRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgBrazil,
    TextMaskModule,
    NgxSpinnerModule
  ],
  providers: [
    ClientService,
    ClientResolve
  ]
})
export class ClientModule { }
