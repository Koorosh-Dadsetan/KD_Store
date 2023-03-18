import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../../shared/models/employee';
import { HttpService } from '../../shared/services/http.service';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeleteComponent implements OnInit {

  constructor(private route: ActivatedRoute, private httpService: HttpService, private router: Router) { }

  employee: Employee = new Employee();

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get("id");

      if (id) {
        this.httpService.getEmployeeById(id).subscribe((employee) => {
          this.employee = employee;
        });
      }
    })
  }

  deleteEmployeeMethod() {
    this.httpService.deleteEmployee(this.employee.id!).subscribe(() => {
      this.router.navigate(['/employees']);
    });
  }

}
