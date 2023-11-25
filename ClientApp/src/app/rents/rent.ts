export interface IRent {
  rentId?: number;
  listingId: number;
  userId: number;
  startDate: Date;
  endDate: Date;
  price: number;
}
