import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { StockDto } from './StockDto';

@Injectable({
  providedIn: 'root',
})
export class StockSignalRService {
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7192/stockHub`) // SignalR hub URL
      .build();
  }

  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('Connection established with SignalR hub');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('Error connecting to SignalR hub:', error);
          observer.error(error);
        });
    });
  }

  updateStockPrice(): Observable<StockDto> {
    return new Observable<StockDto>((observer) => {
      this.hubConnection.on('ReceivePrice', (symbol: string, price: number, timestamp: string) => {
        console.log(`hubConnection: Received stock price update for ${symbol}: ${price} at ${timestamp}`);
        observer.next(new StockDto(price, symbol, timestamp));
      });
    });
  }
}
