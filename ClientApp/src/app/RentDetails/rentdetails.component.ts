
import { Component, OnInit } from '@angular/core';
import { RentService } from '../rents/rents.service';
import { IRent } from '../rents/rent';
import { ListingService } from '../listings/listings.service';
import { IListing } from '../listings/listing';

@Component({
  selector: 'app-rentdetails',
  templateUrl: './rentdetails.component.html',
  styleUrls: ['./rentdetails.component.css']
})
export class RentDetailsComponent implements OnInit {
  rents: IRent[] = [];
  combinedDetails: { rent: IRent, listing: IListing }[] = [];

  constructor(
    private rentService: RentService,
    private listingService: ListingService
  ) { }

  ngOnInit(): void {
    this.loadRents();
  }

  loadRents(): void {
    this.rentService.getRents().subscribe(
      (rents: IRent[]) => {
        this.rents = rents;
        this.loadCombinedDetails();
      },
      (error) => {
        console.error('Error loading rents:', error);
      }
    );
  }

  loadCombinedDetails(): void {
    this.combinedDetails = [];
    for (const rent of this.rents) {
      this.listingService.getListingById(rent.listingId).subscribe(
        (listing: IListing) => {
          this.combinedDetails.push({ rent, listing });
        },
        (error) => {
          console.error(`Error loading listing details for Rent ID ${rent.rentId}:`, error);
        }
      );
    }
  }
}
