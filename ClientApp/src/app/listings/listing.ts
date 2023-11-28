export interface IListing {
  ListingId: number;
  Name: string;
  Price: number;
  Description: string;
  ImageUrl: string;
  AdditionalImageUrl?: string;
  displayImage: boolean;
}
