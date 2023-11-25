import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RentService } from './rents.service';
import { IRent } from './rent';

@Component({
  selector: 'app-listing-detail',
  templateUrl: './listing-detail.component.html',
  styleUrls: ['./listing-detail.component.css']
})

export class ListingsComponent implements OnInit {
  listingId!: number;
  userId!: number;

  constructor(
    private rentService: RentService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id == null) {
      throw new Error('No Listing ID provided in the route.');
    }

    this.listingId = +id;
  }
  rentListing(startDate: Date, endDate: Date, price: number): void {
    const newRent: IRent = {
      listingId: this.listingId,
      userId: this.userId,
      startDate: startDate,
      endDate: endDate,
      price: price
    };


    this.rentService.createRent(newRent).subscribe({
      next: (rent) => {
        console.log('Rent created successfully', rent);
      },

      error: (error) => {
        console.error('Error occurred while creating rent: ', error);
      }
    });
  }
}
