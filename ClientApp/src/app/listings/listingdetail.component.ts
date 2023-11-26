import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListingService } from './listings.service'; 
import { Location } from '@angular/common';

@Component({
  selector: 'app-listing-detail',
  templateUrl: './listingdetail.component.html',
  styleUrls: ['./listingdetail.component.css']
})
export class ListingDetailComponent implements OnInit {
  listing: any; 

  constructor(
    private route: ActivatedRoute,
    private listingService: ListingService,
    private location: Location
  ) { }


  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = +params.get('id')!;
      if (id) {
        this.getListingDetails(id);
      } else {
        console.error('Listing ID is not available');
      }
    });
  }


  getListingDetails(id: number): void {
    this.listingService.getListingById(id).subscribe(
      (listing) => {
        this.listing = listing;
      },
      (error) => {
        console.error('Error fetching listing details:', error);
      }
    );
  }
  goBack(): void {
    this.location.back();
  }
}
