export interface IListing {
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
