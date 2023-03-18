import { Component, OnInit } from '@angular/core';

import { Employee } from '../../shared/models/employee';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(private httpService: HttpService) { }

  employees: Employee[] = [];

  ngOnInit(): void {
    this.httpService.getAllEmployees().subscribe((employees) => {
      this.employees = employees;
    });
  }

}
