import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  public isCollapsed: boolean;

  constructor() {
    this.isCollapsed = true;

  }

  ngOnInit(): void {
  }

}
