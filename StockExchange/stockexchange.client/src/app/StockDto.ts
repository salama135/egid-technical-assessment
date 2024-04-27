export class StockDto
{
  public price: number = 0;
  public symbol: string = "";
  public timestamp: string = "";

  constructor(price: number, symbol: string, timestamp: string) {
    this.price = price;
    this.symbol = symbol;
    this.timestamp = timestamp;
  }

}
