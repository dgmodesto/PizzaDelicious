import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Address, CepSearch } from 'src/app/shared/models/address';
import { Client } from 'src/app/shared/models/client';
import { BaseService } from 'src/app/shared/services/base/base.service';


@Injectable({
  providedIn: 'root'
})
export class ClientService extends BaseService {

  client: Client = new Client();

  constructor(private http: HttpClient) { super(); }

  public getAll(): Observable<Client[]> {
    return this.http
      .get<Client[]>(this.UrlServiceV1 + 'clients');
    // .pipe(catchError(super.serviceError));
  }

  public getById(id: string): Observable<Client> {
    return this.http
      .get<Client>(this.UrlServiceV1 + 'clients/' + id);
    // .pipe(catchError(super.serviceError));
  }

  public addClient(client: Client): Observable<Client> {
    return this.http
      .post<Client>(this.UrlServiceV1 + 'clients', client);
  }

  public updateClient(client: Client): Observable<Client> {
    return this.http
      .put<Client>(this.UrlServiceV1 + 'clients', client);
  }



  public deleteClient(id: string): Observable<Client> {
    return this.http
      .delete<Client>(this.UrlServiceV1 + 'clients/' + id);
    // .pipe(catchError(super.serviceError));
  }


  updateAddress(address: Address): Observable<Address> {
    return this.http
      .put<Address>(this.UrlServiceV1 + 'client/address/' + address.id, address);

  }


  searchCep(cep: string): Observable<CepSearch> {
    return this.http
      .get<CepSearch>(`https://viacep.com.br/ws/${cep}/json/`);
  }

}
