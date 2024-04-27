import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { StockSignalRService } from './stock-signalr.service';
import { StockVm } from './StockVm';
import { Chart } from 'chart.js/auto';
interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public chart: any;
  public prices: number[] = [];
  public timestamp: string[] = [];
  
  constructor(private signalRService: StockSignalRService) { }

  ngOnInit() {
    this.createChart();
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.updateStockPrice().subscribe((stock: StockVm) => {
        console.log(`subscribe: Received stock price update for ${stock.symbol}: ${stock.price} at ${stock.timestamp}`);
        this.prices.push(stock.price)
        this.timestamp.push(stock.timestamp);
        this.chart.update();

        if (this.prices.length >= 10) {
          this.prices.shift();
          this.timestamp.shift();
        }
      });
    });
  }

  createChart() {
    this.chart = new Chart("MyChart", {
      type: 'line', //this denotes tha type of chart

      data: {// values on X-Axis
        labels: this.timestamp,
        datasets: [
          {
            label: "price",
            data: this.prices,
            backgroundColor: 'blue'
          }
        ]
      },
      options: {
        responsive: true,
        aspectRatio: 2.5
      }

    });
  }
  title = 'StockExchange.client';
}
