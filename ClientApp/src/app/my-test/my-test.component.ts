import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-test',
  templateUrl: './my-test.component.html',
  styleUrls: ['./my-test.component.css']
})
export class MyTestComponent implements OnInit {

  hero: any = {
    name: ''
  }

  constructor() { }

  ngOnInit(): void {

  }

}
