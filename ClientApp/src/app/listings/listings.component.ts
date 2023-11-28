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
    private _listingService: ListingService
  ) { }

  private _nameFilter: string = '';
  private _maxPrice: number | null = null;

  get nameFilter(): string {
    return this._nameFilter;
  }

  set nameFilter(value: string) {
    this._nameFilter = value;
    console.log('Name Filter:', value);
    this.filteredListings = this.performFilter();
  }

  get maxPrice(): number | null {
    return this._maxPrice;
  }

  set maxPrice(value: number | null) {
    this._maxPrice = value;
    console.log('Max Price:', value);
    this.filteredListings = this.performFilter();
  }

  deleteListing(listing: IListing): void {
    const confirmDelete = confirm(`Are you sure you want to delete "${listing.Name}"?`);
    if (confirmDelete) {
      this._listingService.deleteListing(listing.ListingId).subscribe(
        (response) => {
          if (response.success) {
            console.log(response.message);
            this.filteredListings = this.filteredListings.filter((i) => i !== listing);
          }
        },
        (error) => {
          console.error('Error deleting listing:', error);
        }
      );
    }
  }

  getListings(): void {
    this._listingService.getListings().subscribe((data) => {
      console.log('All', JSON.stringify(data));
      this.listings = data;
      this.filteredListings = this.listings;
    });
  }

  filteredListings: IListing[] = this.listings;

  performFilter(): IListing[] {
    let filteredListings = this.listings;

    if (this.nameFilter) {
      const nameFilter = this.nameFilter.toLocaleLowerCase();
      filteredListings = filteredListings.filter((listing: IListing) =>
        listing.Name.toLocaleLowerCase().includes(nameFilter)
      );
    }

    if (this.maxPrice !== null) {
      filteredListings = filteredListings.filter(
        (listing: IListing) => listing.Price <= this.maxPrice!
      );
    }

    return filteredListings;
  }

  toggleImage(): void {
    this.displayImage = !this.displayImage;
  }

  navigateToListingform(): void {
    this._router.navigate(['/listingform']);
  }

  ngOnInit(): void {
    this.getListings();
  }

  navigateToListingDetails(listingId: number): void {
    this._router.navigate(['/listingdetail', listingId]);
  }
}
