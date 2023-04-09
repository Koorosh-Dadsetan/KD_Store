import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { Employee } from '../../shared/models/employee';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  constructor(
    private httpService: HttpService,
    private titleService: Title
  ) {
    this.titleService.setTitle("KD_Store - Employees");
  }

  employees: Employee[] = [];
  searchText: string = '';

  ngOnInit(): void {
    this.httpService.getAllEmployees().subscribe((result) => {
      this.employees = result;
    });
  }

  search() {
    if (this.searchText.length > 0) {
      this.httpService.searchEmployees(this.searchText).subscribe((result) => {
        this.employees = result;
      });
    }
    else {
      location.reload();
    }
  }
}
