import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-questions-overview',
  templateUrl: './questions-overview.component.html',
  styleUrl: './questions-overview.component.css'
})
export class QuestionsOverviewComponent {
  apiResponse: string | undefined;
  constructor(private router: Router, private httpClient: HttpClient) {

  }

  helloWorld() {
    this.httpClient.get("https://localhost:7184/questions/hello").subscribe(resp => {
      this.apiResponse = JSON.stringify(resp);
    });
  }
}
