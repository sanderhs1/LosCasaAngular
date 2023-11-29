export interface IListing { // Defined every element from the database which is included in the listing
  ListingId: number;
  Name: string;
  Price: number;
  Description: string;
  ImageUrl: string;
  AdditionalImageUrl?: string;
  displayImage: boolean;
  Rooms: number;
  Toilets: number;
  Area: number;
  Address: string;
}
