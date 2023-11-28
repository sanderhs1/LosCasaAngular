import { Component, OnInit } from '@angular/core';
import { RentService } from '../rents/rents.service';
import { IRent } from '../rents/rent';

@Component({
  selector: 'app-rentdetails',
  templateUrl: './rentdetails.component.html',
  styleUrls: ['./rentdetails.component.css']
})
export class RentDetailsComponent implements OnInit {
  rents: IRent[] = [];

  constructor(private rentService: RentService) { }

  ngOnInit(): void {
    console.log('RentDetailsComponent ngOnInit');
    this.loadRents();
  }

  loadRents(): void {
    console.log('Loading rents...');
    this.rentService.getRents().subscribe(
      (rents: IRent[]) => {
        console.log('Rents loaded successfully:', rents);
        this.rents = rents;
      },
      (error) => {
        console.error('Error loading rents:', error);
      }
    );
  }
}
