import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/shared/models/client';
import { ClientService } from 'src/app/shared/services/client/client.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {

  client: Client = new Client();
  addressMap;
  errors: any[] = [];

  constructor(
    private clientService: ClientService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private sanitizer: DomSanitizer) {

    this.client = this.route.snapshot.data.client;
    this.addressMap =
      this.sanitizer.bypassSecurityTrustResourceUrl(
        'https://www.google.com/maps/embed/v1/place?q=' +
        this.CompleteAddress() +
        '&key=AIzaSyAP0WKpL7uTRHGKWyakgQXbW6FUhrrA5pE');
  }
  ngOnInit(): void {
  }

  public CompleteAddress(): string {
    return this.client.address.publicPlace +
      ', ' +
      this.client.address.number +
      ' - ' +
      this.client.address.district +
      ', ' + this.client.address.city +
      ' - ' +
      this.client.address.state;
  }

  eventDelete(): void {
    this.clientService.deleteClient(this.client.id)
      .subscribe(
        client => { this.deleteSuccess(client); },
        error => { this.fail(error); }
      );
  }

  deleteSuccess(evento: any): void {

    const toast = this.toastr.success('Fornecedor excluido com Sucesso!', 'Good bye :D');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/fornecedores/listar-todos']);
      });
    }
  }

  fail(fail): void {
    this.errors = fail.error.errors;
    this.toastr.error('Houve um erro no processamento!', 'Ops! :(');
  }
}
