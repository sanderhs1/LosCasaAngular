import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRent } from './rent'; // Import your rent model

@Injectable({
  providedIn: 'root'
})


export class RentService {
  private baseUrl = 'api/rent';

  constructor(private _http: HttpClient) { }


  // Getting the RENTS

  getRents(): Observable<IRent[]> {
    return this._http.get<IRent[]>(this.baseUrl);
  }

  // CREATING a new RENTS

  createRent(newRent: IRent): Observable<any> {
    const createUrl = `${this.baseUrl}/create`;
    return this._http.post<any>(createUrl, newRent);
  }


  // Get Rent by ID

  getRentById(rentId: number): Observable<any> {
    const url = `${this.baseUrl}/${rentId}`;
    return this._http.get(url);
  }

  // Update the rents
  updateRent(rentId: number, updatedRent: IRent): Observable<any> {
    const url = `${this.baseUrl}/update/${rentId}`;
    updatedRent.rentId = rentId;
    return this._http.put<any>(url, updatedRent);
  }


  // DELETE a Rent

  deleteRent(rentId: number): Observable<any> {
    const url = `${this.baseUrl}/delete/${rentId}`;
    return this._http.delete(url);
  }
}
