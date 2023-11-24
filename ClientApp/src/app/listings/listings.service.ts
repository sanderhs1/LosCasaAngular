import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IListing } from './listing'; // Import your item model

@Injectable({
  providedIn: 'root'
})
export class ListingService {

  private baseUrl = 'http://localhost:5239/api/listing';

  constructor(private _http: HttpClient) { }

  //getListings(): Observable<IListing[]> {
  //  return this._http.get<IListing[]>(this.baseUrl);
  //}

  createListing(newListing: IListing): Observable<any> {
    const createUrl = `${this.baseUrl}/create`;
    return this._http.post<any>(createUrl, newListing);
  }

  getListingById(listingId: number): Observable<any> {
    const url = `${this.baseUrl}/${listingId}`;
    return this._http.get(url);
  }

  updateListing(listingId: number, newListing: any): Observable<any> {
    const url = `${this.baseUrl}/update/${listingId}`;
    newListing.listingId = listingId;
    return this._http.put<any>(url, newListing);
  }

  deleteListing(listingId: number): Observable<any> {
    const url = `${this.baseUrl}/delete/${listingId}`;
    return this._http.delete(url);
  }

  getListings(maxPrice?: number, minRooms?: number): Observable<any> {
    let params = new HttpParams();
    if (maxPrice != null) {
      params = params.set('maxPrice', maxPrice.toString());
    }
    if (minRooms != null) {
      params = params.set('minRooms', minRooms.toString());
    }
    return this._http.get<any>(this.baseUrl, { params: params });
  }

}
