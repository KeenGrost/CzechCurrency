import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'CzechCurrency';
  currencies = [];
  exchangeRate = "0";

  constructor(private dataService: DataService) { }

  ngOnInit() {

    this.dataService.sendGetRequest('/Currency/GetAll').subscribe((data: any[]) => {
      this.currencies = data;
    })
  }
  loadExchangeRate(event) {
    this.dataService.sendGetRequest('/ExchangeRate/Get?CurrencyCode=RUB&Date=2020-08-01').subscribe((data: any) => {
      console.log(data);
      this.exchangeRate = data;
    })
  }

}
