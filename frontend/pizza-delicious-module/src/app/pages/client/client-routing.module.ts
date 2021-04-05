import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientAppComponent } from 'src/app/pages/client/client.app.component';
import { DeleteComponent } from 'src/app/pages/client/delete/delete.component';
import { DetailComponent } from 'src/app/pages/client/detail/detail.component';
import { EditComponent } from 'src/app/pages/client/edit/edit.component';
import { ListComponent } from 'src/app/pages/client/list/list.component';
import { NewComponent } from 'src/app/pages/client/new/new.component';
import { ClientResolve } from 'src/app/shared/services/client/client.resolve';

const routes: Routes = [
  {
    path: '', component: ClientAppComponent,
    children: [
      { path: 'list', component: ListComponent },
      { path: 'new', component: NewComponent },
      {
        path: 'edit/:id', component: EditComponent,
        resolve: {
          client: ClientResolve
        }
      },
      {
        path: 'detail/:id', component: DetailComponent,
        resolve: {
          client: ClientResolve
        }
      },
      {
        path: 'delete/:id', component: DeleteComponent,
        resolve: {
          client: ClientResolve
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
