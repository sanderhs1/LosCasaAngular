import { Component, OnInit } from '@angular/core';
import { IListing } from './listing';
import { Router } from '@angular/router';
import { ListingService } from './listings.service';


@Component({
  selector: 'app-listings-component',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.css']
})

export class ListingsComponent implements OnInit {
  viewTitle: string = 'Table';
  displayImage: boolean = true;
  listings: IListing[] = [];

 

  constructor(
    private _router: Router,
    private _listingService: ListingService) { }

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    console.log('In setter:', value);
    this.filteredlistings = this.performFilter(value);
  }

  deletelisting(listing: IListing): void {
    const confirmDelete = confirm(`Are you sure you want to delete "${listing.Name}"?`);
    if (confirmDelete) {
      this._listingService.deletelisting(listing.ListingId)
        .subscribe(
          (response) => {
            if (response.success) {
              console.log(response.message);
              this.filteredListings = this.filteredListings.filter(i => i !== listing);
            }
          },
          (error) => {
            console.error('Error deleting listing:', error);
          });
    }
  }

  getlistings(): void {
    this._listingService.getListings()
      .subscribe(data => {
        console.log('All', JSON.stringify(data));
        this.listings = data;
        this.filteredlistings = this.listings;
      }
      );
  }

  filteredlistings: IListing[] = this.listings;

  performFilter(filterBy: string): IListing[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.listings.filter((listing: IListing) =>
      listing.Name.toLocaleLowerCase().includes(filterBy));
  }

  toggleImage(): void {
    this.displayImage = !this.displayImage;
  }

  navigateTolistingform() {
    this._router.navigate(['/listingform']);
  }

  ngOnInit(): void {
    this.getlistings();
  }
  getListings(maxPrice?: number, minRooms?: number): void {
    this._listingService.getListings(maxPrice, minRooms)
      .subscribe(data => {
        console.log('All:', JSON.stringify(data));
        this.listings = data;
        this.filteredlistings = this.listings; // Assuming you want to apply the same filters to the filteredListings
      });
  }
}
