import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { Employee } from '../../shared/models/employee';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {

  employee: Employee = new Employee();
  submitClicked: boolean = false;

  constructor(private httpService: HttpService, private router: Router) { }

  createEmployee() {
    this.httpService.createEmployee(this.employee).subscribe(() => {
      this.router.navigate(['/employees']);
    });
  }


}
