import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient,
  ) { }

  baseApiUrl: string = 'https://localhost:7279';

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseApiUrl + '/api/employees');
  }

  getEmployeeById(id: string): Observable<Employee> {
    return this.http.get<Employee>(this.baseApiUrl + '/api/employees/GetEmployee/' + id);
  }

  createEmployee(addEmployee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.baseApiUrl + '/api/employees/create', addEmployee);
  }

  editEmployee(id: number, updateEmployee: Employee): Observable<Employee> {
    return this.http.put<Employee>(this.baseApiUrl + '/api/employees/edit/' + id, updateEmployee);
  }

  deleteEmployee(id: number): Observable<Employee> {
    return this.http.post<Employee>(this.baseApiUrl + '/api/employees/delete/', id);
  }
}
