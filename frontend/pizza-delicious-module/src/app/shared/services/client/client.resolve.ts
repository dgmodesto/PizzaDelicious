import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Address } from 'src/app/shared/models/address';
import { Client } from 'src/app/shared/models/client';
import { ClientService } from 'src/app/shared/services/client/client.service';

@Injectable()
export class ClientResolve implements Resolve<Client> {

  constructor(private clientService: ClientService) { }

  resolve(route: ActivatedRouteSnapshot): Observable<Client> {

    const client = new Client();
    client.id = '1';
    client.name = 'Douglas Gomes Modesto';
    client.document = '341.898.718-42';
    client.enable = true;
    client.address = new Address();
    client.address.cep = '03015000';
    client.address.city = 'São Paulo';
    client.address.state = 'SP';
    client.address.number = '50';
    client.address.district = 'Brás';
    client.address.clientId = '1';
    client.address.publicPlace = 'Avenida Celso Garcia';


    const myObservable = of(client);

    return myObservable;

  }
}
