import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Employee } from '../../shared/models/employee';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  employee: Employee = new Employee();

  constructor(private route: ActivatedRoute, private httpService: HttpService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get("id");

      if (id) {
        this.httpService.getEmployeeById(id).subscribe((employee) => {
          this.employee = employee;
        });
      }
    });
  }

}
