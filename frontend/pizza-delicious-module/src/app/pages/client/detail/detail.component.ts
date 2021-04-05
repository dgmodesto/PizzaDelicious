import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Client } from 'src/app/shared/models/client';

@Component({
  selector: 'detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  client: Client = new Client();
  addressMap;

  constructor(
    private route: ActivatedRoute,
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
}
