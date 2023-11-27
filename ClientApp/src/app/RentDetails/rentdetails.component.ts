import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-rentdetails',
  templateUrl: './rentdetails.component.html',
  styleUrls: ['./rentdetails.component.css']
})
export class RentDetailsComponent implements OnInit {
  // Example property
  rentDetails: any; // You might want to replace 'any' with the actual type of your rent details

  constructor() { }

  ngOnInit(): void {
    // Example initialization logic
    this.fetchRentDetails(); // Assume you have a method to fetch rent details
  }

  // Example method to fetch rent details
  private fetchRentDetails(): void {
    // You can make an HTTP request or get data from a service
    // For example purposes, I'll just set some dummy data
    this.rentDetails = {
      property: 'Sample Property',
      rentAmount: 1000,
      // Add more properties as needed
    };
  }

  // Example method to perform some action
  public doSomething(): void {
    // Your logic here
  }
}
