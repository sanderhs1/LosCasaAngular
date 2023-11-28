import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RentService } from './rents.service';
import { ListingService } from '../listings/listings.service';
import { IListing } from '../listings/listing';
import { IRent } from './rent';

@Component({
  selector: 'app-rents',
  templateUrl: './rents.component.html',
  styleUrls: ['./rents.component.css']
})

export class RentsComponent implements OnInit {
  rentForm: FormGroup;
  listingId!: number;
  listingPrice!: number;
  userId!: number;



  constructor(
    private fb: FormBuilder,
    private rentService: RentService,
    private listingService: ListingService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.rentForm = this.fb.group({
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (!id) {
        throw new Error('No Listing ID provided in the route.');
      }
      this.listingId = +id;
      this.loadListingPrice(this.listingId);
    });
  }
  loadListingPrice(listingId: number): void {
    this.listingService.getListingById(listingId).subscribe((listing: IListing) => {
      this.listingPrice = listing.Price;
    })
  }


  calculatePrice(): number {
    const startDate = this.rentForm.value.startDate;
    const endDate = this.rentForm.value.endDate;
    if (startDate && endDate && this.listingPrice) {
      const start = new Date(startDate);
      const end = new Date(endDate);
      if (start >= end) {
        console.error('End date must be after the start date.');
        return 0
      }
      const diff = end.getTime() - start.getTime();
      const days = Math.ceil(diff / (1000 * 60 * 60 * 24)); // Convert milliseconds to days
      return days * this.listingPrice;
    }
    return 0;
  }



  // ... (rest of your component code)

  rentListing(): void {
    if (this.rentForm.valid) {
      const price = this.calculatePrice();
      const newRent: IRent = {
        listingId: this.listingId,
        userId: this.userId, // Use the current userId
        startDate: new Date(this.rentForm.value.startDate),
        endDate: new Date(this.rentForm.value.endDate),
        price: price
      };

      this.rentService.createRent(newRent).subscribe({
        next: (rent) => {
          console.log('Rent created successfully', rent);
          // Increment userId for the next submission
          this.userId++;
          this.router.navigate(['/rentdetails', rent.rentId]);
 // Navigate to the desired route after success
        },
        error: (error) => {
          console.error('Error occurred while creating rent: ', error);
        }
      });
    }
  }
}
