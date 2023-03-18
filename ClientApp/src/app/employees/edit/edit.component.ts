import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Employee } from '../../shared/models/employee';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  employee: Employee = new Employee();
  submitClicked: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private httpService: HttpService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get("id");

      if (id) {
        this.httpService.getEmployeeById(id).subscribe((employees) => {
          this.employee = employees;
        });
      }
    })
  }

  editEmployee() {
    this.httpService.editEmployee(this.employee.id!, this.employee).subscribe(() => {
      this.router.navigate(['/employees']);
    });
  }

}
