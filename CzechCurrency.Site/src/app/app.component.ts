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
  exchangeRate = {};
  code = 'AUD';
  date = (new Date()).toLocaleDateString("en-EN");

  constructor(private dataService: DataService) { }

  ngOnInit() {

    this.dataService.sendGetRequest('/Currency/GetAll').subscribe((data: any[]) => {
      this.currencies = data;
    });

    this.loadExchangeRate(this.code, this.date);
  }

  loadExchangeRate(code,date) {
    this.dataService.sendGetRequest('/ExchangeRate/Get?CurrencyCode=' + code + '&Date=' + date).subscribe(
      (data: any) => {
        this.exchangeRate = data;
      });
  }
  changeDatePicker(event) {
   
    this.date = event.value.toLocaleDateString("en-EN");
    this.loadExchangeRate(this.code, this.date);
  }

  changeSelectCurrency(event) {
    this.code = event.target.value;
    this.loadExchangeRate(this.code, this.date);
  }
  
}


